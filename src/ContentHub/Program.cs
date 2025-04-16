using HexaContent.Infrastructure.Extension;
using HexaContent.Services.Extension;
using HexaContent.Minio.Client;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddDbContext(builder.Configuration);
builder.AddRepositories();
builder.AddMessageBus();
builder.AddServices();
builder.AddMinioClient("bucket");

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseRouting();

app.MapStaticAssets();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}")
	.WithStaticAssets();

app.Services.EnsureDatabaseCreated();
await app.Services.SeedTestDataAsync();

app.Run();
