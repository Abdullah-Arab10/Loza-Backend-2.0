using AutoMapper;
using Identity.Provider.DTOs;
using IdentityProvider.Entities;

namespace IdentityProvider.Mappers
{
    public class UserMappers : Profile
    {
        public UserMappers()
        {
            CreateMap<IdentityUserModel, UserDTO>().ReverseMap();

        }
    }
}
