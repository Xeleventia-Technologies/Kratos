using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using DotNetClaim = System.Security.Claims.Claim;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Kratos.Api.Common.Options;
using Kratos.Api.Database.Models.Identity;
using CustomClaimType = Kratos.Api.Common.Constants.Auth.Claim;

using static Kratos.Api.Common.Constants.Auth;

namespace Kratos.Api.Features.Auth.Token;

public interface ITokenService
{
    string GenerateAccessToken(User user, string[] roles, DotNetClaim[] permissions);
    string GenerateRefreshToken();
    string GenerateSessionId();
}

public class TokenService(IOptions<JwtOptions> jwtOptions) : ITokenService
{
    private readonly JwtOptions jwtOptions = jwtOptions.Value;

    public string GenerateAccessToken(User user, string[] roles, DotNetClaim[] permissions)
    {
        DateTime issuedAt = DateTime.UtcNow;
        DateTime expiresAt = DateTime.UtcNow.AddMinutes(jwtOptions.AccessTokenExpiryInMinutes);

        List<DotNetClaim> allClaims = [
            new(CustomClaimType.UserId.Name, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email!.ToString()),
            new(CustomClaimType.TokenType.Name, TokenType.AccessToken.Name)
        ];

        allClaims.AddRange(roles.Select(role => new DotNetClaim(ClaimTypes.Role, role)));
        allClaims.AddRange(permissions);

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey));
        SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256Signature);

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            SigningCredentials = credentials,
            Expires = expiresAt,
            Issuer = jwtOptions.Issuer,
            IssuedAt = issuedAt,
            Subject = new ClaimsIdentity(allClaims)
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
