using HexaContent.Core.Model;
using HexaContent.Core.Repositories.Generic;

namespace HexaContent.Core.Repositories;

/// <summary>
/// Interface for managing media meta in the repository.
/// </summary>
public interface IMediaMetaRepository : IRepository<MediaMeta, long>
{
}
