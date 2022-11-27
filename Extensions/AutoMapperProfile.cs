

using AutoMapper;
using MongoDbExample.DTOs;
using MongoDbExample.Features.Users;
using MongoDbExample.Models;

namespace MongoDbExample.Extensions
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // CreateMap<ApplicationUser, UserDto>();
            CreateMap<RegisterRequestDTo, ApplicationUser>();
            CreateMap<ApplicationUser, RegisterRequestDTo>();
            CreateMap<StudentDTO, Student>();
            CreateMap<CourseDTO, Course>();
            // CreateMap<UpdateUserDto, ApplicationUserDao>();
        }

    }
}