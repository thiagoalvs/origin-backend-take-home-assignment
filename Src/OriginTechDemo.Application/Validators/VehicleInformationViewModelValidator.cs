using FluentValidation;
using FluentValidation.Results;
using OriginTechDemo.Application.ViewModels;
using System;

namespace OriginTechDemo.Domain.Validators
{
    public class VehicleInformationViewModelValidator : AbstractValidator<VehicleInformationViewModel>
    {
        public VehicleInformationViewModelValidator()
        {
            RuleFor(vehicleInfo => vehicleInfo.year)
                .NotNull().WithMessage("This is a required field")
                .Custom((value, context) =>
                {
                    if (value < 1885)
                        context.AddFailure(new ValidationFailure(context.PropertyName, "There were no vehicles before 1885"));

                    if (value > DateTime.Now.AddYears(1).Year)
                        context.AddFailure(new ValidationFailure(context.PropertyName, $"Models from the year {value} are no available yet"));
                });
        }
    }
}
