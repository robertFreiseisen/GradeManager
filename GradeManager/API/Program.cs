using API.Controllers;
using Core.Logic;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Shared.Entities;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<LuaScriptRunner>();
builder.Services.AddControllers().AddJsonOptions(x =>x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddTransient<GradeCalculator>();
builder.Services.AddSingleton<ImportService>();
builder.Services.AddSingleton<ApplicationDbContext>();

//ImportService importService = new ImportService();
//var schoolClasses = importService.ImportSchoolClasses();

var app = builder.Build();

var context = app.Services.GetService<ApplicationDbContext>()!;
await context.Database.EnsureDeletedAsync();
await context.Database.EnsureCreatedAsync();
//await context.Database.MigrateAsync();

var import = app.Services.GetService<ImportService>();
await import!.ImportSubjectsAsync();
await import!.ImportTeachersAsync();
await import!.ImportSchoolClassesAsync();
await import!.ImportGradeKindsAsync();
await import!.ImportGradesToStudentsAsync();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{

//}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
