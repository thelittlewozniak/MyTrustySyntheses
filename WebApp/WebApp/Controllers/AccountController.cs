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


            ViewBag.Message = "Compte créé avec succes !";
            return RedirectToAction("Login");
        }

        //Login Confirmation
        [HttpPost]
        public ActionResult LoginConf(User u)
        {
            if (u.Email == null || u.Email == null)
            {
                ViewBag.Message = "Login ou Mot de passe vide.";
                return View("Login");
            }

            //User u = new User();

            try
            {
                //Valider ????
                //user = CUtilisateur.ChercherUtilisateur(userinit);
            }
            catch
            {
                ViewBag.Message = "Login ou Mot de passe Incorrecte !";
                return View("Login");
            }

            if (u != null)
            {
                //Ouvrir session
                //Session["Id"] = user.Id;
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = "Erreur !";
            return View("Login");
        }

        //Déconnection
        public IActionResult Disconnect()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        



        //Modifier Profil
        /*
        public IActionResult ModifierProfil()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ModifierProfilVerif()
        {
            return View();
        }*/
    }
}