namespace HexaContent.Core.Repositories.Generic;

public interface IRepository<TEntity, TKey> : IRepository, IDisposable
	where TEntity : EntityBase<TKey>
	where TKey : struct
{
	ValueTask<TEntity?> FindAsync(TKey id);
	void Add(TEntity model);
	void Delete(TEntity entity);
	void Update(TEntity entity);
	IQueryable<TEntity> GetAll();
	Task SaveChangesAsync();
}
