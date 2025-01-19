using HexaContent.Core;
using HexaContent.Core.Model;
using HexaContent.Core.Repositories;
using HexaContent.Core.Services;

namespace HexaContent.Services;

public class ContentService(IArticlesRepository _articlesRepository) : IContentService
{
    public async Task<Result<Article>> CreateArticle(Article article)
    {
        try
        {
            _articlesRepository.Add(article);
            await _articlesRepository.SaveChangesAsync();
            return Result<Article>.Success(article);
        }
        catch (Exception ex)
        {
            return Result<Article>.Failure(ex.Message);
        }
    }

    public async Task<Result<bool>> UpdateArticle(Article article)
    {
        try
        {
            _articlesRepository.Update(article);
            await _articlesRepository.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(ex.Message);
        }
    }

    public async Task<Result<bool>> ArchiveArticle(long articleId)
    {
        try
        {
            var article = await _articlesRepository.FindAsync(articleId);
            if (article != null)
            {
                article.Status = ArticleStatus.Archived;
                await _articlesRepository.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            return Result<bool>.Failure("Article not found");
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(ex.Message);
        }
    }

    public async Task<Result<bool>> PublishArticle(long articleId)
    {
        try
        {
            var article = await _articlesRepository.FindAsync(articleId);
            if (article != null)
            {
                article.Status = ArticleStatus.Published;
                article.PublishedAt = DateTime.UtcNow;
                await _articlesRepository.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            return Result<bool>.Failure("Article not found");
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(ex.Message);
        }
    }

    public async Task<Result<bool>> ScheduleArticle(long articleId)
    {
        try
        {
            var article = await _articlesRepository.FindAsync(articleId);
            if (article != null)
            {
                article.Status = ArticleStatus.Scheduled;
                await _articlesRepository.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            return Result<bool>.Failure("Article not found");
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(ex.Message);
        }
    }

	public async Task<Result<Article>> NewArticle()
	{
        var article = new Article();
		_articlesRepository.Add(article);
		await _articlesRepository.SaveChangesAsync();
		return Result<Article>.Success(article);
	}

	public async Task<Result<IEnumerable<Article>>> GetAll(int max = 50, int from = 0)
	{
		try
		{
			var articles = await _articlesRepository.GetAll(includes: [ a => a.Author ], max: max, from: 0);
			return Result<IEnumerable<Article>>.Success(articles);
		}
		catch (Exception ex)
		{
			return Result<IEnumerable<Article>>.Failure(ex.Message);
		}
	}

	public async Task<Result<Article>> GetArticle(long articleId)
	{
        try
        {
			var article = await _articlesRepository.FindAsync(articleId, includes: [ a => a.Author ]);
			return Result<Article>.Success(article);
		}
		catch (Exception ex)
		{
			return Result<Article>.Failure(ex.Message);
		}
	}
}
