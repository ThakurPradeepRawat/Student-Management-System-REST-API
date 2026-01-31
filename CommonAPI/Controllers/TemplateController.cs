using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CommonAPI.Controllers
{
    [Route("/students")]
    [ApiController]
    public class TemplateController : Controller
    {
        [HttpGet("download-template")]
        public IActionResult DownloadTemplate()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Template", "Student Data Template.xltx");
            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.template", "Student Data Template.xltx");
        }

        [HttpPost("upload-template")]
        public async Task <IActionResult> UploadTemplate(IFormFile File)
        {
            if(File == null || File.Length == 0)
            {
                return BadRequest("No files upload");
          
            }
            var path = Path.Combine("Uploads", File.FileName);
            using var stream = new FileStream(path, FileMode.Create);
            await File.CopyToAsync(stream);
            return Ok(new
            {
                message = "Uploaded successfully"
            });

        }

    }
}
