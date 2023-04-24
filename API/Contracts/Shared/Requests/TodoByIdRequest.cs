using System.ComponentModel.DataAnnotations;

namespace Loza.API.Contracts.Shared.Requests
{
    public class TodoByIdRequest
    {
        [Required]
        public int id { get; set; }
    }
}
