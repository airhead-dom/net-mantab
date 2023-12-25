using AutoMapper;

namespace NetMantab.Models
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}