using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

using Kratos.Api.Common.Options;
using Kratos.Api.Common.Constants;

namespace Kratos.Api.Startup;

public static class AuthInitializer
{
    public static void AddJwtAuth(this IServiceCollection services, JwtOptions jwtOptions)
    {
        services.AddJwtAuthentication(jwtOptions);
        services.AddJwtAuthorization();
    }

    private static void AddJwtAuthentication(this IServiceCollection services, JwtOptions jwtOptions)
    {
        var authBuilder = services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = Scheme.ValidJwt.Name;
            x.DefaultChallengeScheme = Scheme.ValidJwt.Name;
            x.DefaultForbidScheme = Scheme.ValidJwt.Name;
        });

        authBuilder.AddJwtBearer(Scheme.ValidJwt.Name, options => ConfigureJwtBearer(options, jwtOptions, validateLifetime: true));
        authBuilder.AddJwtBearer(Scheme.ExpiredJwt.Name, options => ConfigureJwtBearer(options, jwtOptions, validateLifetime: false));
    }

    private static void AddJwtAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.BuildAndAddPolicy(Scheme.ValidJwt.Name, Policy.RequireValidJwt.Name);
            options.BuildAndAddPolicy(Scheme.ExpiredJwt.Name, Policy.AllowExpiredJwt.Name);
            
            options.BuildAndAddPolicy(Scheme.ValidJwt.Name, Policy.RequireValidJwtAdmin.Name, Role.Admin.Name);
            options.BuildAndAddPolicy(Scheme.ExpiredJwt.Name, Policy.AllowExpiredJwtAdmin.Name, Role.Admin.Name);
            
            options.BuildAndAddPolicy(Scheme.ValidJwt.Name, Policy.RequireValidJwtUser.Name, Role.User.Name);
            options.BuildAndAddPolicy(Scheme.ExpiredJwt.Name, Policy.AllowExpiredJwtUser.Name, Role.User.Name);

            options.DefaultPolicy = options.GetPolicy(Policy.RequireValidJwt.Name)!;
        });
    }

    private static JwtBearerOptions ConfigureJwtBearer(JwtBearerOptions bearerOptions, JwtOptions jwtOptions, bool validateLifetime)
    {
        bearerOptions.RequireHttpsMetadata = !jwtOptions.AllowInsecureHttp;
        bearerOptions.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = validateLifetime,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey)),
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = jwtOptions.Issuer
        };

        return bearerOptions;
    }

    private static AuthorizationOptions BuildAndAddPolicy(this AuthorizationOptions authOptions, string schemeName, string policyName, string? role = null)
    {
        AuthorizationPolicyBuilder policyBuilder = new AuthorizationPolicyBuilder()
            .AddAuthenticationSchemes(schemeName)
            .RequireAuthenticatedUser();

        if (role is not null)
            policyBuilder.RequireRole(role);

        AuthorizationPolicy policy = policyBuilder.Build();
        authOptions.AddPolicy(policyName, policy);

        return authOptions;
    }
}
