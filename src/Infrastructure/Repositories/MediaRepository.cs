using HexaContent.Core.Model;
using HexaContent.Core.Repositories;
using HexaContent.Infrastructure.Database;

namespace HexaContent.Infrastructure.Repositories;

/// <summary>
/// Repository for managing media in the database.
/// </summary>
public class MediaRepository : RepositoryBase<Media, long>, IMediaRepository
{
    public MediaRepository(DatabaseContext context) : base(context, context.Set<Media>())
    {
    }
}
