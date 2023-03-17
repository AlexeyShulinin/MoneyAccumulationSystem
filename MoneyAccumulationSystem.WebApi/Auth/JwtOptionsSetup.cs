﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace MoneyAccumulationSystem.WebApi.Auth;

public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
    private const string SectionName = "Jwt";
    private readonly IConfiguration configuration;

    public JwtOptionsSetup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    
    public void Configure(JwtOptions options)
    {
        configuration.GetSection(SectionName).Bind(options);
    }
}