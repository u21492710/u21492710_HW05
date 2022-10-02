using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tutorial16.Models
{
    public class Borrows
    {
        public int borrowID { get; set; }
        public int studentID { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public int bookId { get; set; }
        public string takenDate { get; set; }
        public string broughtDate { get; set; }
    }
}