﻿using Microsoft.AspNetCore.Mvc;

namespace FinalExamAmoeba.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
