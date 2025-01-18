namespace HexaContent.Core.Model;

public sealed class ArticleMeta : EntityBase<long>
{
	public int ArticleId { get; set; }
	public string Key { get; set; }
	public string Value { get; set; }

	public Article? Article { get; set; }
}