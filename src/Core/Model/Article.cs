namespace HexaContent.Core.Model;

/// <summary>
/// Represents an article in the system.
/// </summary>
public class Article
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
	/// Gets or sets the author of the article.
	/// </summary>
	public Author? Author { get; set; }
}
