using FluentValidation;
using OriginTechDemo.Application.ViewModels;
using OriginTechDemo.Domain.Enums;
using OriginTechDemo.Shared.Helpers;

namespace OriginTechDemo.Domain.Validators
{
    public class HouseInformationViewModelValidator : AbstractValidator<HouseInformationViewModel>
    {
        public HouseInformationViewModelValidator()
        {
            RuleFor(houseInfo => houseInfo.ownership_status)
                .Must((value) => EnumHelper.GetNames(typeof(EOwnershipStatus)).Contains(value.ToLower()))
                .WithMessage((userInfo) => $"The value is invalid. Possible options are: {EnumHelper.StringfyNames(typeof(EOwnershipStatus))}");
        }
    }
}
