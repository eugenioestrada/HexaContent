using HexaContent.Core.Services;
using HexaContent.Services.EditorJs;
using Scriban;
using Scriban.Runtime;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace HexaContent.Services;

/// <summary>
/// Provides rendering services for templates and Editor.js content.
/// </summary>
public partial class RenderService : IRenderService
{
    /// <summary>
    /// A thread-safe dictionary to cache compiled templates.
    /// </summary>
    private static readonly ConcurrentDictionary<string, Template> _templates = new();

    /// <summary>
    /// Renders a template with the provided model asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of the model.</typeparam>
    /// <param name="templateMarkup">The template markup to render.</param>
    /// <param name="model">The model to use for rendering.</param>
    /// <returns>A task that represents the asynchronous rendering operation. The task result contains the rendered string.</returns>
    public ValueTask<string> RenderAsync<T>(string templateMarkup, T model) where T : class
	{
		var template = GetTemplate(templateMarkup);

		ScriptObject scriptObject = GetScriptObject(model);

		var context = new TemplateContext(scriptObject);

		return template.RenderAsync(context);
	}

	private static ScriptObject GetScriptObject<T>(T model) where T : class
	{
		var scriptObject = new ScriptObject();
		scriptObject.Import(model);
		scriptObject.Import("editojs_render", EditorJsRender);
		return scriptObject;
	}

	/// <summary>
	/// The regex pattern used to normalize JSON by swapping "id" and "type" properties.
	/// </summary>
	private const string IdAndTypeRegexPattern = """
        "id":"(.*?)","type":"(.*?)"
        """;

    /// <summary>
    /// Compiles the regex for normalizing JSON.
    /// </summary>
    /// <returns>A compiled regex instance.</returns>
    [GeneratedRegex(IdAndTypeRegexPattern)]
    private static partial Regex IdAndTypeRegex();

    /// <summary>
    /// Renders Editor.js content into a string.
    /// </summary>
    /// <param name="content">The raw Editor.js JSON content.</param>
    /// <returns>The rendered content as a string.</returns>
    static string EditorJsRender(string content)
    {
        content = NormalizeJson(content);

        var editorJsContent = Deserialize(content);

        return editorJsContent.Render();
    }

    /// <summary>
    /// Normalizes the JSON content by swapping "id" and "type" properties.
    /// </summary>
    /// <param name="content">The raw JSON content.</param>
    /// <returns>The normalized JSON content.</returns>
    private static string NormalizeJson(string content) => IdAndTypeRegex().Replace(content, "\"type\":\"$2\",\"id\":\"$1\"");

    /// <summary>
    /// Deserializes the JSON content into an <see cref="EditorJsContent"/> object.
    /// </summary>
    /// <param name="content">The raw JSON content.</param>
    /// <returns>The deserialized <see cref="EditorJsContent"/> object.</returns>
    static EditorJsContent Deserialize(string content) => JsonSerializer.Deserialize(content, EditorJsSourceGenerationContext.Default.EditorJsContent);

    /// <summary>
    /// Retrieves a compiled template from the cache or parses a new one if not cached.
    /// </summary>
    /// <param name="template">The template markup to parse.</param>
    /// <returns>A compiled <see cref="Template"/> instance.</returns>
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
