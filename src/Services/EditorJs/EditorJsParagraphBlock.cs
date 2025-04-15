namespace HexaContent.Services.EditorJs;

public sealed class EditorJsParagraphBlock : EditorJsBlock
{
	public EditorJsParagraphBlockData Data { get; set; }

	public override string Render()
	{
		if (Data == null || string.IsNullOrEmpty(Data.Text))
		{
			return string.Empty;
		}

		return $"<p>{Data.Text}</p>";
	}
}
