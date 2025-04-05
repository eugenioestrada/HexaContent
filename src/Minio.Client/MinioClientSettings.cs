using System.Text.RegularExpressions;

namespace HexaContent.Minio.Client;

/// <summary>
/// Represents the settings for a Minio client, initialized from a connection string.
/// </summary>
/// <param name="connectionString">The connection string containing the Minio client settings.</param>
public sealed partial class MinioClientSettings(string connectionString)
{
	/// <summary>
	/// Gets the primary endpoint URL.
	/// </summary>
	public string Primary { get; } = ConnectionStringRegex().Match(connectionString).Groups[1].Value;

	/// <summary>
	/// Gets the console endpoint URL.
	/// </summary>
	public string Console { get; } = ConnectionStringRegex().Match(connectionString).Groups[2].Value;

	/// <summary>
	/// Gets the bucket name.
	/// </summary>
	public string BucketName { get; } = ConnectionStringRegex().Match(connectionString).Groups[3].Value;

	/// <summary>
	/// Gets the access key.
	/// </summary>
	public string AccessKey { get; } = ConnectionStringRegex().Match(connectionString).Groups[4].Value;

	/// <summary>
	/// Gets the access secret.
	/// </summary>
	public string AccessSecret { get; } = ConnectionStringRegex().Match(connectionString).Groups[5].Value;

	/// <summary>
	/// Gets the regular expression used to parse the connection string.
	/// </summary>
	/// <returns>The regular expression for parsing the connection string.</returns>
	[GeneratedRegex("Primary=([^;]+);Console=([^;]+);BucketName=([^;]+);AccessKey=([^;]+);AccessSecret=([^;]+)", RegexOptions.IgnoreCase, "en-US")]
	private static partial Regex ConnectionStringRegex();
}
