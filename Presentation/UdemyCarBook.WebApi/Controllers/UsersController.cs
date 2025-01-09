using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UdemyCarBook.Application.Features.Mediator.Commands.UserCommands;
using UdemyCarBook.Application.Features.Mediator.Queries.UserQueries;
using UdemyCarBook.Application.Interfaces.IService;

namespace UdemyCarBook.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPermissionAuthorizationService _permissionAuthorizationService;

        public UsersController(IMediator mediator, IPermissionAuthorizationService permissionAuthorizationService)
        {
            _mediator = mediator;
            _permissionAuthorizationService = permissionAuthorizationService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var userGuid))
            {
                return Unauthorized();
            }

            if (!await _permissionAuthorizationService.HasPermissionAsync(userGuid, "VIEW_ALL_USERS"))
            {
                return Forbid();
            }

            var result = await _mediator.Send(new GetUserQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var userGuid))
            {
                return Unauthorized();
            }

            if (!await _permissionAuthorizationService.HasPermissionAsync(userGuid, "VIEW_ALL_USERS"))
            {
                return Forbid();
            }

            var result = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
        {
            await _mediator.Send(command);
            return Ok("Kullanıcı başarıyla güncellendi");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new SoftDeleteUserCommand(id));
            return Ok("Kullanıcı başarıyla silindi");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
} 