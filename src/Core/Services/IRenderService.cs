namespace HexaContent.Core.Services;

public interface IRenderService
{
	public ValueTask<string> RenderAsync<T>(string template, T model) where T : class;
}
