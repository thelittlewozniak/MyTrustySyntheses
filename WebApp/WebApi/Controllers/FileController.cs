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
        //public ActionResult<long> Add(string name, string body,User creator,DateTime createdAt)
        //{
        //    var u = (from e in _context.Users where e.
        //    _context.Files.Add(new SharedCode.File { Name = name, Body = body, Creator = creator, CreatedAt = DateTime.UtcNow});
        //}
    }
}