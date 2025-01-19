using HexaContent.Core.Model;

namespace HexaContent.Core.Services
{
    public interface IContentService
    {
        Task<Result<Article>> NewArticle();
        Task<Result<Article>> CreateArticle(Article model);
        Task<Result<bool>> UpdateArticle(Article model);
        Task<Result<bool>> ArchiveArticle(long articleId);
        Task<Result<bool>> PublishArticle(long articleId);
        Task<Result<bool>> ScheduleArticle(long articleId);
        Task<Result<IEnumerable<Article>>> GetAll();
		Task<Result<Article>> GetArticle(long articleId);
	}
}
