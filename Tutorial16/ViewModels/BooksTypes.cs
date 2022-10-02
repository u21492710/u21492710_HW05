using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tutorial16.Models;

namespace Tutorial16.ViewModels
{
    public class BooksTypes
    {
        public List<Student> students  { get; set; }
        public List<string> types { get; set; }
    }
}