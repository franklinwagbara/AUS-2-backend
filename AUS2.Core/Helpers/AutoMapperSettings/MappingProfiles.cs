using AUS2.Core.DBObjects;
using AUS2.Core.ViewModels.Dto.Request;
using AUS2.Core.ViewModels.Dto.Response;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AUS2.Core.Helper.AutoMapperSettings
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<ExtraPayment, ExtraPaymentRequestDto>().ReverseMap();
            CreateMap<OutOfOffice, OutOfOfficeRequestDto>().ReverseMap();
            CreateMap<ApplicationUser, UserMasterRequestDto>().ReverseMap();
            CreateMap<Phase, PhaseDto>().ReverseMap();
            CreateMap<Application, AppRequestViewModel>().ReverseMap();
            CreateMap<ApplicationForm, ApplicationFormDto>().ReverseMap();
            CreateMap<Application, AppRespnseDto>()
                .ForMember(x => x.CategoryCode, y => y.MapFrom(z => z.Phase.Category.Code))
                .ReverseMap();
            CreateMap<Category, ApplicationModulesRequestDTO>().ReverseMap();
            CreateMap<State, StateDto>().ReverseMap();
            CreateMap<FieldOffice, FieldOfficeDto>()
                .ForMember(x => x.StateName, y => y.MapFrom(z => z.State.StateName))
                .ReverseMap();
        }
    }
}
