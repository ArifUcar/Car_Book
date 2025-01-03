﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyCarBook.Application.Features.CQRS.Commands.AboutCommand;
using UdemyCarBook.Application.Features.CQRS.Handlers.AboutHandlers.ReadAboutHandlers;
using UdemyCarBook.Application.Features.CQRS.Handlers.AboutHandlers.WriteAboutHandlers;
using UdemyCarBook.Application.Features.CQRS.Queries.AboutQueries;

namespace UdemyCarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutsController : ControllerBase
    {
        private readonly CreateAboutCommandHandler _createAboutCommandHandler;
        private readonly RemoveAboutCommandHandler _removeAboutCommandHandler;
        private readonly UpdateAboutCommandHandler _updateAboutCommandHandler;

        private readonly GetAboutQueryHandler _getAboutQueryHandler;
        private readonly GetAboutByIdQueryHandler _getAboutByIdQueryHandler;

        public AboutsController(CreateAboutCommandHandler createAboutCommandHandler, RemoveAboutCommandHandler removeAboutCommandHandler, UpdateAboutCommandHandler updateAboutCommandHandler, GetAboutQueryHandler getAboutQueryHandler, GetAboutByIdQueryHandler getAboutByIdQueryHandler)
        {
            _createAboutCommandHandler = createAboutCommandHandler;
            _removeAboutCommandHandler = removeAboutCommandHandler;
            _updateAboutCommandHandler = updateAboutCommandHandler;
            _getAboutQueryHandler = getAboutQueryHandler;
            _getAboutByIdQueryHandler = getAboutByIdQueryHandler;
        }

        [HttpGet]
        public async Task<IActionResult> AboutList()
        {
            var values = await _getAboutQueryHandler.Handle();  
            return Ok(values);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAbout(int id)
        {
            var values = await _getAboutByIdQueryHandler.Handle(new GetAboutByIdQuery(id));
            return Ok(values);
        }
        [HttpPost]
        public async Task <IActionResult> CreateAbout(CreateAboutCommand command)
        {
            await _createAboutCommandHandler.Handle(command);
            return Ok("Hakkında bilgisi eklendi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAbout(UpdateAboutCommand command)
        {
            await _updateAboutCommandHandler.Handle(command);
            return Ok("Hakkımda bilgisi güncellendi");
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveAbout(int id)
        {
            await _removeAboutCommandHandler.Handle(new RemoveAboutCommand(id));
            return Ok("Hakkımda Bilgisi Silindi");
        }
    }
}
