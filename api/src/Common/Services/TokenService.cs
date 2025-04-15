using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BuiltInClaimTypes = System.Security.Claims.ClaimTypes;
using BuiltInClaim = System.Security.Claims.Claim;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Kratos.Api.Common.Options;
using Kratos.Api.Database.Models.Identity;
using CustomClaimTypes = Kratos.Api.Common.Constants.Auth.Claim;

using static Kratos.Api.Common.Constants.Auth;

namespace Kratos.Api.Common.Services;

public interface ITokenService
{
    string GenerateAccessToken(User user, string[] roles, BuiltInClaim[] permissions);
    string GenerateRefreshToken();
    string GenerateSessionId();
}

public class TokenService(IOptions<JwtOptions> jwtOptions) : ITokenService
{
    private readonly JwtOptions jwtOptions = jwtOptions.Value;

    public string GenerateAccessToken(User user, string[] roles, BuiltInClaim[] permissions)
    {
        DateTime issuedAt = DateTime.UtcNow;
        DateTime expiresAt = DateTime.UtcNow.AddMinutes(jwtOptions.AccessTokenExpiryInMinutes);

        ClaimsIdentity claims = new();
        claims.AddClaims([
            // new(CustomClaimTypes.UserId.Name, user.Id.ToString()),
            // new(BuiltInClaimTypes.Email, user.Email!.ToString()),
            // new(CustomClaimTypes.TokenType.Name, TokenType.AccessToken.Name),
            new("TestKey", "Testvalue"),
        ]);
        // claims.AddClaims(roles.Select(role => new BuiltInClaim(BuiltInClaimTypes.Role, role)));
        // claims.AddClaims(permissions);

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey));
        SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256Signature);
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            SigningCredentials = credentials,
            Expires = expiresAt,
            Issuer = jwtOptions.Issuer,
            IssuedAt = issuedAt,
            Subject = claims
        };

        JwtSecurityTokenHandler tokenHandler = new();
        SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);

        string token = tokenHandler.WriteToken(securityToken);
        return token;
    }

    public string GenerateRefreshToken()
    {
        return $"{Guid.NewGuid()}-{Guid.NewGuid()}-{Guid.NewGuid()}";
    }

    public string GenerateSessionId()
    {
        return Guid.NewGuid().ToString();
    }
}
