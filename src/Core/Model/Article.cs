using System.ComponentModel.DataAnnotations.Schema;

namespace HexaContent.Core.Model;

/// <summary>
/// Represents an article in the system.
/// </summary>
[Table("article")]
public sealed class Article : EntityBase<long>
{
	/// <summary>
	/// Gets or sets the title of the article.
	/// </summary>
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the content of the article.
	/// </summary>
	public string Content { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the date and time when the article was published.
	/// </summary>
	public DateTime? PublishedAt { get; set; }

	/// <summary>
	/// Gets or sets the date and time when the article was last updated.
	/// </summary>
	public DateTime UpdatedAt { get; set; }

	/// <summary>
	/// Gets or sets the author id.
	/// </summary>
	public int AuthorId { get; set; }

	/// <summary>
	/// Gets or sets the status of the article.
	/// </summary>
	public ArticleStatus Status { get; set; } = ArticleStatus.Draft;

	/// <summary>
	/// Gets or sets the author of the article.
	/// </summary>
	public Author? Author { get; set; }

	/// <summary>
	/// Gets or sets the metadata associated with the article.
	/// </summary>
	public List<ArticleMeta>? Meta { get; set; }

	/// <summary>
	/// Gets or sets the sections of the article.
	/// </summary>
	public List<ArticleSection>? Sections { get; set; }

	/// <summary>
	/// Gets or sets the featured media id.
	/// </summary>
	public long? FeaturedMediaId { get; set; }

	/// <summary>
	/// Gets or sets the featured media.
	/// </summary>
	public Media? FeaturedMedia { get; set; }
}
