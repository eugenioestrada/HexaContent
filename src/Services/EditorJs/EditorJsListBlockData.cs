namespace HexaContent.Services.EditorJs;

public class EditorJsListBlockData
{
	public string Style { get; set; }
	public EditorJsListBlockMeta Meta { get; set; }
	
	public List<EditorJsListItem> Items { get; set; }
}