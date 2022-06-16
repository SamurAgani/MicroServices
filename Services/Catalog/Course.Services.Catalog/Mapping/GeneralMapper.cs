using AutoMapper;
using Course.Services.Catalog.Dtos;
using Course.Services.Catalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Services.Catalog.Mapping
{
    public class GeneralMapper : Profile
    {
        public GeneralMapper()
        {
            CreateMap<Models.Course, CourseDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();
            CreateMap<Models.Course, CourseCreateDto>().ReverseMap();
            CreateMap<Models.Course, CourseUpdateDto>().ReverseMap();
        }
    }
}
