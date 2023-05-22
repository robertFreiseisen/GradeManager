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
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<LuaScriptRunner>();
builder.Services.AddTransient<CsScriptMicrosoftRunner>();
builder.Services.AddTransient<JavascriptRunner>();
builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; 
    
});
builder.Services.AddTransient<GradeCalculator>();
builder.Services.AddSingleton<ImportService>();
builder.Services.AddSingleton<ApplicationDbContext>();
builder.Services.AddMvc().AddJsonOptions(x =>
    {
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; 
    
    });

//ImportService importService = new ImportService();
//var schoolClasses = importService.ImportSchoolClasses();

var app = builder.Build();

var context = app.Services.GetService<ApplicationDbContext>()!;
await context.Database.EnsureDeletedAsync();
await context.Database.EnsureCreatedAsync();
//await context.Database.MigrateAsync();

var import = app.Services.GetService<ImportService>();
await import!.ImportSubjectsAsync();
await import!.ImportSchoolClassesAsync();
await import!.ImportTeachersAsync();

foreach (var item in context.SchoolClasses)
{
    foreach (var student in item.Students)
    {
        student.SchoolClassId = item.Id;  
    }
}

await context.SaveChangesAsync();

await import!.ImportGradeKindsAsync();
await import!.ImportGradesToStudentsAsync();

await context.SaveChangesAsync();





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
