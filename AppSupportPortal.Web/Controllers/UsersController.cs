using AppSupportPortal.Web.Models;
using AppSupportPortal.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppSupportPortal.Web.Controllers;

public class UsersController : Controller
{
    private readonly UserApiService _users;

    public UsersController(UserApiService users)
    {
        _users = users;
    }

    public async Task<IActionResult> Index()
    {
        var users = await _users.GetAllAsync();
        return View(users);
    }

    public async Task<IActionResult> Details(int id)
    {
        var user = await _users.GetByIdAsync(id);
        if (user == null)
            return NotFound();

        return View(user);
    }

    public IActionResult Create()
    {
        return View(new UserViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _users.CreateAsync(model);
        TempData["Success"] = "User created successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var user = await _users.GetByIdAsync(id);
        if (user == null)
            return NotFound();

        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, UserViewModel model)
    {
        if (id != model.UserId)
            return BadRequest();

        if (!ModelState.IsValid)
            return View(model);

        await _users.UpdateAsync(model);
        TempData["Success"] = "User updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var user = await _users.GetByIdAsync(id);
        if (user == null)
            return NotFound();

        return View(user);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _users.DeleteAsync(id);
        TempData["Success"] = "User deleted successfully.";
        return RedirectToAction(nameof(Index));
    }
}
