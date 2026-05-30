
namespace DesignPatterns.Adapter
{
    public class WeatherAdapter : Weather
    {
        private LatamWeather _latamWeather;
    
        public WeatherAdapter(LatamWeather latamWeather)
        {
            _latamWeather = latamWeather;
        }

        public override int GetFahrenheitDegrees()
        {
            int celsiusDegrees = _latamWeather.GetCelsiusTemperature();
            return (celsiusDegrees * 9 / 5) + 32;
        }
    }
}
