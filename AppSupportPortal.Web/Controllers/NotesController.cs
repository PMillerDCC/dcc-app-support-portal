using Microsoft.AspNetCore.Mvc;
using AppSupportPortal.Web.Models;
using AppSupportPortal.Web.Services;

namespace AppSupportPortal.Web.Controllers
{
    public class NotesController : Controller
    {
        private readonly INotesApiService _notes;
        private readonly IApplicationsApiService _apps;
        private readonly IUsersApiService _users;

        public NotesController(INotesApiService notes, IApplicationsApiService apps, IUsersApiService users)
        {
            _notes = notes;
            _apps = apps;
            _users = users;
        }

        private bool IsAdmin()
        {
            var role = TempData["CurrentRole"]?.ToString() ?? "Regular";
            TempData.Keep("CurrentRole");
            return role == "Admin";
        }

        public async Task<IActionResult> Index()
        {
            var notes = await _notes.GetAllAsync();
            return View(notes);
        }

        public async Task<IActionResult> Details(int id)
        {
            var note = await _notes.GetByIdAsync(id);
            if (note == null) return NotFound();
            return View(note);
        }

        public async Task<IActionResult> Create(int applicationId)
        {
            if (!IsAdmin()) return Forbid();

            ViewBag.Applications = await _apps.GetAllAsync();
            ViewBag.Users = await _users.GetAllAsync();

            return View(new NoteViewModel { ApplicationId = applicationId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NoteViewModel model)
        {
            if (!IsAdmin()) return Forbid();

            if (!ModelState.IsValid)
            {
                ViewBag.Applications = await _apps.GetAllAsync();
                ViewBag.Users = await _users.GetAllAsync();
                return View(model);
            }

            var success = await _notes.CreateAsync(model);

            if (!success)
            {
                TempData["Error"] = "Failed to create note.";
                return View(model);
            }

            TempData["Success"] = "Note created successfully.";
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!IsAdmin()) return Forbid();

            var note = await _notes.GetByIdAsync(id);
            if (note == null) return NotFound();

            ViewBag.Applications = await _apps.GetAllAsync();
            ViewBag.Users = await _users.GetAllAsync();

            return View(note);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NoteViewModel model)
        {
            if (!IsAdmin()) return Forbid();

            if (!ModelState.IsValid)
            {
                ViewBag.Applications = await _apps.GetAllAsync();
                ViewBag.Users = await _users.GetAllAsync();
                return View(model);
            }

            var success = await _notes.UpdateAsync(model);

            if (!success)
            {
                TempData["Error"] = "Failed to update note.";
                return View(model);
            }

            TempData["Success"] = "Note updated successfully.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!IsAdmin()) return Forbid();

            var note = await _notes.GetByIdAsync(id);
            if (note == null) return NotFound();

            return View(note);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsAdmin()) return Forbid();

            var note = await _notes.GetByIdAsync(id);
            if (note == null) return NotFound();

            var success = await _notes.DeleteAsync(id);

            if (!success)
            {
                TempData["Error"] = "Failed to delete note.";
                return RedirectToAction(nameof(Index));
            }

            TempData["Success"] = "Note deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}