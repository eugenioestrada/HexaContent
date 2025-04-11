using HexaContent.Core.Model;
using HexaContent.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace HexaContent.ContentHub.Controllers;

public class AuthorController([FromServices] IAuthorService _authorService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var result = await _authorService.GetAllAuthors();

        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }

        return View(result.Value);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Author author)
    {
        if (!ModelState.IsValid)
        {
            return View(author);
        }

        var result = await _authorService.CreateAuthor(author);

        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var result = await _authorService.GetAuthor(id);

        if (!result.IsSuccess)
        {
            return NotFound(result.ErrorMessage);
        }

        return View(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Author author)
    {
        if (!ModelState.IsValid)
        {
            return View(author);
        }

        var result = await _authorService.UpdateAuthor(author);

        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var result = await _authorService.GetAuthor(id);

        if (!result.IsSuccess)
        {
            return NotFound(result.ErrorMessage);
        }

        return View(result.Value);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _authorService.DeleteAuthor(id);

        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }

        return RedirectToAction("Index");
    }
}
