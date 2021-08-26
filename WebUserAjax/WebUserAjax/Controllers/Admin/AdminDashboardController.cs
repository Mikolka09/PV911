using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace WebUserAjax.Controllers.Admin
{
    public class AdminDashboardController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View("~/Views/Admin/index.cshtml");
        }
    }
}
