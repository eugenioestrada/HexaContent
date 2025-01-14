using HexaContent.Infrastructure.Database;
using HexaContent.Infrastructure.Extension;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
builder.AddDbContext(builder.Configuration);

var app = builder.Build();

app.MapGet("/", async (DatabaseContext db) => await db.Articles.CountAsync());

using (var scope = app.Services.CreateScope())
{
	if (!scope.ServiceProvider.GetRequiredService<DatabaseContext>().Database.EnsureCreated())
	{
		Console.WriteLine("FAIL: Database does not exist.");
		return;
	}
}

app.Run();