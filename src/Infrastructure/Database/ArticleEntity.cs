using System.ComponentModel.DataAnnotations.Schema;

namespace HexaContent.Infrastructure.Database;

/// <summary>
/// Represents an article entity in the database.
/// </summary>
[Table("article")]
public class ArticleEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the article.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the article.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the content of the article.
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the article was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the article was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

	/// <summary>
	/// Gets or sets the unique identifier for the author of the article.
    /// </summary>
	public int AuthorId { get; set; }

	/// <summary>
	/// Gets or sets the author of the article.
	/// </summary>
	public AuthorEntity Author { get; set; }
}
