using HexaContent.Minio.Client;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.AddMinioClient("minio");

var app = builder.Build();

app.MapGet("/", (MinioFactory minio) => 
	"Hello World!");

app.Run();