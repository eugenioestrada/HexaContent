using HexaContent.Infrastructure.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using HexaContent.Core.Repositories;
using HexaContent.Infrastructure.Repositories;
using HexaContent.Core.Model;
using HexaContent.Core.Messaging;
using HexaContent.Infrastructure.Messaging;
using HexaContent.Core;

namespace HexaContent.Infrastructure.Extension;

/// <summary>
/// Provides extension methods for configuring the application builder.
/// </summary>
public static partial class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds repositories to the application builder.
    /// </summary>
    /// <param name="builder">The application builder.</param>
    public static void AddRepositories(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IArticlesRepository, ArticlesRepository>();
        builder.Services.AddScoped<IAuthorsRepository, AuthorsRepository>();
        builder.Services.AddScoped<IArticleMetaRepository, ArticleMetaRepository>();
        builder.Services.AddScoped<IArticleSectionsRepository, ArticleSectionsRepository>();
        builder.Services.AddScoped<ISectionsRepository, SectionsRepository>();
        builder.Services.AddScoped<ISectionMetaRepository, SectionMetaRepository>();
        builder.Services.AddScoped<IAuthorMetaRepository, AuthorMetaRepository>();
        builder.Services.AddScoped<IMediaRepository, MediaRepository>();
		builder.Services.AddScoped<IMediaMetaRepository, MediaMetaRepository>();
		builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void AddMessageBus(this IHostApplicationBuilder builder)
    {
		builder.AddRabbitMQClient(connectionName: "messaging");
		builder.Services.AddSingleton<IMessageBus, MessageBus>();
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

            var authors = new List<Author>
            {
                new Author
                {
                    Name = "Test Author",
                    Email = "author@example.com",
                    Bio = "Bio",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Author
                {
                    Name = "Jane Doe",
                    Email = "jane.doe@example.com",
                    Bio = "Jane's bio",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Author
                {
                    Name = "John Smith",
                    Email = "john.smith@example.com",
                    Bio = "John's bio",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            foreach (var author in authors)
            {
                authorsRepository.Add(author);
            }

            await authorsRepository.SaveChangesAsync();

            var articles = new List<Article>
            {
                new Article
                {
                    Title = "Test Article",
                    Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    PublishedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Author = authors[0]
                },
                new Article
                {
                    Title = "Another Article",
                    Content = "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
					PublishedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Author = authors[1]
                },
                new Article
                {
                    Title = "Yet Another Article",
                    Content = "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
					PublishedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Author = authors[2]
                }
            };

            foreach (var article in articles)
            {
                articlesRepository.Add(article);
            }

            await articlesRepository.SaveChangesAsync();
		}
    }
}
