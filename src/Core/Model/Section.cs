namespace HexaContent.Core.Model;

public sealed class Section : EntityBase<int>
{
	public string Name { get; set; }
	public string Description { get; set; }

	public List<ArticleSection>? Articles { get; set; }
	public List<SectionMeta>? Meta { get; set; }
}