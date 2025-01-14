using Microsoft.AspNetCore.Mvc;
using HexaContent.Core.Repositories;
using HexaContent.ContentHub.Models;
using HexaContent.Core.Model;

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
        var articles = await _articlesRepository.GetAllAsync();
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
        return View(article);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Article article)
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
        existingArticle.Author.Name = article.Author.Name;
        existingArticle.UpdatedAt = DateTime.UtcNow;

        await _articlesRepository.UpdateAsync(existingArticle);

        return RedirectToAction("Index");
    }
}
