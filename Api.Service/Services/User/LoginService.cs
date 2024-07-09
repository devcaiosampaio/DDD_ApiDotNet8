using Api.Domain.Dtos;
using Api.Domain.Entities;
using Api.Domain.Interfaces.User.Repositories;
using Api.Domain.Interfaces.User.Services;
using Api.Domain.Security;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Api.Service.Services.User;
public class LoginService : ILoginService
{
    private readonly IUserRepository _userRepository;
    private readonly SigningConfigurations _signingConfigurations;
    private readonly IOptions<TokenConfiguration> _tokenConfiguration;
    public LoginService(IUserRepository userRepository,
                        SigningConfigurations signingConfigurations,
                        IOptions<TokenConfiguration> tokenConfiguration)
    {
        _userRepository = userRepository;
        _signingConfigurations = signingConfigurations;
        _tokenConfiguration = tokenConfiguration;
    }
    public async Task<object?> FindByEmail(LoginDto loginDto)
    {
        if (loginDto is null || string.IsNullOrWhiteSpace(loginDto.Email))
            return new
            {
                authenticated = false,
                message = "Falha ao autenticar"
            };

        var baseUser = await _userRepository.FindByEmail(loginDto.Email);

        if (baseUser is null)
            return new
            {
                authenticated = false,
                message = "Falha ao autenticar"
            };

        ClaimsIdentity identity = new(
                        new GenericIdentity(loginDto.Email),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, loginDto.Email),
                        }
                    );

        DateTime createDate = DateTime.UtcNow;
        DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfiguration.Value.Seconds);

        var handler = new JwtSecurityTokenHandler();
        string token = CreateToken(identity, createDate, expirationDate, handler);
        return SuccessObject(createDate, expirationDate, token, baseUser);
    }
    private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
    {
        var securityToken = handler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _tokenConfiguration.Value.Issuer,
            Audience = _tokenConfiguration.Value.Audience,
            SigningCredentials = _signingConfigurations.SigningCredentials,
            Subject = identity,
            NotBefore = createDate,
            Expires = expirationDate,
        });

        var token = handler.WriteToken(securityToken);
        return token;
    }

    private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, UserEntity user)
    {
        return new
        {
            authenticated = true,
            create = createDate.ToString("o", CultureInfo.InvariantCulture),
            expiration = expirationDate.ToString("o", CultureInfo.InvariantCulture),
            accessToken = token,
            userName = user.Email,
            name = user.Name,
            message = "Usuário Logado com sucesso"
        };
    }
}
