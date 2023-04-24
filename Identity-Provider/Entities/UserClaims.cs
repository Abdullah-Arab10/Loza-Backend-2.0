
using System.ComponentModel.DataAnnotations;

namespace IdentityProvider.Entities
{
    public class UserClaims : IConcurrencyAware
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(250)]
        [Required]
        public string Type { get; set; }

        [MaxLength(250)]
        [Required]
        public string Value { get; set; }

        [ConcurrencyCheck]
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public int UserId { get; set; }

        public IdentityUserModel User { get; set; }
    }
}
