using System;
using System.Collections.Generic;
using System.Text;

namespace SharedCode
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Lesson Lesson { get; set; }
        virtual public List<Grade> Grades { get; set; }
    }
}
