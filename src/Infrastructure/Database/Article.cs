namespace HexaContent.Infrastructure.Database;

public class Article
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Content { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }

	public Author Author { get; set; }
}
