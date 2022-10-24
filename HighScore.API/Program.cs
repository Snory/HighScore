

using HighScore.Data.Repositories;
using HighScore.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers((options) =>
{
    options.ReturnHttpNotAcceptable = true;
})
.AddNewtonsoftJson() // replace defaul json input and output formatters (used for patch methods)
;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// for in memory data it has to be singleton otherwise changes will not be reflected, otherwise scope is better?
builder.Services.AddSingleton(typeof(IRepository<UserDTO>), typeof(InMemoryUserRepository));
builder.Services.AddSingleton(typeof(IRepository<HighScoreDTO>), typeof(InMemoryHighScoreRepository));

var app = builder.Build();

// Configure the HTTP request pipeline (aka middleware)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(
        (endpoints) => endpoints.MapControllers()    
    );

app.MapControllers();


//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync("Hello world");
//});



app.Run();
