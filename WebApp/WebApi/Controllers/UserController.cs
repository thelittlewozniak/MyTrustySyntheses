using System;
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
    public class UserController : ControllerBase
    {
        private Context _context;
        public UserController(Context context)
        {
            _context = context;
        }
        [Route("Login")]
        [HttpPost]
        public ActionResult<long> Login(UserJson json)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(json.password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            json.password = System.Text.Encoding.ASCII.GetString(data);
            var u = (from e in _context.Users where e.Email == json.email && e.Password == json.password select e).FirstOrDefault();
            if (u != null)
            {
                var date = DateTime.UtcNow;
                _context.Users.Where(e => e.Email == u.Email).First().LastLogin = date;
                _context.Users.Where(e=>e.Email==u.Email).FirstOrDefault().AccessToken= Math.Abs(u.Email.GetHashCode() + u.Password.GetHashCode() * u.LastLogin.Ticks);
                _context.SaveChanges();
                return _context.Users.Where(e => e.Email == u.Email).FirstOrDefault().AccessToken;
            }
            else
                return 0;
        }
        [Route("Create")]
        [HttpPost]
        public ActionResult<long> Create(UserJson json)
        {
            var u = (from e in _context.Users where e.Email == json.email select e).FirstOrDefault();
            if (u == null)
            {
                byte[] data = System.Text.Encoding.ASCII.GetBytes(json.password);
                data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
                json.password = System.Text.Encoding.ASCII.GetString(data);
                var date = DateTime.UtcNow;
                _context.Users.Add(new SharedCode.User { Name = json.name, Firstname = json.firstname, Password = json.password, Email = json.email, StuPro = json.stuPro, Experience = 0, CreationDate = DateTime.UtcNow, LastLogin = date,AccessToken=Math.Abs(json.email.GetHashCode()+json.password.GetHashCode())*date.Ticks });
                _context.SaveChanges();
                u = (from e in _context.Users where e.Email == json.email select e).First();
                return u.AccessToken;
            }
            else
                return 0;
        }
        [Route("SeeUser")]
        [HttpGet]
        public ActionResult<User> SeeUser( string id,[FromHeader]string AccessToken)
        {
            var u = (from e in _context.Users where e.AccessToken == Convert.ToInt64(AccessToken) select e).FirstOrDefault();
            if (u != null)
            {
                var d = u.LastLogin.AddMinutes(15);
                if (d.TimeOfDay < DateTime.UtcNow.TimeOfDay)
                    return null;
                else
                    return (from e in _context.Users where e.Id == Convert.ToInt32(id) select e).FirstOrDefault();
            }
            else
                return null;
        }
    }
}