using MediatR;
using Microsoft.EntityFrameworkCore;
using Loza.Domain.Entities;
using Loza.Domain.Exceptions;
using Loza.Application.Features.UserProfiles.Commands;
using Loza.Application.Models.SharedModels;
using Loza.Infrastructure;

namespace Loza.Application.Features.UserProfiles.CommandsHandler
{
    internal class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, OperationsResult<User>>
    {
        private readonly AppDataContext _appDataContext;


        public UserCreateCommandHandler(AppDataContext appDataContext)
        {
            _appDataContext = appDataContext;
        }

        public async Task<OperationsResult<User>> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {

            var result = new OperationsResult<User>();

            try
            {

                bool isExist = await _appDataContext.Users.AnyAsync(user => user.EmailAddress.ToLower() == request.EmailAddress.ToLower());

                if (isExist)
                {
                    throw new DuplicatedEmailException("This email is already taken");
                }


                var user = User.CreateUserProfile(
                    request.FirstName,
                    request.LastName,
                    request.EmailAddress,
                    request.PhoneNumber,
                    request.DateOfBirth,
                    request.Address,
                    request.Password,
                    Guid.NewGuid().ToString()
                    );


                _appDataContext.Users.Add(user);

                await _appDataContext.SaveChangesAsync();

                result.Data.Add(user);
        
                return result;
            }
            catch (DuplicatedEmailException ex)
            {
                result.AddError( ex.Message);
                result.StatusCode = 400;
            }
            catch (UserProfileModelNotValidException ex)
            {

                ex.ValidationErrors.ForEach(e => result.AddError(e));
                result.StatusCode =400;

            }

            catch (Exception e)
            {
                result.AddUnknownError(e.Message);
                result.StatusCode=500;
            }

            return result;
        }
    }
}
