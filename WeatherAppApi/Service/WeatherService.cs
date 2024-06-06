using Newtonsoft.Json.Linq;
using WeatherAppApi.Model;

namespace WeatherAppApi.Service
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public WeatherService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["OpenWeatherMapApiKey"];
        }

        public async Task<WeatherData> GetWeatherAsync(string location)
        {
            var response = await _httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={location}&appid={_apiKey}&units=metric");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var weatherJson = JObject.Parse(content);

                return new WeatherData
                {
                    Location = location,
                    Temperature = weatherJson["main"]["temp"].Value<float>(),
                    WeatherDescription = weatherJson["weather"][0]["description"].Value<string>(),
                    WeatherIcon = weatherJson["weather"][0]["icon"].Value<string>(),
                    Date = DateTime.Now
                };
            }
            throw new Exception("Unable to fetch weather data.");
        }
    }
}
