using System.ComponentModel.DataAnnotations;

namespace Loza.API.Contracts.UserProfiles.Requests
{
    public class UserUpdateRequest
    {

        [Required]
        public int UserId { get; set; }

        [MinLength(3)]
        public string FirstName { get; set; } = string.Empty;

 
        [MinLength(3)]
        public string LastName { get; set; } = string.Empty;

    
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        
        public DateOnly DateOfBirth { get; set; }

        public string Address { get; set; } = string.Empty;
    }
}
