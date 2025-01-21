using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ornit.Backend.src.Shared.ResultPattern;

namespace Ornit.Backend.src.Shared.Abstractions
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class EntityControllerBase<T>(ILogger<EntityControllerBase<T>> logger, IRepository<T> repository) : ControllerBase where T : IIdentity
    {
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await repository.GetById(id);
                return result.Resolve(suc => Ok(result.Data),
                    err => BadRequest(result.Message));
            }
            catch (Exception e)
            {
                logger.LogError(e, "GetBydId");
                return StatusCode(500, "Internal server error.");
            }
        }

        [Authorize]
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await repository.GetAll();
                return result.Resolve(suc => Ok(suc.Data),
                    err => BadRequest(result.Message));
            }
            catch (Exception e)
            {
                logger.LogError(e, "GetAll");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}