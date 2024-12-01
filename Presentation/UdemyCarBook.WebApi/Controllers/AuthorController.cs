using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyCarBook.Application.Features.CQRS.Handlers.AboutHandlers.ReadAboutHandlers;
using UdemyCarBook.Application.Features.Mediator.Commands.AuthorCommands;
using UdemyCarBook.Application.Features.Mediator.Handlers.AuthorHandlers.ReadAuthorHandlers;
using UdemyCarBook.Application.Features.Mediator.Handlers.AuthorHandlers.WriteAuthorHandlers;
using UdemyCarBook.Application.Features.Mediator.Queries.AuthorQueries;
using UdemyCarBook.Application.Features.Mediator.Queries.FeatureQueries;

namespace UdemyCarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> AuthorList()
        {
            var values = await _mediator.Send(new GetAuthorQuery());
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAuthor(int id)
        {
            var values = await _mediator.Send(new GetAuthorByIdQuery(id));
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAuthor(CreateAuthorCommand command)
        {
            await _mediator.Send(command);
            return Ok("Author başarıyla eklendi");
        }
        [HttpDelete] 
        public async Task<IActionResult> RemoveAuthor(int id)
        {
            await _mediator.Send(new RemoveAuthorCommand(id));
            return Ok("Author başarıyla silindi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAuthor(UpdateAuthorCommand command)
        {
            await _mediator.Send(command);
            return Ok("Author başarıyla güncellendi");
        }

    }
}
