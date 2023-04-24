using MediatR;
using Loza.Domain.Entities;
using Loza.Application.Models.SharedModels;

namespace Loza.Application.Features.UserProfiles.Queries
{
    public class UserGetAllUsersQuery : IRequest<OperationsResult<User>>
    {
        public QueriesModel? queries = new QueriesModel();

        public UserGetAllUsersQuery(QueriesModel? queries)
        {
            this.queries = queries;
        }
    }
}
