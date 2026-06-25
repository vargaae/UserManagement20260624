using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Data;
using UserManagement.Models;

namespace UserManagement.Controllers;

[Authorize(Roles = "SuperAdmin,Admin")]
public class UserRolesController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserRolesController(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IActionResult> Index()
    {
        var users = _userManager.Users.ToList();
        var userRolesViewModel = new List<UserRolesViewModel>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);

            userRolesViewModel.Add(new UserRolesViewModel
            {
                UserId = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = string.Join(", ", roles)
            });
        }

        return View(userRolesViewModel);
    }

    public async Task<IActionResult> Manage(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        ViewBag.UserId = userId;
        ViewBag.UserName = user.UserName;

        var roles = _roleManager.Roles.ToList();
        var model = new List<ManageUserRolesViewModel>();

        foreach (var role in roles)
        {
            model.Add(new ManageUserRolesViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name,
                Selected = await _userManager.IsInRoleAsync(user, role.Name!)
            });
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Manage(List<ManageUserRolesViewModel> model, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        var roles = await _userManager.GetRolesAsync(user);
        var result = await _userManager.RemoveFromRolesAsync(user, roles);

        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Cannot remove existing roles.");
            return View(model);
        }

        result = await _userManager.AddToRolesAsync(
            user,
            model.Where(x => x.Selected).Select(y => y.RoleName)
        );

        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Cannot add selected roles.");
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }
}