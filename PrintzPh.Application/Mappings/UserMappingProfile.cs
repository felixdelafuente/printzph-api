using System;
using AutoMapper;
using PrintzPh.Application.DTOs;
using PrintzPh.Domain.Entities;

namespace PrintzPh.Application.Mappings;

public class UserMappingProfile : Profile
{
  public UserMappingProfile()
  {
    CreateMap<User, UserDto>()
        .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
  }
}
