using System;

namespace myapp.Services
{
    public class DatabaseLogger : ILogger
    {
        public void Log(string message)
        {
            // In a real application, you would write to a database here.
            Console.WriteLine($"Database Logger: {message}");
        }
    }
}
