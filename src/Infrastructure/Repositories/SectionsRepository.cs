using HexaContent.Core.Model;
using HexaContent.Core.Repositories;
using HexaContent.Infrastructure.Database;

namespace HexaContent.Infrastructure.Repositories;

/// <summary>
/// Repository for managing sections in the database.
/// </summary>
public class SectionsRepository : RepositoryBase<Section, int>, ISectionsRepository
{
    public SectionsRepository(DatabaseContext _context) : base(_context, _context.Sections)
    {
    }
}
