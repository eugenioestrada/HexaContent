using HexaContent.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

builder.AddMySqlDataSource("mysql");

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();