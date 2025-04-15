namespace HexaContent.Services.EditorJs;

public sealed class EditorJsQuoteBlock : EditorJsBlock
{
	public EditorJsQuoteBlockData Data { get; set; }
	public override string Render()
	{
		if (Data == null || string.IsNullOrEmpty(Data.Text))
		{
			return string.Empty;
		}

		string caption = string.IsNullOrEmpty(Data.Caption) ? string.Empty : $"<figcaption>{Data.Caption}</figcaption>";

		return $"""
			<figure>
				<blockquote>{Data.Text}</blockquote>
				{caption}
			</figure>
			""";
	}
}