﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApiActividadesApoyo.Controllers
{
    public class FicIndexController : Controller
    {
        public IActionResult FicMetIndex()
        {
            return View();
        }
    }
}