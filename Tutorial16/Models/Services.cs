using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Tutorial16.Models
{
    public static class Services
    {
        public static string ConnectionString = "Data Source=LAPTOP-0JP9TJ8I\\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True";
        private static SqlConnection currConnection;
        public static void openConnection()
        {
            //bool status = true;
            try
            {
                String conString = ConnectionString;
                currConnection = new SqlConnection(conString);
                currConnection.Open();
            }
            catch (Exception exc)
            {

                //status = false;
            }
            //return status;
        }
        public static bool closeConnection()
        {
            if (currConnection != null)
            {
                currConnection.Close();
            }
            return true;
        }

        public static List<Borrows> getBorrowsTable()
        {
            List<Borrows> borrows = new List<Borrows>();
            try
            {
                openConnection();
                //Read all person records for table
                SqlCommand myCommand = new SqlCommand("select borrows.borrowId, borrows.studentId,borrows.bookId, borrows.takenDate, borrows.broughtDate, students.name, students.surname from borrows, students where borrows.studentId = students.studentId order by borrows.borrowId Desc", currConnection);
                SqlDataReader myReader = myCommand.ExecuteReader();
                //Globals.booksList.Clear(); No need now

                while (myReader.Read())
                {
                    Borrows borrow = new Borrows();
                    borrow.borrowID = (int)myReader["borrowId"];
                    borrow.studentID = (int)myReader["studentId"]; 
                    borrow.name = myReader["name"].ToString();
                    borrow.surname = myReader["surname"].ToString();
                    borrow.bookId = (int)myReader["bookId"];
                    borrow.takenDate =  myReader["takenDate"].ToString();
                    borrow.broughtDate = myReader["broughtDate"].ToString();
                    borrows.Add(borrow);
                    
                }
            }
            catch (Exception err)
            {

            }
            finally
            {
                closeConnection();
            }
            return borrows;

        }
        public static List<string> getTypes()
        {
            List<string> types = new List<string>();
            try
            {
                openConnection();
                SqlCommand myCommand = new SqlCommand("select types.name from types", currConnection);
                SqlDataReader myReader = myCommand.ExecuteReader();
                //Globals.booksList.Clear(); No need now

                while (myReader.Read())
                {
                    types.Add(myReader["name"].ToString());
                }
            }
            catch (Exception err)
            {

            }
            finally
            {
                closeConnection();
            }
            return types;
        }
        public static List<string> getAuthors()
        {
            List<string> authors = new List<string>();
            try
            {
                openConnection();
                SqlCommand myCommand = new SqlCommand("select Authors.surname from Authors", currConnection);
                SqlDataReader myReader = myCommand.ExecuteReader();
                //Globals.booksList.Clear(); No need now

                while (myReader.Read())
                {
                    authors.Add(myReader["surname"].ToString());
                }
            }
            catch (Exception err)
            {

            }
            finally
            {
                closeConnection();
            }
            return authors;
        }
        public static List<Books> getAllBooks()
        {
            List<Borrows> borr = getBorrowsTable();
            List<Books> books = new List<Books>();
            try
            {
                openConnection();
                //Read all person records for table
                SqlCommand myCommand = new SqlCommand("select books.bookId, books.name, Authors.surname,types.Name as [Type],books.pagecount,books.point from books,authors,types where books.authorid = authors.authorId and books.typeId = types.typeId", currConnection);
                SqlDataReader myReader = myCommand.ExecuteReader();
                //Globals.booksList.Clear(); No need now
                
                while (myReader.Read())
                {
                    Books book = new Books();
                    book.ID = (int)myReader["bookId"];
                    book.Name = myReader["name"].ToString();
                    book.Surname = myReader["surname"].ToString();
                    book.Type = myReader["Type"].ToString();
                    //book.Title.Description = myReader["pagecount"].ToString();
                    book.Pagecount = (int)myReader["pagecount"];
                    book.Points = (int)myReader["point"];
                    var row = borr.Where(x => x.bookId == book.ID && x.broughtDate == "").SingleOrDefault();//Then the book must be available !!
                    if (row ==null )
                    {
                        //Then the book must be available!!
                        book.status = "Available";
                    }
                    else
                    {
                        //Not Available 
                        book.status = "Out";
                    }
                    books.Add(book);
                    //Globals.booksList.Add(book);
                }
            }
            catch (Exception err)
            {
               
            }
            finally
            {
                closeConnection();
            }
            return books;
        }

        public static List<Books> getSearchedBooks(string searchText, string type, string author)
        {

            List<Borrows> borr = getBorrowsTable();
            List<Books> books = new List<Books>();
            //Build Command 
            string comm = "select books.bookId, books.name, Authors.surname,types.Name as [Type],books.pagecount,books.point from books,authors,types where books.authorid = authors.authorId and books.typeId = types.typeId ";
            bool where = true;
            bool text = false;
            bool typeB = false;
            bool authorB = false;
            if (searchText !="" &&where ==false &&text ==false)
            {
                comm += "where books.name LIKE '%" + searchText + "%'";
                where = true;
                text = true;
            }
            if (searchText != "" && where && text == false)
            {
                comm += "and books.name LIKE '%" + searchText + "%'";
                text = true;
            }
            if (type !="NADA" && where && typeB == false)
            {
                comm += "and types.Name = '" + type+"'";
                typeB = true;
            }
            if (type !="NADA" && where == false&&  typeB == false)
            {
                comm += "where types.Name = '" + type + "'";
                where = true;
                typeB = true;
            }
            if (author != "NADA" && where && authorB == false)
            {
                comm += "and Authors.surname = '" + author + "'";
                authorB = true;
            }
            if (author !="NADA" && where ==false && authorB == false)
            {
                comm += "where Authors.surname = '" + author + "'";
                where = true;
                authorB = true;
            }
            try
            {
                openConnection();
                //Read all person records for table
                SqlCommand myCommand = new SqlCommand(comm, currConnection);
                SqlDataReader myReader = myCommand.ExecuteReader();
                //Globals.booksList.Clear(); No need now

                while (myReader.Read())
                {
                    Books book = new Books();
                    book.ID = (int)myReader["bookId"];
                    book.Name = myReader["name"].ToString();
                    book.Surname = myReader["surname"].ToString();
                    book.Type = myReader["Type"].ToString();
                    //book.Title.Description = myReader["pagecount"].ToString();
                    book.Pagecount = (int)myReader["pagecount"];
                    book.Points = (int)myReader["point"];
                    var row = borr.Where(x => x.bookId == book.ID && x.broughtDate == "").SingleOrDefault();//Then the book must be available !!
                    if (row == null)
                    {
                        //Then the book must be available!!
                        book.status = "Available";
                    }
                    else
                    {
                        //Not Available 
                        book.status = "Out";
                    }
                    books.Add(book);
                    //Globals.booksList.Add(book);
                }
            }
            catch (Exception err)
            {

            }
            finally
            {
                closeConnection();
            }
            return books;
        }

        public static int getNumberBooksBorrowed(int bookId)
        {
            List<Borrows> borr = getBorrowsTable();
            return   borr.Where(x => x.bookId == bookId).Count();
            
        }

        public static List<Borrows> getBorrowsByID(int bookID)
        {
            return getBorrowsTable().Where(x => x.bookId == bookID).ToList(); ;

        }

        //Students related Stuff
        public static List<Student> getAllStudents()
        {
            List<Borrows> borr = getBorrowsTable();
            List<Student> students = new List<Student>();
            try
            {
                openConnection();
                //Read all person records for table
                SqlCommand myCommand = new SqlCommand("select students.studentId,students.name, students.surname,students.class,students.point from students", currConnection);
                SqlDataReader myReader = myCommand.ExecuteReader();
                //Globals.booksList.Clear(); No need now

                while (myReader.Read())
                {
                    Student student = new Student();
                    student.studentId = (int)myReader["studentId"];
                    student.name = myReader["name"].ToString();
                    student.surname = myReader["surname"].ToString();
                    student.classGrade = myReader["class"].ToString();
                    student.Points = (int)myReader["point"];                  
                    students.Add(student);
                    //Globals.booksList.Add(book);
                }
            }
            catch (Exception err)
            {

            }
            finally
            {
                closeConnection();
            }
            return students;
        }
        public static List<string> getAllClasses()
        {
            List<string> types = new List<string>();
            try
            {
                openConnection();
                SqlCommand myCommand = new SqlCommand("select DISTINCT students.class from students ", currConnection);
                SqlDataReader myReader = myCommand.ExecuteReader();
                //Globals.booksList.Clear(); No need now

                while (myReader.Read())
                {
                    types.Add(myReader["class"].ToString());
                }
            }
            catch (Exception err)
            {

            }
            finally
            {
                closeConnection();
            }
            return types;
        }
        public static List<Student> getSearchedStudents(string searchText, string className)
        {

            List<Borrows> borr = getBorrowsTable();
            List<Student> students = new List<Student>();
            //Build Command 
            string comm = "select students.studentId,students.name, students.surname,students.class,students.point from students ";
            bool where = false;
            bool text = false;
            bool classNameB = false;
            
            if (searchText != "" && where == false &&text ==false)
            {
                comm += "where students.name LIKE '%" + searchText + "%'";
                where = true;
                text = true;
            }
            if (searchText != "" && where && text == false)
            {
                comm += "and students.name LIKE '%" + searchText + "%'";
                text = true;
            }
            if (className != "NADA" && where && classNameB == false)
            {
                comm += "and students.class = '" + className + "'";
                classNameB = true;
            }
            if (className != "NADA" && where == false && classNameB == false)
            {
                comm += "where students.class = '" + className + "'";
                where = true;
                classNameB = true;
            }
            
            try
            {
                openConnection();
                //Read all person records for table
                SqlCommand myCommand = new SqlCommand(comm, currConnection);
                SqlDataReader myReader = myCommand.ExecuteReader();
                //Globals.booksList.Clear(); No need now

                while (myReader.Read())
                {
                    Student student = new Student();
                    student.studentId = (int)myReader["studentId"];
                    student.name = myReader["name"].ToString();
                    student.surname = myReader["surname"].ToString();
                    student.classGrade = myReader["class"].ToString();
                    student.Points = (int)myReader["point"];
                    
                    students.Add(student);
                    //Globals.booksList.Add(book);
                }
            }
            catch (Exception err)
            {

            }
            finally
            {
                closeConnection();
            }
            return students;
        }
        //Finna be used by the book a book function
        public static Student bookedStu = new Student();
        public static Student getStudentById(int stuId)
        {
            List<Borrows> borr = getBorrowsTable();
            List<Student> students = new List<Student>();
            try
            {
                openConnection();
                //Read all person records for table
                SqlCommand myCommand = new SqlCommand("select students.studentId,students.name, students.surname,students.class,students.point from students where students.studentId = "+stuId, currConnection);
                SqlDataReader myReader = myCommand.ExecuteReader();
                //Globals.booksList.Clear(); No need now

                while (myReader.Read())
                {
                    Student student = new Student();
                    student.studentId = (int)myReader["studentId"];
                    student.name = myReader["name"].ToString();
                    student.surname = myReader["surname"].ToString();
                    student.classGrade = myReader["class"].ToString();
                    student.Points = (int)myReader["point"];
                    students.Add(student);
                    //Globals.booksList.Add(book);
                }
            }
            catch (Exception err)
            {

            }
            finally
            {
                closeConnection();
            }
            return students[0];
        }
        public static Books getBookByIdandFindStudentIfBooked(int bookId)
        {
            List<Borrows> borr = getBorrowsTable();
            List<Books> books = new List<Books>();
            try
            {
                openConnection();
                //Read all person records for table
                SqlCommand myCommand = new SqlCommand("select books.bookId, books.name, Authors.surname,types.Name as [Type],books.pagecount,books.point from books,authors,types where books.authorid = authors.authorId and books.typeId = types.typeId and books.bookId ="+ bookId, currConnection);
                SqlDataReader myReader = myCommand.ExecuteReader();
                //Globals.booksList.Clear(); No need now

                while (myReader.Read())
                {
                    Books book = new Books();
                    book.ID = (int)myReader["bookId"];
                    book.Name = myReader["name"].ToString();
                    book.Surname = myReader["surname"].ToString();
                    book.Type = myReader["Type"].ToString();
                    //book.Title.Description = myReader["pagecount"].ToString();
                    book.Pagecount = (int)myReader["pagecount"];
                    book.Points = (int)myReader["point"];
                    var row = borr.Where(x => x.bookId == book.ID && x.broughtDate == "").SingleOrDefault();//Then the book must be available !!
                    if (row == null)
                    {
                        //Then the book must be available!!
                        book.status = "Available";
                    }
                    else
                    {
                        //Not Available 
                        book.status = "Out";
                        bookedStu = getStudentById(row.studentID);
                    }
                    books.Add(book);
                    //Globals.booksList.Add(book);
                }
            }
            catch (Exception err)
            {

            }
            finally
            {
                closeConnection();
            }
            return books[0];
        }
        public static Books getBookByIdONLY(int bookId)
        {
            List<Borrows> borr = getBorrowsTable();
            List<Books> books = new List<Books>();
            try
            {
                openConnection();
                //Read all person records for table
                SqlCommand myCommand = new SqlCommand("select books.bookId, books.name, Authors.surname,types.Name as [Type],books.pagecount,books.point from books,authors,types where books.authorid = authors.authorId and books.typeId = types.typeId and books.bookId =" + bookId, currConnection);
                SqlDataReader myReader = myCommand.ExecuteReader();
                //Globals.booksList.Clear(); No need now

                while (myReader.Read())
                {
                    Books book = new Books();
                    book.ID = (int)myReader["bookId"];
                    book.Name = myReader["name"].ToString();
                    book.Surname = myReader["surname"].ToString();
                    book.Type = myReader["Type"].ToString();
                    //book.Title.Description = myReader["pagecount"].ToString();
                    book.Pagecount = (int)myReader["pagecount"];
                    book.Points = (int)myReader["point"];
                    var row = borr.Where(x => x.bookId == book.ID && x.broughtDate == "").SingleOrDefault();//Then the book must be available !!
                    if (row == null)
                    {
                        //Then the book must be available!!
                        book.status = "Available";
                    }
                    else
                    {
                        //Not Available 
                        book.status = "Out";
                       
                    }
                    books.Add(book);
                    //Globals.booksList.Add(book);
                }
            }
            catch (Exception err)
            {

            }
            finally
            {
                closeConnection();
            }
            return books[0];
        }
        public static void BorrowBook(int bookId, int studentId)
        {
            
            try
            {
                openConnection();
                string comm2 = "Insert Into Borrows values("+studentId+","+bookId+",'" + DateTime.Now + "',null)";
                SqlCommand myCommand = new SqlCommand(comm2, currConnection);
                myCommand.ExecuteNonQuery();

            }
            catch (Exception err)
            {

            }
            finally
            {
                closeConnection();
            }
           
        }
        public static void ReturnBook(int bookId, int studentId)
        {

            try
            {
                openConnection();
                string comm2 = "UPDATE Borrows SET Borrows.broughtDate = '"+DateTime.Now+"' WHERE Borrows.studentId ="+studentId+" and Borrows.bookId = "+bookId;
                SqlCommand myCommand = new SqlCommand(comm2, currConnection);
                myCommand.ExecuteNonQuery();

            }
            catch (Exception err)
            {

            }
            finally
            {
                closeConnection();
            }

        }
    }
}