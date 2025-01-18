using HexaContent.Core;

namespace HexaContent.ContentHub.Extensions;

public static class ViewExtensions
{
	public static string ToView(this ArticleStatus status) => status switch
	{
		ArticleStatus.Draft => "Draft",
		ArticleStatus.Published => "Published",
		ArticleStatus.Scheduled => "Scheduled",
		ArticleStatus.Archived => "Archived",
		_ => "Unknown"
	};
}
