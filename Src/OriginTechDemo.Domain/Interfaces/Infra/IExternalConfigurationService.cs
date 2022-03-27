using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginTechDemo.Domain.Interfaces.Infra
{
    public interface IExternalConfigurationService
    {
        Task<bool> IsRuleActive(string ruleName);
    }
}
