
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Persistance.Context;
using UdemyCarBook.Persistance.Repositories;
using UdemyCarBook.Application.Services;
using Scrutor;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddScoped<NewsContext>();


builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));



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
