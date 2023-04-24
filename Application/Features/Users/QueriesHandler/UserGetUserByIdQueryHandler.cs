using MediatR;
using Microsoft.EntityFrameworkCore;
using Loza.Domain.Entities;
using Loza.Application.Features.UserProfiles.Queries;
using Loza.Application.Models.SharedModels;
using Loza.Infrastructure;
using Loza.Domain.Exceptions;

namespace Loza.Application.Features.UserProfiles.QueriesHandler
{
    internal class UserGetUserByIdQueryHandler : IRequestHandler<UserGetUserByIdQuery, OperationsResult<User>>
    {
        private readonly AppDataContext _appDataContext;

        public UserGetUserByIdQueryHandler(AppDataContext appDataContext)
        {
            _appDataContext = appDataContext;
        }

        public async Task<OperationsResult<User>> Handle(UserGetUserByIdQuery request,
                                                         CancellationToken cancellationToken)
        {
            var result = new OperationsResult<User>();

            try
            {
                var user = await _appDataContext.Users.SingleOrDefaultAsync(userProfile =>
                                                                            userProfile.UserId == request.UserId);

                if (user == null) throw new UserNotFoundException($"No user found with Id {request.UserId}");


                result.Data.Add(user);


            }

            catch (UserNotFoundException ex)
            {
                result.StatusCode = 404;
                result.AddError(ex.Message);
            }

            catch (Exception ex)
            {
                result.StatusCode = 500;
                result.AddUnknownError(ex.Message);
            }

            return result;
        }
    }
}
