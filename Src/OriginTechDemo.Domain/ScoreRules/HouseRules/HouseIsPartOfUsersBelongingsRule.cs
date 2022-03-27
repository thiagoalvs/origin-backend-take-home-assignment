using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Enums;
using OriginTechDemo.Domain.Interfaces;
using OriginTechDemo.Domain.Interfaces.Infra;
using System.Threading.Tasks;

namespace OriginTechDemo.Domain.ScoreRules.HouseRules
{
    public class HouseIsPartOfUsersBelongingsRule : BaseScoreRule, IScoreRule
    {
        public HouseIsPartOfUsersBelongingsRule(IExternalConfigurationService externalConfigurationService) : base(externalConfigurationService)
        {
            Name = this.GetType().Name;
            InsuranceLine = EIsuranceLine.House;
        }

        public override async Task<int?> Validate(UserInformation userInformation)
        {
            if (userInformation.House == null)
                return null;

            return 0;
        }
    }
}
