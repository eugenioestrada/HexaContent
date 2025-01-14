namespace HexaContent.ContentHub.Models;

public class EditArticleModel
{
    public int Id { get; set; }
	public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int AuthorId { get; set; }
}
