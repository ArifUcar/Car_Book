using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyCarBook.Application.Features.Mediator.Commands.TagCloudCommands;
using UdemyCarBook.Application.Features.Mediator.Queries.TagCloudQueries;

namespace UdemyCarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagCloudController : ControllerBase
    {
        private readonly IMediator mediator;

        public TagCloudController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> TagList()
        {
            var values = await mediator.Send(new GetTagCloudQuery());
            return Ok(values);
        }
        [HttpGet("id")]

        public async Task<IActionResult> GetByIdTagList(int id)
        {
            var values = await mediator.Send(new GetTagCloudByIdQuery(id));
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTagCloud(CreateTagCloudCommand command)
        {
            await mediator.Send(command);
            return Ok("TagCloud Başarıyla eklendi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTagCloud(UpdateTagCloudCommand command)
        {
            await mediator.Send(command);
            return Ok("TagCloud başarıyla güncellendi");
        }
        [HttpDelete]
        public async Task<IActionResult> ReomeveTagCloud(RemoveTagCloudCommand command)
        {
            await mediator.Send(command);
            return Ok("TagCloud başarıyla silindi");
        }
        [HttpGet("BlogId")]
        public async Task<IActionResult> GetTagCloudByBlogId(int id)
        {
            var values=await mediator.Send(new GetTagCloudByBlogIdQuery(id));
            return Ok(values);
        }

    }
}
