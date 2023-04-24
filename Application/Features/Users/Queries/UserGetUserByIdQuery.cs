using MediatR;
using Loza.Domain.Entities;
using Loza.Application.Models.SharedModels;

namespace Loza.Application.Features.UserProfiles.Queries
{
    public class UserGetUserByIdQuery : IRequest<OperationsResult<User>>
    {
        public int UserId { get; set; }
    }
}
