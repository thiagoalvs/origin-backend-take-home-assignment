using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Enums;
using OriginTechDemo.Domain.Interfaces;
using OriginTechDemo.Domain.Interfaces.Infra;
using System.Threading.Tasks;

namespace OriginTechDemo.Domain.ScoreRules.SharedRules
{
    public class AgeBetween30and40Rule : BaseScoreRule, IScoreRule
    {
        public AgeBetween30and40Rule(IExternalConfigurationService externalConfigurationService) : base(externalConfigurationService)
        {
            Name = this.GetType().Name;
            InsuranceLine = EIsuranceLine.Shared;
        }

        public override async Task<int?> Validate(UserInformation userInformation)
        {
            if (userInformation.Age >= 30 && userInformation.Age <= 40)
                return -1;

            return 0;
        }
    }
}
