namespace HexaContent.Services.EditorJs;

public sealed class EditorJsListBlock : EditorJsBlock
{
	public EditorJsListBlockData Data { get; set; }
	public override string Render()
	{
		if (Data == null || Data.Items == null || Data.Items.Count == 0)
		{
			return string.Empty;
		}

		var listTag = Data.Style == "ordered" ? "ol" : "ul";

		var listItems = string.Join(string.Empty, Data.Items.Select(item => item.Render()));

		return $"<{listTag}>{listItems}</{listTag}>";
	}
}