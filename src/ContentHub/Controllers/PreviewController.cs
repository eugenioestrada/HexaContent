using HexaContent.Core.Model;
using HexaContent.Core.Services;
using HexaContent.Core.Utils.Http;
using Microsoft.AspNetCore.Mvc;

namespace HexaContent.ContentHub.Controllers;

[Route("[controller]")]
public class PreviewController([FromServices] IRenderService renderService, [FromServices] IContentService contentService) : Controller
{
	[Route("{articleId:long}")]
	public async Task<IActionResult> Index(long articleId)
	{
		var result = await contentService.GetArticle(articleId);

		if (!result.IsSuccess)
		{
			return NotFound(result.ErrorMessage);
		}

		string template = """
						<!DOCTYPE html>
			<html lang="en">
			<head>
			    <meta charset="UTF-8">
			    <meta name="viewport" content="width=device-width, initial-scale=1.0">
			    <title>{{ title | html.escape }}</title>
			    <style>
			        body {
			            font-family: Arial, sans-serif;
			            line-height: 1.6;
			            margin: 0;
			            padding: 0;
			            background-color: #f4f4f4;
			            color: #333;
			        }
			        .container {
			            max-width: 800px;
			            margin: 20px auto;
			            background: #fff;
			            padding: 20px;
			            border-radius: 8px;
			            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
			        }
			        h1 {
			            color: #444;
			        }
			        p {
			            margin: 10px 0;
			        }
			        .footer {
			            text-align: center;
			            margin-top: 20px;
			            font-size: 0.9em;
			            color: #777;
			        }
			    </style>
			</head>
			<body>
			    <div class="container">
			        <h1>{{ title | html.escape }}</h1>
			        <p>By {{ author.name }}</p>
					<p>Published on {{ published_at }}</p>
					<p>Modified on {{ updated_at }}</p>

					{{ content | editojs_render }}

			        <div class="footer">
			            &copy; 2025 My Blog
			        </div>
			    </div>
			</body>
			</html>

			""";

		var content = await renderService.RenderAsync(template, result.Value);

		return Content(content, ContentTypes.TextHtml);
	}
}
