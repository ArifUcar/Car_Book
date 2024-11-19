using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyCarBook.Application.Features.Mediator.Commands.FooterAddressCommands;
using UdemyCarBook.Application.Features.Mediator.Queries.FooterAddressQueries;

namespace UdemyCarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FooterAddressController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FooterAddressController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> FooterList()
        {
            var values= await _mediator.Send(new GetFooterAddressQuery());
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdFooterAddress(int id)
        {
            var values= await _mediator.Send(new GetFooterAddressByIdQuery(id));
            return Ok(values);
        }
        [HttpPost] 
        public async Task<IActionResult> CreateFooterAddres(CreateFooterAddressCommand command)
        {
            await _mediator.Send(command);
            return Ok("FooterAddres başarıyla eklendi");

        }
        [HttpPut]
        public async Task<IActionResult> UpdateFooterAddress(UpdateFooterAddresssCommand command)
        {
            await _mediator.Send(command);
            return Ok("FooterAddress başarıyla güncellendi");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteFooterAddress(RemoveFooterAddressCommand command)
        {
            await _mediator.Send(command);
            return Ok("FooterAddress başarıyla silindi");
        }
    }
}
