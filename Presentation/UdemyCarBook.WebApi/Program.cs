using UdemyCarBook.Application.Features.CQRS.Handlers.AboutHandlers.ReadAboutHandlers;
using UdemyCarBook.Application.Features.CQRS.Handlers.AboutHandlers.WriteAboutHandlers;
using UdemyCarBook.Application.Features.CQRS.Handlers.BannerHandlers.ReadBannerHandlers;
using UdemyCarBook.Application.Features.CQRS.Handlers.BannerHandlers.WriteAboutHandlers;
using UdemyCarBook.Application.Features.CQRS.Handlers.BannerHandlers.WriteBannerHandlers;
using UdemyCarBook.Application.Features.CQRS.Handlers.BrandHandlers.ReadBannerHandlers;
using UdemyCarBook.Application.Features.CQRS.Handlers.BrandHandlers.WriteBannerHandlers;
using UdemyCarBook.Application.Features.CQRS.Handlers.CarHandlers.ReadCarHandlers;
using UdemyCarBook.Application.Features.CQRS.Handlers.CarHandlers.WriteCarHandlers;
using UdemyCarBook.Application.Features.CQRS.Handlers.CategoryHandlers.ReadCategoryHandlers;
using UdemyCarBook.Application.Features.CQRS.Handlers.CategoryHandlers.WriteCategoryHandlers;
using UdemyCarBook.Application.Features.CQRS.Handlers.ContactHandlers.ReadContactHandlers;
using UdemyCarBook.Application.Features.CQRS.Handlers.ContactHandlers.WriteContactHandlers;
using UdemyCarBook.Application.Features.CQRS.Queries.AboutQueries;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Application.Interfaces.IncludeRepository;
using UdemyCarBook.Persistance.Context;
using UdemyCarBook.Persistance.Repositories;
using UdemyCarBook.Persistance.Repositories.IncludeRepostiries;
using UdemyCarBook.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<CarBookContext>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(ICarBrandRepository), typeof(CarBrandRepository));

builder.Services.AddScoped<GetAboutQueryHandler>();
builder.Services.AddScoped<GetAboutByIdQueryHandler>();
builder.Services.AddScoped<CreateAboutCommandHandler>();
builder.Services.AddScoped<RemoveAboutCommandHandler>();
builder.Services.AddScoped<UpdateAboutCommandHandler>();

builder.Services.AddScoped<GetBannerQueryHandler>();
builder.Services.AddScoped<GetBannerByIdQueryHandler>();
builder.Services.AddScoped<CreateBannerCommandHandler>();
builder.Services.AddScoped<RemoveBannerCommandHandler>();
builder.Services.AddScoped<UpdateBannerCommandHandler>();

builder.Services.AddScoped<GetBrandQueryHandler>();
builder.Services.AddScoped<GetBrandByIdQueryHandler>();
builder.Services.AddScoped<CreateBrandCommandHandler>();
builder.Services.AddScoped<RemoveBrandCommandHandler>();
builder.Services.AddScoped<UpdateBrandCommandHandler>();

builder.Services.AddScoped<GetCarQueryHandler>();
builder.Services.AddScoped<GetCarByIdQueryHandler>();
builder.Services.AddScoped<CreateCarCommandHandler>();
builder.Services.AddScoped<RemoveCarCommandHandler>();
builder.Services.AddScoped<UpdateCarCommandHandler>();
builder.Services.AddScoped<GetCarWithBrandQueryHandler>();

builder.Services.AddScoped<GetCategoryQueryHandler>();
builder.Services.AddScoped<GetCategoryByIdQueryHandler>();
builder.Services.AddScoped<CreateCategoryCommandHandler>();
builder.Services.AddScoped<RemoveCategoryCommandHandler>();
builder.Services.AddScoped<UpdateCategoryCommandHandler>();

builder.Services.AddScoped<GetContactQueryHandler>();
builder.Services.AddScoped<GetContactByIdQueryHandler>();
builder.Services.AddScoped<CreateContactCommandHandler>();
builder.Services.AddScoped<RemoveContactCommandHandler>();
builder.Services.AddScoped<UpdateContactCommandHandler>();

builder.Services.AddApplicationService(builder.Configuration);

builder.Services.AddControllers();
// Swagger/OpenAPI yapýlandýrmasý
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS servisinin doðru sýrayla eklenmesi
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// CORS middleware'inin doðru sýrayla kullanýlmasý
app.UseCors("AllowAllOrigins");

// HTTP istek pipeline'ýný yapýlandýrma
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
