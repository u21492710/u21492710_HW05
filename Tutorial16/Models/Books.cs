using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tutorial16.Models
{
    public class Books
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Type { get; set; }
        public int Pagecount { get; set; }
        public int Points { get; set; }
        public string status { get; set; }
    }
}