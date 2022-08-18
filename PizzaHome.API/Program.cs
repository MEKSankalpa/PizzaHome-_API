using Microsoft.IdentityModel.Tokens;
using PizzaHome.API.Extensions;
using PizzaHome.API.Middlewares;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.JwtAuthConfiguration(builder.Configuration["Jwt:Issuer"], builder.Configuration["Jwt:Audience"],builder.Configuration["Jwt:Key"]);
builder.Services.CustomServices();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.CustomMiddlewares();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
