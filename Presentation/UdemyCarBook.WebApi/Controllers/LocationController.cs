using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyCarBook.Application.Locations.Mediator.Commands.LocationCommands;
using UdemyCarBook.Application.Locations.Mediator.Commands.LocationsCommands;
using UdemyCarBook.Application.Locations.Mediator.Queries.LocationQueries;



namespace UdemyCarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LocationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> FooterList()
        {
            var values = await _mediator.Send(new GetLocationQuery());
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdLocation(int id)
        {
            var values = await _mediator.Send(new GetLocationByIdQuery(id));
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateLocation(CreateLocationCommand command)
        {
            await _mediator.Send(command);
            return Ok("FooterAddres başarıyla eklendi");

        }
        [HttpPut]
        public async Task<IActionResult> UpdateLocation(UpdateLocationCommand command)
        {
            await _mediator.Send(command);
            return Ok("Location başarıyla güncellendi");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteLocation(RemoveLocationCommand command)
        {
            await _mediator.Send(command);
            return Ok("Location başarıyla silindi");
        }
    }
}
