using AutoMapper;
using PizzaHome.API.ViewModels.Dtos;
using PizzaHome.Core.Entities;
using PizzaHome.ViewModels.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHome.ViewModels.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, LoginDto>();
            CreateMap<User, LoginDto>().ReverseMap();
        }
    }
}
