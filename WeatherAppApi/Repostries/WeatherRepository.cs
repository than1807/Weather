using Npgsql;
using WeatherAppApi.Model;
using Dapper;

namespace WeatherAppApi.Repostries
{
    public class WeatherRepository
    {
        private readonly string _connectionString;

        public WeatherRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<WeatherData>> GetWeatherDataAsync(string location, DateTime fromDate, DateTime toDate)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var query = "SELECT * FROM WeatherData WHERE Location = @Location AND Date BETWEEN @FromDate AND @ToDate";
                return await connection.QueryAsync<WeatherData>(query, new { Location = location, FromDate = fromDate, ToDate = toDate });
            }
        }

        public async Task AddWeatherDataAsync(WeatherData weatherData)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var query = "INSERT INTO WeatherData (Location, Temperature, WeatherDescription, WeatherIcon, Date) VALUES (@Location, @Temperature, @WeatherDescription, @WeatherIcon, @Date)";
                await connection.ExecuteAsync(query, weatherData);
            }
        }
    }
}
