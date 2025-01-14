namespace HexaContent.Core.Repositories;

public interface IRepository<TModel, TKey>
{
	Task<TModel?> FindAsync(TKey id);
	Task DeleteAsync(TKey id);
	Task AddAsync(TModel model);
	Task<int> CountAsync();
}