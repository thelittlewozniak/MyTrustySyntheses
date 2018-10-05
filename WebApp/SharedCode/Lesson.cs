using System;
using System.Collections.Generic;
using System.Text;

namespace SharedCode
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Year { get; set; }
        public Categorie Categorie { get; set; }
        public User Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public string School { get; set; }

    }
}
