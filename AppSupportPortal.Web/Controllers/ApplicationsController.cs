using AppSupportPortal.Web.Models;
using AppSupportPortal.Web.Services;
using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppSupportPortal.Web.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly IApplicationsApiService _applications;
        private readonly IServersApiService _servers;
        private readonly IUsersApiService _users;
        private readonly INotesApiService _notes;
        

        private bool IsAdmin()
        {
            TempData.Keep("CurrentRole");
            return TempData["CurrentRole"]?.ToString() == "Admin";
        }

        public ApplicationsController(
            IApplicationsApiService applications,
            IServersApiService servers,
            IUsersApiService users,
            INotesApiService notes)
        {
            _applications = applications;
            _servers = servers;
            _users = users;
            _notes = notes;
        }

        // -----------------------------
        // INDEX
        // -----------------------------
        public async Task<IActionResult> Index()
        {
            var apps = await _applications.GetAllAsync();
            return View(apps);
        }

        // -----------------------------
        // DETAILS
        // -----------------------------
        public async Task<IActionResult> Details(int id)
        {
            var app = await _applications.GetByIdAsync(id);
            if (app == null) return NotFound();

            var notes = await _notes.GetByApplicationAsync(id);

            ViewBag.Notes = notes;

            return View(app);
        }

        // -----------------------------
        // CREATE
        // -----------------------------
        public async Task<IActionResult> Create()
        {
            var servers = await _servers.GetAllAsync();

            if (!servers.Any())
            {
                TempData["Error"] = "You must create a server before creating an application.";
                return RedirectToAction("Index");
            }

            var vm = new ApplicationViewModel
            {
                Servers = servers.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                })
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationViewModel model)
        {
            if (!IsAdmin())
                return Forbid();

            if (!ModelState.IsValid)
            {
                model.Servers = (await _servers.GetAllAsync())
                    .Select(s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.Name
                    });

                return View(model);
            }

            var created = await _applications.CreateAsync(model);
            
            if (!created)
            {
                ModelState.AddModelError("", "Unable to create application.");

                model.Servers = (await _servers.GetAllAsync())
                    .Select(s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.Name
                    });
           
                return View(model);
            }
            TempData["Success"] = "Application created successfully.";
            return RedirectToAction(nameof(Index));
        }

        // -----------------------------
        // EDIT
        // -----------------------------
        public async Task<IActionResult> Edit(int id)
        {
            if (!IsAdmin())
                return Forbid();

            var app = await _applications.GetByIdAsync(id);
            if (app == null)
                return NotFound();

            return View(app);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ApplicationViewModel model)
        {
            if (!IsAdmin())
                return Forbid();

            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var updated = await _applications.UpdateAsync(model);
            if (!updated)
            {
                ModelState.AddModelError("", "Unable to update application.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // -----------------------------
        // DELETE
        // -----------------------------
        public async Task<IActionResult> Delete(int id)
        {
            if (!IsAdmin())
                return Forbid();

            var app = await _applications.GetByIdAsync(id);

            if (app == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(app);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsAdmin())
                return Forbid();

            var error = await _applications.DeleteAsync(id);

            if (error != null)
            {
                TempData["Error"] = error;
                return RedirectToAction(nameof(Delete), new { id });
            }

            TempData["Success"] = "Application deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}