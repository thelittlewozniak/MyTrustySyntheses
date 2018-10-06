using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SharedCode;

namespace WebApp.Controllers
{
    public class FIleController : Controller
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
    }
}