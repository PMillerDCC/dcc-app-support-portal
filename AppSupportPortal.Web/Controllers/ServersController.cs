using AppSupportPortal.Web.Models;
using AppSupportPortal.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppSupportPortal.Web.Controllers
{
    public class ServersController : Controller
    {
        private readonly IServersApiService _servers;

        private bool IsAdmin()
        {
            TempData.Keep("CurrentRole");
            return TempData["CurrentRole"]?.ToString() == "Admin";
        }

        public ServersController(IServersApiService servers)
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

        // -----------------------------
        // CREATE
        // -----------------------------
        public IActionResult Create()
        {
            if (!IsAdmin())
                return Forbid();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServerViewModel model)
        {
            if (!IsAdmin())
                return Forbid();

            if (!ModelState.IsValid)
                return View(model);

            var created = await _servers.CreateAsync(model);
            if (!created)
            {
                ModelState.AddModelError("", "Unable to create server.");
                return View(model);
            }
            TempData["Success"] = "Server created successfully.";
            return RedirectToAction(nameof(Index));
        }

        // -----------------------------
        // EDIT
        // -----------------------------
        public async Task<IActionResult> Edit(int id)
        {
            if (!IsAdmin())
                return Forbid();

            var server = await _servers.GetByIdAsync(id);
            if (server == null)
                return NotFound();

            return View(server);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ServerViewModel model)
        {
            if (!IsAdmin())
                return Forbid();

            if (id != model.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var updated = await _servers.UpdateAsync(model);
            if (!updated)
            {
                ModelState.AddModelError("", "Unable to update server.");
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

            var server = await _servers.GetByIdAsync(id);
            if (server == null)
                return NotFound();

            return View(server);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsAdmin())
                return Forbid();

            var error = await _servers.DeleteAsync(id);

            if (error != null)
            {
                TempData["Error"] = error;
                return RedirectToAction(nameof(Delete), new { id });
            }

            TempData["Success"] = "Server deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}