namespace DesignPatterns.Adapter
{
    public class Weather        // Destino, legacy
    {
        public int _fahrenheitDegrees { get; set; }

        public Weather()
        {
            _fahrenheitDegrees = 0;
        }

        public Weather(int fahrenheitDegrees)
        {
            _fahrenheitDegrees = fahrenheitDegrees;
        }

        public virtual int GetFahrenheitDegrees() { 
            return _fahrenheitDegrees;
        }
    }
}
