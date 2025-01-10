using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UdemyCarBook.Application.Features.Mediator.Commands.NewsCommands;
using UdemyCarBook.Application.Features.Mediator.Queries.NewsQueries;
using UdemyCarBook.Application.Interfaces.IService;

namespace UdemyCarBook.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPermissionAuthorizationService _permissionAuthorizationService;
        private readonly ILogService _logService;
        private readonly IUserService _userService;

        public NewsController(
            IMediator mediator,
            IPermissionAuthorizationService permissionAuthorizationService,
            ILogService logService,
            IUserService userService)
        {
            _mediator = mediator;
            _permissionAuthorizationService = permissionAuthorizationService;
            _logService = logService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _mediator.Send(new GetNewsQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                await _logService.CreateErrorLog(
                    ex,
                    "GetAllNews",
                    "Haberler listelenirken hata oluştu"
                );
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _mediator.Send(new GetNewsByIdQuery(id));
                return Ok(result);
            }
            catch (Exception ex)
            {
                await _logService.CreateErrorLog(
                    ex,
                    "GetNewsById",
                    "Haber detayı görüntülenirken hata oluştu"
                );
                throw;
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateNewsCommand command)
        {
            try
            {
                var userId = await _userService.GetCurrentUserIdAsync();
                if (!await _permissionAuthorizationService.HasPermissionAsync(userId, "NEWS_CREATE"))
                {
                    await _logService.CreateLog(
                        "Yetki Hatası",
                        $"Kullanıcı ID: {userId}, İzin: NEWS_CREATE, İşlem: Haber Oluşturma",
                        "Error",
                        "Authorization"
                    );
                    return Forbid();
                }

                command.CreatedById = userId;
                await _mediator.Send(command);
                
                await _logService.CreateLog(
                    "Haber Oluşturuldu",
                    $"Kullanıcı ID: {userId}, Başlık: {command.Title}",
                    "Information",
                    "News"
                );

                return Ok("Haber başarıyla oluşturuldu");
            }
            catch (Exception ex)
            {
                await _logService.CreateErrorLog(
                    ex,
                    "CreateNews",
                    "Haber oluşturulurken hata oluştu"
                );
                throw;
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateNewsCommand command)
        {
            try
            {
                var userId = await _userService.GetCurrentUserIdAsync();
                if (!await _permissionAuthorizationService.HasPermissionAsync(userId, "NEWS_UPDATE"))
                {
                    await _logService.CreateLog(
                        "Yetki Hatası",
                        $"Kullanıcı ID: {userId}, İzin: NEWS_UPDATE, İşlem: Haber Güncelleme",
                        "Error",
                        "Authorization"
                    );
                    return Forbid();
                }

                command.LastModifiedById = userId;
                await _mediator.Send(command);

                await _logService.CreateLog(
                    "Haber Güncellendi",
                    $"Kullanıcı ID: {userId}, Haber ID: {command.Id}",
                    "Information",
                    "News"
                );

                return Ok("Haber başarıyla güncellendi");
            }
            catch (Exception ex)
            {
                await _logService.CreateErrorLog(
                    ex,
                    "UpdateNews",
                    "Haber güncellenirken hata oluştu"
                );
                throw;
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var userId = await _userService.GetCurrentUserIdAsync();
                if (!await _permissionAuthorizationService.HasPermissionAsync(userId, "NEWS_DELETE"))
                {
                    await _logService.CreateLog(
                        "Yetki Hatası",
                        $"Kullanıcı ID: {userId}, İzin: NEWS_DELETE, İşlem: Haber Silme",
                        "Error",
                        "Authorization"
                    );
                    return Forbid();
                }

                var command = new SoftDeleteNewsCommand { Id = id, LastModifiedById = userId };
                await _mediator.Send(command);

                await _logService.CreateLog(
                    "Haber Silindi",
                    $"Kullanıcı ID: {userId}, Haber ID: {id}",
                    "Information",
                    "News"
                );

                return Ok("Haber başarıyla silindi");
            }
            catch (Exception ex)
            {
                await _logService.CreateErrorLog(
                    ex,
                    "DeleteNews",
                    "Haber silinirken hata oluştu"
                );
                throw;
            }
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(Guid categoryId)
        {
            try
            {
                var result = await _mediator.Send(new GetNewsByCategoryQuery(categoryId));
                return Ok(result);
            }
            catch (Exception ex)
            {
                await _logService.CreateErrorLog(
                    ex,
                    "GetNewsByCategory",
                    "Kategoriye göre haberler listelenirken hata oluştu"
                );
                throw;
            }
        }

        [HttpGet("author/{authorId}")]
        public async Task<IActionResult> GetByAuthor(Guid authorId)
        {
            try
            {
                var result = await _mediator.Send(new GetNewsByAuthorQuery(authorId));
                return Ok(result);
            }
            catch (Exception ex)
            {
                await _logService.CreateErrorLog(
                    ex,
                    "GetNewsByAuthor",
                    "Yazara göre haberler listelenirken hata oluştu"
                );
                throw;
            }
        }

        [HttpGet("tag/{tagId}")]
        public async Task<IActionResult> GetByTag(Guid tagId)
        {
            try
            {
                var result = await _mediator.Send(new GetNewsByTagQuery(tagId));
                return Ok(result);
            }
            catch (Exception ex)
            {
                await _logService.CreateErrorLog(
                    ex,
                    "GetNewsByTag",
                    "Etikete göre haberler listelenirken hata oluştu"
                );
                throw;
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchNewsQuery query)
        {
            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                await _logService.CreateErrorLog(
                    ex,
                    "SearchNews",
                    "Haber araması yapılırken hata oluştu"
                );
                throw;
            }
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatest([FromQuery] int count = 5)
        {
            try
            {
                var result = await _mediator.Send(new GetLatestNewsQuery(count));
                return Ok(result);
            }
            catch (Exception ex)
            {
                await _logService.CreateErrorLog(
                    ex,
                    "GetLatestNews",
                    "Son haberler listelenirken hata oluştu"
                );
                throw;
            }
        }

        [HttpGet("popular")]
        public async Task<IActionResult> GetPopular([FromQuery] int count = 5)
        {
            try
            {
                var result = await _mediator.Send(new GetPopularNewsQuery(count));
                return Ok(result);
            }
            catch (Exception ex)
            {
                await _logService.CreateErrorLog(
                    ex,
                    "GetPopularNews",
                    "Popüler haberler listelenirken hata oluştu"
                );
                throw;
            }
        }
    }
} 