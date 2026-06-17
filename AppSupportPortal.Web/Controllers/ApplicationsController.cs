using AppSupportPortal.Web.Models;
using AppSupportPortal.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppSupportPortal.Web.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly IApplicationsApiService _applicationsService;

        public ApplicationsController(IApplicationsApiService applicationsService)
        {
            _applicationsService = applicationsService;
        }

        // GET: Applications
        public async Task<IActionResult> Index()
        {
            var apps = await _applicationsService.GetAllAsync();
            return View(apps);
        }

        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var app = await _applicationsService.GetByIdAsync(id);
            if (app == null)
                return NotFound();

            return View(app);
        }

        // GET: Applications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Applications/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var created = await _applicationsService.CreateAsync(model);

            if (!created)
            {
                ModelState.AddModelError("", "Unable to create application via API.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var app = await _applicationsService.GetByIdAsync(id);
            if (app == null)
                return NotFound();

            return View(app);
        }

        // POST: Applications/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ApplicationViewModel model)
        {
            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var updated = await _applicationsService.UpdateAsync(model);

            if (!updated)
            {
                ModelState.AddModelError("", "Unable to update application via API.");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var app = await _applicationsService.GetByIdAsync(id);
            if (app == null)
                return NotFound();

            return View(app);
        }

        // POST: Applications/DeleteConfirmed/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _applicationsService.DeleteAsync(id);

            if (!deleted)
            {
                ModelState.AddModelError("", "Unable to delete application via API.");
                return RedirectToAction(nameof(Delete), new { id });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}