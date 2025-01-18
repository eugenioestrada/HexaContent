using HexaContent.Core.Model;
using HexaContent.Core.Repositories;
using HexaContent.Infrastructure.Database;

namespace HexaContent.Infrastructure.Repositories;

/// <summary>
/// Repository for managing section meta in the database.
/// </summary>
public class SectionMetaRepository : RepositoryBase<SectionMeta, int>, ISectionMetaRepository
{
    public SectionMetaRepository(DatabaseContext _context) : base(_context, _context.SectionMeta)
    {
    }
}
