using System.ComponentModel.DataAnnotations.Schema;

namespace HexaContent.Core.Model;

[Table("media_meta")]
public sealed class MediaMeta : MetaEntityBase<long, Media, long>
{
}
