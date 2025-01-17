namespace HexaContent.Core.Model;

public class AuthorMeta : EntityBase<int>
{
	public int AuthorId { get; set; }
	public string Key { get; set; }
	public string Value { get; set; }
	public Author? Author { get; set; }
}