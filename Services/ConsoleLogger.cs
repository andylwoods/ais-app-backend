using System;

namespace myapp.Services
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"Console Logger: {message}");
        }
    }
}
