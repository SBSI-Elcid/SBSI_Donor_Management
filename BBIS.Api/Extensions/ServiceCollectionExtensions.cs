using BBIS.Application.ConnectionProvider;
using AutoMapper;
using BBIS.Application.Contracts;
using BBIS.Application.Repositories;
using BBIS.Application.Services;
using BBIS.Common;
using BBIS.Common.Encryption;
using BBIS.Common.Jwt;
using BBIS.Common.Logging;
using BBIS.Common.Settings;
using BBIS.Database;
using BBIS.Domain.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NinjaNye.SearchExtensions;
using System.Linq.Dynamic.Core;
using BBIS.Application.DTOs.Common;
using BBIS.Application.DTOs.Schedule;

namespace BBIS.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
           services.AddTransient<IRepositoryWrapper, RepositoryWrapper>();
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IUserAccountService, UserAccountService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IDonorTestOrderService, DonorTestOrderService>();
            services.AddTransient<ILookupService,LookupService>();
            services.AddTransient<IDonorScreeningService, DonorScreeningService>();
            services.AddTransient<IScheduleService, ScheduleServices>();
            services.AddTransient<IDonorRegistrationService, DonorRegistrationService>();
            services.AddTransient<IApplicationSettingService, ApplicationSettingService>();
            services.AddTransient<IInventoryService, InventoryService>();
            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<IRequisitionService, RequisitionService>();
            services.AddTransient<IDashboardService, DashboardService>();
            services.AddTransient<ITestOrderService, TestOrderService>();
            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<IPrintReportService, PrintReportService>();
            services.AddTransient<ISyncDataService, SyncDataService>();
            services.AddTransient<ISignatoryService, SignatoryService>();

        }

        public static void ConfigureMailSetting(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureMysqlDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("BloodbankDbConnectionString");
            services.AddDbContext<BBDbContext>(options => options.UseMySql(connectionString, MySqlServerVersion.LatestSupportedServerVersion), ServiceLifetime.Scoped);

            services.AddSingleton<IConnectionProvider, DbConnectionProvider>(s => new DbConnectionProvider(connectionString));

            var liveDbConnectionString = configuration.GetConnectionString("ExternalDbConnectionString");
            services.AddDbContext<ExternalBBDbContext>(options => options.UseMySql(liveDbConnectionString, MySqlServerVersion.LatestSupportedServerVersion), ServiceLifetime.Scoped);
        }

        public static void ConfigureCorsPolicy(this IServiceCollection services, string spaCorsPolicy)
        {           
            services.AddCors(options => options.AddPolicy(spaCorsPolicy, builder => builder
                 .AllowAnyOrigin()
                 .AllowAnyHeader()
                 .AllowAnyMethod()));
        }

        public static void ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtHandler = new JwtHandler(configuration);
            services.AddSingleton<IJwtHandler, JwtHandler>(h => jwtHandler);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => { options.TokenValidationParameters = jwtHandler.Parameters; });
            services.AddTransient<IEncryptionHandler, EncryptionHandler>();
        }

        public static void ConfigureAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(ApplicationRoles.AdminPolicy, policy =>
                {
                    policy.RequireRole(ApplicationRoles.AdminRole);
                });

                options.AddPolicy(ApplicationRoles.TestOrderPolicy, policy =>
                {
                    policy.RequireRole(
                        ApplicationRoles.AdminRole,
                        ApplicationRoles.MedTechRole
                    );
                });

                options.AddPolicy(ApplicationRoles.DonorAdminPolicy, policy =>
                {
                    policy.RequireRole(
                        ApplicationRoles.DonorAdminRole,
                        ApplicationRoles.AdminRole
                    );
                });

                options.AddPolicy(ApplicationRoles.DonorScreeningPolicy, policy =>
                {
                    policy.RequireRole(
                        ApplicationRoles.AdminRole,
                        ApplicationRoles.DonorAdminRole,
                        ApplicationRoles.InitialScreenerRole,
                        ApplicationRoles.PhysicalExamScreenerRole,
                        ApplicationRoles.BloodCollectorRole);
                });

                options.AddPolicy(ApplicationRoles.InventoryPolicy, policy =>
                {
                    policy.RequireRole(ApplicationRoles.InventoryUserRole, ApplicationRoles.AdminRole);
                });

                options.AddPolicy(ApplicationRoles.RequisitionPolicy, policy =>
                {
                    policy.RequireRole(
                        ApplicationRoles.RequisitionUserRole, 
                        ApplicationRoles.AdminRole);
                });

                options.AddPolicy(ApplicationRoles.InitialScreeningPolicy, policy =>
                {
                    policy.RequireRole(
                        ApplicationRoles.AdminRole,
                        ApplicationRoles.DonorAdminRole,
                        ApplicationRoles.InitialScreenerRole);
                });

                options.AddPolicy(ApplicationRoles.PhysicalExaminationPolicy, policy =>
                {
                    policy.RequireRole(
                        ApplicationRoles.AdminRole,
                        ApplicationRoles.DonorAdminRole,
                        ApplicationRoles.PhysicalExamScreenerRole);
                });

                options.AddPolicy(ApplicationRoles.BloodCollectionPolicy, policy =>
                {
                    policy.RequireRole(
                        ApplicationRoles.AdminRole,
                        ApplicationRoles.DonorAdminRole,
                        ApplicationRoles.BloodCollectorRole);
                });

            });
        }

        public static void ConfigureSwaggerOption(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Blood bank Investory System API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.\r\n\r\nEnter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
        }

        public static void ConfigureTimedHostedService(this IServiceCollection services)
        {
            services.AddHostedService<TimedHostedService>();
        }
    }
}
