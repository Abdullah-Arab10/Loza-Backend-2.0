using MediatR;
using Microsoft.EntityFrameworkCore;
using Loza.Domain.Entities;
using Loza.Application.Features.UserProfiles.Commands;
using Loza.Application.Models.SharedModels;
using Loza.Infrastructure;
using IdentityProvider.Services;
using Loza.Domain.Exceptions;

namespace Loza.Application.Features.UserProfiles.CommandsHandler
{
    internal class UserDeleteCommandHandler : IRequestHandler<UserDeleteCommand, OperationsResult<User>>
    {
        private readonly AppDataContext _appDataContext;
        private readonly IUserService _userService;

        public UserDeleteCommandHandler(AppDataContext appDataContext, IUserService userService)
        {
            _appDataContext = appDataContext;
            _userService = userService;
        }
        public async Task<OperationsResult<User>> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationsResult<User>();

            try
            {

                var user = await _appDataContext.Users.SingleOrDefaultAsync(userProfile => userProfile.UserId == request.UserId);

                if (user is null) throw new UserNotFoundException($"No user found with Id {request.UserId}");


                var deleteIdentityUser = await _userService.DeleteIdentityUser(user.IdentityId);

                if (!deleteIdentityUser) throw new Exception("identity user not deleted something went wrong!");

                _appDataContext.Users.Remove(user);

                await _appDataContext.SaveChangesAsync();

                result.Data.Add(user);
            }

            catch (UserNotFoundException ex)
            {
                result.StatusCode = 404;
                result.AddError(ex.Message);
            }

            catch (Exception ex)
            {
                result.AddUnknownError(ex.Message);
            }

            return result;
        }
    }
}
