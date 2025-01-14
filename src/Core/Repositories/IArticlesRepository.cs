using HexaContent.Core.Model;

namespace HexaContent.Core.Repositories;

/// <summary>
/// Interface for managing articles in the repository.
/// </summary>
public interface IArticlesRepository : IRepository<Article, int>
{
}
