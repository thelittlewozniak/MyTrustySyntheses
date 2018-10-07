using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using SharedCode;
using Newtonsoft.Json;

using WebApi.Models;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        //string json = JsonConvert.SerializeObject();
        private static readonly HttpClient client = new HttpClient();

        //Index
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
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

            UserJson JsonU = new UserJson();
            JsonU.email = u.Email;
            JsonU.firstname = u.Firstname;
            JsonU.name = u.Name;
            JsonU.password = u.Password;
            HttpClient client = new HttpClient();
            string json = JsonConvert.SerializeObject(JsonU);
            var res = await client.PostAsync("http://apimts.azurewebsites.net/api/User/Create", new StringContent(json, Encoding.UTF8, "application/json"));
            var responseString = await res.Content.ReadAsStringAsync();

            string AccessToken = JsonConvert.DeserializeObject<string>(responseString);
            HttpContext.Session.SetString("AccessToken", AccessToken);

            ViewBag.Message = "Compte créé avec succes !";
            return RedirectToAction("Login");
        }

        //Login Confirmation
        [HttpPost]

        public async Task<ActionResult> LoginConf(User u)
        {
            UserJson j = new UserJson();
            j.email = u.Email;
            j.password = u.Password;
            string json = JsonConvert.SerializeObject(j);
            var res = await client.PostAsync("http://apimts.azurewebsites.net/api/User/Login", new StringContent(json, Encoding.UTF8, "application/json"));
            var responseString = await res.Content.ReadAsStringAsync();
            string AccessToken = JsonConvert.DeserializeObject<string>(responseString);
            HttpContext.Session.SetString("AccessToken", Convert.ToString(AccessToken));

            return View("Login");
        }

        //Déconnection
        public IActionResult Disconnect()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}