﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharedCode
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public Lesson Lesson { get; set; }
        public User Creator { get; set; }
        public DateTime CreatedAt { get; set; }

        virtual public List<Grade> Grades { get; set; }
    }
}
