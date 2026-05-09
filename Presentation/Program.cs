using MediatR;
using System.Reflection;
using Application.Interfaces;
using Infrastructure.Repositories;
using Application.DTOs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Repository
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
Console.WriteLine("Repository registered - MAIN branch fix");
builder.Services.AddMediatR(typeof(Application.DTOs.StudentDto).Assembly);


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