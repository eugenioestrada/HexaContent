using System.ComponentModel.DataAnnotations.Schema;

namespace HexaContent.Infrastructure.Database;

[Table("authors")]
public class AuthorEntity
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string Email { get; set; }
	public string Bio { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	public List<ArticleEntity> Articles { get; } = new();
}
