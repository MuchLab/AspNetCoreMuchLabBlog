using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MuchBlog.Api.Dtos;
using MuchBlog.Api.Filters;
using MuchBlog.Domain.Helpers.Parameters;
using MuchBlog.Domain.Services.IService;
using MuchBlog.Infrastructure.Entities;

namespace MuchBlog.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController:ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public UserController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper ?? throw new ArgumentException(nameof(repositoryWrapper));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersAsync()
        //{
        //    var userList = await _repositoryWrapper.User.GetAllAsync();
        //    var userDtoList = _mapper.Map<IEnumerable<UserDto>>(userList);
        //    return Ok(userDtoList);
        //}

        [HttpGet("{userId}", Name = nameof(GetUserAsync))]
        public async Task<ActionResult<UserDto>> GetUserAsync(Guid userId)
        {
            var user = await _repositoryWrapper.User.GetByIdAsync(userId);
            if (user==null)
            {
                return NotFound();
            }

            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }
        [HttpGet]
        public async Task<IActionResult> AuthorizeAsync([FromQuery]UserAuthorizeParameters parameters)
        {
            var user = await _repositoryWrapper.User.GetUserByAuthorizeAsync(parameters.UserName, parameters.Password);
            if (user==null)
            {
                return Ok(new {authorize = false});
            }

            var userDto = _mapper.Map<UserDto>(user);
            var authorizeUser = new
            {
                user = userDto,
                authorize = true
            };
            return Ok(authorizeUser);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserForCreationDto userForCreationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!userForCreationDto.Password.Equals(userForCreationDto.ConfirmedPassword))
            {
                return BadRequest("Password is not equal to confirmedPassword");
            }

            var user = _mapper.Map<User>(userForCreationDto);
            user.Id = Guid.NewGuid();
            
            _repositoryWrapper.User.Create(user);
            if (!await _repositoryWrapper.User.SaveAsync())
            {
                throw new Exception("Create User Failed.");
            }

            var userDto = _mapper.Map<UserDto>(user);
            return CreatedAtRoute(nameof(GetUserAsync), new {userId = user.Id}, userDto);
        }

        [HttpDelete("{userId}")]
        [ServiceFilter(typeof(CheckUserExistFilterAttribute))]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            var user = await _repositoryWrapper.User.GetByIdAsync(userId);
            _repositoryWrapper.User.Delete(user);
            if (!await _repositoryWrapper.User.SaveAsync())
            {
                throw new Exception("Delete User Failed.");
            }

            return NoContent();
        }

        [HttpPut("{userId}")]
        [ServiceFilter(typeof(CheckUserExistFilterAttribute))]
        public async Task<IActionResult> UpdateUser(Guid userId, UserForUpdateDto userForUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _repositoryWrapper.User.GetByIdAsync(userId);
            _mapper.Map(userForUpdateDto, user, typeof(UserForUpdateDto), typeof(User));
            _repositoryWrapper.User.Update(user);
            if (!await _repositoryWrapper.User.SaveAsync())
            {
                throw new Exception("Update User Failed.");
            }

            return NoContent();
        }

        [HttpPatch("{userId}")]
        [ServiceFilter(typeof(CheckUserExistFilterAttribute))]
        public async Task<IActionResult> PartiallyUpdateUser(Guid userId, JsonPatchDocument<UserForUpdateDto> patchDocument)
        {
            var user = await _repositoryWrapper.User.GetByIdAsync(userId);
            var userToPatch = _mapper.Map<UserForUpdateDto>(user);
            patchDocument.ApplyTo(userToPatch, error =>
            {
                ModelState.AddModelError("UserPatchError", error.ErrorMessage);
            });
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(userToPatch, user, typeof(UserForUpdateDto), typeof(User));
            if (!await _repositoryWrapper.User.SaveAsync())
            {
                throw new Exception("Update User Failed.");
            }

            return NoContent();
        }
    }
}