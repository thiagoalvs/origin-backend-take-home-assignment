using AutoMapper;
using OriginTechDemo.Application.ViewModels;
using OriginTechDemo.Domain.Entities;
using OriginTechDemo.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginTechDemo.Application.Mapping
{
    public class UserInformationViewModelToUserInformationProfile : Profile
    {
        public UserInformationViewModelToUserInformationProfile()
        {            
            CreateMap<UserInformationViewModel, UserInformation>()
                .ForMember(dest => dest.RiskQuestions, opt => opt.MapFrom((origin, dest) =>
                {
                    if (origin.risk_questions == null)
                        return new List<int>();

                    return origin.risk_questions;
                }))
                .ForMember(dest => dest.MaritalStatus, opt => opt.MapFrom((origin, dest) =>
                {
                    if (Enum.TryParse(typeof(EMaritalStatus), origin.marital_status, true, out object maritalStatus))
                        return (EMaritalStatus)maritalStatus;

                    return EMaritalStatus.None;
                }));
        }
    }
}
