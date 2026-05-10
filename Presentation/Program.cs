using MediatR;
using System.Reflection;
using Application.Interfaces;
using Infrastructure.Repositories;
using Application.DTOs;
using Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Repository
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

// ✅ Application layer (MediatR + FluentValidation) register ho raha hai
builder.Services.AddApplication();

Console.WriteLine("Repository registered - MEDIATOR branch fix");
Console.WriteLine("Repository registered - MAIN branch fix");



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();