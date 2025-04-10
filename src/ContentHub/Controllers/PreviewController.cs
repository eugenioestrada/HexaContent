using HexaContent.Core.Model;
using HexaContent.Core.Services;
using HexaContent.Core.Utils.Http;
using Microsoft.AspNetCore.Mvc;

namespace HexaContent.ContentHub.Controllers;

public class PreviewController([FromServices] IRenderService renderService, [FromServices] IContentService contentService) : Controller
{
	public async Task<IActionResult> Index(long articleId)
	{
		var result = await contentService.GetArticle(articleId);

		if (!result.IsSuccess)
		{
			return NotFound(result.ErrorMessage);
		}

		string template = """
			<h1>{{ title }}</h1>
			<p>By {{ author.name }}</p>
			<p>Published on {{ published_at }}</p>
			<p>Modified on {{ updated_at }}</p>
			""";

		var content = await renderService.RenderAsync(template, result.Value);

		return Content(content, ContentTypes.TextHtml);
	}
}
