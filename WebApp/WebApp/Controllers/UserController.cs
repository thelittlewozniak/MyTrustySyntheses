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
        public IActionResult ShowUser()
        {
            return View();
        }

        //Ajouter un cours
        public async Task<IActionResult> AddFileConfAsync(File f)
        {
            FileJson JsonU = new FileJson();
            JsonU.Name = f.Name;
            JsonU.Body = f.Body;
            JsonU.CreatedAt = DateTime.Now;
            //JsonU.AccessToken = long.Parse(HttpContext.Session.GetString("AccessToken"));
            HttpClient client = new HttpClient();
            string json = JsonConvert.SerializeObject(JsonU);
            var res = await client.PostAsync("http://apimts.azurewebsites.net/api/File/AddFile", new StringContent(json, Encoding.UTF8, "application/json"));
            bool response = await res.Content.ReadAsAsync<bool>();
            if (response)
            {
                ViewBag.Message = "Fichier créé !";
            }
            else
            {
                ViewBag.Message = "Erreur lors de la création du fichier";
            }
            return View("ShowFile");
        }

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