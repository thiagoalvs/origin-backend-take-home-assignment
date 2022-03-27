using FluentValidation.Results;
using OriginTechDemo.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OriginTechDemo.Application.ViewModels
{
    public class UserInformationViewModel
    {
        public int age { get; set; }

        public int dependents { get; set; }

        public HouseInformationViewModel house { get; set; }

        public int income { get; set; }

        public string marital_status { get; set; }

        public IList<int> risk_questions { get; set; }

        public VehicleInformationViewModel vehicle { get; set; }

        public async Task<ValidationResult> ValidateAsync() 
        {
            return await new UserInformationViewModelValidator().ValidateAsync(this);
        }
    }
}
