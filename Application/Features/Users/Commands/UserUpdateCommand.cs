using MediatR;
using Loza.Domain.Entities;
using Loza.Application.Models.SharedModels;
using Loza.Application.Models.UserProfileModels;

namespace Loza.Application.Features.UserProfiles.Commands
{
    public class UserUpdateCommand : IRequest<OperationsResult<User>>
    {

        public UserUpdateCommand(UserUpdateModel user) {
            UserId = user.UserId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            EmailAddress = user.EmailAddress;
            PhoneNumber = user.PhoneNumber;
            DateOfBirth = user.DateOfBirth;
            Address = user.Address;
        }
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string Address { get; set; }
    }
}
