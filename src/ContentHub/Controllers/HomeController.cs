using Microsoft.AspNetCore.Mvc;

namespace HexaContent.ContentHub.Controllers;

public class HomeController : Controller
{
	public IActionResult Index()
	{
		return View();
	}
}
