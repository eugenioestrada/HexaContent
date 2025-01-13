namespace HexaContent.Infrastructure.Database;

public class Author
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Email { get; set; }
	public string Bio { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	public List<Article> Articles { get; } = new();
}
