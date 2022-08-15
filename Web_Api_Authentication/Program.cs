using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Web_Api_Authentication.Data;
using Web_Api_Authentication.Interfaces.Repository;
using Web_Api_Authentication.Interfaces.Services;
using Web_Api_Authentication.Repository;
using Web_Api_Authentication.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", config =>
                     config.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .Build()
                );
            });


builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

builder.Services.AddDbContext<ApiDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Web_Api_Authentication_Context"));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
            {
                var titleBase = "Teste Senior Mega";
                var description = "Esta API foi contruida usando dotnet 6.0 e esta consumindo uma API externa para enviar dados e também para guardar dados em uma base Local";
                var contact = new OpenApiContact()
                {
                    Email = "kayky7277@gmail.com",
                    Name = "Kayky Matos Santana"
                };

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = titleBase,
                    Description = description,
                    Contact = contact
                });
            });
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowAllOrigins");

app.Run();
