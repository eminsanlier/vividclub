﻿using Microsoft.AspNetCore.Mvc;

namespace VividClub.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}