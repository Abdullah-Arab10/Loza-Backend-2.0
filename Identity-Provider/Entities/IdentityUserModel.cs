
using Microsoft.AspNetCore.Identity;

namespace IdentityProvider.Entities
{
    public class IdentityUserModel : IdentityUser
    {
        public string Name { get; set; }
    }
}
