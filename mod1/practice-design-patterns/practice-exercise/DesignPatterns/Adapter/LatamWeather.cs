namespace DesignPatterns.Adapter
{
    public class LatamWeather   // Origen nuevo moderno
    {
        private int _celsiusDegrees;

        public LatamWeather(int celsiusDegrees)
        {
            _celsiusDegrees = celsiusDegrees;
        }

        public int GetCelsiusTemperature()
        {
            return _celsiusDegrees;
        }
    }
}
