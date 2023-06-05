namespace RPGShop.Logging
{
    public class FileLogger
    {
        private readonly string _filePath;

        public FileLogger(string filePath)
        {
            _filePath = filePath;
        }
        public void Log(string message)
        {
            try
            {
                File.AppendAllText(_filePath, $"\n{message}");
            }
            catch { } // We will use a better logging system later on when we add Serilog.
        }
    }
}
