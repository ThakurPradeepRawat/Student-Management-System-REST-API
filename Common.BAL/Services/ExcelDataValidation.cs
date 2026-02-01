using Common.BAL.Interfaces;
using Common.DAL.Interface;
using Common.Model.DTO;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.BAL.Services
{
    public class ExcelDataValidation : IExcelDataValidation
    {
        private readonly IStudentRepository _studentRepository;
        public ExcelDataValidation(IStudentRepository studentRepository, IExcelService excelService)
        {
            _studentRepository = studentRepository;

        }
        public async Task<(List<CreateStudentRequestDTO> valid, List<object> invalid)> ValidateExcel(List<CreateStudentRequestDTO> ExcelSheet)
        {
            var valid = new List<CreateStudentRequestDTO>();
            var invalid = new List<object>();

            // Emails already in DB
            HashSet<string> existingEmails = _studentRepository.FetchEmail();

            // Track Excel-level duplicates
            HashSet<string> excelEmails = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var dto in ExcelSheet)
            {
                var results = new List<ValidationResult>();

                bool isValid = Validator.TryValidateObject(
                    dto,
                    new ValidationContext(dto),
                    results,
                    validateAllProperties: true
                );

                // DTO validation failed
                if (!isValid)
                {
                    invalid.Add(new
                    {
                        Data = dto,
                        Errors = results.Select(r => r.ErrorMessage)
                    });
                    continue;
                }

                // Duplicate in DB
                if (existingEmails.Contains(dto.Email))
                {
                    invalid.Add(new
                    {
                        Data = dto,
                        Errors = new[] { "Email already exists in database" }
                    });
                    continue;
                }

                // Duplicate in same Excel file
                if (!excelEmails.Add(dto.Email))
                {
                    invalid.Add(new
                    {
                        Data = dto,
                        Errors = new[] { "Duplicate email in Excel file" }
                    });
                    continue;
                }

                // All good
                valid.Add(dto);
            }

            return (valid, invalid);
        }

    }

}
