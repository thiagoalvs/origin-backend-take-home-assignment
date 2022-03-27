using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Enums;
using OriginTechDemo.Domain.Interfaces;
using OriginTechDemo.Domain.Interfaces.Infra;
using System;
using System.Threading.Tasks;

namespace OriginTechDemo.Domain.ScoreRules.VehicleRules
{
    public class VehicleIsAtLeastFiveYearsOldRule : BaseScoreRule, IScoreRule
    {
        public VehicleIsAtLeastFiveYearsOldRule(IExternalConfigurationService externalConfigurationService) : base(externalConfigurationService)
        {
            Name = this.GetType().Name;
            InsuranceLine = EIsuranceLine.Vehicle;
        }

        public override async Task<int?> Validate(UserInformation userInformation)
        {
            if (userInformation.Vehicle == null)
                return 0;

            if (userInformation.Vehicle.Year >= DateTime.Now.AddYears(-5).Year)
                return 1;

            return 0;
        }
    }
}
