using Microsoft.AspNetCore.Mvc;

namespace HexaContent.ContentHub.Controllers;

public class ImagesController : Controller
{

	[HttpPost]
	public IActionResult UploadFile(IFormFile image)
	{
		return Ok();
	}

	public IActionResult FetchUrl()
	{
		return Ok();
	}
}
