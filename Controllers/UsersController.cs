using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Controllers
{
    [Authorize]
    [Route("Dashboard/[controller]/[action]")]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Authorize(Roles = "admin")]
        [Authorize(Roles = "admin")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> List()
        {
            var userRoleName = "user";
            var candidateRoleName = "candidate";

            // Get users in "user" role
            var usersInUserRoles = await _userManager.GetUsersInRoleAsync(userRoleName);

            // Get users in "candidate" role
            var usersInCandidateRoles = await _userManager.GetUsersInRoleAsync(candidateRoleName);

            // Combine both lists
            var users = usersInUserRoles.Concat(usersInCandidateRoles).Distinct();

            var userRolesViewModels = new List<UserRolesViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var userRolesViewModel = new UserRolesViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles.ToList()
                };
                userRolesViewModels.Add(userRolesViewModel);
            }

            return View(userRolesViewModels);
        }



        public IActionResult Edit(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            if (user == null)
            {
                return NotFound("User Not Found");
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, IdentityUser user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByIdAsync(id);
                if (existingUser == null)
                {
                    return NotFound();
                }

                existingUser.UserName = user.UserName;
                existingUser.Email = user.Email;

                var result = await _userManager.UpdateAsync(existingUser);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(List));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(user);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(List));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(user);
        }
    }
}
