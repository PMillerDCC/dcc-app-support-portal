using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AppSupportPortal.Web.Models;

namespace AppSupportPortal.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public IActionResult SetRole(string role)
    {
        TempData["CurrentRole"] = role;

        var referer = Request.Headers["Referer"].ToString();

        if (!string.IsNullOrEmpty(referer))
            return Redirect(referer);

        return RedirectToAction("Index", "Home");
    }
}
