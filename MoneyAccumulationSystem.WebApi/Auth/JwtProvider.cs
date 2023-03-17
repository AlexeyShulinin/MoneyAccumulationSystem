using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MoneyAccumulationSystem.CrossCutting.Auth;

namespace MoneyAccumulationSystem.WebApi.Auth;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions jwtOptions;
    public JwtProvider(IOptions<JwtOptions> jwtOptions)
    {
        this.jwtOptions = jwtOptions.Value;
    }
    
    public string Generate(AuthUser authUser)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtOptions.SecretKey);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new []
            {
                new Claim(AuthConstants.AuthUserClaim, JsonSerializer.Serialize(authUser)),
            }),
            Expires = DateTime.UtcNow.AddHours(6),
            Audience = jwtOptions.Audience,
            Issuer = jwtOptions.Issuer,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);

        var jwtToken = jwtTokenHandler.WriteToken(token);

        return jwtToken;
    }
}