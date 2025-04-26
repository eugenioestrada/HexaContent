using SixLabors.ImageSharp.Web.Providers;
using SixLabors.ImageSharp.Web.Resolvers;

namespace HexaContent.ImageOptimizer;

public class CustomImageProvider : IImageProvider
{
	public ProcessingBehavior ProcessingBehavior => ProcessingBehavior.All;

	public Func<HttpContext, bool> Match { get; set; }

	public Task<IImageResolver?> GetAsync(HttpContext context)
	{
		throw new NotImplementedException();
	}

	public bool IsValidRequest(HttpContext context) => true;
}
