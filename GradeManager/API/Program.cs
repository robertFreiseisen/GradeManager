using Core.Contracts;
using Core.Logic;
using Persistence;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>(_ => new UnitOfWork());
builder.Services.AddSingleton<ImportController, ImportController>();



builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
var app = builder.Build();

var uow = app.Services.GetService<IUnitOfWork>()!;
uow.MigrateDatabaseAsync().Wait();
var import = app.Services.GetService<ImportController>();

await import.InitUnitOfWork();
//await import?.ReadFromCSV();

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
