using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
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
        public async Task<IActionResult> RoleCreate(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("RoleIndex");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
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
        private readonly IWebHostEnvironment _environment; // Данные по настроке хоста


        public AccountController(UserManager<ProjectUser> userManager, SignInManager<ProjectUser> signInManager, RoleManager<IdentityRole> roleManager, IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _environment = environment;
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
        public async Task<IActionResult> Create(string Email, string Password, string[] addRoles, IFormFile AvatarFile)
        {
            if (ModelState.IsValid)
            {
                ProjectUser user = new ProjectUser { Email = Email, UserName = Email };


                #region Обработка изображения

                var wwwRootPath = _environment.WebRootPath; // URL - для сайта
                var fileName = Path.GetRandomFileName().Replace('.', '_')
                     + Path.GetExtension(AvatarFile.FileName);
                var filePath = Path.Combine(wwwRootPath + "\\storage\\avatars\\", fileName); // Реальный путь 
                
                user.Avatar = "/storage/avatars/" + fileName; // ссылка на картинку

                using (var stream = System.IO.File.Create(filePath))
                {
                    await AvatarFile.CopyToAsync(stream);
                }


                #endregion

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

    }
}
