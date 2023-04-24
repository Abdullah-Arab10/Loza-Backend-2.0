
using IdentityProvider.DbContexts;
using Microsoft.EntityFrameworkCore;
using Loza.API.Registars;
using Loza.Infrastructure;

namespace Loza.API.Registrars
{
    public class DbRegistrar : IWebApplicationBuilderRegistrar
    {

        public void RegisterServices(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("Default");
            builder.Services
                   .AddDbContext<AppDataContext>
                   (options =>
            
                  options.UseSqlServer(
                    connectionString)

                     );

            var identityConnectionString = builder.Configuration.GetConnectionString("DefaultForIdentityDb");
            builder.Services.AddDbContext<IdentityDataContext>(options => options.UseSqlServer(identityConnectionString));


            //builder.Services.AddTransient<Fakers>();

        }
    }
}
