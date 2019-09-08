using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (!HttpContext.Session.Keys.Contains("Time"))
                HttpContext.Session.SetString("Time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            ViewBag.SessionId = HttpContext.Session.Id;
            return View();
        }
    }
}