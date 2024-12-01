using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyCarBook.Application.Features.CQRS.Commands.CarCommand;
using UdemyCarBook.Application.Features.CQRS.Handlers.CarHandlers.ReadCarHandlers;
using UdemyCarBook.Application.Features.CQRS.Handlers.CarHandlers.WriteCarHandlers;
using UdemyCarBook.Application.Features.CQRS.Queries.CarQueries;

namespace UdemyCarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {

        private readonly GetCarByIdQueryHandler _getCarByIdQueryHandler;
        private readonly GetCarQueryHandler _getCarQueryHandler;
        private readonly GetCarWithBrandQueryHandler _getCarWithBrandQueryHandler;
        private readonly GetLast5CarWithBrandQueryHandler _getLast5CarWithBrandQueryHandler;


        private readonly CreateCarCommandHandler _createCarCommandHandler;
        private readonly UpdateCarCommandHandler _updateCarCommandHandler;
        private readonly RemoveCarCommandHandler _removeCarCommandHandler;
       


        public CarController(GetCarByIdQueryHandler getCarByIdQueryHandler, GetCarQueryHandler getCarQueryHandler, GetCarWithBrandQueryHandler getCarWithBrandQueryHandler, CreateCarCommandHandler createCarCommandHandler, UpdateCarCommandHandler updateCarCommandHandler, RemoveCarCommandHandler removeCarCommandHandler , GetLast5CarWithBrandQueryHandler getLast5CarWithBrandQueryHandler)
        {
            _getCarByIdQueryHandler = getCarByIdQueryHandler;
            _getCarQueryHandler = getCarQueryHandler;
            _getCarWithBrandQueryHandler = getCarWithBrandQueryHandler;
            _getLast5CarWithBrandQueryHandler = getLast5CarWithBrandQueryHandler;
           _createCarCommandHandler = createCarCommandHandler;
            _updateCarCommandHandler = updateCarCommandHandler;
            _removeCarCommandHandler = removeCarCommandHandler;
        }

        [HttpGet]
        public async Task<IActionResult> CarList()
        {
            var values = await _getCarQueryHandler.Handle();
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCar(int id)
        {
            var valeus = await _getCarByIdQueryHandler.Handle(new GetCarByIdQuery(id));
            return Ok(valeus);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCar(CreateCarCommand createCarCommand)
        {

            await _createCarCommandHandler.Handle(createCarCommand);
            return Ok("Car başarıyla eklendi");
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveCar(int id)
        {
            await _removeCarCommandHandler.Handle(new RemoveCarCommand(id));
            return Ok("Car başarıyla silindi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCar(UpdateCarCommand updateCarCommand)
        {
            await _updateCarCommandHandler.Handle(updateCarCommand);
            return Ok("Car başarıylsa güncellendi");
        }
        [HttpGet("GetCarWithBrand")]
        public async Task<IActionResult> GetCarWithBrandList()
        {
            var values= await _getCarWithBrandQueryHandler.Handle();
            return Ok(values);
        }
        [HttpGet("GetLast5CarsWithBrand")]
        public async Task<IActionResult> GetLast5CarsWithBrandList()
        {
            var values = await _getLast5CarWithBrandQueryHandler.Handle();
            return Ok(values);
        }

    }
}
