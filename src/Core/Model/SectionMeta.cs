namespace HexaContent.Core.Model;

public sealed class SectionMeta : EntityBase<int>
{
	public int SectionId { get; set; }
	public string Key { get; set; }
	public string Value { get; set; }

	public Section? Section { get; set; }
}