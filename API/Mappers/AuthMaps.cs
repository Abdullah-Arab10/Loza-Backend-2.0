using AutoMapper;
using Loza.API.Contracts.Auths.Requests;
using Loza.API.Contracts.Shared.Responses;
using Loza.Application.Models.AuthModels;
using Loza.Application.Models.SharedModels;

namespace Loza.API.Mappers
{
    public class AuthMaps : Profile
    {
        public AuthMaps()
        {
            CreateMap<LoginRequest, LoginModel>();

            CreateMap<RegisterRequest, RegisterModel>();
            
            CreateMap<ErrorModel, ErrorTest>();

         
        }
    }
}
