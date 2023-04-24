using Loza.API.Registars;
using Loza.Application.Features.UserProfiles.Queries;

namespace Loza.API.Registrars
{
    public class AutoMapperRegitrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
      

            builder.Services.AddAutoMapper(typeof(Program), typeof(UserGetAllUsersQuery));

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<UserGetAllUsersQuery>());

       
        }
    }
}
