using System.ComponentModel.DataAnnotations.Schema;

namespace HexaContent.Core.Model;

[Table("section")]
public sealed class Section : EntityBase<int>
{
	public string Name { get; set; }
	public string Description { get; set; }

	public List<ArticleSection>? Articles { get; set; }
	public List<SectionMeta>? Meta { get; set; }
}