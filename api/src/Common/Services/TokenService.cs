using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BuiltInClaim = System.Security.Claims.Claim;
using BuiltInClaimTypes = System.Security.Claims.ClaimTypes;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Kratos.Api.Common.Constants;
using Kratos.Api.Common.Options;
using Kratos.Api.Common.Repositories;
using Kratos.Api.Common.Types;
using Kratos.Api.Database.Models;
using Kratos.Api.Database.Models.Identity;
using CustomClaimTypes = Kratos.Api.Common.Constants.Claim;

namespace Kratos.Api.Common.Services;

public interface ITokenService
{
    string GenerateAccessToken(User user, string[] roles, BuiltInClaim[] permissions);
    string GenerateRefreshToken();
    string GenerateSessionId();

    Task<GeneratedTokens> GenerateAndSaveAuthTokensAsync(User user, CancellationToken cancellationToken);
    Task<GeneratedTokens> GenerateAndSaveAuthTokensAsync(string? sessionId, User user, CancellationToken cancellationToken);
}

public class TokenService(
    [FromServices] UserManager<User> userManager,
    [FromServices] IUserSessionRepository userSessionRepository,
    [FromServices] IOptions<JwtOptions> jwtOptions
) : ITokenService
{
    public string GenerateAccessToken(User user, string[] roles, BuiltInClaim[] permissions)
    {
        DateTime issuedAt = DateTime.UtcNow;
        DateTime expiresAt = DateTime.UtcNow.AddMinutes(jwtOptions.Value.AccessTokenExpiryInMinutes);

        ClaimsIdentity claims = new();
        claims.AddClaims([
            new(BuiltInClaimTypes.Email, user.Email!.ToString()),
            new(CustomClaimTypes.UserId.Name, user.Id.ToString()),
            new(CustomClaimTypes.TokenType.Name, TokenType.AccessToken.Name),
        ]);
        claims.AddClaims(roles.Select(role => new BuiltInClaim(BuiltInClaimTypes.Role, role)));
        claims.AddClaims(permissions);

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(jwtOptions.Value.SecurityKey));
        SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256Signature);
        SecurityTokenDescriptor tokenDescriptor = new()
        {
            SigningCredentials = credentials,
            Expires = expiresAt,
            Issuer = jwtOptions.Value.Issuer,
            IssuedAt = issuedAt,
            Subject = claims,
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

    public async Task<GeneratedTokens> GenerateAndSaveAuthTokensAsync(User user, CancellationToken cancellationToken)
    {
        return await GenerateAndSaveAuthTokensAsync(null, user, cancellationToken);
    }

    public async Task<GeneratedTokens> GenerateAndSaveAuthTokensAsync(string? sessionId, User user, CancellationToken cancellationToken)
    {
        IList<string> userRoles = await userManager.GetRolesAsync(user);
        IList<BuiltInClaim> permissions = await userManager.GetClaimsAsync(user);

        string accessToken = GenerateAccessToken(user, [.. userRoles], [.. permissions]);
        string refreshToken = GenerateRefreshToken();

        DateTime loggedInAt = DateTime.UtcNow;
        DateTime refreshTokenExpiry = loggedInAt.AddDays(jwtOptions.Value.RefreshTokenExpiryInDays);

        UserSession? userSession = sessionId is not null ? await userSessionRepository.GetUserSessionAsync(user.Id, sessionId, cancellationToken) : null;
        if (userSession is null)
        {
            string generatedSessionId = GenerateSessionId();
            userSession = new()
            {
                UserId = user.Id,
                SessionId = generatedSessionId,
                LoggedInWith = Enums.LoggedInWith.Google,
                LoggedInAt = loggedInAt,
            };
        }

        userSession.LoggedInAt = loggedInAt;
        userSession.RefreshToken = refreshToken;
        userSession.RefreshTokenExpiresAt = refreshTokenExpiry;

        await userSessionRepository.AddOrUpdateUserSessionAsync(userSession, cancellationToken);
        return new GeneratedTokens(accessToken, refreshToken, userSession.SessionId);
    }
}
