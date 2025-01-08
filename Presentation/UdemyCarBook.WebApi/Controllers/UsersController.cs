using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UdemyCarBook.Application.Attributes;
using UdemyCarBook.Application.Features.Mediator.Commands.UserCommands;
using UdemyCarBook.Application.Features.Mediator.Queries.UserQueries;
using UdemyCarBook.Application.Features.Mediator.Results.UserResults;

namespace UdemyCarBook.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AUAuthorize]
        public async Task<IActionResult> GetAll()
        {
            var values = await _mediator.Send(new GetUserQuery());
            return Ok(values);
        }

        [HttpGet("{id}")]
        [AUAuthorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            var value = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(value);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            await _mediator.Send(command);
            return Ok("Kullanıcı başarıyla oluşturuldu");
        }

        [HttpPut]
        [AUAuthorize]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
        {
            await _mediator.Send(command);
            return Ok("Kullanıcı başarıyla güncellendi");
        }

        [HttpDelete("{id}")]
        [AUAuthorize("Admin")]
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