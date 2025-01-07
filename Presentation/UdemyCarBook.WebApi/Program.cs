using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Persistance.Context;
using UdemyCarBook.Persistance.Repositories;
using UdemyCarBook.Application.Services;
using Scrutor;
using UdemyCarBook.Persistance.Service;
using UdemyCarBook.Application.Interfaces.IService;
using UdemyCarBook.WebApi.Middleware;
using Microsoft.EntityFrameworkCore;
using UdemyCarBook.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Database Configuration
builder.Services.AddDbContext<NewsContext>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 21)),
        mySqlOptions => mySqlOptions.MigrationsAssembly("UdemyCarBook.Persistance")
    );
});

// Service registrations
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<IHistoryService, HistoryService>();
builder.Services.AddScoped<IUserService, UserService>();

// Repository registrations
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<INewsletterRepository, NewsletterRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ISocialMediaRepository, SocialMediaRepository>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();

builder.Services.AddApplicationService(builder.Configuration);

builder.Services.AddControllers();
// Swagger/OpenAPI yapılandırması
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS servisinin doğru sırayla eklenmesi
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// CORS middleware'inin doğru sırayla kullanılması
app.UseCors("AllowAllOrigins");

// HTTP istek pipeline'ını yapılandırma
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionMiddleware();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
