using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult ShowNote()
        {
            return View();
        }

        //Afficher un user
        public IActionResult ShowUser()
        {
            return View();
        }
    }
}