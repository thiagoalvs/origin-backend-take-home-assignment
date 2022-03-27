using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginTechDemo.Domain.Interfaces
{
    public interface ILifeScoreCalculator
    {
        public Task<EScore> Calculate(UserInformation userInformation);
    }
}
