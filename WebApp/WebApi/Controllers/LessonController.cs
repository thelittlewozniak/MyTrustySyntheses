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
    public class LessonController : ControllerBase
    {
        private Context _context;
        public LessonController(Context context)
        {
            _context = context;
        }
        [Route("LessonsList")]
        public ActionResult<List<Lesson>> LessonsList()
        {
            List<Lesson> list = new List<Lesson>();
            foreach(Lesson l in _context.Lessons)
            {
                list.Add(l);
            }
            return list;
        }
    }
}