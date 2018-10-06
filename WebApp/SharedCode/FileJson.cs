using System;
using System.Collections.Generic;
using System.Text;

namespace SharedCode
{
   public class FileJson
    {
        public string Name { get; set; }
        public string Body { get; set; }
        public Lesson Lesson { get; set; }
        public DateTime CreatedAt { get; set; }
        public long AccessToken { get; set; }
        virtual public List<Grade> Grades { get; set; }
    }
}
