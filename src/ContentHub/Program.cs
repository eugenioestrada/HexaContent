using HexaContent.Infrastructure.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddDbContext(builder.Configuration);
builder.AddRepositories();
builder.AddMessageBus();
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
