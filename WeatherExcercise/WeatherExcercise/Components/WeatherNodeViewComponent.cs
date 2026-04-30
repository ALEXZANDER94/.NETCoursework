using Microsoft.AspNetCore.Mvc;
using WeatherExcercise.Models;
namespace WeatherExcercise.Components
{
    public class WeatherNodeViewComponent : ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync(CityWeather city)
        {
            return View(city);
        }
    }
}
