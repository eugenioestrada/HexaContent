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
    public async Task AddAsync(Article model)
    {
		// Check if the author exists
		var authorEntity = await _databaseContext.Authors
			.FirstOrDefaultAsync(a => a.Id == model.Author.Id);

		// If the author does not exist, create a new author
		if (authorEntity == null)
		{
			authorEntity = _mapper.Map<AuthorEntity>(model.Author);
			_databaseContext.Authors.Add(authorEntity);
			await _databaseContext.SaveChangesAsync();
		}

		// Map the article model to the article entity
		var articleEntity = _mapper.Map<ArticleEntity>(model);
		articleEntity.Author = authorEntity;

		// Add the article entity to the database
		_databaseContext.Articles.Add(articleEntity);
		await _databaseContext.SaveChangesAsync();
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
			entity.Author = await _databaseContext.Authors.FindAsync(entity.AuthorId);

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

    /// <summary>
    /// Updates an existing article in the repository.
    /// </summary>
    /// <param name="model">The article to update.</param>
    public async Task UpdateAsync(Article model)
    {
        var entity = await _databaseContext.Articles.FindAsync(model.Id);

        if (entity != null)
        {
            entity.Title = model.Title;
            entity.Content = model.Content;
            entity.UpdatedAt = model.UpdatedAt;

            if (entity.Author.Id != model.Author.Id)
            {
                var authorEntity = await _databaseContext.Authors.FindAsync(model.Author.Id);
                if (authorEntity != null)
                {
                    entity.Author = authorEntity;
                }
            }

            await _databaseContext.SaveChangesAsync();
        }
    }
}
