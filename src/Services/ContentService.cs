using HexaContent.Core.Model;
using HexaContent.Core.Repositories;

namespace HexaContent.Services;

public class ContentService(IArticlesRepository articlesRepository)
{
	public async Task CreateArticle(Article article)
	{
		await articlesRepository.AddAsync(article);
	}
}
