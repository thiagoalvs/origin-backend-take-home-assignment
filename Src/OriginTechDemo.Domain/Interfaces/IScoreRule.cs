using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Enums;
using OriginTechDemo.Domain.Interfaces.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginTechDemo.Domain.Interfaces
{
    public interface IScoreRule
    {
        public string Name { get; set; }

        public EIsuranceLine InsuranceLine { get; set; }

        public Task<bool> IsActive();

        public Task<int?> Validate(UserInformation userInformation);

        public Task SetExternalConfigurationService(IExternalConfigurationService service);
    }
}
