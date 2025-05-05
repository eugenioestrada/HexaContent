using System.Text.Json.Serialization;

namespace HexaContent.Services.EditorJs;

[JsonSourceGenerationOptions(WriteIndented = true, PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(EditorJsContent))]
internal partial class EditorJsSourceGenerationContext : JsonSerializerContext
{
}
