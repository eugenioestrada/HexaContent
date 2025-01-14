using Microsoft.EntityFrameworkCore;

namespace HexaContent.Infrastructure.Database;

public class DatabaseContext : DbContext
{
	public DbSet<Article> Articles { get; set; }
	public DbSet<Author> Authors { get; set; }

	public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
	{
	}
}