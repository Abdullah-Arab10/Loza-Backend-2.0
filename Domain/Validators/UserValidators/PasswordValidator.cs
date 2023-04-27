using FluentValidation;
using Loza.Domain.Exceptions;

namespace Loza.Validators.UserValidators
{
    public class PasswordValidator : AbstractValidator<string>
    {
        public PasswordValidator()
        {
            RuleFor(password => password).NotEmpty().WithMessage("Password is required.")
                                         .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                                         .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                                         .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                                         .Matches("[0-9]").WithMessage("Password must contain at least one digit.");
        }

        public static void ValidatePassword(string password)
        {
            var exception = new InvalidPasswordModelException();

            var validator = new PasswordValidator();

            var validationResult = validator.Validate(password);

            if (validationResult.IsValid) return;

            foreach (var error in validationResult.Errors)
            {
                exception.ValidationErrors.Add(error.ErrorMessage);
            }

            throw exception;
        }
    }
}
