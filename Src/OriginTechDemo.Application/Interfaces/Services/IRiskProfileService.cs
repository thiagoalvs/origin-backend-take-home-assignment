using OriginTechDemo.Application.Models;
using OriginTechDemo.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginTechDemo.Application.Interfaces.Services
{
    public interface IRiskProfileService
    {
        Task<GenericResult> CalculateRiskProfile(UserInformationViewModel vm);
    }
}
