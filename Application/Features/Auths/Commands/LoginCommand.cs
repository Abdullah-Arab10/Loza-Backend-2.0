using Loza.Application.Models.AuthModels;
using Loza.Application.Models.SharedModels;
using MediatR;

namespace Loza.Application.Features.Auths.Commands
{
    public class LoginCommand : IRequest<OperationsResult<AccessTokenModel>>
    {


        public LoginCommand(LoginModel loginRequestBody)
        {
            this.UserName = loginRequestBody.Email;
            this.Password = loginRequestBody.Password;
        }

        public string UserName { get; set; }

        public string Password { get; set; }



    }
}
