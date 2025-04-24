namespace HexaContent.Services.EditorJs;

public class EditorJsTableBlockData
{
	public List<List<string>> Content { get; set; } = new();
	public bool WithHeadings { get; set; } = false;
	public bool Stretched { get; set; } = false;
}