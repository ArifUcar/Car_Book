using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Commands.AuthorCommands;
using UdemyCarBook.Application.Features.Mediator.Queries.AuthorQueries;
using UdemyCarBook.Application.Interfaces.IService;

namespace UdemyCarBook.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPermissionAuthorizationService _permissionAuthorizationService;
        private readonly ILogService _logService;
        private readonly IUserService _userService;

        public AuthorsController(
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
                var result = await _mediator.Send(new GetAuthorQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                await _logService.CreateErrorLog(
                    ex,
                    "GetAllAuthors",
                    "Yazarlar listelenirken hata oluştu"
                );
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _mediator.Send(new GetAuthorByIdQuery(id));
                return Ok(result);
            }
            catch (Exception ex)
            {
                await _logService.CreateErrorLog(
                    ex,
                    "GetAuthorById",
                    "Yazar detayı görüntülenirken hata oluştu"
                );
                throw;
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateAuthorCommand command)
        {
            try
            {
                var userId = await _userService.GetCurrentUserIdAsync();
                if (!await _permissionAuthorizationService.HasPermissionAsync(userId, "AUTHOR_CREATE"))
                {
                    await _logService.CreateLog(
                        "Yetki Hatası",
                        $"Kullanıcı ID: {userId}, İzin: AUTHOR_CREATE, İşlem: Yazar Oluşturma",
                        "Error",
                        "Authorization"
                    );
                    return Forbid();
                }

                command.CreatedById = userId;
                await _mediator.Send(command);

                await _logService.CreateLog(
                    "Yazar Oluşturuldu",
                    $"Kullanıcı ID: {userId}, Yazar: {command.FirstName} {command.LastName}",
                    "Information",
                    "Author"
                );

                return Ok("Yazar başarıyla oluşturuldu");
            }
            catch (Exception ex)
            {
                await _logService.CreateErrorLog(
                    ex,
                    "CreateAuthor",
                    "Yazar oluşturulurken hata oluştu"
                );
                throw;
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateAuthorCommand command)
        {
            try
            {
                var userId = await _userService.GetCurrentUserIdAsync();
                if (!await _permissionAuthorizationService.HasPermissionAsync(userId, "AUTHOR_UPDATE"))
                {
                    await _logService.CreateLog(
                        "Yetki Hatası",
                        $"Kullanıcı ID: {userId}, İzin: AUTHOR_UPDATE, İşlem: Yazar Güncelleme",
                        "Error",
                        "Authorization"
                    );
                    return Forbid();
                }

                command.LastModifiedById = userId;
                await _mediator.Send(command);

                await _logService.CreateLog(
                    "Yazar Güncellendi",
                    $"Kullanıcı ID: {userId}, Yazar ID: {command.Id}",
                    "Information",
                    "Author"
                );

                return Ok("Yazar başarıyla güncellendi");
            }
            catch (Exception ex)
            {
                await _logService.CreateErrorLog(
                    ex,
                    "UpdateAuthor",
                    "Yazar güncellenirken hata oluştu"
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
                if (!await _permissionAuthorizationService.HasPermissionAsync(userId, "AUTHOR_DELETE"))
                {
                    await _logService.CreateLog(
                        "Yetki Hatası",
                        $"Kullanıcı ID: {userId}, İzin: AUTHOR_DELETE, İşlem: Yazar Silme",
                        "Error",
                        "Authorization"
                    );
                    return Forbid();
                }

                var command = new SoftDeleteAuthorCommand { Id = id, LastModifiedById = userId };
                await _mediator.Send(command);

                await _logService.CreateLog(
                    "Yazar Silindi",
                    $"Kullanıcı ID: {userId}, Yazar ID: {id}",
                    "Information",
                    "Author"
                );

                return Ok("Yazar başarıyla silindi");
            }
            catch (Exception ex)
            {
                await _logService.CreateErrorLog(
                    ex,
                    "DeleteAuthor",
                    "Yazar silinirken hata oluştu"
                );
                throw;
            }
        }
    }
} 