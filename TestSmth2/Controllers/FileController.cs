using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TestSmth2.Repos;

namespace Medical.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileRepo fileRespository;

        public FileController(IFileRepo fileRespository)
        {
            this.fileRespository = fileRespository;
        }


        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            // call a repository
            var FileURL = await fileRespository.UploadAsync(file);

            if (FileURL == null)
            {
                return Problem("Someting went wrong!", null, (int)HttpStatusCode.InternalServerError);
            }

            return new JsonResult(new { link = FileURL });
        }
    }
}