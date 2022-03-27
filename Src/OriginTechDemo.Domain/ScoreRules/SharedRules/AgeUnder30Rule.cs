using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Enums;
using OriginTechDemo.Domain.Interfaces;
using OriginTechDemo.Domain.Interfaces.Infra;
using System.Threading.Tasks;

namespace OriginTechDemo.Domain.ScoreRules.SharedRules
{
    public class AgeUnder30Rule : BaseScoreRule, IScoreRule
    {
        public AgeUnder30Rule(IExternalConfigurationService externalConfigurationService) : base(externalConfigurationService)
        {
            Name = this.GetType().Name;
            InsuranceLine = EIsuranceLine.Shared;
        }

        public override async Task<int?> Validate(UserInformation userInformation)
        {
            if (userInformation.Age < 30)
                return -2;

            return 0;
        }
    }
}
