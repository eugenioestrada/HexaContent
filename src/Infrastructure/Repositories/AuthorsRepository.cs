using AutoMapper;
using HexaContent.Core.Model;
using HexaContent.Core.Repositories;
using HexaContent.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace HexaContent.Infrastructure.Repositories;

public class AuthorsRepository : IAuthorsRepository
{
    protected readonly DatabaseContext _databaseContext;
    protected readonly IMapper _mapper;

    public AuthorsRepository(DatabaseContext databaseContext, IMapper mapper)
    {
        this._databaseContext = databaseContext;
        this._mapper = mapper;
    }

    public Task AddAsync(Author model)
    {
        var entity = _mapper.Map<AuthorEntity>(model);
        _databaseContext.Add(entity);
        return _databaseContext.SaveChangesAsync();
    }

    public Task<int> CountAsync()
    {
        return _databaseContext.Authors.CountAsync();
    }

    public Task DeleteAsync(int id)
    {
        _databaseContext.Remove<AuthorEntity>(new()
        {
            Id = id
        });

        return this._databaseContext.SaveChangesAsync();
    }

    public async Task<Author?> FindAsync(int id)
    {
        var entity = await _databaseContext.Authors.FindAsync(id);

        if (entity != null)
        {
            return _mapper.Map<Author>(entity);
        }

        return null;
    }
}
