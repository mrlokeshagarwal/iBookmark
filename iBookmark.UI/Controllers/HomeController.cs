using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using iBookmark.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace iBookmark.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<iBookmarkConfig> _config;
        public HomeController(IOptions<iBookmarkConfig> config)
        {
            this._config = config;
        }
        public IActionResult Index()
        {
            ViewData["APIUrl"] = this._config.Value.APIURL;
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
