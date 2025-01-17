namespace HexaContent.Core.Repositories.Generic;

public interface IRepository<TEntity> : IRepository where TEntity : EntityBase
{
	ValueTask<TEntity?> FindAsync(int id);
	void Add(TEntity model);
	void Delete(TEntity entity);
	void Update(TEntity entity);
	IQueryable<TEntity> GetAll();
	Task SaveChangesAsync();
}
