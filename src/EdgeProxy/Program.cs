using HexaContent.Core.Utils;
using HexaContent.EdgeProxy.Config;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);

var bucketConnectionString = builder.Configuration.GetConnectionString("bucket");

Argument.ThrowInvalidOperationIfStringEmpty(bucketConnectionString, "The connection string for the bucket is not set.");

var primaryMatch = Regex.Match(bucketConnectionString, "Primary=([^;]+)", RegexOptions.IgnoreCase);

Argument.ThrowInvalidOperationIfNotSuccess(primaryMatch, "The connection string for the bucket is invalid.");

var bucketUrl = primaryMatch.Groups[1].Value;

var bucketNameMatch = Regex.Match(bucketConnectionString, "BucketName=([^;]+)", RegexOptions.IgnoreCase);

Argument.ThrowInvalidOperationIfNotSuccess(bucketNameMatch, "The connection string for the bucket is invalid.");

var bucketName = bucketNameMatch.Groups[1].Value;

builder.ConfigureReverseProxy(bucketUrl, bucketName);

var app = builder.Build();

app.MapReverseProxy();

app.MapGet("/_health", () => "Ok!");

app.MapWhen(ctx => ctx.Request.Method == "HEAD" && ctx.Request.Path == "/", HandleHead);

app.Run();

static void HandleHead(IApplicationBuilder app)
{
	app.Run(async context =>
	{
		context.Response.StatusCode = 200;
		await context.Response.WriteAsync($"Ok!");
	});
}