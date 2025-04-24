using Amazon.S3.Model;
using Amazon.S3;
using HexaContent.Minio.Client;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;

namespace HexaContent.ContentHub.Controllers;

public class ImagesController(MinioFactory minioFactory) : Controller
{

	[HttpPost]
	public async Task<IActionResult> UploadFile(IFormFile image)
	{
		var client = await minioFactory.GetAmazonS3ClientAsync();

		var buckets = await client.ListBucketsAsync();

		foreach (var bucket in buckets.Buckets)
		{
			// NetVips.Image vipsImage = NetVips.Image.NewFromStream(image.OpenReadStream(), access: NetVips.Enums.Access.Sequential);


			using MemoryStream memoryStream = new();
			using var image1 = Image.Load(image.OpenReadStream());
			image1.Save(memoryStream, image1.Metadata.DecodedImageFormat);
			memoryStream.Position = 0;
			var response = await PutContent(client, bucket, $"images/{image.FileName}", memoryStream, image1.Metadata.DecodedImageFormat.DefaultMimeType);

			var url = await client.GetPreSignedURLAsync(new GetPreSignedUrlRequest
			{
				BucketName = bucket.BucketName,
				Key = $"images/{image.FileName}",
				Expires = DateTime.UtcNow.AddDays(7)
			});

			url = url.Replace("https://", "http://");
			int height = image1.Height;
			int width = image1.Width;

			return Json(new
			{
				success = 1,
				file = new
				{
					url,
					height,
					width
				}
			});
		}

		return BadRequest();

		static async Task<PutObjectResponse> PutContent(AmazonS3Client client, S3Bucket bucket, string key, Stream content, string contentType)
		{
			return await client.PutObjectAsync(new PutObjectRequest
			{
				BucketName = bucket.BucketName,
				Key = key,
				ContentType = contentType,
				CannedACL = S3CannedACL.PublicRead,
				InputStream = content,
			});
		}
	}

	public IActionResult FetchUrl()
	{
		return Ok();
	}
}
