using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Enums;
using OriginTechDemo.Domain.Interfaces;
using OriginTechDemo.Domain.Interfaces.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginTechDemo.Domain.ScoreRules
{
    public abstract class BaseScoreRule : IScoreRule
    {
        private IExternalConfigurationService _externalConfigurationService;

        public string Id { get { return InsuranceLine.ToString() + Name; } }

        public string Name { get; set; }

        public EIsuranceLine InsuranceLine { get; set; }

        public BaseScoreRule(IExternalConfigurationService externalConfigurationService)
        {
            _externalConfigurationService = externalConfigurationService;
        }

        public async Task<bool> IsActive()
        {
            return await _externalConfigurationService.IsRuleActive(Id);
        }

        public virtual Task<int?> Validate(UserInformation userInformation)
        {
            throw new NotImplementedException();
        }

        public async Task SetExternalConfigurationService(IExternalConfigurationService externalConfiguration)
        {
            _externalConfigurationService = externalConfiguration;
        }
    }
}
