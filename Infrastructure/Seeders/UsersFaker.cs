using Bogus;
using Loza.Domain.Entities;

namespace Loza.Infrastructure.Seeders
{
    public class UsersFaker : Faker<User>
    {
      
        public UsersFaker()
        {
            var maxAge = new DateTime(DateTime.Now.AddYears(-80).Ticks);
            var minAge = new DateTime(DateTime.Now.AddYears(-18).Ticks);

            UseSeed(1000)
           .RuleFor(c => c.UserId, f => ++f.IndexFaker)
           .RuleFor(c => c.FirstName, f => f.Person.FirstName)
           .RuleFor(c => c.LastName, f => f.Person.LastName)
           .RuleFor(c => c.EmailAddress, f => f.Person.Email)
           .RuleFor(c => c.PhoneNumber, f => f.Person.Phone)
           .RuleFor(c => c.Address, f => f.Person.Address.City)
           .RuleFor(c => c.DateOfBirth, f => f.Date.BetweenDateOnly(DateOnly.FromDateTime(maxAge),DateOnly.FromDateTime(minAge)).ToString());
        }
    }
}
