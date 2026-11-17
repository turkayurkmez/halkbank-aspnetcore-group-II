using ECommerce.Application.Features.Users.Commands.RegisterUser;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ECommerce.Application.Auth
{
    public record AccessToken(string Value, DateTime ExpiresAt);



    public interface ITokenService
    {
        AccessToken CreateAccessToken(User user);

        string GenerateRefreshToken();

        TimeSpan RefreshTokenLifeTime { get; }


    }

    public class JwtOptions
    {
        public const string SectionName = "Jwt";
        public string Issuer { get; set; } = string.Empty; //kim verdi?
        public string Audience { get; set; } = string.Empty; //kime verdi?
        public string SigninKey { get; set; } = string.Empty; //gizli cümle

        public int RefreshTokenDays { get; set; }
        public int AccessTokenMinutes { get; set; }



    }

    public class JwtTokenService(IOptions<JwtOptions> options) : ITokenService
    {
        private readonly JwtOptions _option = options.Value;
        public TimeSpan RefreshTokenLifeTime => TimeSpan.FromDays(_option.RefreshTokenDays);

        public AccessToken CreateAccessToken(User user)
        {
            var now = DateTime.Now;
            var expiresAt = now.AddMinutes(_option.AccessTokenMinutes);

            var claims = new Dictionary<string, object>
            {
                [JwtRegisteredClaimNames.Sub] = user.Id.ToString(),
                [JwtRegisteredClaimNames.Email] = user.Email,
                [JwtRegisteredClaimNames.Name] = user.FullName,
                [JwtRegisteredClaimNames.Birthdate] = user.DateOfBirth,
                ["role"] = user.Roles.Select(r => r.Name).ToArray()
                //["takim"] = user.Team
            };

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = _option.Issuer,
                Audience = _option.Audience,
                NotBefore = now,
                Expires = expiresAt,
                Claims = claims,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_option.SigninKey)), SecurityAlgorithms.HmacSha256)
            };

            var token = new JsonWebTokenHandler().CreateToken(descriptor);
            return new AccessToken(token, expiresAt);
          
        }

        public string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)).Replace('+', '-').Replace('/', '_').TrimEnd('=');
        }

        


    }

    /*Üç handler (Kullanıcı kayıt, Login, refresh token) aynı yapıyı kullanacak*/

    public class AuthTokenIssuer(ITokenService tokenService, IUnitOfWork uow)
    {
        //1. db'de kaydedilecek token'ın hash'i
        public static string Hash(string token) => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));
        //2. refresh token'ı db'ye ekler.

        public async Task<string> AddRefreshTokenAsync(User user, string? refreshPlain= null)
        {
            refreshPlain ??= tokenService.GenerateRefreshToken();

            var token = new RefreshToken
            {
                ExpiresAt = DateTime.Now.Add(tokenService.RefreshTokenLifeTime),
                Token = Hash(refreshPlain)
            };

            if (user.Id == 0)
            {
                token.User = user;
            }
            else
            {
                token.UserId = user.Id;
            }

            await uow.RefreshTokens.CreateAsync(token);
            return refreshPlain;
        }

        public AuthResponse CreateResponse(User user, string refreshPlain)
        {
            var access = tokenService.CreateAccessToken(user);
            return new AuthResponse(access.Value, refreshPlain, access.ExpiresAt);
        }

        
    }
}
