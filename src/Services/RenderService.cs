using HexaContent.Core.Services;
using Scriban;
using System.Collections.Concurrent;

namespace HexaContent.Services;

public class RenderService : IRenderService
{
	private static readonly ConcurrentDictionary<string, Template> _templates = new();

	public ValueTask<string> RenderAsync<T>(string template, T model) where T : class => GetTemplate(template).RenderAsync(model);

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
