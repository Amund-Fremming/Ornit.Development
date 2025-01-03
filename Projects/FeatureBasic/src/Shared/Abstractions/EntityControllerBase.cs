﻿using AutoMapper;
using FeatureBasic.src.Features.User;
using Microsoft.AspNetCore.Mvc;

namespace FeatureBasic.src.Shared.Abstractions
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
                var user = await repository.GetByID(id);
                var dto = mapper.Map<UserDto>(user);
                return Ok(dto);
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
                var users = await repository.GetAll();
                var dtos = mapper.Map<List<UserDto>>(users);
                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}