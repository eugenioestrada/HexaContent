using HexaContent.Core;
using HexaContent.Core.Repositories.Generic;
using HexaContent.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace HexaContent.Infrastructure.Repositories;

public abstract class RepositoryBase<TEntity, Tkey>(DatabaseContext _context, DbSet<TEntity> _dbSet) : IRepository<TEntity, Tkey>
	where TEntity : EntityBase<Tkey>
	where Tkey : struct
{
	protected DatabaseContext Context { get; } = _context;
	protected DbSet<TEntity> DbSet { get; } = _dbSet;

	public ValueTask<TEntity?> FindAsync(Tkey id) => DbSet.FindAsync(id);

	public IQueryable<TEntity> GetAll() => DbSet.AsQueryable();

	public void Add(TEntity model) => DbSet.Add(model);

	public void Delete(TEntity entity) => DbSet.Remove(entity);

	public void Update(TEntity entity) => DbSet.Update(entity);

	public Task SaveChangesAsync() => Context.SaveChangesAsync();

	public void Dispose() => this.Context.Dispose();
}