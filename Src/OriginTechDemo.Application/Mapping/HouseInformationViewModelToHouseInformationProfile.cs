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
    public class HouseInformationViewModelToHouseInformationProfile : Profile
    {
        public HouseInformationViewModelToHouseInformationProfile()
        {
            CreateMap<HouseInformationViewModel, HouseInformation>()
                .ForMember(dest => dest.OwnershipStatus, opt => opt.MapFrom((origin, dest) =>
                {
                    if (Enum.TryParse(typeof(EOwnershipStatus), origin.ownership_status, true, out object ownershipStatus))
                        return (EOwnershipStatus)ownershipStatus;

                    return EOwnershipStatus.None;
                }));
        }
    }
}
