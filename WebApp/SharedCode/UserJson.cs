using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class UserJson
    {
        public string email { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string firstname { get; set; }
        public bool stuPro { get; set; }
    }
}
