using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace PRM.Infrastructure.Authentication
{
    internal static class Settings
    {
        internal static string Secret = "62c5732f-001e-4660-8c1a-7889a8371529";
    }

    public static class AuthenticationSettingsExtensions
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });;
            
            return services;
        }
    }
}

