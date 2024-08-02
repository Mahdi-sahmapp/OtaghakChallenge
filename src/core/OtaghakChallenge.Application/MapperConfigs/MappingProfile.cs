﻿using AutoMapper;
using OtaghakChallenge.Application.Features.Products.Query;
using OtaghakChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtaghakChallenge.Application.MapperConfigs
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
