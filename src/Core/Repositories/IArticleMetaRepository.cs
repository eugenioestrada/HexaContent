using HexaContent.Core.Model;
using HexaContent.Core.Repositories.Generic;

namespace HexaContent.Core.Repositories;

/// <summary>
/// Interface for managing article meta in the repository.
/// </summary>
public interface IArticleMetaRepository : IRepository<ArticleMeta, long>
{
}
