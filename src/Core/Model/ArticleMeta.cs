using System.ComponentModel.DataAnnotations.Schema;

namespace HexaContent.Core.Model;

[Table("article_meta")]
public sealed class ArticleMeta : MetaEntityBase<long, Article, long>
{
}