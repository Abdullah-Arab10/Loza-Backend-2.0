using AutoMapper;
using Identity.Provider.DTOs;
using Loza.API.Contracts.Shared.Requests;
using Loza.API.Contracts.UserProfiles.Requests;
using Loza.API.Contracts.UserProfiles.Responses;
using Loza.Application.Features.Auths.Commands;
using Loza.Application.Features.UserProfiles.Commands;
using Loza.Application.Models.SharedModels;
using Loza.Application.Models.UserProfileModels;
using Loza.Domain.Entities;

namespace Loza.API.Mappers
{
    public class UserMaps : Profile
    {

        public UserMaps()
        {
            CreateMap<UserCreateRequest, UserCreateCommand>();


            CreateMap<User, UserResponse>();
            CreateMap<UserUpdateRequest, UserUpdateModel>();
            CreateMap<UserCreateRequest, UserCreateModel>();
            CreateMap<QueriesRequest, QueriesModel>();

            CreateMap<LoginCommand, LoginRequestDTO>();

            /* CreateMap<UserProfileResponse, UserProfile>();*/
        }
    }
}
