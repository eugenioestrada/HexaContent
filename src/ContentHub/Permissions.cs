namespace HexaContent.ContentHub;

/// <summary>
/// Defines a set of permissions used in the content hub.
/// </summary>
public static class Permissions
{
    /// <summary>
    /// Permission to read content.
    /// </summary>
    public const string CanRead = "CanRead";

    /// <summary>
    /// Permission to write or create content.
    /// </summary>
    public const string CanWrite = "CanWrite";

    /// <summary>
    /// Permission to delete content.
    /// </summary>
    public const string CanDelete = "CanDelete";

    /// <summary>
    /// Permission to publish content.
    /// </summary>
    public const string CanPublish = "CanPublish";

    /// <summary>
    /// Permission to unpublish content.
    /// </summary>
    public const string CanUnpublish = "CanUnpublish";

    /// <summary>
    /// Permission to manage users in the system.
    /// </summary>
    public const string CanManageUsers = "CanManageUsers";

    /// <summary>
    /// Permission to manage roles in the system.
    /// </summary>
    public const string CanManageRoles = "CanManageRoles";

    /// <summary>
    /// Permission to manage content types in the system.
    /// </summary>
    public const string CanManageContentTypes = "CanManageContentTypes";

    /// <summary>
    /// Permission to manage content in the system.
    /// </summary>
    public const string CanManageContent = "CanManageContent";
}
