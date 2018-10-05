using System;
using System.Collections.Generic;
using System.Text;

namespace SharedCode
{
    public class Grade
    {
        public int Id { get; set; }
        public User Creator { get; set; }
        public int TrustLvl { get; set; }
        public DateTime CreationDate { get; set; }
        public File File { get; set; }
    }
}
