using AutoMapper;
using HexaContent.Core.Model;
using HexaContent.Core.Repositories;
using HexaContent.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace HexaContent.Infrastructure.Repositories;

public class ArticlesRepository : IArticlesRepository
{
	protected readonly DatabaseContext _databaseContext;
	protected readonly IMapper _mapper;

	public ArticlesRepository(DatabaseContext databaseContext, IMapper mapper)
	{
		this._databaseContext = databaseContext;
		this._mapper = mapper;
	}

	public Task AddAsync(Article model)
	{
		var entity = _mapper.Map<ArticleEntity>(model);
		_databaseContext.Add(entity);
		return _databaseContext.SaveChangesAsync();
	}

	public Task<int> CountAsync()
	{
		return _databaseContext.Articles.CountAsync();
	}

	public Task DeleteAsync(int id)
	{
		_databaseContext.Remove<ArticleEntity>(new()
		{
			Id = id
		});

		return this._databaseContext.SaveChangesAsync();
	}

	public async Task<Article?> FindAsync(int id)
	{
		var entity = await _databaseContext.Articles.FindAsync(id);

		if (entity != null)
		{
			return _mapper.Map<Article>(entity);
		}

		return null;
	}
}
