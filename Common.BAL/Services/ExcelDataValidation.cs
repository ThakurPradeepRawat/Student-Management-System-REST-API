using Common.BAL.Interfaces;
using Common.DAL.Interface;
using Common.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.BAL.Services
{
    internal class ExcelDataValidation : IExcelDataValidation
    {
        private readonly IStudentRepository _studentRepository;
        public ExcelDataValidation(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public Task<(List<CreateStudentRequestDTO> valid, List<object> invalid)> ValidateExcel(List<CreateStudentRequestDTO> list)
        {
            
            
            
           
        }




    }
}
