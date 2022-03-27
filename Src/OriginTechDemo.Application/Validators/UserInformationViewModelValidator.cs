using FluentValidation;
using OriginTechDemo.Application.ViewModels;
using OriginTechDemo.Domain.Enums;
using OriginTechDemo.Shared.Helpers;

namespace OriginTechDemo.Domain.Validators
{
    public class UserInformationViewModelValidator : AbstractValidator<UserInformationViewModel>
    {
        public UserInformationViewModelValidator()
        {
            RuleFor(userInfo => userInfo.age)
                .NotNull().WithMessage("This is a required field")
                .GreaterThanOrEqualTo(0).WithMessage("This field cannot be less than 0");

            RuleFor(userInfo => userInfo.dependents)
                .NotNull().WithMessage("This is a required field")
                .GreaterThanOrEqualTo(0).WithMessage("This field cannot be less than 0");

            RuleFor(userInfo => userInfo.income)
                .NotNull().WithMessage("This is a required field")
                .GreaterThanOrEqualTo(0).WithMessage("This field cannot be less than 0");

            RuleFor(userInfo => userInfo.marital_status)
                .Must((value) => EnumHelper.GetNames(typeof(EMaritalStatus)).Contains(value.ToLower()))
                .WithMessage((userInfo) => $"The value is invalid. Possible options are: {EnumHelper.StringfyNames(typeof(EMaritalStatus))}");

            RuleFor(userInfo => userInfo.risk_questions)
                .Must(x => x != null && x.Count == 3).WithMessage("The value is not valid. Expecting 3 answers.");

            When(userInfo => userInfo.house != null, () =>
            {
                RuleFor(userInfo => userInfo.house).SetValidator(new HouseInformationViewModelValidator());
            });

            When(userInfo => userInfo.vehicle != null, () =>
            {
                RuleFor(userInfo => userInfo.vehicle).SetValidator(new VehicleInformationViewModelValidator());
            });
        }
    }
}
