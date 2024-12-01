using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyCarBook.Application.Features.CQRS.Handlers.CarHandlers.ReadCarHandlers;
using UdemyCarBook.Application.Features.Mediator.Commands.BlogCommands;
using UdemyCarBook.Application.Features.Mediator.Queries.BlogQueries;

namespace UdemyCarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BlogController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> BlogList() {

            var values = await _mediator.Send(new GetBlogQuery());
            return Ok(values);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdBlog(int id)
    {
            var values = await _mediator.Send(new GetBlogByIdQuery(id));
            return Ok(values);

    }
        [HttpPost]
        public async Task<IActionResult> CreateBlog(CreateBlogCommand createBlogCommand)
        {
            await _mediator.Send(createBlogCommand);
            return Ok("Blog başarıyla eklendi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateBlog(UpdateBlogCommand updateBlogCommand)
        {
            await _mediator.Send(updateBlogCommand);

            return Ok("Blog başarıyla güncellendi");
           
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveBlog(RemoveBlogCommand removeBlogCommand)
        {
            await _mediator.Send(removeBlogCommand);
            return Ok("Blog başarıyla silindi");
        }

        [HttpGet("GetLast3BlogsWithAuthorList")]
        public async Task<IActionResult> GetLast3BlogsWithAuthorList()
        {
            var values = await _mediator.Send(new GetLast3BlogWithAuthorQuery());
            return Ok(values);
        }
    }
}
