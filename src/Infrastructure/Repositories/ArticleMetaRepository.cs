using HexaContent.Core.Model;
using HexaContent.Core.Repositories;
using HexaContent.Infrastructure.Database;

namespace HexaContent.Infrastructure.Repositories;

/// <summary>
/// Repository for managing article meta in the database.
/// </summary>
public class ArticleMetaRepository : RepositoryBase<ArticleMeta, long>, IArticleMetaRepository
{
    public ArticleMetaRepository(DatabaseContext _context) : base(_context, _context.Set<ArticleMeta>())
    {
    }
}
