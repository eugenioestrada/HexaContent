namespace HexaContent.Services.EditorJs;

public class EditorJsListItem
{
	public string Content { get; set; }

	public EditorJsListItemMeta Meta { get; set; }

	public List<EditorJsListItem> Items { get; set; }

	public string Render() => $"<li>{Content}</li>";
}