using Loza.Application.Models.SharedModels;

namespace Loza.API.Contracts.Shared.Responses
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }

        public string StatusPhrase { get; set; }

        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();

        public DateTime Timestamp { get; set; }
    }
}
