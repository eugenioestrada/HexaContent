using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace HexaContent.Infrastructure.Database;

public class DatabaseContext : DbContext
{
	public DbSet<Article> Articles { get; set; }
	public DbSet<Author> Authors { get; set; }

	public DatabaseContext()
	{
	}
}