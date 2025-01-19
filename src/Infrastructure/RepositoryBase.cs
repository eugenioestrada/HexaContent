using HexaContent.Core;
using HexaContent.Core.Repositories.Generic;
using HexaContent.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HexaContent.Infrastructure;

public abstract class RepositoryBase<TEntity, TKey>(DatabaseContext _context, DbSet<TEntity> _dbSet) : IRepository<TEntity, TKey>
	where TEntity : EntityBase<TKey>
	where TKey : struct
{
	protected DatabaseContext Context { get; } = _context;
	protected DbSet<TEntity> DbSet { get; } = _dbSet;

	public async Task<TEntity?> FindAsync(TKey id, List<Expression<Func<TEntity, object>>>? includes = null)
	{
		if (includes != null)
		{
			IQueryable<TEntity> query = DbSet;

			foreach (var include in includes)
			{
				query = query.Include(include);
			}

			return await query.Where(t => t.Id.Equals(id)).FirstOrDefaultAsync();
		}

		return await DbSet.FindAsync(id);
	}

	public Task<List<TEntity>> GetAll(List<Expression<Func<TEntity, object>>>? includes = null, int? max = null, int? from = null)
	{
		IQueryable<TEntity> query = DbSet;

		foreach (var include in includes)
		{
			query = query.Include(include);
		}

		if (max.HasValue)
		{
			query = query.Take(max.Value);
		}

		if (from.HasValue)
		{
			query = query.Skip(from.Value);
		}

		return query.ToListAsync();
	}

	public void Add(TEntity model) => DbSet.Add(model);

	public void Delete(TEntity entity) => DbSet.Remove(entity);

	public void Update(TEntity entity) => DbSet.Update(entity);

	public Task SaveChangesAsync() => Context.SaveChangesAsync();

	public void Dispose() => Context.Dispose();
}