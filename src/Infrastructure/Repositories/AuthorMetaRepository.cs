using HexaContent.Core.Model;
using HexaContent.Core.Repositories;
using HexaContent.Infrastructure.Database;

namespace HexaContent.Infrastructure.Repositories;

/// <summary>
/// Repository for managing author meta in the database.
/// </summary>
public class AuthorMetaRepository : RepositoryBase<AuthorMeta, int>, IAuthorMetaRepository
{
    public AuthorMetaRepository(DatabaseContext _context) : base(_context, _context.AuthorMeta)
    {
    }
}
