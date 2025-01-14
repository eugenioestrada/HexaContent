using HexaContent.Infrastructure.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using HexaContent.Core.Repositories;
using HexaContent.Infrastructure.Repositories;
using HexaContent.Infrastructure.Mapping;
using HexaContent.Core.Model;

namespace HexaContent.Infrastructure.Extension;

/// <summary>
/// Provides extension methods for configuring the application builder.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds AutoMapper to the application builder.
    /// </summary>
    /// <param name="builder">The application builder.</param>
    public static void AddAutoMapper(this IHostApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(MappingProfile));
    }

    /// <summary>
    /// Adds repositories to the application builder.
    /// </summary>
    /// <param name="builder">The application builder.</param>
    public static void AddRepositories(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IArticlesRepository, ArticlesRepository>();
        builder.Services.AddScoped<IAuthorsRepository, AuthorsRepository>();
    }

    /// <summary>
    /// Adds the database context to the application builder.
    /// </summary>
    /// <param name="builder">The application builder.</param>
    /// <param name="configuration">The configuration.</param>
    public static void AddDbContext(this IHostApplicationBuilder builder, IConfiguration configuration)
    {
        builder.Services.AddDbContext<DatabaseContext>(options =>
        {
            ServerVersion serverVersion = new MySqlServerVersion(new Version(9, 1, 0));
            string connectionString = configuration.GetConnectionString("mysqldb");
            options.UseMySql(connectionString, serverVersion);
        });
    }

    /// <summary>
    /// Ensures that the database is created.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <returns>True if the database is created; otherwise, false.</returns>
    public static bool EnsureDatabaseCreated(this IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            return scope.ServiceProvider.GetRequiredService<DatabaseContext>().Database.EnsureCreated();
        }
    }

    /// <summary>
    /// Seeds test data into the database.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    public static async Task SeedTestDataAsync(this IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var authorsRepository = scope.ServiceProvider.GetRequiredService<IAuthorsRepository>();
            var articlesRepository = scope.ServiceProvider.GetRequiredService<IArticlesRepository>();

            var author = new Author
            {
                Id = 1,
                Name = "Test Author",
                Email = "author@example.com",
                Bio = "Bio",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await authorsRepository.AddAsync(author);

            var article = new Article
            {
                Id = 1,
                Title = "Test Article",
                Content = "Content",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Author = author
            };

            await articlesRepository.AddAsync(article);
        }
    }
}
