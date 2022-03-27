using Microsoft.Extensions.Caching.Memory;
using OriginTechDemo.Domain.Interfaces.Infra;
using System;
using System.Threading.Tasks;

namespace OriginTechDemo.Infra.Services
{
    public class ExternalConfigurationService : IExternalConfigurationService
    {
        private readonly IMemoryCache _memoryCache;

        public ExternalConfigurationService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public async Task<bool> IsRuleActive(string ruleName)
        {
            if (!_memoryCache.TryGetValue(ruleName, out bool result))
            {
                result = await Task.Run(() => true);

                _memoryCache.Set(ruleName, result, DateTime.Now.AddMinutes(30));
            }

            return result;
        }
    }
}
