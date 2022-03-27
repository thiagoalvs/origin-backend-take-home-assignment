using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Enums;
using OriginTechDemo.Domain.Interfaces;
using OriginTechDemo.Domain.Interfaces.Infra;
using System.Threading.Tasks;

namespace OriginTechDemo.Domain.ScoreRules.DisabilityRules
{
    public class AgeOver60Rule : BaseScoreRule, IScoreRule
    {
        public AgeOver60Rule(IExternalConfigurationService externalConfigurationService) : base(externalConfigurationService)
        {
            Name = this.GetType().Name;
            InsuranceLine = EIsuranceLine.Disability;
        }

        public override async Task<int?> Validate(UserInformation userInformation)
        {
            if (userInformation.Age > 60)
                return null;

            return 0;
        }
    }
}
