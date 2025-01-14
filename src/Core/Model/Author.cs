namespace HexaContent.Core.Model;

/// <summary>
/// Represents an author in the system.
/// </summary>
public record Author(int Id, string Name, string Email, string Bio, DateTime CreatedAt, DateTime UpdatedAt, List<Article>? Articles = null);