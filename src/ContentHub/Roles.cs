namespace HexaContent.ContentHub;

/// <summary>
/// Defines a set of roles used in the content hub.
/// </summary>
public static class Roles
{
    /// <summary>
    /// Represents the administrator role with the highest level of permissions.
    /// </summary>
    public const string AdminRole = "Admin";

    /// <summary>
    /// Represents the manager role with permissions to manage users, roles, and content.
    /// </summary>
    public const string ManagerRole = "Manager";

    /// <summary>
    /// Represents the editor role with permissions to edit, publish, and unpublish content.
    /// </summary>
    public const string EditorRole = "Editor";

    /// <summary>
    /// Represents the author role with permissions to create and manage their own content.
    /// </summary>
    public const string AuthorRole = "Author";

    /// <summary>
    /// Represents the read-only role with permissions to view content but not modify it.
    /// </summary>
    public const string ReadOnly = "Read Only";
}