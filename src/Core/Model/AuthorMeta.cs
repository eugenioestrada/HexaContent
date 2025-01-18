namespace HexaContent.Core.Model;

public sealed class AuthorMeta : EntityBase<int>
{
	public int AuthorId { get; set; }
	public string Key { get; set; }
	public string Value { get; set; }
	public Author? Author { get; set; }
}