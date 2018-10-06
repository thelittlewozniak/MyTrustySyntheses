using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedCode;
using WebApi.Models;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        //Index
        public IActionResult Index()
        {
            return View();
        }

        //About
        public IActionResult AboutUs()
        {
            ViewData["Message"] = "GameJam 2018 KYT";
            return View();
        }

        //Register
        public IActionResult Register()
        {
            return View();
        }

        //Login
        public IActionResult Login()
        {
            return View();
        }

        //Inscription Confirmation
        [HttpPost]
        public async Task<IActionResult> RegisterConfAsync(User u)
        {
            /*if (!ModelState.IsValid)
            {
                ViewBag.Message = "Incomplet !";
                return View("Inscription");
            }*/

            /*if (u != null)
            {
                ViewBag.Message = "Utilisateur déjà existant.";
                return View("Inscription");
            }*/

            UserJson JsonU = new UserJson();
            JsonU.email = u.Email;
            JsonU.firstname = u.Firstname;
            JsonU.name = u.Name;
            JsonU.password = u.Password;
            HttpClient client = new HttpClient();
            string json = JsonConvert.SerializeObject(JsonU);
            var res = await client.PostAsync("http://apimts.azurewebsites.net/api/User/Create", new StringContent(json, Encoding.UTF8, "application/json"));
            var responseString = await res.Content.ReadAsStringAsync();
            ViewBag.Message = responseString;//"Compte créé avec succes !";
            return RedirectToAction("Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }        
    }
}
