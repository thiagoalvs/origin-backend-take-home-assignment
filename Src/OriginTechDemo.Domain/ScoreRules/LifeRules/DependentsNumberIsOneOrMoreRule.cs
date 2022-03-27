using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Enums;
using OriginTechDemo.Domain.Interfaces;
using OriginTechDemo.Domain.Interfaces.Infra;
using System.Threading.Tasks;

namespace OriginTechDemo.Domain.ScoreRules.LifeRules
{
    public class DependentsNumberIsOneOrMoreRule : BaseScoreRule, IScoreRule
    {
        public DependentsNumberIsOneOrMoreRule(IExternalConfigurationService externalConfigurationService) : base(externalConfigurationService)
        {
            Name = this.GetType().Name;
            InsuranceLine = EIsuranceLine.Life;
        }

        public override async Task<int?> Validate(UserInformation userInformation)
        {
            if (userInformation.Dependents > 0)
                return 1;

            return 0;
        }
    }
}
