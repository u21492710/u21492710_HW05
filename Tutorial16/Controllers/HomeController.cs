using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tutorial16.ViewModels;
using Tutorial16.Models;

namespace Tutorial16.Controllers
{
    public class HomeController : Controller
    {
        private SqlConnection myConnection = new SqlConnection(Globals.ConnectionString);
        
        public ActionResult Index()
        {
            
            ViewBag.SearchStatus = 1;
            ViewBag.Status = 1;
            BooksTypeAuthor pass = new BooksTypeAuthor
            {
                books = Services.getAllBooks(),
                types = Services.getTypes(),
                authors = Services.getAuthors()
            };
            return View(pass);
        }

       

       

        public ActionResult ComplexSearch(string searchText, string type, string author)
        {
            ViewBag.SearchStatus = 1;
            ViewBag.Status = 1;
            BooksTypeAuthor pass = new BooksTypeAuthor
            {
                books = Services.getSearchedBooks(searchText,type,author),
                types = Services.getTypes(),
                authors = Services.getAuthors()
            };
            return View("Index", pass);
        }

        

       
    }
}