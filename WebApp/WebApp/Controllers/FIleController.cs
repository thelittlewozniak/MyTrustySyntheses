using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedCode;
using WebApi.Models;
using System.Text;

namespace WebApp.Controllers
{
    public class FileController : Controller
    {
        private static readonly HttpClient client = new HttpClient();
        public async Task<IActionResult> Index()
        {
            var res = await client.GetAsync("http://apimts.azurewebsites.net/api/File/SeeFileUnco");
            var responseString = await res.Content.ReadAsStringAsync();
            List<File> files = JsonConvert.DeserializeObject<List<File>>(responseString);
            ViewBag.files = files;//"Compte créé avec succes !";
            return View();
        }
        public async Task<IActionResult> IndexCo()
        {
            client.DefaultRequestHeaders.Add("AccessToken", HttpContext.Session.GetString("AccessToken"));
            var res = await client.GetAsync("http://apimts.azurewebsites.net/api/File/SeeFileUnco");
            var responseString = await res.Content.ReadAsStringAsync();
            List<File> files = JsonConvert.DeserializeObject<List<File>>(responseString);
            ViewBag.files = files;//"Compte créé avec succes !";
            return View();
        }
        public ActionResult AddFile()
        {
            return View();
        }
        public async Task<IActionResult> ShowFile(string id)
        {
            if (HttpContext.Session.GetString("AccessToken") != null && HttpContext.Session.GetString("AccessToken") != "0")
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("AccessToken", HttpContext.Session.GetString("AccessToken"));
                var res = await client.GetAsync("https://localhost:44343/api/File/ShowFile?id="+id);
                var responseString = await res.Content.ReadAsStringAsync();
                File file = JsonConvert.DeserializeObject<File>(responseString);
                ViewBag.file = file;
                return View();
            }
            else
            {
                ViewBag.Message = "Erreur, vous n'êtes pas connecté !";
                return RedirectToAction("ShowFiles");
            }
           
        }
        public async Task<IActionResult> ShowFiles()
        {
            if (HttpContext.Session.GetString("AccessToken") != null && HttpContext.Session.GetString("AccessToken") != "0")
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("AccessToken", HttpContext.Session.GetString("AccessToken"));
                var res = await client.GetAsync("http://apimts.azurewebsites.net/api/File/SeeFileCo");
                var responseString = await res.Content.ReadAsStringAsync();
                List<File> files = JsonConvert.DeserializeObject<List<File>>(responseString);
                ViewBag.files = files;
                return View();
            }
            else
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("AccessToken", HttpContext.Session.GetString("AccessToken"));
                var res = await client.GetAsync("http://apimts.azurewebsites.net/api/File/SeeFileUnco");
                var responseString = await res.Content.ReadAsStringAsync();
                List<File> files = JsonConvert.DeserializeObject<List<File>>(responseString);
                ViewBag.files = files;
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddFileConfAsync(File f)
        {
            FileJson JsonU = new FileJson();
            JsonU.Name = f.Name;
            JsonU.Body = f.Body;
            JsonU.Lesson = new Lesson() { Name = "C# premier quad" };
            JsonU.CreatedAt = DateTime.Now;
            HttpClient client = new HttpClient();
            string access = HttpContext.Session.GetString("test");
            client.DefaultRequestHeaders.Add("AccessToken", HttpContext.Session.GetString("AccessToken"));
            string json = JsonConvert.SerializeObject(JsonU);
            var res = await client.PostAsync("https://localhost:44343/api/File/AddFile", new StringContent(json, Encoding.UTF8, "application/json"));
            bool response = await res.Content.ReadAsAsync<bool>();
            if (response)
            {
                ViewBag.Message = "Fichier créé !";
            }
            else
            {
                ViewBag.Message = "Erreur lors de la création du fichier";
            }
            return View("AddFile");
        }

  
    }
}