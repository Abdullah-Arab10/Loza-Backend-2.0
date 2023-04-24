
namespace Loza.Application.Models.UserProfileModels
{
    public class UserUpdateModel
    {
        public int UserId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string Address { get; set; }
    }
}
