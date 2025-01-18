namespace HexaContent.Core;

/// <summary>
/// The status of an article
/// </summary>
public enum ArticleStatus
{
	/// <summary>
	/// Draft articles are not visible to the public
	/// </summary>
	Draft = 0,
	/// <summary>
	/// Scheduled articles will be published at a future date
	/// </summary>
	Scheduled = 1,
	/// <summary>
	/// Published articles are visible to the public
	/// </summary>
	Published = 2,
	/// <summary>
	/// Archived articles are not visible to the public
	/// </summary>
	Archived = 3
}