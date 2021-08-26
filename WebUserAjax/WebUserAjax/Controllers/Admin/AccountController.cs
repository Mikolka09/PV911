using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUserAjax.Entities;

namespace WebUserAjax.Controllers.Admin
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ProjectUser> _userManager;
        private readonly SignInManager<ProjectUser> _signInManager;



        public AccountController(UserManager<ProjectUser> userManager, SignInManager<ProjectUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            //Вывожу список пользователей
            return View("~/Views/Admin/Account/index.cshtml", _userManager.Users);
        }
    }
}
