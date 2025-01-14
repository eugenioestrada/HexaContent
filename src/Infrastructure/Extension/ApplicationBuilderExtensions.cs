using HexaContent.Infrastructure.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using HexaContent.Core.Repositories;
using HexaContent.Infrastructure.Repositories;
using HexaContent.Infrastructure.Mapping;

namespace HexaContent.Infrastructure.Extension;

public static class ApplicationBuilderExtensions
{
	public static void AddAutoMapper(this IHostApplicationBuilder builder)
	{
		builder.Services.AddAutoMapper(typeof(MappingProfile));
	}

	public static void AddRepositories(this IHostApplicationBuilder builder)
	{
		builder.Services.AddScoped<IArticlesRepository, ArticlesRepository>();
	}

	public static void AddDbContext(this IHostApplicationBuilder builder, IConfiguration configuration)
	{
		builder.Services.AddDbContext<DatabaseContext>(options =>
		{
			ServerVersion serverVersion = new MySqlServerVersion(new Version(9, 1, 0));
			string connectionString = configuration.GetConnectionString("mysqldb");
			options.UseMySql(connectionString, serverVersion);
		});
	}

	public static bool EnsureDatabaseCreated(this IServiceProvider serviceProvider)
	{
		using (var scope = serviceProvider.CreateScope())
		{
			return scope.ServiceProvider.GetRequiredService<DatabaseContext>().Database.EnsureCreated();
		}
	}
}
