using Microsoft.AspNetCore.Mvc;
using HexaContent.ContentHub.Models;
using HexaContent.Core.Services;
using Microsoft.AspNetCore.Authorization;

namespace HexaContent.ContentHub.Controllers;

public class HomeController([FromServices] IContentService _contentService, [FromServices] IAuthorService _authorService) : Controller
{
	[Authorize(Policy = Permissions.CanRead)]
	public async Task<IActionResult> Index()
    {
        var result = await _contentService.GetAll();

        if (result.IsSuccess == false)
		{
			return BadRequest(result.ErrorMessage);
		}

		var model = new HomeModel
        {
            Articles = result.Value.ToList()
		};

        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var result = await _contentService.GetArticle(id);

		if (!result.IsSuccess || result == null)
        {
            return NotFound();
        }

		var article = result.Value;

        var authorsResult = await _authorService.GetAllAuthors();
        if (!authorsResult.IsSuccess)
        {
            return BadRequest(authorsResult.ErrorMessage);
        }

		return View(new EditArticleModel {
            Id = article.Id,
			Content = article.Content,
			Title = article.Title,
			AuthorId = article.Author.Id,
			CreatedAt = article.PublishedAt,
			UpdatedAt = article.UpdatedAt,
            Authors = authorsResult.Value.ToList()
		});
    }

    [HttpPost]
    public async Task<IActionResult> Update(EditArticleModel article)
    {
        if (!ModelState.IsValid)
        {
            return View("Edit", article);
        }

        var result = await _contentService.GetArticle(article.Id);
        
        if (!result.IsSuccess || result == null)
        {
            return NotFound();
        }

        var existingArticle = result.Value;
		existingArticle.Title = article.Title;
        existingArticle.Content = article.Content;
        existingArticle.AuthorId = article.AuthorId;
		existingArticle.UpdatedAt = DateTime.UtcNow;

        await _contentService.UpdateArticle(existingArticle);

        return RedirectToAction("Index");
    }
}
