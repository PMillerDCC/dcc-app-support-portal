using AppSupportPortal.Web.Models;
using AppSupportPortal.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppSupportPortal.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersApiService _users;

        private bool IsAdmin()
        {
            TempData.Keep("CurrentRole");
            return TempData["CurrentRole"]?.ToString() == "Admin";
        }

        public UsersController(IUsersApiService users)
        {
            _users = users;
        }

        // -----------------------------
        // INDEX
        // -----------------------------
        public async Task<IActionResult> Index()
        {
            var users = await _users.GetAllAsync();
            return View(users);
        }

        // -----------------------------
        // DETAILS
        // -----------------------------
        public async Task<IActionResult> Details(int id)
        {
            var user = await _users.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
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
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (!IsAdmin())
                return Forbid();

            if (!ModelState.IsValid)
                return View(model);

            var success = await _users.CreateAsync(model);

            if (!success)
            {
                TempData["Error"] = "Failed to create user.";
                return View(model);
            }

            TempData["Success"] = "User created successfully.";
            return RedirectToAction(nameof(Index));
        }

        // -----------------------------
        // EDIT
        // -----------------------------
        public async Task<IActionResult> Edit(int id)
        {
            if (!IsAdmin())
                return Forbid();

            var user = await _users.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel model)
        {
            if (!IsAdmin())
                return Forbid();

            if (!ModelState.IsValid)
                return View(model);

            var success = await _users.UpdateAsync(model);

            if (!success)
            {
                TempData["Error"] = "Failed to update user.";
                return View(model);
            }

            TempData["Success"] = "User updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        // -----------------------------
        // DELETE
        // -----------------------------
        public async Task<IActionResult> Delete(int id)
        {
            if (!IsAdmin())
                return Forbid();

            var user = await _users.GetByIdAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsAdmin())
                return Forbid();

            var errorMessage = await _users.DeleteAsync(id);

            if (errorMessage != null)
            {
                TempData["Error"] = errorMessage;
                return RedirectToAction(nameof(Delete), new { id });
            }

            TempData["Success"] = "User deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}