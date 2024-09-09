﻿using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.Controllers
{
    public class SessionController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.SetInt32("age", 22);
            HttpContext.Session.SetString("name","Jack");
            return Ok();
        }

        public IActionResult Get()
        {
            var name = HttpContext.Session.GetString("name");
            var age = HttpContext.Session.GetInt32("age");
            return Ok($"Name : {name} Age : {age}");
        }
    }
}
