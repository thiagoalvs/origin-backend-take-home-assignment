using OriginTechDemo.Domain.Interfaces.Infra;
using System;

namespace OriginTechDemo.Infra.Services
{
    public class ConsoleLoggingService : ILoggingService
    {
        public void Log(string content)
        {
            Console.WriteLine(DateTime.Now.ToString("hh:mm:ss dd/MM/yyyy") + ": " + content);
        }
    }
}
