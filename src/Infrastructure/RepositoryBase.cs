using HexaContent.Core;
using HexaContent.Core.Repositories.Generic;
using HexaContent.Core.Utils;
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

	public async Task<TEntity?> FindAsync(TKey id)
	{
		return await DbSet.FindAsync(id);
	}

	public Task<List<TEntity>> Get(Expression<Func<TEntity, bool>>? predicate = null, int ? max = null, int? from = null, Expression<Func<TEntity, object>>? orderBy = null, Expression<Func<TEntity, object>>? orderByDesc = null)
	{
		Argument.EnsuresNot(orderBy != null && orderByDesc != null, "Cannot specify both an order by and an order by descending");

		IQueryable<TEntity> query = DbSet;

		if (predicate != null)
		{
			query = query.Where(predicate);
		}

		if (orderBy != null)
		{
			query = query.OrderBy(orderBy);
		}
		else if (orderByDesc != null)
		{
			query = query.OrderByDescending(orderByDesc);
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