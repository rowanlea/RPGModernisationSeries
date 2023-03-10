﻿namespace RPGShop
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
            File.AppendAllText(_filePath, $"\n{message}");
        }
    }
}
