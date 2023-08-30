
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

            //   var connectionString = builder.Configuration.GetSection("DefaultCS").Value;
            builder.Services
                   .AddDbContext<AppDataContext>
                   (options =>

                  options.UseSqlServer(
                    connectionString)

                     );

            var identityConnectionString = builder.Configuration.GetConnectionString("DefaultForIdentityDb");
            // var identityConnectionString = builder.Configuration.GetSection("DefaultForIdentityDbCS").Value;
            builder.Services.AddDbContext<IdentityDataContext>(options => options.UseSqlServer(identityConnectionString));


            //builder.Services.AddTransient<Fakers>();

        }
    }
}
