using System.Text;

namespace HexaContent.Services.EditorJs;

public class EditorJsContent
{
	public long Time { get; set; }
	public string Version { get; set; }
	public List<EditorJsBlock> Blocks { get; set; }

	public string Render()
	{
		if (Blocks == null || Blocks.Count == 0)
		{
			return string.Empty;
		}
		var renderedContent = new StringBuilder();
		
		foreach (var block in Blocks)
		{
			renderedContent.Append(block.Render());
		}

		return renderedContent.ToString();
	}
}
