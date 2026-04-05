using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using ViewsExercises.Enums;
using ViewsExercises.Models;

namespace ViewsExercises.Controllers
{
    public class HomeController : Controller
    {
        [Route("home")]
        [Route("/")]
        public IActionResult Index()
        {
            ViewData["appTitle"] = "Asp.Net Core Demo App";
            List<Person> people = new List<Person>()
            {
                new Person()
                {
                    Name = "John",
                    DateOfBirth = DateTime.Parse("2000-07-01"),
                    PersonGender = Gender.Male.ToString()
                },
                new Person()
                {
                    Name = "Linda",
                    DateOfBirth = DateTime.Parse("2005-01-19"),
                    PersonGender = Gender.Female.ToString()
                },
                new Person()
                {
                    Name = "Susan",
                    DateOfBirth = DateTime.Parse("2008-07-14"),
                    PersonGender = Gender.Other.ToString()
                }
            };
            return View(people); //Views/Home/index.cshtml
            //return View("abc"); //abc.cshtml
        }

        [Route("person-details/{name}")]
        public IActionResult Details(string? name)
        {
            if(name == null)
            {
                return Content("Person name can't be null");
            }
            List<Person> people = new List<Person>()
            {
                new Person()
                {
                    Name = "John",
                    DateOfBirth = DateTime.Parse("2000-07-01"),
                    PersonGender = Gender.Male.ToString()
                },
                new Person()
                {
                    Name = "Linda",
                    DateOfBirth = DateTime.Parse("2005-01-19"),
                    PersonGender = Gender.Female.ToString()
                },
                new Person()
                {
                    Name = "Susan",
                    DateOfBirth = DateTime.Parse("2008-07-14"),
                    PersonGender = Gender.Other.ToString()
                }
            };
            Person matchingPerson = people.Where(temp => temp.Name == name).FirstOrDefault();

            return View(matchingPerson); //Views/Home/Details.cshtml
        }

        [Route("person-with-product")]
        public IActionResult PersonWithProduct()
        {
            Person person = new Person()
            {
                Name = "Sara", PersonGender = Gender.Female.ToString(), DateOfBirth = Convert.ToDateTime("2004-07-01")
            };

            Product product = new Product() { ProductId = 1, ProductName = "Air Conditioner" };
            PersonAndProductWrapper wrapper = new PersonAndProductWrapper() { PersonData = person, ProductData = product };
            return View(wrapper);
        }
    }
}
