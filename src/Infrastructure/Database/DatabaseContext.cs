﻿using Microsoft.EntityFrameworkCore;

namespace HexaContent.Infrastructure.Database;

/// <summary>
/// Represents the database context for the application, providing access to the Articles and Authors DbSets.
/// </summary>
public class DatabaseContext : DbContext
{
    /// <summary>
    /// Gets or sets the DbSet for articles.
    /// </summary>
    public DbSet<ArticleEntity> Articles { get; set; }

    /// <summary>
    /// Gets or sets the DbSet for authors.
    /// </summary>
    public DbSet<AuthorEntity> Authors { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseContext"/> class.
    /// </summary>
    /// <param name="options">The options to be used by the DbContext.</param>
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
}
