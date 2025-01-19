using HexaContent.Core.Model;
using HexaContent.Core.Repositories;
using HexaContent.Infrastructure.Database;

namespace HexaContent.Infrastructure.Repositories;

public class MediaMetaRepository : RepositoryBase<MediaMeta, long>, IMediaMetaRepository
{
    public MediaMetaRepository(DatabaseContext context) : base(context, context.Set<MediaMeta>())
    {
    }
}
