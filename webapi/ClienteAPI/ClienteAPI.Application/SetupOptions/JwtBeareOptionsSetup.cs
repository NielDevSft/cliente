﻿using ClienteAPI.Application.Authentications;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ClienteAPI.Application.SetupOptions
{
    public class JwtBeareOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>, IOptions<JwtBearerOptions>
    {
        private readonly JwtOptions _jwtOptions;
        public JwtBeareOptionsSetup(IOptions<JwtOptions> options)
        {
            _jwtOptions = options.Value;
        }

        public JwtBearerOptions Value { get; private set; } = null!;

        public void Configure(JwtBearerOptions options)
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtOptions.Issuer,
                ValidAudience = _jwtOptions.Audience,
                ClockSkew = TimeSpan.FromMinutes(1),
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtOptions.SecretKey))
            };
            Value = options;
        }

        public void Configure(string? name, JwtBearerOptions options)
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtOptions.Issuer,
                ValidAudience = _jwtOptions.Audience,
                ClockSkew = TimeSpan.FromMinutes(1),
                IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(_jwtOptions.SecretKey))
            };
            Value = options;
        }
    }
}
