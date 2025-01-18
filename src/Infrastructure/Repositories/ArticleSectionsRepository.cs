using HexaContent.Core.Model;
using HexaContent.Core.Repositories;
using HexaContent.Infrastructure.Database;

namespace HexaContent.Infrastructure.Repositories;

/// <summary>
/// Repository for managing article sections in the database.
/// </summary>
public class ArticleSectionsRepository : RepositoryBase<ArticleSection, long>, IArticleSectionsRepository
{
    public ArticleSectionsRepository(DatabaseContext _context) : base(_context, _context.ArticleSections)
    {
    }
}
