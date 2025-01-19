using System.Linq.Expressions;

namespace HexaContent.Core.Repositories.Generic;

public interface IRepository<TEntity, TKey> : IRepository, IDisposable
	where TEntity : EntityBase<TKey>
	where TKey : struct
{
	Task<TEntity?> FindAsync(TKey id, List<Expression<Func<TEntity, object>>>? includes = null);
	void Add(TEntity model);
	void Delete(TEntity entity);
	void Update(TEntity entity);
	Task<List<TEntity>> GetAll(List<Expression<Func<TEntity, object>>>? includes = null, int? max = null, int? from = null);
	Task SaveChangesAsync();
}
