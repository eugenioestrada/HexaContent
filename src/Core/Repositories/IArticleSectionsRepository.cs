using HexaContent.Core.Model;
using HexaContent.Core.Repositories.Generic;

namespace HexaContent.Core.Repositories;

/// <summary>
/// Interface for managing article sections in the repository.
/// </summary>
public interface IArticleSectionsRepository : IRepository<ArticleSection, long>
{
}
