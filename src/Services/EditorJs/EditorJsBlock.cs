using System.Text.Json.Serialization;

namespace HexaContent.Services.EditorJs;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(EditorJsParagraphBlock), typeDiscriminator: "paragraph")]
[JsonDerivedType(typeof(EditorJsHeadingBlock), typeDiscriminator: "heading")]
[JsonDerivedType(typeof(EditorJsListBlock), typeDiscriminator: "list")]
[JsonDerivedType(typeof(EditorJsQuoteBlock), typeDiscriminator: "quote")]
public class EditorJsBlock
{
	public string Id { get; set; }
	public virtual string Render() => string.Empty;
}
