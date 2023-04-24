using Application.Features.Auths.Commands;
using IdentityProvider.DTOs;
using IdentityProvider.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Loza.Domain.Entities;
using Loza.Domain.Exceptions;
using Loza.Application.Models.SharedModels;
using Loza.Infrastructure;
using Loza.Validators.UserValidators;

namespace Loza.Application.Features.Auths.CommandsHandler
{
    internal class RegisterCommandHandler : IRequestHandler<RegisterCommand, OperationsResult<User>>
    {
        private readonly AppDataContext _appDataContext;
        private readonly IUserService _userService;

        public RegisterCommandHandler(AppDataContext appDataContext, IUserService userService)
        {
            _appDataContext = appDataContext;

            _userService = userService;
        }


        public async Task<OperationsResult<User>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationsResult<User>();


            try
            {
                var checkEmailExist = await _appDataContext.Users.SingleOrDefaultAsync(user => request.EmailAddress == user.EmailAddress);
                if (checkEmailExist is not null)
                {
                    throw new DuplicatedEmailException("This email is already taken");
                }

                
                PasswordValidator.ValidatePassword(request.Password);


                var identityUserRequestObj = new RegisterRequestDTO { Name = request.FirstName, Email = request.EmailAddress, Password = request.Password };
                var identityUser = await _userService.Register(identityUserRequestObj);

                if (identityUser.Id is null) throw new Exception("User was not added something went wrong!");


                var user = User.CreateUserProfile(
                              request.FirstName,
                              request.LastName,
                              request.EmailAddress,
                              request.PhoneNumber,
                              request.DateOfBirth,
                              request.Address,
                              request.Password,
                              identityUser.Id

                              );

                _appDataContext.Users.Add(user);


                await _userService.SaveChangesAsync();
                await _appDataContext.SaveChangesAsync();

                result.Data.Add(user);



            } 
            
            catch (InvalidPasswordModelException ex)
            {
                ex.ValidationErrors.ForEach(e => result.AddError(e));
                result.StatusCode = 400;
            }
            catch (DuplicatedEmailException ex)
            {
                result.AddError(ex.Message);
                result.StatusCode = 400;
            }

           


            catch (UserProfileModelNotValidException ex)
            {

                ex.ValidationErrors.ForEach(e => result.AddError(e));
                result.StatusCode = 400;

            }

            catch (Exception e)
            {
                result.AddUnknownError(e.Message);
                result.StatusCode = 500;
            }
            return result;
        }
    }
}
