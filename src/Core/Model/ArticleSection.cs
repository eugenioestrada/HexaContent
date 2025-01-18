using System.ComponentModel.DataAnnotations.Schema;

namespace HexaContent.Core.Model;

[Table("article_section")]
public sealed class ArticleSection : EntityBase<long>
{
	public long ArticleId { get; set; }
	public int SectionId { get; set; }

	public Article? Article { get; set; }
	public Section? Section { get; set; }
}