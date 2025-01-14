using System.ComponentModel.DataAnnotations.Schema;

namespace HexaContent.Infrastructure.Database;

[Table("articles")]
public class ArticleEntity
{
	public int Id { get; set; }
	public string Title { get; set; }
	public string Content { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	public AuthorEntity Author { get; set; }
}
