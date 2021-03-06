﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedCode;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private Context _context;
        public FileController(Context context)
        {
            _context = context;
        }
        [Route("AddFile")]
        [HttpPost]
        public ActionResult<bool> Add(FileJson Json,[FromHeader] long accessToken)
        {
            var u = (from e in _context.Users where e.AccessToken == accessToken select e).FirstOrDefault();
            if (u != null)
            {
                var d = u.LastLogin.AddMinutes(15);
                if (d.TimeOfDay < DateTime.UtcNow.TimeOfDay)
                    return false;
                else
                {
                    var l = (from x in _context.Lessons where x.Name == Json.Lesson.Name select x).FirstOrDefault();
                    if (l != null)
                    {
                        _context.Files.Add(new SharedCode.File { Name = Json.Name, Body = Json.Body, CreatedAt = DateTime.UtcNow, Creator = u, Lesson = l });
                        _context.Users.Where(e => e.Email == u.Email).FirstOrDefault().Experience += 10;
                        _context.SaveChanges();
                    }
                    return true;
                }
            }
            else
                return false;
        }
        [Route("DeleteFile")]
        public ActionResult<bool> Delete(File file, [FromHeader] long accessToken)
        {
            var u = (from e in _context.Users where e.AccessToken == accessToken select e).FirstOrDefault();
            if (u != null)
            {
                var d = u.LastLogin.AddMinutes(15);
                if (d.TimeOfDay < DateTime.UtcNow.TimeOfDay)
                    return false;
                else
                {
                    var f = (from p in _context.Files where p.Id == file.Id select p).FirstOrDefault();
                    if (f != null)
                    {
                        _context.Files.Remove(f);
                        _context.SaveChanges();
                    }
                    return true;
                }
            }
            else
                return false;
        }
        [Route("RateFile")]
        [HttpPost]
        public ActionResult<bool> Rate(GradeJson json, [FromHeader] long accessToken)
        {
            var u = (from e in _context.Users where e.AccessToken == accessToken select e).FirstOrDefault();
            if (u != null)
            {
                var d = u.LastLogin.AddMinutes(15);
                if (d.TimeOfDay < DateTime.UtcNow.TimeOfDay)
                    return false;
                else
                {
                    var f = (from p in _context.Files where p.Id == Convert.ToInt32(json.idFile) select p).FirstOrDefault();
                    if (f != null)
                    {
                        var g = (from e in _context.Grades where e.Creator == u && e.File == f select e).FirstOrDefault();
                        if(g==null)
                        {
                            _context.Grades.Add(new SharedCode.Grade { Creator = u, TrustLvl = json.trustlvl, CreationDate = DateTime.UtcNow, File = f });
                            _context.Users.Where(e => e.Email == u.Email).FirstOrDefault().Experience += 5;
                            _context.SaveChanges();
                        }
                    }
                    return true;
                }
            }
            else
                return false;
        }
        [Route("ShowFile")]
        [HttpGet]
        public ActionResult<File> ShowFile(string id, [FromHeader] long AccessToken)
        {
            var u = (from e in _context.Users where e.AccessToken == AccessToken select e).FirstOrDefault();
            if (u != null)
            {
                var d = u.LastLogin.AddMinutes(15);
                if (d.TimeOfDay < DateTime.UtcNow.TimeOfDay)
                    return null;
                else
                {
                    var i = Convert.ToInt32(id);
                    return (from e in _context.Files where e.Id == i select e).FirstOrDefault();
                }
            }
            else
                return null;
        }
        [Route("AlterFile")]
        [HttpPost]
        public ActionResult<bool> Alter(string title, string body,File oldFile,[FromHeader] long accessToken)
        {
            var u = (from e in _context.Users where e.AccessToken == accessToken select e).FirstOrDefault();
            if (u != null)
            {
                var d = u.LastLogin.AddMinutes(15);
                if (d.TimeOfDay < DateTime.UtcNow.TimeOfDay)
                    return false;
                else
                {
                    if (oldFile.Creator == u)
                    {
                        var f = (from p in _context.Files where p.Id == oldFile.Id select p).FirstOrDefault();
                        _context.Files.Add(new SharedCode.File { Name = title, Body = body, CreatedAt = DateTime.UtcNow, Creator = u, Lesson = oldFile.Lesson });
                        _context.Files.Remove(f);
                        _context.SaveChanges();
                    }
                    return true;
                }
            }
            else
                return false;
        }
        [Route("SimpleSearchContentFile")]
        [HttpPost]
        public ActionResult<List<File>> SimpleSearchContent(string search)
        {
            List<File> list = new List<File>();

            foreach(File f in _context.Files)
            {
                if(f.Name.Contains(search) || f.Body.Contains(search))
                {
                    list.Add(f);
                }
            }
            return list;
        }

        [Route("SimpleSearchUserFile")]
        [HttpPost]
        public ActionResult<List<User>> SimpleSearchUser(string search) //IL faut préciser dans le placeholder que c'est le nom qui doit être entrer en 1er et ensuite le prénom
        {
            List<User> list = new List<User>();
            string find = " ";
            int position = search.IndexOf(find);
            string lastname = search.Substring(0,position - 1);
            string firstname = search.Substring(position + 1, search.Length);
            foreach (User u in _context.Users)
            {
                if (u.Firstname == firstname && u.Name == lastname)
                {
                    list.Add(u);
                }
            }
            return list;
        }
        [Route("SeeFileCo")]
        [HttpGet]
        public ActionResult<List<File>> SeeFileCo([FromHeader] string AccessToken)
        {
            var u = (from e in _context.Users where e.AccessToken == Convert.ToInt64(AccessToken) select e).FirstOrDefault();
            if (u != null)
            {
                var d = u.LastLogin.AddMinutes(15);
                if (d.TimeOfDay < DateTime.UtcNow.TimeOfDay)
                    return null;
                else
                    //return (from e in _context.Files where e.Lesson.School==u.School select e).ToList();
                    return (from e in _context.Files select e).Include(e=>e.Creator).ToList();
            }
            else
                return null;
        }
        [Route("SeeFileUnco")]
        [HttpGet]
        public ActionResult<List<File>> SeeFileUnco()
        {
            return _context.Files.Include(e=>e.Creator).ToList();
        }
    }
}