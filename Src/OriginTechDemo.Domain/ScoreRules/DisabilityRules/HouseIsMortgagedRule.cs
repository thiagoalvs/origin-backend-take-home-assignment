using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Enums;
using OriginTechDemo.Domain.Interfaces;
using OriginTechDemo.Domain.Interfaces.Infra;
using System;
using System.Threading.Tasks;

namespace OriginTechDemo.Domain.ScoreRules.DisabilityRules
{
    public class HouseIsMortgagedRule : BaseScoreRule, IScoreRule
    {
        public HouseIsMortgagedRule(IExternalConfigurationService externalConfigurationService) : base(externalConfigurationService)
        {
            Name = this.GetType().Name;
            InsuranceLine = EIsuranceLine.Disability;
        }

        public override async Task<int?> Validate(UserInformation userInformation)
        {
            if (userInformation.House == null)
                return 0;

            if (userInformation.House.OwnershipStatus == EOwnershipStatus.None)
                throw new Exception("OwnershipStatus could not be checked. Please provide a valid value");

            if (userInformation.House.OwnershipStatus == EOwnershipStatus.Mortgaged)
                return 1;

            return 0;
        }
    }
}
