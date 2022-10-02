using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tutorial16.ViewModels;
using Tutorial16.Models;
namespace Tutorial16.Controllers
{
    public static class Globals
    {
        public static string ConnectionString = "Data Source=LAPTOP-0JP9TJ8I\\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True";
        public static List<Books> booksList = new List<Books>();
        //public static List<Borrow> booksList = new List<Borrow>();
    }
}