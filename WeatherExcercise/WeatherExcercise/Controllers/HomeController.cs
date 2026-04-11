using Microsoft.AspNetCore.Mvc;
using WeatherExcercise.Models;

namespace WeatherExcercise.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            ViewData["Title"] = "Weather Exercise";
            List<CityWeather> cities = new List<CityWeather>()
            {
                new CityWeather()
                {
                    CityUniqueCode = "LDN", CityName = "London", DateAndTime = DateTime.Parse("2030-01-01 8:00"), TemperatureFahrenheit = 33
                },
                new CityWeather()
                {
                    CityUniqueCode = "NYC", CityName = "New York", DateAndTime = DateTime.Parse("2030-01-01 3:00"), TemperatureFahrenheit = 60
                },
                new CityWeather()
                {
                    CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = DateTime.Parse("2030-01-01 9:00"), TemperatureFahrenheit = 82
                },
            };
            return View(cities);
        }

        [Route("/weather/{cityCode}")]
        public IActionResult Details(string? cityCode)
        {
            ViewData["Title"] = "Weather Exercise";
            if (cityCode == null)
            {
                return Content("cityCode cannot be null");
            }
            List<CityWeather> cities = new List<CityWeather>()
            {
                new CityWeather()
                {
                    CityUniqueCode = "LDN", CityName = "London", DateAndTime = DateTime.Parse("2030-01-01 8:00"), TemperatureFahrenheit = 33
                },
                new CityWeather()
                {
                    CityUniqueCode = "NYC", CityName = "New York", DateAndTime = DateTime.Parse("2030-01-01 3:00"), TemperatureFahrenheit = 60
                },
                new CityWeather()
                {
                    CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = DateTime.Parse("2030-01-01 9:00"), TemperatureFahrenheit = 82
                },
            };

            CityWeather? city = cities.Where(c => c.CityUniqueCode == cityCode).FirstOrDefault();
            if(city == null)
            {
                return BadRequest("Invalid City Code provided");
            }
            return View(city);
        }
    }
}
