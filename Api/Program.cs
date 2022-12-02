using Api.Logging;
using Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// My implementation of Logger
builder.Services.AddSingleton<ILogging, LoggingV2>();
builder.Services.AddDbContext<DbConnect>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultMsSqlConnection"));
});
//option.UseMySql(builder.Configuration.GetConnectionString("DefaultMySqlConnection"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));

builder.Services.AddMvc().AddNewtonsoftJson(o =>
{
    o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

//builder.Services.AddControllers(option => option.ReturnHttpNotAcceptable = true).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

JsonSerializerOptions options = new()
{
    ReferenceHandler = ReferenceHandler.Preserve,
    WriteIndented = true
};

app.Run();

