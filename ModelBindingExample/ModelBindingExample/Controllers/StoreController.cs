using Microsoft.AspNetCore.Mvc;
using ModelBindingExample.Models;

namespace ModelBindingExample.Controllers
{
    public class StoreController : Controller
    {
        [Route("bookstore/{bookid?}/{isloggedin?}")]
        public IActionResult Index([FromQuery]int? bookid, [FromRoute]bool? isloggedin, Book book)
        {
            if(bookid.HasValue == false)
            {
                return BadRequest();
            }

            return Content(book.ToString(), "text/plain");
        }
        [Route("store/books/{id}")]
        public IActionResult Books()
        {
            int id = Convert.ToInt32(Request.RouteValues["id"]);
            return Content("<h1>Book Store</h1>", "text/html");
        }

        [Route("bookstore")]
        public IActionResult BookStore()
        {
            if (!Request.Query.ContainsKey("bookid"))
            {
                return BadRequest("Book id is not supplied"); // Status Code Result
            }
            int bookid = Convert.ToInt32(Request.RouteValues["bookid"]);
            
            return LocalRedirect($"store/books/{bookid}"); // shortcut for LocalRedirectResult

        }
    }
}
