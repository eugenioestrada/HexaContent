﻿using HexaContent.Core.Model;
using HexaContent.Core.Repositories;
using HexaContent.Infrastructure.Database;

namespace HexaContent.Infrastructure.Repositories;

/// <summary>
/// Repository for managing articles in the database.
/// </summary>
public class ArticlesRepository : RepositoryBase<Article>, IArticlesRepository
{
	public ArticlesRepository(DatabaseContext _context) : base(_context, _context.Articles)
	{
	}
}
