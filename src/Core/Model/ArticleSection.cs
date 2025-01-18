namespace HexaContent.Core.Model;

public sealed class ArticleSection : EntityBase<long>
{
	public long ArticleId { get; set; }
	public int SectionId { get; set; }

	public Article? Article { get; set; }
	public Section? Section { get; set; }
}