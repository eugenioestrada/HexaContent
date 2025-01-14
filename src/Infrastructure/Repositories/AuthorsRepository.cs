using AutoMapper;
using HexaContent.Core.Model;
using HexaContent.Core.Repositories;
using HexaContent.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace HexaContent.Infrastructure.Repositories;

/// <summary>
/// Repository for managing authors in the database.
/// </summary>
public class AuthorsRepository : RepositoryBase, IAuthorsRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorsRepository"/> class.
    /// </summary>
    /// <param name="databaseContext">The database context.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public AuthorsRepository(DatabaseContext databaseContext, IMapper mapper) : base(databaseContext, mapper)
	{
	}

    /// <summary>
    /// Adds a new author to the repository.
    /// </summary>
    /// <param name="model">The author to add.</param>
    public Task AddAsync(Author model)
    {
        var entity = _mapper.Map<AuthorEntity>(model);
        _databaseContext.Add(entity);
        return _databaseContext.SaveChangesAsync();
    }

    /// <summary>
    /// Counts the total number of authors in the repository.
    /// </summary>
    /// <returns>The total number of authors.</returns>
    public Task<int> CountAsync()
    {
        return _databaseContext.Authors.CountAsync();
    }

    /// <summary>
    /// Deletes an author by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the author.</param>
    public Task DeleteAsync(int id)
    {
        _databaseContext.Remove<AuthorEntity>(new()
        {
            Id = id
        });

        return this._databaseContext.SaveChangesAsync();
    }

    /// <summary>
    /// Finds an author by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the author.</param>
    /// <returns>The author if found; otherwise, null.</returns>
    public async Task<Author?> FindAsync(int id)
    {
        var entity = await _databaseContext.Authors.FindAsync(id);

        if (entity != null)
        {
            return _mapper.Map<Author>(entity);
        }

        return null;
    }

    /// <summary>
    /// Retrieves all authors from the repository.
    /// </summary>
    /// <returns>A list of all authors.</returns>
    public async Task<List<Author>> GetAllAsync()
    {
        var entities = await _databaseContext.Authors.ToListAsync();
        return _mapper.Map<List<Author>>(entities);
    }

    /// <summary>
    /// Updates an existing author in the repository.
    /// </summary>
    /// <param name="model">The author to update.</param>
    public async Task UpdateAsync(Author model)
    {
        var entity = await _databaseContext.Authors.FindAsync(model.Id);

        if (entity != null)
        {
            entity.Name = model.Name;
            entity.Email = model.Email;
            entity.Bio = model.Bio;
            entity.UpdatedAt = model.UpdatedAt;

            await _databaseContext.SaveChangesAsync();
        }
    }
}
