using System.ComponentModel.DataAnnotations.Schema;

namespace HexaContent.Core.Model;

[Table("section_meta")]
public sealed class SectionMeta : MetaEntityBase<int, Section, int>
{
}