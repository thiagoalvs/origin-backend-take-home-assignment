using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Enums;
using OriginTechDemo.Domain.Interfaces;
using OriginTechDemo.Domain.Interfaces.Infra;
using System.Threading.Tasks;

namespace OriginTechDemo.Domain.ScoreRules.VehicleRules
{
    public class VehicleIsPartOfUsersBelongingsRule : BaseScoreRule, IScoreRule
    {
        public VehicleIsPartOfUsersBelongingsRule(IExternalConfigurationService externalConfigurationService) : base(externalConfigurationService)
        {
            Name = this.GetType().Name;
            InsuranceLine = EIsuranceLine.Vehicle;
        }

        public override async Task<int?> Validate(UserInformation userInformation)
        {
            if (userInformation.Vehicle == null)
                return null;

            return 0;
        }
    }
}
