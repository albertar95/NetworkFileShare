using Application.DTO.User;
using Application.Features.User.Request;
using Application.Helpers;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetworkApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("Users")]
        public async Task<IActionResult> GetUsers([FromQuery]bool includeAll = true) 
        {
            return Ok(await _mediator.Send(new GetUserListRequest() { IncludeDisabled = includeAll}));
        }
        [HttpGet("Users/{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            return Ok(await _mediator.Send(new GetUserRequest() { Id = id }));
        }
        [HttpPost("Users")]
        public async Task<IActionResult> AddUser([FromBody]CreateUserDTO user)
        {
            user.Password = Encryption.EncryptString(user.Password);
            return Ok(await _mediator.Send(new AddUserRequest() { User = user }));
        }
        [HttpPatch("Users")]
        public async Task<IActionResult> UpdateUser([FromBody]UpdateUserDTO user)
        {
            return Ok(await _mediator.Send(new UpdateUserRequest() { User = user }));
        }
        [HttpDelete("Users/{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteUserRequest() { Id = id }));
        }
        [HttpPatch("Users/Password")]
        public async Task<IActionResult> UpdateUserPassword([FromBody]UpdateUserPasswordDTO password)
        {
            password.Password = Encryption.EncryptString(password.Password);
            return Ok(await _mediator.Send(new UpdatePasswordRequest() { Password = password }));
        }
        [HttpPost("Users/Login")]
        public async Task<IActionResult> LoginUser([FromBody]LoginCredential credential)
        {
            credential.Password = Encryption.EncryptString(credential.Password);
            var result = await _mediator.Send(new UserLoginRequest() { Credential = credential });
            switch (result.Item1)
            {
                case 0:
                    return NotFound("user not found!");
                case 1:
                    return BadRequest("user is disabled");
                case 2:
                    return BadRequest("password not match");
                case 3:
                    return Ok(result.Item2);
                default:
                    return BadRequest("unknown error occured");
            }
        }
    }
}
