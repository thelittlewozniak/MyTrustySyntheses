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
        public ActionResult<long> Login(string password,string email)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            password = System.Text.Encoding.ASCII.GetString(data);
            var u = (from e in _context.Users where e.Email == email && e.Password == password select e).FirstOrDefault();
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
        public ActionResult<long> Create(string name,string firstname,string password,string email,string stuPro)
        {
            var u = (from e in _context.Users where e.Email == email select e).FirstOrDefault();
            if (u == null)
            {
                byte[] data = System.Text.Encoding.ASCII.GetBytes(password);
                data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
                password = System.Text.Encoding.ASCII.GetString(data);
                var date = DateTime.UtcNow;
                _context.Users.Add(new SharedCode.User { Name = name, Firstname = firstname, Password = password, Email = email, StuPro = Convert.ToBoolean(stuPro), Experience = 0, CreationDate = DateTime.UtcNow, LastLogin = date,AccessToken=(email.GetHashCode()+password.GetHashCode())*date.Ticks });
                _context.SaveChanges();
                u = (from e in _context.Users where e.Email == email select e).First();
                return u.AccessToken;
            }
            else
                return 0;
        }
        public ActionResult<User> SeeUser([FromBody] string id,[FromHeader] string AccessToken)
        {
            var u = (from e in _context.Users where e.AccessToken == Convert.ToInt64(AccessToken) select e).FirstOrDefault();
            if (u != null)
            {
                var d = u.LastLogin.AddMinutes(15);
                if (d.TimeOfDay < DateTime.UtcNow.TimeOfDay)
                    return null;
                else
                    return (from e in _context.Users where e.Id == Convert.ToInt32(id) select e).Include(e => e.Files).FirstOrDefault();
            }
            else
                return null;
        }
    }
}