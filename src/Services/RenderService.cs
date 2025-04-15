using HexaContent.Core.Services;
using HexaContent.Services.EditorJs;
using Scriban;
using Scriban.Runtime;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace HexaContent.Services;

public partial class RenderService : IRenderService
{
	private static readonly ConcurrentDictionary<string, Template> _templates = new();

	public ValueTask<string> RenderAsync<T>(string templateMarkup, T model) where T : class
	{
		var template = GetTemplate(templateMarkup);
		var scriptObject = new ScriptObject();
		
		scriptObject.Import(model);

		scriptObject.Import("editojs_render", EditorJsRender);

		var context = new TemplateContext();

		context.PushGlobal(scriptObject);

		return template.RenderAsync(context);
	}

	private const string IdAndTypeRegexPattern = """
		"id":"(.*?)","type":"(.*?)"
		""";

	[GeneratedRegex(IdAndTypeRegexPattern)]
	private static partial Regex IdAndTypeRegex();

	static string EditorJsRender(string content)
	{
		content = IdAndTypeRegex().Replace(content, "\"type\":\"$2\",\"id\":\"$1\"");

		JsonSerializerOptions options = new()
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
		};

		var editorJsContent = JsonSerializer.Deserialize<EditorJsContent>(content, options);

		return editorJsContent.Render();
	}

	private Template GetTemplate(string template)
	{
		if (_templates.TryGetValue(template, out var cachedTemplate))
		{
			return cachedTemplate;
		}

		var newTemplate = Template.Parse(template);
		_templates.TryAdd(template, newTemplate);

		return newTemplate;
	}
}
