using AppSupportPortal.Web.Models;
using AppSupportPortal.Web.Models.AppSupportPortal.Web.Services;
using AppSupportPortal.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppSupportPortal.Web.Controllers
{
    public class ServersController : Controller
    {
        private readonly IServersApiService _servers;

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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServerViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var created = await _servers.CreateAsync(model);
            if (!created)
            {
                ModelState.AddModelError("", "Unable to create server.");
                return View(model);
            }

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ServerViewModel model)
        {
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

        public async Task<IActionResult> Delete(int id)
        {
            var server = await _servers.GetByIdAsync(id);
            if (server == null)
                return NotFound();

            return View(server);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _servers.DeleteAsync(id);
            if (!deleted)
            {
                ModelState.AddModelError("", "Unable to delete server.");
                return RedirectToAction(nameof(Delete), new { id });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
