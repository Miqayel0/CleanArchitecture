using AutoMapper;
using CleanArch.Application.Dtos;
using CleanArch.Domain.Entities;

namespace CleanArch.Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Course, CourseDto>();         
        }
    }
}
