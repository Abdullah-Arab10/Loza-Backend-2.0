using MediatR;
using Loza.Domain.Entities;
using Loza.Application.Models.SharedModels;
using Loza.Application.Models.UserProfileModels;

namespace Loza.Application.Features.UserProfiles.Commands
{
    public class UserCreateCommand : IRequest<OperationsResult<User>>
    {

        public UserCreateCommand(UserCreateModel user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            EmailAddress = user.EmailAddress;
            PhoneNumber = user.PhoneNumber;
            DateOfBirth = user.DateOfBirth;
            Address = user.Address;
            Password = user.Password;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string Address { get; set; }

        public string Password { get; set; }
    }
}
