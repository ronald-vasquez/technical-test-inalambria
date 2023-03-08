using Inalambria.Core.Interfaces.Infraestructure;
using Inalambria.Core.Interfaces.Services;
using Inalambria.Core.Service;
using Inalambria.Infrastructure.Options;
using Inalambria.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inalambria.Infrastructure.Extensions
{
    public static class ServiceCollectionExtension
    {

        public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "Cors",
                    builder =>
                    {
                        builder.WithOrigins(configuration["WebBase"])
                        .WithHeaders("*")
                        .WithMethods("PUT", "DELETE", "GET", "POST");
                    });
            });
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {

            services.AddTransient<IDominoService, DominoService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }

        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PasswordOptions>(options => configuration.GetSection("PasswordOptions").Bind(options));
            services.Configure<AuthenticationOptions>(options => configuration.GetSection("Authentication").Bind(options));
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(doc =>
            {
                doc.SwaggerDoc("v1", new OpenApiInfo { Title = "Inalambria API", Version = "v1" });
            });

            return services;
        }
    }
}
