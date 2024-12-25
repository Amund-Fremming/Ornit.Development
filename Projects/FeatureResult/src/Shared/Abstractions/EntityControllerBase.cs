using AutoMapper;
using FeatureResult.src.Features.User;
using Microsoft.AspNetCore.Mvc;
using NucleusResults.Core;

namespace FeatureResult.src.Shared.Abstractions
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class EntityControllerBase<T>(IRepository<T> repository, IMapper mapper) : ControllerBase where T : IIdentityEntity
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            try
            {
                var result = await repository.GetByID(id);
                return result.Resolve(suc => Ok(mapper.Map<UserDto>(result.Data)),
                    err => BadRequest(result.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await repository.GetAll();
                return result.Resolve(suc => Ok(mapper.Map<List<UserDto>>(result.Data)),
                    err => BadRequest(result.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}