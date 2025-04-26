using HexaContent.Infrastructure.Extension;
using HexaContent.Services.Extension;
using HexaContent.Minio.Client;
using Auth0.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddDbContext(builder.Configuration);
builder.AddRepositories();
builder.AddMessageBus();
builder.AddServices();
builder.AddMinioClient("bucket");

builder.Services.AddAuth0WebAppAuthentication(options =>
{
	options.Domain = builder.Configuration["Auth0:Domain"];
	options.ClientId = builder.Configuration["Auth0:ClientId"];
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}")
	.WithStaticAssets();

app.Services.EnsureDatabaseCreated();
await app.Services.SeedTestDataAsync();

app.Run();
