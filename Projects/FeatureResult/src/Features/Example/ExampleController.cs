using AutoMapper;
using FeatureResult.src.Shared.Abstractions;
using FeatureResult.src.Shared.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FeatureResult.src.Features.Example
{
    public class ExampleController(ILogger<ExampleController> logger, IExampleRepository repository, IMapper mapper) : EntityControllerBase<ExampleEntity>(logger, repository, mapper)
    {
        // Add your specific methods here.

        [HttpPost]
        public async Task<IActionResult> Create(ExampleEntity entity)
        {
            try
            {
                await repository.Create(entity);
                return Ok("Example created!");
            }
            catch (Exception e)
            {
                logger.LogError(e, "Create");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("test-auth")]
        [Authorize]
        public IActionResult TestAuth()
        {
            try
            {
                var userId = TokenDecoder.GetUserId(User);
                return Ok($"Auth Ok from user: {userId}");
            }
            catch (Exception e)
            {
                logger.LogError(e, "TestAuth");
                throw;
            }
        }
    }
}