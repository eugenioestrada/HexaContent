using System.Web;

namespace HexaContent.Services.EditorJs;

public sealed class EditorJsImageBlock : EditorJsBlock
{
	public EditorJsImageBlockData Data { get; set; }

	public override string Render()
	{
		bool hasCaption = !string.IsNullOrEmpty(Data.Caption);
		bool hasBorder = Data.WithBorder;
		bool hasBackground = Data.WithBackground;
		bool isStretched = Data.Stretched;

		string alt = string.Empty;
		string caption = string.Empty;

		List<string> classes = ["image"];

		if (hasCaption)
		{
			string encodedCaption = HttpUtility.HtmlEncode(Data.Caption);
			alt = $"alt=\"{encodedCaption}\"";
			caption = $"<figcaption>{Data.Caption}</figcaption>";
			classes.Add("with-caption");
		}

		if (hasBorder)
		{
			classes.Add("with-border");
		}

		if (hasBackground)
		{
			classes.Add("with-background");
		}

		if (isStretched)
		{
			classes.Add("stretched");
		}

		string style = string.Empty;

		if (Data.File.Height > 0 && Data.File.Width > 0)
		{
			style = $"style=\"aspect-ratio: {Data.File.Width} / {Data.File.Height}\"";
		}

		return $$"""
			<figure class="{{string.Join(' ', classes)}}">
				<img src="{{Data.File.Url}}" {{alt}} {{style}} />
				{{caption}}
			</figure>
			""";
	}
}