using AppSupportPortal.Web.Models;
using AppSupportPortal.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppSupportPortal.Web.Controllers;

public class ServersController : Controller
{
    private readonly ServerApiService _servers;

    public ServersController(ServerApiService servers)
    {
        _servers = servers;
    }

    public async Task<IActionResult> Index()
    {
        var servers = await _servers.GetAllAsync();
        return View(servers);
    }

    public async Task<IActionResult> Details(int id)
    {
        var server = await _servers.GetByIdAsync(id);
        if (server == null)
            return NotFound();

        return View(server);
    }

    public IActionResult Create()
    {
        return View(new ServerViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(ServerViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _servers.CreateAsync(model);
        TempData["Success"] = "Server created successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var server = await _servers.GetByIdAsync(id);
        if (server == null)
            return NotFound();

        return View(server);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, ServerViewModel model)
    {
        if (id != model.ServerId)
            return BadRequest();

        if (!ModelState.IsValid)
            return View(model);

        await _servers.UpdateAsync(model);
        TempData["Success"] = "Server updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var server = await _servers.GetByIdAsync(id);
        if (server == null)
            return NotFound();

        return View(server);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _servers.DeleteAsync(id);
        TempData["Success"] = "Server deleted successfully.";
        return RedirectToAction(nameof(Index));
    }
}
