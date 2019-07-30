using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using D6.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Http;
using eT3.LibraryApplication.Models;
using eT3.LibraryApplication.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace D6.Controllers
{
    [Authorize]  // To Start form Login Page
    public class HomeController : Controller
    {
        private readonly MyContext Context ;
        UserManager<ApplicationUser> UserManagers;
        public HomeController(MyContext _Context , UserManager<ApplicationUser> usrmgr)
        {
            Context = _Context;
            UserManagers = usrmgr;

        }
        // GET: 
        public IActionResult Index()
        {           

            ViewBag.catgories = Context.Categories.ToList();
            ViewBag.books = Context.Books.ToList();

            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Details(int book_id)
        {
            ViewBag.book = Context.Books.SingleOrDefault(p=>p.ID == book_id);
            ViewBag.catgories = Context.Categories.ToList();

            return View("Details");
        }

        public async Task<IActionResult> BorrowBook(int Book_id )
        {
      if (await Context.Borrowers.AnyAsync(p => p.Book_ID == Book_id && p.User_ID == UserManagers.GetUserId(User)))
         {
                TempData["Message"] = "BorrowedBefore";
                string aka = TempData["Message"] as string;
               return View(aka);
               // return JavaScript("Callback()");
                //return Content("<script language='javascript' type='text/javascript'>alert('Thanks for Feedback!');</script>");
            }
            Borrower_Details borrower;
              if (ModelState.IsValid)
            {
                if (Context.Books.SingleOrDefault(p => p.ID == Book_id).No_Of_Copies_Current > 0)
                    Context.Books.SingleOrDefault(p => p.ID == Book_id).No_Of_Copies_Current--;
                else
                    return NotFound();
                borrower  = new Borrower_Details() {
                    Book_ID = Book_id,
                    User_ID = UserManagers.GetUserId(User) ,
                    Borrowed_From_Date = DateTime.Today ,
                    Borrowed_To_Date = DateTime.Now.AddDays(10) ,
                    Actual_Return_date = DateTime.MinValue };

           
                if (borrower != null)
                {
                Context.Borrowers.Add(borrower);
                await Context.SaveChangesAsync();
                }

            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ReturnBook(int Book_id)
        {
            if (ModelState.IsValid)
            {
                var current = Context.Books.SingleOrDefault(p => p.ID == Book_id).No_Of_Copies_Current;
                var actual = Context.Books.SingleOrDefault(p => p.ID == Book_id).No_Of_Copies_Actual;
                if ( current < actual )
                    Context.Books.SingleOrDefault(p => p.ID == Book_id).No_Of_Copies_Current++;
                else
                    return NotFound();
                
        Context.Borrowers.SingleOrDefault(p => p.Book_ID == Book_id && p.User_ID == UserManagers.GetUserId(User))
                    .Actual_Return_date = DateTime.Now;
                                
                Context.SaveChanges();
                

            }
            return RedirectToAction(nameof(BorrowedBooks));
        }
        public IActionResult BorrowedBooks()
        {

            ViewBag.BooksBorrowed = (from o in Context.Borrowers
                                     join d in Context.Books
                                     on o.Book_ID equals d.ID
                                     where o.User_ID == UserManagers.GetUserId(User) 
                                     && ( o.Actual_Return_date == DateTime.MinValue 
                                     || o.Actual_Return_date >= DateTime.Now )
                                      select d).ToList();


            return View("Borrowed");
        }
        public IActionResult BooksOfCategriey(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var prods = Context.Books.Where(p => p.Category_Type == id).ToList();

            ViewBag.BookCat = prods;

            return View("BooksofCategory");
        }
 



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
