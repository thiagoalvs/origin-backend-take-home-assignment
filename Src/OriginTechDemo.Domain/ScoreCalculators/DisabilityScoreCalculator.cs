using OriginTechDemo.Domain.Enums;
using OriginTechDemo.Domain.Interfaces;
using OriginTechDemo.Domain.Interfaces.Infra;
using System.Collections.Generic;

namespace OriginTechDemo.Domain.ScoreCalculators
{
    public class DisabilityScoreCalculator : BaseScoreCalculator, IDisabilityScoreCalculator
    {
        public DisabilityScoreCalculator(IEnumerable<IScoreRule> rules, ILoggingService loggingService) : base(EIsuranceLine.Disability, rules, loggingService)
        {

        }
    }
}
