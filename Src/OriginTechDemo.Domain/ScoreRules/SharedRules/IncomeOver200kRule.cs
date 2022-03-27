using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Enums;
using OriginTechDemo.Domain.Interfaces;
using OriginTechDemo.Domain.Interfaces.Infra;
using System.Threading.Tasks;

namespace OriginTechDemo.Domain.ScoreRules.SharedRules
{
    public class IncomeOver200kRule : BaseScoreRule, IScoreRule
    {
        public IncomeOver200kRule(IExternalConfigurationService externalConfigurationService) : base(externalConfigurationService)
        {
            Name = this.GetType().Name;
            InsuranceLine = EIsuranceLine.Shared;
        }

        public override async Task<int?> Validate(UserInformation userInformation)
        {
            if (userInformation.Income > 200_000)
                return -1;

            return 0;
        }
    }
}
