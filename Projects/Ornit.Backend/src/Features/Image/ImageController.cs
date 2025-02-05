using Microsoft.AspNetCore.Mvc;
using Ornit.Backend.src.Shared.Image;

namespace Ornit.Backend.src.Features.Image
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController(IImageHandler imageHandler) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile form)
        {
            var stream = form.OpenReadStream() as MemoryStream;
            await imageHandler.Upload(stream!, form.ContentType);

            return Ok();
        }
    }
}