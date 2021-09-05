using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using AK.Application.Commands;
using AK.Application.Commands.Drug;
using AK.Application.Commands.User;
using AK.Application.DTOs;
using AK.Application.Queries;
using AK.Application.Queries.Drug;
using AK.Application.Queries.User;
using AK.Domain.Models;

namespace AK.Application.Mappers
{
    public class AutoMapperExtensionProfile : Profile
    {
        public AutoMapperExtensionProfile()
        {
            CreateMap<User, UserDto>().ReverseMap().ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordByte));
            CreateMap<List<User>, List<UserDto>>().ReverseMap();
            CreateMap<User, UserRegisterCommand>().ReverseMap();
            CreateMap<UserDto, UserRegisterCommand>().ReverseMap();
            CreateMap<UserDto, GetUserByIdQuery>().ReverseMap();
            CreateMap<User, GetUserByIdQuery>().ReverseMap();
            CreateMap<UserDto, UserAddCommand>().ReverseMap();
            CreateMap<User, UserAddCommand>().ReverseMap();
            CreateMap<UserCommandBase, UserAddCommand>().ReverseMap();
            CreateMap<UserCommandBase, UserDto>().ReverseMap();
            //Map Drug entities
            CreateMap<List<Drug>, List<DrugDto>>().ReverseMap();
            CreateMap<Drug, DrugDto>().ReverseMap();
            CreateMap<Drug, DrugCreateCommand>().ReverseMap();
            CreateMap<Drug, DrugUpdateCommand>().ReverseMap();
            CreateMap<Drug, DrugDeleteCommand>().ReverseMap();
            CreateMap<Drug, DrugGetAllQuery>().ReverseMap();
            CreateMap<List<Drug>, List<DrugGetAllQuery>>().ReverseMap();
            CreateMap<Drug, DrugGetByIdQuery>().ReverseMap();
            CreateMap<DrugDto, DrugCreateCommand>().ReverseMap();
            CreateMap<DrugDto, DrugUpdateCommand>().ReverseMap();
            CreateMap<DrugDto, DrugDeleteCommand>().ReverseMap();
            CreateMap<DrugDto, DrugGetAllQuery>().ReverseMap();
            CreateMap<List<DrugDto>, List<DrugGetAllQuery>>().ReverseMap();
            CreateMap<DrugDto, DrugGetByIdQuery>().ReverseMap();
        }
    }
}
