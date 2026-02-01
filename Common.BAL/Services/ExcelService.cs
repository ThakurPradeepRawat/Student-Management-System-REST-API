using ClosedXML.Excel;
using Common.BAL.Interfaces;
using Common.Model.DTO;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.BAL.Services
{
    public class ExcelService:IExcelService
    {
        public List<CreateStudentRequestDTO> ReadStudents(Stream Stream)
        {
            using var workbook = new XLWorkbook(Stream);
            var ws = workbook.Worksheet(1);
            var list = new List<CreateStudentRequestDTO>();
            foreach (var row in ws.RowsUsed().Skip(1))
            {
                try {
                    list.Add(new CreateStudentRequestDTO
                    {
                        FirstName = row.Cell(1).GetString(),
                        LastName = row.Cell(2).GetString(),
                        Email = row.Cell(3).GetString(),
                        MobileNumber = row.Cell(4).GetString(),
                        DateOfBirth = DateOnly.FromDateTime(row.Cell(5).GetDateTime()),
                        FathersName = row.Cell(6).GetString(),
                        MothersName = row.Cell(7).GetString(),
                        Address = row.Cell(8).GetString()
                    });
                        }
                catch (Exception ex)
                {
                    Console.WriteLine("Error");
                }
            }
            Console.WriteLine(list);
            return list;
        }
       
    }
}
