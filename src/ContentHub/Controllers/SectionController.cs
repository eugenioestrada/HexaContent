using HexaContent.Core.Model;
using HexaContent.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace HexaContent.ContentHub.Controllers;

public class SectionController([FromServices] ISectionService _sectionService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var result = await _sectionService.GetAllSections();

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
    public async Task<IActionResult> Create(Section section)
    {
        if (!ModelState.IsValid)
        {
            return View(section);
        }

        var result = await _sectionService.CreateSection(section);

        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var result = await _sectionService.GetSection(id);

        if (!result.IsSuccess)
        {
            return NotFound(result.ErrorMessage);
        }

        return View(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Section section)
    {
        if (!ModelState.IsValid)
        {
            return View(section);
        }

        var result = await _sectionService.UpdateSection(section);

        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var result = await _sectionService.GetSection(id);

        if (!result.IsSuccess)
        {
            return NotFound(result.ErrorMessage);
        }

        return View(result.Value);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _sectionService.DeleteSection(id);

        if (!result.IsSuccess)
        {
            return BadRequest(result.ErrorMessage);
        }

        return RedirectToAction("Index");
    }
}
