using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginTechDemo.Domain.Interfaces.Infra
{
    public interface ILoggingService
    {
        public void Log(string content);
    }
}
