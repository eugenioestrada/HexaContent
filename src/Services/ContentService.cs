using HexaContent.Core;
using HexaContent.Core.Model;
using HexaContent.Core.Repositories;
using HexaContent.Core.Services;

namespace HexaContent.Services;

public class ContentService : IContentService
{
    private readonly IArticlesRepository _articlesRepository;

    public ContentService(IArticlesRepository articlesRepository)
    {
        _articlesRepository = articlesRepository;
    }

    public async Task<Result> CreateArticle(Article article)
    {
        try
        {
            _articlesRepository.Add(article);
            await _articlesRepository.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(ex.Message);
        }
    }

    public async Task<Result> UpdateArticle(Article article)
    {
        try
        {
            _articlesRepository.Update(article);
            await _articlesRepository.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(ex.Message);
        }
    }

    public async Task<Result> ArchiveArticle(long articleId)
    {
        try
        {
            var article = await _articlesRepository.FindAsync(articleId);
            if (article != null)
            {
                article.Status = ArticleStatus.Archived;
                await _articlesRepository.SaveChangesAsync();
                return Result.Success();
            }
            return Result.Failure("Article not found");
        }
        catch (Exception ex)
        {
            return Result.Failure(ex.Message);
        }
    }

    public async Task<Result> PublishArticle(long articleId)
    {
        try
        {
            var article = await _articlesRepository.FindAsync(articleId);
            if (article != null)
            {
                article.Status = ArticleStatus.Published;
                article.PublishedAt = DateTime.UtcNow;
                await _articlesRepository.SaveChangesAsync();
                return Result.Success();
            }
            return Result.Failure("Article not found");
        }
        catch (Exception ex)
        {
            return Result.Failure(ex.Message);
        }
    }

    public async Task<Result> ScheduleArticle(long articleId)
    {
        try
        {
            var article = await _articlesRepository.FindAsync(articleId);
            if (article != null)
            {
                article.Status = ArticleStatus.Scheduled;
                await _articlesRepository.SaveChangesAsync();
                return Result.Success();
            }
            return Result.Failure("Article not found");
        }
        catch (Exception ex)
        {
            return Result.Failure(ex.Message);
        }
    }
}
