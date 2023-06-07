using Microsoft.EntityFrameworkCore;
using Udemy_NZWalks.API.Data;
using Udemy_NZWalks.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(); //the services that will be used by the app
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//injecting a new service, injects DBContext class 
builder.Services.AddDbContext<Udemy_NZWalksDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString")));

builder.Services.AddScoped<iRegionRepository, SQLRegionRepository>(); //injects the iregion rep with the sql region rep.
//now have to inject the sql into the constructor 

var app = builder.Build();

// Configure the HTTP request pipeline. Middleware to handle requests and responses
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
