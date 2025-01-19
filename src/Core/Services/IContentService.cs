using HexaContent.Core.Model;

namespace HexaContent.Core.Services
{
    public interface IContentService
    {
        Task<Result> CreateArticle(Article model);
        Task<Result> UpdateArticle(Article model);
        Task<Result> ArchiveArticle(long articleId);
        Task<Result> PublishArticle(long articleId);
        Task<Result> ScheduleArticle(long articleId);
    }
}
