using Loza.Domain.Exceptions;
using Loza.Domain.Validators.UserProfileValidators;

namespace Loza.Domain.Entities
{
    public class User
    {
        public User()
        {

        }

        public int UserId { get; private set; }

        public string IdentityId { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string EmailAddress { get; private set; }

        public string PhoneNumber { get; private set; }

        public string Password { get; private set; }

        public string DateOfBirth { get; private set; }

        public string? Address { get; private set; }

        public DateTime DateCreated { get; private set; }

        public DateTime LastModified { get; private set; }

        public static User CreateUserProfile(
            string firstName,
            string lastName,
            string emailAddress,
            string phoneNumber,
            DateOnly dateOfBirth,
            string currentCity,
            string password,
            string identityId
            )
        {
            var validator = new UserValidator();

            var objToValidate = new User()
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = emailAddress,
                PhoneNumber = phoneNumber,
                DateOfBirth = dateOfBirth.ToString(),
                Address = currentCity,
                Password = password,
                IdentityId = identityId
            };

            var validationResult = validator.Validate(objToValidate);

            if (validationResult.IsValid)
            {
                objToValidate.IdentityId = identityId;
                objToValidate.DateCreated = DateTime.UtcNow;
                objToValidate.LastModified = DateTime.UtcNow;
                return objToValidate;
            };

            var exception = new UserProfileModelNotValidException();

            foreach (var error in validationResult.Errors)
            {

                exception.ValidationErrors.Add(error.ErrorMessage);
            }

            throw exception;
        }

        public void UpdateUserProfile(
            string firstName,
            string lastName,
            string emailAddress,
            string phoneNumber,
            DateOnly dateOfBirth,
            string currentCity)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth.ToString();
            Address = currentCity;
            LastModified = DateTime.UtcNow;
        }

    }
}
