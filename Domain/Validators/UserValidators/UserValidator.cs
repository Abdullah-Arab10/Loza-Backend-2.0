using FluentValidation;
using Loza.Domain.Entities;


namespace Loza.Domain.Validators.UserProfileValidators
{
    internal class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            var maxAge = new DateTime(DateTime.Now.AddYears(-80).Ticks);
            var minAge = new DateTime(DateTime.Now.AddYears(-18).Ticks);

            RuleFor(info => info.FirstName).NotNull().WithMessage("first name is required").MinimumLength(3).MaximumLength(25);

            RuleFor(info => info.LastName).NotNull().MinimumLength(3).MaximumLength(25);

            RuleFor(info => info.EmailAddress).NotNull().EmailAddress();

            RuleFor(info => DateOnly.Parse(info.DateOfBirth)).NotNull().InclusiveBetween(DateOnly.FromDateTime(maxAge), DateOnly.FromDateTime(minAge));



            RuleFor(info => info.Address).NotEmpty() ;

            RuleFor(info=>info.IdentityId).NotEmpty();
        }
    }
}
