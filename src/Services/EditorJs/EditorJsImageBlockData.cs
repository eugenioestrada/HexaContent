namespace HexaContent.Services.EditorJs;

public class EditorJsImageBlockData
{
	public EditorJsImageBlockDataFile File { get; set; }
	public string Caption { get; set; }
	public bool WithBorder { get; set; } = false;
	public bool WithBackground { get; set; } = false;
	public bool Stretched { get; set; } = false;
}