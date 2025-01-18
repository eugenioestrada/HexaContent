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

	public ArticleStatus Status { get; set; } = ArticleStatus.Draft;

	/// <summary>
	/// Gets or sets the author of the article.
	/// </summary>
	public Author? Author { get; set; }

	public List<ArticleMeta>? Meta { get; set; }
	public List<ArticleSection>? Sections { get; set; }
}
