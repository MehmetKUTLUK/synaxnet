﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Education.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
