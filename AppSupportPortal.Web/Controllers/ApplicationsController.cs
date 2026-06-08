using AppSupportPortal.Web.Models;
using AppSupportPortal.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppSupportPortal.Web.Controllers;

public class ApplicationsController : Controller
{
    private readonly ApplicationApiService _applications;
    private readonly ServerApiService _servers;

    public ApplicationsController(ApplicationApiService applications, ServerApiService servers)
    {
        _applications = applications;
        _servers = servers;
    }

    public async Task<IActionResult> Index()
    {
        var apps = await _applications.GetAllAsync();
        return View(apps);
    }

    public async Task<IActionResult> Create()
    {
        var vm = new ApplicationViewModel
        {
            Servers = await _servers.GetAllAsync()
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ApplicationViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Servers = await _servers.GetAllAsync();
            return View(model);
        }

        await _applications.CreateAsync(model);
        TempData["Success"] = "Application created successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var app = await _applications.GetByIdAsync(id);
        if (app == null)
            return NotFound();

        app.Servers = await _servers.GetAllAsync();
        return View(app);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, ApplicationViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Servers = await _servers.GetAllAsync();
            return View(model);
        }

        await _applications.UpdateAsync(id, model);
        TempData["Success"] = "Application updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var app = await _applications.GetByIdAsync(id);
        if (app == null)
            return NotFound();

        return View(app);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _applications.DeleteAsync(id);
        TempData["Success"] = "Application deleted successfully.";
        return RedirectToAction(nameof(Index));
    }
}