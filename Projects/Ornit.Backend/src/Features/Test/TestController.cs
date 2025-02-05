using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ornit.Backend.src.Features.Test
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [HttpDelete("{prm}")]
        public IActionResult Deletee([FromBody] string bodyString, TestEnum testEnum) => Ok();

        [HttpPatch("patch/{str}")]
        [Authorize]
        public IActionResult Patch(string str) => Ok();

        [HttpGet("get/extra/param")]
        public IActionResult Get() => Ok();
    }
}