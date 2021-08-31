using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebUserAjax.Entities;

namespace WebUserAjax.Controllers.Admin
{
    [Authorize]
    public class AccountController : Controller
    {

        #region Работа с ролями
        public IActionResult RoleIndex()
        {
            // Вывожу список пользователей
            return View("~/Views/Admin/Account/RoleIndex.cshtml", _roleManager.Roles.ToList());
        }

        public IActionResult RoleCreate()
        {
            // Вывожу список пользователей
            return View("~/Views/Admin/Account/RoleCreate.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Create(string Email, string Password, string[] addRoles)
        {
            if (ModelState.IsValid)
            {
                ProjectUser user = new ProjectUser { Email = Email, UserName = Email };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, Password);
                if (result.Succeeded)
                {

                    // var addRoles = newUser.Roles.Select(r => r.Name).ToList();
                    await _userManager.AddToRolesAsync(user, addRoles);

                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View("~/Views/Admin/Account/Create.cshtml");
        }

        #endregion

        //VM расположенная прямо в контролере
        public class VM_RegisterUser
        {

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            public List<IdentityRole> Roles { get; set; }
        }

        private readonly UserManager<ProjectUser> _userManager;
        private readonly SignInManager<ProjectUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AccountController(UserManager<ProjectUser> userManager, SignInManager<ProjectUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        #region Работа с пользователями

        public IActionResult Index()
        {
            //Вывожу список пользователей
            return View("~/Views/Admin/Account/Index.cshtml", _userManager.Users);
        }

        // GET: Account/Create
        public IActionResult Create()
        {
            ViewData["Roles"] = new SelectList(_roleManager.Roles.ToList(), "Id", "Name");
            return View("~/Views/Admin/Account/Create.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Email, Password, Roles")] VM_RegisterUser newUser)
        {
            if (ModelState.IsValid)
            {
                ProjectUser user = new ProjectUser { Email = newUser.Email, UserName = newUser.Email };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, newUser.Password);
                if (result.Succeeded)
                {

                    var addRoles = newUser.Roles.Select(r => r.Name).ToList();
                    await _userManager.AddToRolesAsync(user, addRoles);

                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(newUser);
        }

        #endregion

    }
}
