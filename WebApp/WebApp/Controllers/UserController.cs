using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharedCode;
using Newtonsoft.Json;
using WebApi.Models;
using System.Text;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        //Index
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        //Recherche d'utilisateur
        public IActionResult ResearchUser()
        {
            return View();
        }

        //Recherche de cours
        public IActionResult ResearchCourse()
        {
            return View();
        }

        //Recherche de notes
        public IActionResult ResearchNote()
        {
            return View();
        }

        //Afficher une note
        public IActionResult ShowFile()
        {
            return View();
        }

        //Afficher un user
        public async Task<IActionResult> ShowUser()
        {
            if (HttpContext.Session.GetString("AccessToken") != null && HttpContext.Session.GetString("AccessToken") != "0")
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("AccessToken", HttpContext.Session.GetString("AccessToken"));
                var res = await client.GetAsync("http://apimts.azurewebsites.net/api/User/SeeUser");
                var responseString = await res.Content.ReadAsStringAsync();
                var u= JsonConvert.DeserializeObject<User>(responseString);
                ViewBag.user = u;
                return View();
            }
            else
            {
                
                return View("Index");
            }
        }

        //Ajouter un cours
       

        //Ajouter un cours Confirmation
        public IActionResult AddFile()
        {
            return View();
        }

        //Trust
        public IActionResult AddTrust()
        {
            return View();
        }
    }
}