using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MvcApp.Controllers
{
    public class SalamController : Controller
    {
        [Route("Salam")]
        public string Index()
        {
            return "Hai, dunia!";
        }

        [Route("Salam/Welcome/{nama}/{umur}")]
        public string Welcome(string nama, int umur)
        {
            return HtmlEncoder.Default.Encode($"Helllo, nama saya {nama}, umur {umur}");
        }

        // parsing data to view
        public IActionResult About()
        {
            ViewData["nama"] = "Ihsan Miftahul Huda";
            ViewData["divisi"] = "Education";
            ViewData["role"] = "Fullstack Developer";
            return View();
        }

        public IActionResult Loop(string name, int n = 5)
        {
            ViewData["Pesan"] = "Selamat Pagi" + name;
            ViewData["Jml"] = n;
            return View();
        }

    }
}