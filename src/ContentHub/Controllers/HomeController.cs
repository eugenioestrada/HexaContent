using Microsoft.AspNetCore.Mvc;
using HexaContent.Core.Repositories;
using HexaContent.ContentHub.Models;
using HexaContent.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace HexaContent.ContentHub.Controllers;

public class HomeController : Controller
{
    private readonly IArticlesRepository _articlesRepository;

    public HomeController([FromServices] IArticlesRepository articlesRepository)
    {
        _articlesRepository = articlesRepository;
    }

    public async Task<IActionResult> Index()
    {
        var articles = await _articlesRepository.GetAll().ToListAsync();
        var model = new HomeModel
        {
            Articles = articles.ToList()
        };
        return View(model);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var article = await _articlesRepository.FindAsync(id);

        if (article == null)
        {
            return NotFound();
        }
        return View(new EditArticleModel {
            Id = article.Id,
			Content = article.Content,
			Title = article.Title,
			AuthorId = article.Author.Id,
			CreatedAt = article.CreatedAt,
			UpdatedAt = article.UpdatedAt
		});
    }

    [HttpPost]
    public async Task<IActionResult> Update(EditArticleModel article)
    {
        if (!ModelState.IsValid)
        {
            return View("Edit", article);
        }

        var existingArticle = await _articlesRepository.FindAsync(article.Id);
        if (existingArticle == null)
        {
            return NotFound();
        }

        existingArticle.Title = article.Title;
        existingArticle.Content = article.Content;
        existingArticle.Author = new Author { Id = article.AuthorId };
		existingArticle.UpdatedAt = DateTime.UtcNow;

        _articlesRepository.Update(existingArticle);
        await _articlesRepository.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}
