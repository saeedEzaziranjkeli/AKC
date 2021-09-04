using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using AK.Application.Commands;
using AK.Application.Commands.User;
using AK.Application.DTOs;
using AK.Application.Queries;
using AK.Domain.Models;

namespace AK.Application.Mappers
{
    public class AutoMapperExtensionProfile : Profile
    {
        public AutoMapperExtensionProfile()
        {
            CreateMap<User, UserDto>().ReverseMap().ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordByte));
            CreateMap<List<User>, List<UserDto>>().ReverseMap();
            CreateMap<User, UserRegisterCommand>().ReverseMap();//.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Addresse));
            CreateMap<UserDto, UserRegisterCommand>().ReverseMap();//.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Addresse));
            CreateMap<UserDto, GetUserByIdQuery>().ReverseMap();//.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Addresse));
            CreateMap<User, GetUserByIdQuery>().ReverseMap();//.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Addresse));
            CreateMap<UserDto, UserAddCommand>().ReverseMap();//.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Addresse));
            CreateMap<User, UserAddCommand>().ReverseMap();//.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Addresse));
            CreateMap<UserCommandBase, UserAddCommand>().ReverseMap();//.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Addresse));
            CreateMap<UserCommandBase, UserDto>().ReverseMap();//.ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Addresse));
        }
    }
}
