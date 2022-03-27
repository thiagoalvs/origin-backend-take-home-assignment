using OriginTechDemo.Domain.Enums;
using OriginTechDemo.Domain.Interfaces;
using OriginTechDemo.Domain.Interfaces.Infra;
using System.Collections.Generic;

namespace OriginTechDemo.Domain.ScoreCalculators
{
    public class HouseScoreCalculator : BaseScoreCalculator, IHouseScoreCalculator
    {
        public HouseScoreCalculator(IEnumerable<IScoreRule> rules, ILoggingService loggingService) : base(EIsuranceLine.House, rules, loggingService)
        {
            
        }
    }
}
