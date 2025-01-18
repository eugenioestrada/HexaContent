using System.ComponentModel.DataAnnotations.Schema;

namespace HexaContent.Core.Model;

[Table("author_meta")]
public sealed class AuthorMeta : MetaEntityBase<int, Author, int>
{
}