using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tutorial16.Models;

namespace Tutorial16.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index(int bookId)
        {
            Books selectedBook = Services.getBookByIdONLY(bookId);
            ViewBag.bookID = bookId;
            ViewBag.name = selectedBook.Name;
            ViewBag.status = selectedBook.status;
            ViewBag.borrowed = Services.getNumberBooksBorrowed(bookId);
            return View(Services.getBorrowsByID(bookId));
        }
        public ActionResult CallerId()
        {
            int id = (int)Session["CallerId"];
            Books selectedBook = Services.getBookByIdONLY(id);
            ViewBag.bookID = id;
            ViewBag.name = selectedBook.Name;
            ViewBag.status = selectedBook.status;
            ViewBag.borrowed = Services.getNumberBooksBorrowed(id);
            return View("Index", Services.getBorrowsByID(id));
            //return View("Index", id);//This does not even call the Index function, it just goes straight to the view and assumes that you already have done functionality needed
        }
        
    }
}