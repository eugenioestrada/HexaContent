using HexaContent.Minio.Client;
using HexaContent.StaticForge;

var builder = Host.CreateApplicationBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();
builder.AddMinioClient("bucket");
var host = builder.Build();

host.Run();
