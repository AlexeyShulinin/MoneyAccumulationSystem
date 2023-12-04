using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using MoneyAccumulationSystem.Database.EF;
using MoneyAccumulationSystem.Repositories.UnitOfWork;
using MoneyAccumulationSystem.Services.Extensions;
using MoneyAccumulationSystem.Services.Reports;
using MoneyAccumulationSystem.Services.Reports.Excel;
using MoneyAccumulationSystem.WebApi.Auth;
using MoneyAccumulationSystem.WebApi.Services;
using MoneyAccumulationSystem.WebApi.Swagger;

namespace MoneyAccumulationSystem.WebApi;

public static class ServiceCollectionExtensions
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddAndConfigureAuthentication(configuration);
        services.AddEndpointsApiExplorer();
        services.AddAndConfigureSwagger();
        services.AddAndConfigureMediatr();
        services.AddAndConfigureValidation();

        services.AddAndConfigureDatabase(configuration);
        services.AddAndConfigureUnitOfWork();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddHostedService<DatabaseDeploymentBackgroundService>();

        services.AddScoped<IReportService, ExcelReportService>();
        
        services.AddHostedService<SeedBackgroundService>();
    }

    private static void AddAndConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
        
        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        
        /*services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"] ?? throw new InvalidOperationException())),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true
            };
        });*/
        
        services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"] ?? throw new InvalidOperationException())),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true
            };
        });
        
        services.AddAuthorization();
    }
    
    private static void AddAndConfigureSwagger(this IServiceCollection services)
    {
        services.AddApiVersioning(setup =>
        {
            setup.DefaultApiVersion = new ApiVersion(1, 0);
            setup.AssumeDefaultVersionWhenUnspecified = true;
            setup.ReportApiVersions = true;
        });
        
        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });
        
        services.AddSwaggerGen();

        services.ConfigureOptions<SwaggerOptions>();
    }

    private static void AddAndConfigureUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddAndConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {

#if USE_SQL_SERVER
        services.AddDbContext<AccumulationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("MoneyAccumulationSystem")));
#endif
        
        services.AddDbContext<MasDbContext>(options =>
        {
            options.UseInMemoryDatabase("AccumulationApp");
            options.EnableSensitiveDataLogging();
        });
    }
    
    public static IServiceCollection Replace<T>(this IServiceCollection services, object instance)
        => services.Replace(new ServiceDescriptor(typeof(T), instance));
    
    public static bool Remove<T>(this IServiceCollection services)
        => services.Remove(new ServiceDescriptor(typeof(T), services
            .First(x => x.ServiceType == typeof(T))));
}