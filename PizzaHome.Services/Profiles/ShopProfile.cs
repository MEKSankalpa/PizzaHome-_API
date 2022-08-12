using AutoMapper;
using PizzaHome.Models;
using PizzaHome.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHome.Services.Profiles
{
    public class ShopProfile : Profile
    {
        public ShopProfile()
        {
            CreateMap<Shop, ShopDto>()
                .ForMember(des => des.Address,
                 option => option.MapFrom(src => $"{src.AddressNo}, {src.Street}, {src.City}"));
                
        }
    }
}
