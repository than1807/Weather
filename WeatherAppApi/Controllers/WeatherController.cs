using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherAppApi.Repostries;
using WeatherAppApi.Service;

namespace WeatherAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService _weatherService;
        private readonly WeatherRepository _weatherRepository;

        public WeatherController(WeatherService weatherService, WeatherRepository weatherRepository)
        {
            _weatherService = weatherService;
            _weatherRepository = weatherRepository;
        }

        [HttpGet("{location}")]
        public async Task<IActionResult> GetWeather(string location)
        {
            var weather = await _weatherService.GetWeatherAsync(location);
            await _weatherRepository.AddWeatherDataAsync(weather);
            return Ok(weather);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistoricalWeather([FromQuery] string location, [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            var weatherData = await _weatherRepository.GetWeatherDataAsync(location, fromDate, toDate);
            return Ok(weatherData);
        }
    }
}
