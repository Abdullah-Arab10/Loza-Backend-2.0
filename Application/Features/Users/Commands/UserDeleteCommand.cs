using MediatR;
using Loza.Domain.Entities;
using Loza.Application.Models.SharedModels;

namespace Loza.Application.Features.UserProfiles.Commands
{
    public class UserDeleteCommand : IRequest<OperationsResult<User>>
    {
       public int UserId { get; set; }

    }
}
