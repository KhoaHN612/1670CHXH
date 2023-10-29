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

        public UserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
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
    }
}