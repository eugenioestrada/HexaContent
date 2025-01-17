using HexaContent.Core.Model;
using HexaContent.Core.Repositories;
using HexaContent.Infrastructure.Database;

namespace HexaContent.Infrastructure.Repositories;

/// <summary>
/// Repository for managing authors in the database.
/// </summary>
public class AuthorsRepository : RepositoryBase<Author>, IAuthorsRepository
{
	public AuthorsRepository(DatabaseContext _context) : base(_context, _context.Authors)
	{
	}
}
