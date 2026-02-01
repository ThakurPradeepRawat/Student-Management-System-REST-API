using Common.BAL.Interfaces;
using Common.Model.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CommonAPI.Controllers
{
    [ApiController]
    [Route("students")]
    public class TemplateController : ControllerBase
    {
        private readonly IExcelService _excelService;
        private readonly IExcelDataValidation _excelDataValidation;

        public TemplateController(
            IExcelService excelService,
            IExcelDataValidation excelDataValidation)
        {
            _excelService = excelService;
            _excelDataValidation = excelDataValidation;
        }

        // ---------------- DOWNLOAD TEMPLATE ----------------
        [HttpGet("download-template")]
        public IActionResult DownloadTemplate()
        {
            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Template",
                "Student Data Template.xltx"
            );

            if (!System.IO.File.Exists(path))
                return NotFound("Template file not found");

            var bytes = System.IO.File.ReadAllBytes(path);

            return File(
                bytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.template",
                "Student_Data_Template.xltx"
            );
        }

        // ---------------- UPLOAD TEMPLATE ----------------
        [HttpPost("upload-template")]
        public async Task<IActionResult> UploadTemplate( IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            using var stream = file.OpenReadStream();

            // Read Excel
            List<CreateStudentRequestDTO> students =
                _excelService.ReadStudents(stream);

            // Validate Excel Data
            var (valid, invalid) =
                await _excelDataValidation.ValidateExcel(students);

            return Ok(new
            {
                Total = students.Count,
                ValidCount = valid.Count,
                InvalidCount = invalid.Count,
                ValidRecords = valid,
                InvalidRecords = invalid
            });
        }
    }
}