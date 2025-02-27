using System.Linq.Expressions;

namespace HexaContent.Core.Repositories.Generic;

public interface IRepository<TEntity, TKey> : IRepository, IDisposable
	where TEntity : EntityBase<TKey>
	where TKey : struct
{
	Task<TEntity?> FindAsync(TKey id);
	void Add(TEntity model);
	void Delete(TEntity entity);
	void Update(TEntity entity);
	Task<List<TEntity>> Get(Expression<Func<TEntity, bool>>? predicate = null, int? max = null, int? from = null, Expression<Func<TEntity, object>>? orderBy = null, Expression<Func<TEntity, object>>? orderByDesc = null);
	Task SaveChangesAsync();
}
