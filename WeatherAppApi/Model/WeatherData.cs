namespace WeatherAppApi.Model
{
    public class WeatherData
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public float Temperature { get; set; }
        public string WeatherDescription { get; set; }
        public string WeatherIcon { get; set; }
        public DateTime Date { get; set; }
    }
}
