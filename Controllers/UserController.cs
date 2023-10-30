using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ASMProject.Models;

namespace ASMProject.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly Db1670asmContext _context;

        public UserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager,Db1670asmContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        public IActionResult Delete(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            if (user != null)
            {
                var result = _userManager.DeleteAsync(user).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return View("Error");
        }

        public IActionResult ManageRoles(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            var roles = _roleManager.Roles.Select(r => r.Name).ToList();
            return View(model: new UserRoleViewModel
            {
                User = user,
                Roles = roles
            });
        }

        [HttpPost]
        public IActionResult SetRoles(string id, List<string> Roles)
        {
            var user = _userManager.FindByIdAsync(id).Result;

            var result = _userManager.AddToRolesAsync(user, Roles).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
                return View("Error");
        }

        public async Task<IActionResult> RemoveRoles(string? id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);

            var userRoleViewModel = new UserRoleViewModel
            {
                User = user,
                Roles = roles.ToList() 
            };

            return View(userRoleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveRoles(string userId, string selectedRole)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return View("Error");
            }

            if (!string.IsNullOrEmpty(selectedRole))
            {
                var result = await _userManager.RemoveFromRoleAsync(user, selectedRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error");
                }
            }

            return RedirectToAction("Index");
        }

    }
}