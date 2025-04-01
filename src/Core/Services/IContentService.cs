using HexaContent.Core.Model;

namespace HexaContent.Core.Services;

/// <summary>
/// Provides operations for managing article content in the system.
/// </summary>
public interface IContentService
{
	/// <summary>
	/// Creates a new draft article for the specified author.
	/// </summary>
	/// <param name="authorId">The ID of the author creating the article.</param>
	/// <returns>A result containing the newly created article if successful.</returns>
	Task<Result<Article>> NewArticle(int authorId);

	/// <summary>
	/// Creates an article based on the provided model.
	/// </summary>
	/// <param name="model">The article model containing all necessary information.</param>
	/// <returns>A result containing the created article if successful.</returns>
	Task<Result<Article>> CreateArticle(Article model);

	/// <summary>
	/// Updates an existing article with the provided model data.
	/// </summary>
	/// <param name="model">The article model containing updated information.</param>
	/// <returns>A result indicating whether the update was successful.</returns>
	Task<Result<bool>> UpdateArticle(Article model);

	/// <summary>
	/// Archives an article, changing its status to Archived.
	/// </summary>
	/// <param name="articleId">The ID of the article to archive.</param>
	/// <returns>A result indicating whether the archiving was successful.</returns>
	Task<Result<bool>> ArchiveArticle(long articleId);

	/// <summary>
	/// Publishes an article, making it visible to the public.
	/// </summary>
	/// <param name="articleId">The ID of the article to publish.</param>
	/// <returns>A result indicating whether the publishing was successful.</returns>
	Task<Result<bool>> PublishArticle(long articleId);

	/// <summary>
	/// Schedules an article for future publication.
	/// </summary>
	/// <param name="articleId">The ID of the article to schedule.</param>
	/// <returns>A result indicating whether the scheduling was successful.</returns>
	Task<Result<bool>> ScheduleArticle(long articleId);

	/// <summary>
	/// Retrieves a paginated list of articles.
	/// </summary>
	/// <param name="max">The maximum number of articles to retrieve. Default is 50.</param>
	/// <param name="from">The starting index for pagination. Default is 0.</param>
	/// <returns>A result containing the collection of articles if successful.</returns>
	Task<Result<IEnumerable<Article>>> GetAll(int max = 50, int from = 0);

	/// <summary>
	/// Retrieves a specific article by its ID.
	/// </summary>
	/// <param name="articleId">The ID of the article to retrieve.</param>
	/// <returns>A result containing the requested article if successful.</returns>
	Task<Result<Article>> GetArticle(long articleId);
}
