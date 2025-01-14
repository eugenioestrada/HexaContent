using AutoMapper;
using HexaContent.Core.Model;
using HexaContent.Core.Repositories;
using HexaContent.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace HexaContent.Infrastructure.Repositories;

/// <summary>
/// Repository for managing articles in the database.
/// </summary>
public class ArticlesRepository : RepositoryBase, IArticlesRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ArticlesRepository"/> class.
    /// </summary>
    /// <param name="databaseContext">The database context.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public ArticlesRepository(DatabaseContext databaseContext, IMapper mapper) : base(databaseContext, mapper)
    {
    }

    /// <summary>
    /// Adds a new article to the repository.
    /// </summary>
    /// <param name="model">The article to add.</param>
    public Task AddAsync(Article model)
    {
        var entity = _mapper.Map<ArticleEntity>(model);
        _databaseContext.Add(entity);
        return _databaseContext.SaveChangesAsync();
    }

    /// <summary>
    /// Counts the total number of articles in the repository.
    /// </summary>
    /// <returns>The total number of articles.</returns>
    public Task<int> CountAsync()
    {
        return _databaseContext.Articles.CountAsync();
    }

    /// <summary>
    /// Deletes an article by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the article.</param>
    public Task DeleteAsync(int id)
    {
        _databaseContext.Remove<ArticleEntity>(new()
        {
            Id = id
        });

        return this._databaseContext.SaveChangesAsync();
    }

    /// <summary>
    /// Finds an article by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the article.</param>
    /// <returns>The article if found; otherwise, null.</returns>
    public async Task<Article?> FindAsync(int id)
    {
        var entity = await _databaseContext.Articles.FindAsync(id);

        if (entity != null)
        {
            return _mapper.Map<Article>(entity);
        }

        return null;
    }

    /// <summary>
    /// Retrieves all articles from the repository.
    /// </summary>
    /// <returns>A list of all articles.</returns>
    public async Task<List<Article>> GetAllAsync()
    {
        var entities = await _databaseContext.Articles.Include(a => a.Author).ToListAsync();
        return _mapper.Map<List<Article>>(entities);
    }
}
