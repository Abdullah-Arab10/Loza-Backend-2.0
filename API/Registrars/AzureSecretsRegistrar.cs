using Azure.Identity;
using Loza.API.Registars;

namespace Loza.API.Registrars
{
    public class AzureSecretsRegistrar : IWebApplicationBuilderRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            var keyValutUrl = new Uri(builder.Configuration.GetSection("LozaKeyVault").Value!);
            var azureCredential = new DefaultAzureCredential();
            builder.Configuration.AddAzureKeyVault(keyValutUrl, azureCredential);
        }
    }
}
