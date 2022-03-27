using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Enums;
using OriginTechDemo.Domain.Interfaces;
using OriginTechDemo.Domain.Interfaces.Infra;
using System.Threading.Tasks;

namespace OriginTechDemo.Domain.ScoreRules.DisabilityRules
{
    public class IncomeIsZeroRule : BaseScoreRule, IScoreRule
    {
        public IncomeIsZeroRule(IExternalConfigurationService externalConfigurationService) : base(externalConfigurationService)
        {
            Name = this.GetType().Name;
            InsuranceLine = EIsuranceLine.Disability;
        }

        public override async Task<int?> Validate(UserInformation userInformation)
        {
            if (userInformation.Income <= 0)
                return null;

            return 0;
        }
    }
}
