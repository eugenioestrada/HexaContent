namespace HexaContent.ContentHub;

/// <summary>
/// Provides role-based permissions mapping for the content hub.
/// </summary>
public static class PermissionsInRoles
{
	/// <summary>
	/// Permissions available to read-only users.
	/// </summary>
	static List<string> readOnlyPermissions = [Permissions.CanRead];

	/// <summary>
	/// Permissions available to authors, including read-only permissions.
	/// </summary>
	static List<string> authorPermissions = [.. readOnlyPermissions, Permissions.CanWrite, Permissions.CanDelete];

	/// <summary>
	/// Permissions available to editors, including author permissions.
	/// </summary>
	static List<string> editorPermissions = [.. authorPermissions, Permissions.CanPublish, Permissions.CanUnpublish];

	/// <summary>
	/// Permissions available to managers, including editor permissions.
	/// </summary>
	static List<string> managerPermissions = [.. editorPermissions, Permissions.CanManageUsers, Permissions.CanManageRoles];

	/// <summary>
	/// Permissions available to administrators, including manager permissions.
	/// </summary>
	static List<string> adminPermissions = [.. managerPermissions, Permissions.CanManageContentTypes, Permissions.CanManageContent];

	/// <summary>
	/// Gets a dictionary mapping roles to their respective permissions.
	/// </summary>
	/// <returns>A dictionary where the key is the role name and the value is a list of permissions.</returns>
	public static Dictionary<string, List<string>> RolePermissions() => new()
	{
		{ Roles.AdminRole, adminPermissions },
		{ Roles.ManagerRole, managerPermissions },
		{ Roles.EditorRole, editorPermissions },
		{ Roles.AuthorRole, authorPermissions },
		{ Roles.ReadOnly, readOnlyPermissions }
	};

	/// <summary>
	/// Gets a dictionary mapping permissions to the roles that have them.
	/// </summary>
	/// <returns>A dictionary where the key is the permission and the value is a list of roles that have the permission.</returns>
	public static Dictionary<string, List<string>> PermissionsRoles() =>
		RolePermissions().Values.SelectMany(x => x).Distinct()
		.ToDictionary(x => x, x => RolePermissions().Where(y => y.Value.Contains(x)).Select(y => y.Key).ToList());
}