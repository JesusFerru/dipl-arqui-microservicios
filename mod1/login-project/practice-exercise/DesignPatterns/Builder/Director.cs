
namespace DesignPatterns.Builder
{
    public class Director
    {
        private readonly IBuilder _builder;

        public Director(IBuilder builder)
        {
            _builder = builder;
        }

        public IBuilder BuildMinimalReport()
        {
            _builder.Reset();
            _builder.SetTitle("MinimalReport");
            _builder.SetAuthor("John Doe");
      //      _builder.SetCreationDate(DateTime.Now());
            return _builder;

        }
        public IBuilder BuildFulllReport()
        {
            _builder.Reset();
            _builder.SetTitle("MinimalReport");
            _builder.SetAuthor("John Doe");
            _builder.SetContent("Lorem Ipsum Ad hominem Stat");
            _builder.SetFooter("Copyright 2026");
         //   _builder.SetCreationDate(DateTime.UtcNow());
            _builder.DownloadAudioReport();
            return _builder;

        }

        // En lugar de devolver el builder en cada metodo, se puede crear un metodo para obtenerlo.
        /// public IBuilder GetBuilder();
        /// {
        /// return _builder;
        /// }
    }
}
