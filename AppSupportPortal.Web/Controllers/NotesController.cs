using AppSupportPortal.Web.Models;
using AppSupportPortal.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppSupportPortal.Web.Controllers;

public class NotesController : Controller
{
    private readonly NoteApiService _notes;
    private readonly ApplicationApiService _applications;
    private readonly UserApiService _users;

    public NotesController(NoteApiService notes, ApplicationApiService applications, UserApiService users)
    {
        _notes = notes;
        _applications = applications;
        _users = users;
    }

    public async Task<IActionResult> Index()
    {
        var notes = await _notes.GetAllAsync();
        return View(notes);
    }

    public async Task<IActionResult> Create()
    {
        var vm = new NoteViewModel
        {
            Applications = await _applications.GetAllAsync(),
            Users = await _users.GetAllAsync()
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Create(NoteViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Applications = await _applications.GetAllAsync();
            model.Users = await _users.GetAllAsync();
            return View(model);
        }

        await _notes.CreateAsync(model);
        return RedirectToAction(nameof(Index));
    }
}