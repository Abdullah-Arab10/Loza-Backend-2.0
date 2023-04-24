using MediatR;
using Microsoft.EntityFrameworkCore;
using Loza.Domain.Entities;
using Loza.Domain.Exceptions;
using Loza.Application.Features.UserProfiles.Commands;
using Loza.Application.Models.SharedModels;
using Loza.Infrastructure;

namespace Loza.Application.Features.UserProfiles.CommandsHandler
{
    internal class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand, OperationsResult<User>>
    {
        private readonly AppDataContext _appDataContext;

        public UserUpdateCommandHandler(AppDataContext appDataContext)
        {
            _appDataContext = appDataContext;
        }


        public async Task<OperationsResult<User>> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {

            var result = new OperationsResult<User>();

            try
            {
                var user = await _appDataContext.Users.SingleOrDefaultAsync<User>(userProfile => userProfile.UserId == request.UserId);

                if (user == null)
                {

                    result.IsError = true;
                    var error = new ErrorModel
                    {
                        Message = $"No user found with Id {request.UserId}",
                     
                    };
                    result.StatusCode = 404;
                    result.Errors.Add(error);
                    return result;
                }

                user.UpdateUserProfile(request.FirstName, request.LastName, request.EmailAddress, request.PhoneNumber, request.DateOfBirth, request.Address);
                _appDataContext.Users.Update(user);
                await _appDataContext.SaveChangesAsync();
                result.Data.Add(user);
            }
            catch (UserProfileModelNotValidException ex)
            {
                ex.ValidationErrors.ForEach(e => result.AddError(e));
                result.IsError = true;
                result.StatusCode = 400;
            }

            catch (Exception ex)
            {
                result.IsError = true;
                var error = new ErrorModel
                {
                    Message = ex.Message,
                  
                };
                result.Errors.Add(error);
                result.StatusCode = 500;
            }
            return result;

        }
    }
}
