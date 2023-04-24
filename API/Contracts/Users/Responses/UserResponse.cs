
namespace Loza.API.Contracts.UserProfiles.Responses
{
    public record UserResponse
    {
    //    [JsonIgnore]
        public Guid UserProfileId { get; set; }

        

        public DateTime DateCreated { get; set; }

        public DateTime LastModified { get; set; }
    }
}
