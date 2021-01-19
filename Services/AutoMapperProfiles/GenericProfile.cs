using AutoMapper;
using DataLayer.Entities;
using Services.Dtos;

namespace Services.AutoMapperProfiles
{
    public class GenericProfile : Profile
    {
        public GenericProfile()
        {
            CreateMap<UserDto, User>().ForMember(d => d.UserName, s => s.MapFrom(x => x.Email));
            CreateMap<User, UserDto>();
        }
    }
}
