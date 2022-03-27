using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Enums;
using OriginTechDemo.Domain.Interfaces;
using OriginTechDemo.Domain.Interfaces.Infra;
using System;
using System.Threading.Tasks;

namespace OriginTechDemo.Domain.ScoreRules.DisabilityRules
{
    public class MaritalStatusIsMarriedRule : BaseScoreRule, IScoreRule
    {
        public MaritalStatusIsMarriedRule(IExternalConfigurationService externalConfigurationService) : base(externalConfigurationService)
        {
            Name = this.GetType().Name;
            InsuranceLine = EIsuranceLine.Disability;
        }

        public override async Task<int?> Validate(UserInformation userInformation)
        {
            if (userInformation.MaritalStatus == EMaritalStatus.None)
                throw new Exception("MaritalStatus could not be checked. Please provide a valid value");

            if (userInformation.MaritalStatus == EMaritalStatus.Married)
                return -1;

            return 0;
        }
    }
}
