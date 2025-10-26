using Application.Guests;
using Application.Guests.Ports;
using Application.Rooms;
using Application.Rooms.Ports;
using Data;
using Data.Guests;
using Data.Rooms;
using Domain.Ports;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger/OpenAPI configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Main");
builder.Services
    .AddDbContext<HotelDbContext>(options => 
        options.UseSqlServer(connectionString));

//INJECAO DE DEPENDENCIA
builder.Services.AddScoped<IGuestManager, GuestManager>();
builder.Services.AddScoped<IGuestRepository, GuestRepositoryInMemory>();

builder.Services.AddScoped<IRoomManager, RoomManager>();
builder.Services.AddScoped<IRoomRepository, RoomsRepositoryInMemory>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotel API v1");
        options.RoutePrefix = string.Empty; // Abre direto na raiz (http://localhost:5000)
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
