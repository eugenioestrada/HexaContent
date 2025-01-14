using HexaContent.Infrastructure.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace HexaContent.Infrastructure.Extension;

public static class ApplicationBuilderExtensions
{
	public static void AddDbContext(this IHostApplicationBuilder builder, IConfiguration configuration)
	{
		builder.Services.AddDbContext<DatabaseContext>(options =>
		{
			ServerVersion serverVersion = new MySqlServerVersion(new Version(9, 1, 0));
			string connectionString = configuration.GetConnectionString("mysqldb");
			options.UseMySql(connectionString, serverVersion);
		});
	}
}
