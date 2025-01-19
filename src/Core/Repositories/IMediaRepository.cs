using HexaContent.Core.Model;
using HexaContent.Core.Repositories.Generic;

namespace HexaContent.Core.Repositories;

/// <summary>
/// Interface for managing media in the repository.
/// </summary>
public interface IMediaRepository : IRepository<Media, long>
{
}
