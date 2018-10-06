using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<long> Add(string name, string body, string lesson,[FromHeader] long accessToken)
        {
            var u = (from e in _context.Users where e.AccessToken == accessToken select e).FirstOrDefault();
            if (u != null)
            {
                var l = (from x in _context.Lessons where x.Name == lesson select x).FirstOrDefault();
                if(l != null)
                {
                    _context.Files.Add(new SharedCode.File { Name = name, Body = body, CreatedAt = DateTime.UtcNow, Creator = u, Lesson = l });
                    _context.SaveChanges();
                }
                return u.AccessToken;
            }
            else
                return 0;
        }
        
        public ActionResult<long> Delete(File file, [FromHeader] long accessToken)
        {
            var u = (from e in _context.Users where e.AccessToken == accessToken select e).FirstOrDefault();
            if (u != null)
            {
                var f = (from p in _context.Files where p.Id == file.Id select p).FirstOrDefault();
                if(f != null){
                    _context.Files.Remove(f);
                    _context.SaveChanges();
                }
                return u.AccessToken;
            }
            else
                return 0;
        }

        public ActionResult<long> Rate(int trustLvl,File file, [FromHeader] long accessToken)
        {
            var u = (from e in _context.Users where e.AccessToken == accessToken select e).FirstOrDefault();
            if (u != null)
            {
                var f = (from p in _context.Files where p.Id == file.Id select p).FirstOrDefault();
                if (f != null)
                {
                    _context.Grades.Add(new SharedCode.Grade { Creator = u, TrustLvl = trustLvl, CreationDate = DateTime.UtcNow, File = f});
                    _context.SaveChanges();
                }
                return u.AccessToken;
            }
            else
                return 0;
        }
    }
}