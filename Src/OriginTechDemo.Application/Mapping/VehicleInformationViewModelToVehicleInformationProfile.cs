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
    public class VehicleInformationViewModelToVehicleInformationProfile : Profile
    {
        public VehicleInformationViewModelToVehicleInformationProfile()
        {
            CreateMap<VehicleInformationViewModel, VehicleInformation>();
        }
    }
}
