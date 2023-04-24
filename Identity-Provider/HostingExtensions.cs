using Duende.IdentityServer.Services;
using IdentityProvider.DbContexts;
using IdentityProvider.Entities;
using IdentityProvider.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IdentityProvider;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        // uncomment if you want to add a UI
        builder.Services.AddRazorPages();
  
        
   

        builder.Services.AddIdentity<IdentityUserModel, IdentityRole>()
                        .AddEntityFrameworkStores<IdentityDataContext>()
                        .AddDefaultTokenProviders()
                        
                        ;

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
            .AddDeveloperSigningCredential()

            ;
        //.AddTestUsers(TestUsers.Users);



        builder.Services.AddAutoMapper(typeof(Program));


        builder.Services.AddDbContext<IdentityDataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultForIdentityDb")));
        builder.Services.AddScoped<IProfileService, ProfileService>();
        builder.Services.AddScoped<IDbInitializer, DbInitializer>();
        builder.Services.AddScoped<IUserService, UserService>();


        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // uncomment if you want to add a UI
        app.UseStaticFiles();


        //for seed identity database
    /*    using (var scope = app.Services.CreateScope())
        {
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            dbInitializer.Initialize();
        }*/


        app.UseRouting();

        app.UseIdentityServer();

        // uncomment if you want to add a UI
        app.UseAuthorization();

        app.MapRazorPages().RequireAuthorization();



        return app;
    }
}
