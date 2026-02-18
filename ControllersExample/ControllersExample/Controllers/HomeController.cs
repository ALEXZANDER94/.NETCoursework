using Microsoft.AspNetCore.Mvc;
using ControllersExample.Models;

namespace ControllersExample.Controllers
{
    public class HomeController : Controller
    {
        [Route("home")]
        [Route("/")]
        public ContentResult Index()
        {
            //return new ContentResult()
            //{
            //    Content = "Hello from Index",
            //    ContentType = "text/plain"
            //};

            //return Content("Hello from Index", "text/plain");

            return Content("<h1>Welcome</h1><h2>Hello from Index</h2>", "text/html");
        }

        [Route("about")]
        public string About()
        {
            return "Hello From About!";
        }

        [Route("contact-us")]
        public string Contact()
        {
            return "Hello From Contact!";
        }

        [Route("person")]
        public JsonResult Person()
        {
            Person person = new Person()
            {
                Id = Guid.NewGuid(),
                FirstName = "David",
                LastName = "Smith",
                Age = 25
            };
            //return new JsonResult(person);

            return Json(person);
        }

        /* respond with a Virtual FileResult */
        [Route("file-download")]
        public VirtualFileResult FileDownload()
        {
            //return new VirtualFileResult("sample.pdf", "application.pdf");
            return File("sample.pdf", "application.pdf");
        }

        /* respond with a PhysicalFileResult */
        [Route("file-download2")]
        public PhysicalFileResult FileDownload2()
        {
            //return new PhysicalFileResult(@"c:\Downloads\sample.pdf", "application.pdf");
            return PhysicalFile(@"c:\Downloads\sample.pdf", "application.pdf");
        }


        /* respond with a FileContentResult ( byte array )*/
        [Route("file-download3")]
        public FileContentResult FileDownload3()
        {
            byte[] bytes = System.IO.File.ReadAllBytes(@"c:\Downloads\sample.pdf");
            //return new FileContentResult(bytes, "application/pdf");
            return File(bytes, "application/pdf");
        }

        /* respond as an IActionResult, will allow any child of IActionResult as a return type */
        [Route("file-download4")]
        public IActionResult FileDownload4()
        {
            byte[] bytes = System.IO.File.ReadAllBytes(@"c:\Downloads\sample.pdf");
            return File(bytes, "application/pdf");
        }

        /* respond as a Redirect request */
        [Route("bookstore")]
        public IActionResult BookStore()
        {
            if(!Request.Query.ContainsKey("bookid"))
            {
                return BadRequest("Book id is not supplied"); // Status Code Result
            }
            int bookid = Convert.ToInt32(Request.RouteValues["bookid"]);
            //return new RedirectToActionResult("Books", "Store", new { }); //302 - Found
            //return new RedirectToActionResult("Books", "Store", new { }, true); //301 - Moved Permanently

            //return RedirectToAction("Books", "Store", new { id = bookid }); // shortcut for 302 Redirect
            //return RedirectToActionPermanent("Books", "Store", new { id = bookid }); // shortcut for 302 Redirect

            /* LocalRedirect is for redirecting using a relative url */
            /* LocalRedirect CANNOT redirect to another domain */
            //return new LocalRedirectResult($"store/books/{bookid}");
            return LocalRedirect($"store/books/{bookid}"); // shortcut for LocalRedirectResult

        }
    }
}
