using System;
using System.Collections.Generic;
using System.Text;

namespace SharedCode
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Firstname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Experience { get; set; }
        public bool StuPro { get; set; }
        public string School { get; set; }
        public long AccessToken { get; set; }
        public List<User> Trusters { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastLogin { get; set; }
        virtual public List<File> Files { get; set; }
    }
}
