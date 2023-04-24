namespace Loza.Application.Models.AuthModels
{
    public class RegisterModel
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string Address { get; set; }
    }
}
