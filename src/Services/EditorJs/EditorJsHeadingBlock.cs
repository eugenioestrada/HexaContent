namespace HexaContent.Services.EditorJs;

public sealed class EditorJsHeadingBlock : EditorJsBlock
{
	public EditorJsHeadingBlockData Data { get; set; }

	public override string Render()
	{
		if (Data == null || string.IsNullOrEmpty(Data.Text))
		{
			return string.Empty;
		}

		return $"<h{Data.Level}>{Data.Text}</h{Data.Level}>";
	}
}
