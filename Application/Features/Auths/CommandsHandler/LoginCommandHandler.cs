using AutoMapper;
using Identity.Provider.DTOs;
using IdentityProvider.Services;
using MediatR;
using System.Text.Json;
using Loza.Application.Features.Auths.Commands;
using Loza.Application.Models.SharedModels;
using Loza.Domain.Exceptions;
using Loza.Validators.UserValidators;

namespace Loza.Application.Features.Auths.CommandsHandler
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, OperationsResult<AccessTokenModel>>
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public LoginCommandHandler(IUserService userService , IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;

        }

        public async Task<OperationsResult<AccessTokenModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
         var response = new OperationsResult<AccessTokenModel>();


            try
            {
                var user = _mapper.Map<LoginRequestDTO>(request);

                var res = await _userService.Login(user);

                PasswordValidator.ValidatePassword(request.Password);
                if (res == null) throw new UserNotFoundException("User is not found");
                if (res.Token == null) throw new IncorrectPasswordException("Password is wrong");


                var accessResponse = new AccessTokenModel() { Token = res.Token };

                response.Data.Add(accessResponse);

            }
            catch (InvalidPasswordModelException ex)
            {
                ex.ValidationErrors.ForEach(e => response.AddError(e));
                response.StatusCode = 400;
            }

            catch (UserNotFoundException ex)
            {
                response.AddError( ex.Message);
                response.StatusCode = 404;

            }
            catch (IncorrectPasswordException ex)
            {
                response.AddError( ex.Message);
                response.StatusCode = 400;
            }
            catch (Exception ex)
            {
                response.AddError( ex.Message);
                response.StatusCode = 500;
            }

            return response;

             
        }
    }

}
