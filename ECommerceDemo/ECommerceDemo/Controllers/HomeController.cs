using ECommerceDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/order")]
        public IActionResult Order([FromForm]Order order)
        {
            if (!ModelState.IsValid)
            {
                string errors = string.Join("\n",
                    (ModelState.Values
                    .SelectMany(value => value.Errors)
                    .Select(err => err.ErrorMessage))
                );
                Response.StatusCode = 400;
                return BadRequest(errors);
            }
            Response.StatusCode = 200;
            return Json("{ 'orderNo' : 1233456 }");
        }
    }
}
