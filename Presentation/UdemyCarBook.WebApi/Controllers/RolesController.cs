using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Commands.RoleCommands;
using UdemyCarBook.Application.Features.Mediator.Queries.RoleQueries;

namespace UdemyCarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var values = await _mediator.Send(new GetRoleQuery());
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var value = await _mediator.Send(new GetRoleByIdQuery { Id = id });
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleCommand command)
        {
            await _mediator.Send(command);
            return Ok("Rol başarıyla oluşturuldu");
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateRoleCommand command)
        {
            await _mediator.Send(command);
            return Ok("Rol başarıyla güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            await _mediator.Send(new RemoveRoleCommand { Id = id });
            return Ok("Rol başarıyla silindi");
        }

        [HttpDelete("soft-delete/{id}")]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            await _mediator.Send(new SoftDeleteRoleCommand { Id = id });
            return Ok("Rol başarıyla arşivlendi");
        }
    }
} 