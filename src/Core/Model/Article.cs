namespace HexaContent.Core.Model;

/// <summary>
/// Represents an article in the system.
/// </summary>
public record Article(int Id, string Title, string Content, DateTime CreatedAt, DateTime UpdatedAt, Author Author);
