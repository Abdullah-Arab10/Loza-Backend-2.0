using Bogus;
using Loza.Domain.Entities;


namespace Loza.Infrastructure.Seeders
{
    public class Fakers
    {
        private UsersFaker _userProfileFaker;

        //    public Faker<UserProfile> GetUserProfileFaker() => _userProfileFaker;

        public Fakers()
        {
            /*    Randomizer.Seed = new Random(300);
                _userProfileFaker = new Faker<UserProfile>();*/

            _userProfileFaker = new UsersFaker();

        }

        public IEnumerable<User>  GenerateUsersProfile()
        {
            return _userProfileFaker.Generate(100);
        }
    }
}
