

using HighScore.Data.Context;
using HighScore.Data.Repositories;
using HighScore.Domain.Entities;
using HighScore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers((options) =>
{
    options.ReturnHttpNotAcceptable = true;
})
.AddNewtonsoftJson(); // replace defaul json input and output formatters (used for patch methods)


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<HighScoreContext>(
    //comment in HighScoreContext
    //(dbContextOptions) => dbContextOptions.UseSqlServer(builder.Configuration["ConnectionStrings:HighScoreConnectionString"])
);

builder.Services.AddScoped(typeof(IRepository<UserEntity>), typeof(EntityUserRepository));
builder.Services.AddScoped(typeof(IRepository<HighScoreEntity>), typeof(EntityHighScoreRepository));
builder.Services.AddScoped(typeof(IRepository<LeaderBoardEntity>), typeof(LeaderBoardRepository));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"])
                           )
                
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline (aka middleware)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(
        (endpoints) => endpoints.MapControllers()    
    );

app.MapControllers();

app.Run();
