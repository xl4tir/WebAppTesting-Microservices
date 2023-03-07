using Microsoft.AspNetCore.Http.HttpResults;
using TestingCompleteService.Dtos;
using TestingCompleteService.Models;
using AutoMapper;
using WebAppTesting_cyber;

namespace TestingCompleteService.Profiles;

public class TestingCompleteProfile : Profile
{
    public TestingCompleteProfile()
    {
        //Soure --->>> Target

        CreateMap<Testing, TestingReadDto>();
        CreateMap<TestingCompleteCreateDto, TestingComplete>();
        CreateMap<TestingComplete, TestingCompleteReadDto>();
        CreateMap<TestingPublishedDto, Testing>()
            .ForMember(dest => dest.ExternalID,
                opt => opt.MapFrom(src => src.Id));
        CreateMap<GrpcTestingModel, Testing>()
            .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.TestingId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));


    }
    
}