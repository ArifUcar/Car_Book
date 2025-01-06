using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Commands.AuthorCommands;
using UdemyCarBook.Application.Features.Mediator.Queries.AuthorQueries;

namespace UdemyCarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var values = await _mediator.Send(new GetAuthorQuery());
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var value = await _mediator.Send(new GetAuthorByIdQuery { Id = id });
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAuthorCommand command)
        {
            await _mediator.Send(command);
            return Ok("Yazar başarıyla oluşturuldu");
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateAuthorCommand command)
        {
            await _mediator.Send(command);
            return Ok("Yazar başarıyla güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            await _mediator.Send(new RemoveAuthorCommand { Id = id });
            return Ok("Yazar başarıyla silindi");
        }

        [HttpDelete("soft-delete/{id}")]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            await _mediator.Send(new SoftDeleteAuthorCommand { Id = id });
            return Ok("Yazar başarıyla arşivlendi");
        }
    }
} 