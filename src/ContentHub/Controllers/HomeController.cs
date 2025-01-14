using Microsoft.AspNetCore.Mvc;
using HexaContent.Core.Repositories;
using HexaContent.ContentHub.Models;

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
}
