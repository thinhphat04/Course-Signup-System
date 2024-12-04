using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
    [Route("api/[controller]")]
    [ApiController]
    public class FileStorageController : ControllerBase
    {
        private readonly IFileStorageService _fileStorageService;
        public FileStorageController(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;
        }
        [HttpPost]
        public async Task<IActionResult> FileStorage(IFormFile file)
        {
            if (file == null ||  file.Length == 0)
            {
                 return BadRequest("No file uploaded.");
            }
            try
            {
                using (var memory = new MemoryStream())
                {
                    await file.CopyToAsync(memory);
                    var filedata = memory.ToArray();
                    var image = await _fileStorageService.UploadFile(filedata, file.FileName, file.ContentType);
                    return Ok(image);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }

