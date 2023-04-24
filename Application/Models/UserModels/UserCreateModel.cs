
namespace Loza.Application.Models.UserProfileModels
{
    public class UserCreateModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string Address { get; set; }
    }
}
