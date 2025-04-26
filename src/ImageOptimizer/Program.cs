using HexaContent.ImageOptimizer;
using SixLabors.ImageSharp.Web.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

/* builder.Services.AddImageSharp(options =>
{
}).AddProvider<CustomImageProvider>();*/

var app = builder.Build();

// app.UseImageSharp();

app.MapGet("/_health", () => "Ok!");

app.Run();
