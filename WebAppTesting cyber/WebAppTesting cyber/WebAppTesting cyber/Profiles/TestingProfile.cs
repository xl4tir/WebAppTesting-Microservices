using AutoMapper;
using WebAppTesting_cyber.Dtos;
using WebAppTesting_cyber.Models;

namespace WebAppTesting_cyber.Profiles;

public class TestingProfile : Profile
{
    public TestingProfile()
    {
      //Source -> Target
      CreateMap<Testing, TestingReadDto>();
      CreateMap<TestingCreateDto, Testing>();
      CreateMap<TestingReadDto, TestingPublishedDto>();
      CreateMap<Testing, GrpcTestingModel>()
          .ForMember(dest => dest.TestingId, opt => opt.MapFrom(src => src.Id))
          .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

    }
    
}