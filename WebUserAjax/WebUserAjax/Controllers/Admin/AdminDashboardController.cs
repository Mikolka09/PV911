﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebUserAjax.Entities;

namespace WebUserAjax.Controllers.Admin
{
    [Authorize]
    public class AdminDashboardController : Controller
    {

        private readonly UserManager<ProjectUser> _userManager;
        private readonly SignInManager<ProjectUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminDashboardController(UserManager<ProjectUser> userManager, SignInManager<ProjectUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

      
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            return View("~/Views/Admin/index.cshtml");
        }
    }
}
