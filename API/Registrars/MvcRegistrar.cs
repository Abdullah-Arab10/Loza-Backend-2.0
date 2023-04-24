using Loza.API.Filters;
using Loza.API.Registars;

namespace Loza.API.Registrars
{
    public class MvcRegistrar: IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers(config =>
            {
                config.Filters.Add(typeof(InternalExceptionFilter));
                config.Filters.Add(typeof(ModelAttributeValidationFilter));
            });
        }

        
    }
}
