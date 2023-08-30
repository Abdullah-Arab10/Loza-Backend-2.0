using Loza.Application.Enums;

namespace Loza.Application.Models.SharedModels
{

    public class OperationsResult<T>
    {
        public int? StatusCode { get; set; }
        public bool IsError { get; set; }
        public List<T> Data { get; set; } = new List<T>();
        public List<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
        public int? TotalCount { get; set; }

        public void AddError( string message)
        {
            HandleError(message);
        }


        public void AddUnknownError(string message)
        {
            HandleError( message);
        }

        public void ResetIsErrorFlag()
        {
            IsError = false;
        }

        public void HandleError( string message)
        {
            Errors?.Add(new ErrorModel { Message = message });
            IsError = true;
        }


    }
}
