using AutoMapper;
using JwtExample.Entities.Models;
using JwtExample.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtExample
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterRequest, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
        }
    }
}
