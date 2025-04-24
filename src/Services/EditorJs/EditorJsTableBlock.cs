namespace HexaContent.Services.EditorJs;

public sealed class EditorJsTableBlock : EditorJsBlock
{
	public EditorJsTableBlockData Data { get; set; }

	public override string Render()
	{
		if (Data == null || Data.Content == null || Data.Content.Count == 0)
		{
			return string.Empty;
		}

		if (Data.Content.Count == 1)
		{
			return $"""
				<table>
					<tr>
						{ string.Join("", Data.Content[0].Select(cell => $"<td>{cell}</td>")) }
					</tr>
				</table>
				""";
		}
		else
		{
			bool withHeadings = Data.WithHeadings;

			if (withHeadings)
			{
				return $"""
					<table>
						<thead>
							<tr>
							{string.Join("", Data.Content[0].Select(cell => $"<th>{cell}</th>"))}
							</tr>
						</thead>
						<tbody>
							{string.Join("", Data.Content.Skip(1).Select(row => $"<tr>{string.Join("", row.Select(cell => $"<td>{cell}</td>"))}</tr>"))}
						</tbody>
					</table>
					""";
			}

			return $"""
					<table>
						{string.Join("", Data.Content.Select(row => $"<tr>{string.Join("", row.Select(cell => $"<td>{cell}</td>"))}</tr>"))}
					</table>
					""";
		}
	}
}