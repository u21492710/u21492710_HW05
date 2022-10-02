using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tutorial16.Models;
using Tutorial16.ViewModels;

namespace Tutorial16.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        int BookID;
        public ActionResult Index(int bookId)
        {

            ViewBag.SearchStatus = 1;
            ViewBag.Status = 1;
            Session["BookID"] = bookId;
            BookID = bookId;
            BooksTypes pass = new BooksTypes
            {
                students = Services.getAllStudents(),
                types = Services.getAllClasses(),
                
            };
            //if the book is booked, get intel on who booked it we running the whole check if the book was booked call
            if (Services.getBookByIdandFindStudentIfBooked(bookId).status == "Out")
            {
                Session["BookedStu"] = Services.bookedStu.studentId;
            }
            else
            {
                Session["BookedStu"] = -1;
            }
            return View(pass);
        }
        
        public ActionResult CallBorrowBook(int studentId)
        {
            Services.BorrowBook((int)Session["BookID"], studentId);
            //Coding to load up the correct books information
            Session["CallerId"] = (int)Session["BookID"];
            return RedirectToAction("CallerId", "Book");
        }
        public ActionResult ReturnBook(int studentId)
        {
            Services.ReturnBook((int)Session["BookID"], studentId);
            //Coding to load up the correct books information
            Session["CallerId"] = (int)Session["BookID"];
            return RedirectToAction("CallerId", "Book");
        }
        public ActionResult ComplexSearch(string searchText, string className)
        {
            ViewBag.SearchStatus = 1;
            ViewBag.Status = 1;
            BooksTypes pass = new BooksTypes
            {
                students = Services.getSearchedStudents(searchText,className),
                types = Services.getAllClasses(),

            };
            return View("Index", pass);
        }
    }
}