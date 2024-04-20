using AutoTradeHubDataWebApi.RabbitMQ;
using AutoTradeHubDataWebApi.Data;
using AutoTradeHubDataWebApi.Interfaces;
using AutoTradeHubDataWebApi.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using AutoTradeHubDataWebApi.Services.RabbitMqListeners;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddHostedService<BackgroundWorkerService>();
//builder.Services.AddHostedService<RabbitMqListener>();

// RabbitMq background listeners
builder.Services.AddHostedService<CarListener>();

builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IRabbitMqService, RabbitMqService>();

MyConfig.CloudAMQPUri = builder.Configuration.GetConnectionString("CloudAMQPUri");

var connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
