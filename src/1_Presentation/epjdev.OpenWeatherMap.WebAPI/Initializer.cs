using epjdev.OpenWeatherMap.Cache;
using epjdev.OpenWeatherMap.OWMIntegration;
using epjdev.OpenWeatherMap.WeatherInfoCollector;
using epjdev.OpenWeatherMap.WeatherInfoStorer;
using epjdev.OpenWeatherMap.WeatherConsultApplication;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using epjdev.OpenWeatherMap.WeatherSourceApplication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace epjdev.OpenWeatherMap.WebAPI
{
    public class Initializer
    {
        readonly static string TokenSecret = "b2797113-d41d-46c6-850a-cc928418921a";

        public static ServiceProvider Start(IServiceCollection serviceCollection)
        {
            serviceCollection.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            serviceCollection.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenSecret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false
                };
            });

            //start infra
            serviceCollection.StartOMWIntegration();

            //start domain
            serviceCollection.StartWeatherInfoCollector();
            serviceCollection.StartWeatherInfoStorer();

            //start application
            serviceCollection.StartWeatherSourceApplication();
            serviceCollection.StartWeatherConsultApplication();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
