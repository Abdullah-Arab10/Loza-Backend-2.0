namespace Identity.Provider.DTOs
{
    public class UserDTO
    {

        public UserDTO()
        {
            
        }
        
        
        public UserDTO(string id, string email, string name, string username)
        {
            Id = id;
            Email = email;
            Name = name;
            UserName = username;
        }

        public string Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }
    }
}
