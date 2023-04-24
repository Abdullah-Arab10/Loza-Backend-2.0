using IdentityProvider.DbContexts;
using IdentityProvider.Entities;
using IdentityProvider.Services;
using IdentityProvider;
using Microsoft.AspNetCore.Identity;
using Loza.API.Registars;

namespace Loza.API.Registrars
{
    public class IdentityRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<IdentityUserModel, IdentityRole>()
                     .AddEntityFrameworkStores<IdentityDataContext>()
                     .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = null;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;


            });

            builder.Services.AddIdentityServer(options =>
            {
                // https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/api_scopes#authorization-based-on-scopes
                options.EmitStaticAudienceClaim = true;
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
                .AddProfileService<ProfileService>()
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryApiResources(Config.ApiResources)
                .AddInMemoryClients(Config.Clients)
                .AddAspNetIdentity<IdentityUserModel>()
                .AddProfileService<ProfileService>()
                .AddDeveloperSigningCredential();


            builder.Services.AddScoped<IUserService, UserService>();
        }
    }
}
