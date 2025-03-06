using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

using Kratos.WebApi.Common.Options;
using Kratos.WebApi.Common.Constants;

namespace Kratos.WebApi.Startup;

public static class AuthPolicies
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
            x.DefaultAuthenticateScheme = Auth.Schemes.ValidJwt;
            x.DefaultChallengeScheme = Auth.Schemes.ValidJwt;
            x.DefaultForbidScheme = Auth.Schemes.ValidJwt;
        });

        authBuilder.AddJwtBearer(Auth.Schemes.ValidJwt, options => ConfigureJwtBearer(options, jwtOptions, validateLifetime: true));
        authBuilder.AddJwtBearer(Auth.Schemes.ExpiredJwt, options => ConfigureJwtBearer(options, jwtOptions, validateLifetime: false));
    }

    private static void AddJwtAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.BuildAndAddPolicy(schemeName: Auth.Schemes.ValidJwt, policyName: Auth.Policies.RequireValidJwt);
            options.BuildAndAddPolicy(schemeName: Auth.Schemes.ValidJwt, policyName: Auth.Policies.RequireValidJwt, role: Auth.Roles.Admin);
            options.BuildAndAddPolicy(schemeName: Auth.Schemes.ValidJwt, policyName: Auth.Policies.RequireValidJwt, role: Auth.Roles.User);
            
            options.BuildAndAddPolicy(schemeName: Auth.Schemes.ExpiredJwt, policyName: Auth.Policies.AllowExpiredJwt);
            options.BuildAndAddPolicy(schemeName: Auth.Schemes.ExpiredJwt, policyName: Auth.Policies.AllowExpiredJwt, role: Auth.Roles.Admin);
            options.BuildAndAddPolicy(schemeName: Auth.Schemes.ExpiredJwt, policyName: Auth.Policies.AllowExpiredJwt, role: Auth.Roles.User);

            options.DefaultPolicy = options.GetPolicy(Auth.Policies.RequireValidJwt)!;
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
