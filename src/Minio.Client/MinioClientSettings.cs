namespace HexaContent.Minio.Client;

public sealed class MinioClientSettings
{
	internal const string DefaultConfigSectionName = "Minio:Client";
	public string Endpoint { get; set; }
	public string PrimaryEndpoint { get; set; }
	public string ConnectionString { get; set; }
}