using MediatR;
using Loza.Domain.Entities;
using System.Linq.Dynamic.Core;
using Loza.Application.Features.UserProfiles.Queries;
using Loza.Application.Models.SharedModels;
using Loza.Infrastructure;
using Loza.Application.Helpers;

namespace Loza.Application.Features.UserProfiles.QueriesHandler
{
    internal class UserGetAllUsersQueryHandler : IRequestHandler<UserGetAllUsersQuery, OperationsResult<User>>
    {
        private readonly AppDataContext _appDataContext;

        public UserGetAllUsersQueryHandler(AppDataContext appDataContext)
        {
            this._appDataContext = appDataContext;
        }

        public async Task<OperationsResult<User>> Handle(UserGetAllUsersQuery request,
                                                                CancellationToken cancellationToken)
        {

            var result = new OperationsResult<User>();
            var queries = request.queries;
            // To dive in
            var collection = _appDataContext.Users as IQueryable<User>;


            if (!string.IsNullOrWhiteSpace(queries?.SearchBy))
            {
                var searchQuery = queries.SearchBy.Replace(" ", "");


                collection = collection.Where(user => user.FirstName.Contains(searchQuery)
                                                   || user.LastName.Contains(searchQuery)
                                                   || (user.FirstName + user.LastName).Contains(searchQuery));
            }

            var list = await PaginationHelper<User>.CreatePaginatedList(collection,
                                                                         queries.PageSize,
                                                                         queries.PageNumber);


            foreach (var item in list)
            {
                result.Data?.Add(item);
            }
            //   result.TotalCount = list?.TotalCount;
            return result;
        }
    }
}
