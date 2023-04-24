using System.ComponentModel.DataAnnotations;

namespace Loza.API.Contracts.Auths.Requests
{
   
    public class RegisterRequest
    { 
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string FirstName { get; set; }


        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8 )]
        public string Password { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public DateOnly DateOfBirth { get; set; }
    }
}
