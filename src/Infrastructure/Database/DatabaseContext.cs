using Microsoft.EntityFrameworkCore;

namespace HexaContent.Infrastructure.Database;

public class DatabaseContext : DbContext
{
	public DbSet<ArticleEntity> Articles { get; set; }
	public DbSet<AuthorEntity> Authors { get; set; }

	public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
	{
	}
}