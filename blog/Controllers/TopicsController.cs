﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controllers
{
    public class TopicsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}