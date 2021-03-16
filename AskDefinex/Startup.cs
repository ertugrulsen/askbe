using AskDefinex.Business.Service;
using AskDefinex.Business.Service.Interface;
using AskDefinex.Common.Extension;
using AskDefinex.Common.Helper;
using AskDefinex.Common.Models;
using AskDefinex.Common.Utility;
using AskDefinex.DataAccess.DAO;
using AskDefinex.DataAccess.DAO.Interface;
using AskDefinex.DataAccess.Query;
using AskDefinex.Rest.Filter;
using AutoMapper;
using DefineXwork.Library.Api.Swagger;
using DefineXwork.Library.Cache;
using DefineXwork.Library.Caching.MemoryCache;
using DefineXwork.Library.Configuration;
using DefineXwork.Library.DataAccess.Helper;
using DefineXwork.Library.DataAccess.Manager;
using DefineXwork.Library.Integration;
using DefineXwork.Library.Logging;
using DefineXwork.Library.Logging.Database;
using DefineXwork.Library.Security;
using DefineXwork.Library.Security.Common;
using DefineXwork.Library.Security.Jwt;
using DefineXwork.Library.Transaction;
using DefineXwork.Library.Utility.Swagger.Attribute;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Text;

namespace AskDefinex
{
    public class Startup
    {
        private readonly JwtOptions _jwtOptions;
        private readonly AppSettings _appSettings;
        private readonly string[] _corsOrigins = { };
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _jwtOptions = Configuration.GetSection("JwtOptions").Get<JwtOptions>();
            _appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();
      

            string corsOriginValue = Configuration.GetValue<string>("AllowedOrigin");

            if (!string.IsNullOrEmpty(corsOriginValue))
            {
                _corsOrigins = corsOriginValue.Split(',', StringSplitOptions.RemoveEmptyEntries);
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {

                services.AddCors(options =>
                {
                    options.AddDefaultPolicy(
                        builder =>
                        {
                            var corsUrl= _corsOrigins[1].ToString();
                            builder.WithOrigins(corsUrl).AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithMethods("GET", "PUT", "POST", "DELETE", "UPDATE", "OPTIONS");
                        });

                });

                services.AddControllers(
                    opt =>
                    {
                        opt.Filters.Add(new ProducesAttribute("application/json"));
                    }
                    ).AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = _jwtOptions.Issuer,
                        ValidAudience = _jwtOptions.Audience,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey)),
                    };
                    options.EventsType = typeof(CustomJwtEventHandler);
                });

                services.AddApiVersioning(
                    o =>
                    {
                        o.ReportApiVersions = true;
                        o.AssumeDefaultVersionWhenUnspecified = true;
                        o.DefaultApiVersion = new ApiVersion(1, 0);
                        o.ApiVersionReader = new UrlSegmentApiVersionReader();
                    }
                    );

                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });

                //SWAGGER
                if (_appSettings.Swagger.Enabled)
                {
                    services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

                    services.AddSwaggerGen(options =>
                    {
                        options.OperationFilter<SwaggerDefaultValues>();
                    });
                }

                services.AddHttpContextAccessor();
                services.AddDistributedMemoryCache();

                services.AddSession();

                services.AddMemoryCache();

                services.AddAutoMapper(typeof(Startup));
                services.AddLogging();

                // Dapper Property Mapper
                TypeMapper.Initialize("AskDefinex.DataAccess.Model.Data");

                services.AddScoped<ILogManager<string>, DatabaseLogManager<string>>(x => new DatabaseLogManager<string>(x.GetService<IDatabaseLogManagerService>(), "MediaDashboard.Api"));
                services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
                services.AddSingleton<IConfigManager, ConfigHelper>();
                services.AddSingleton<IRecaptchaValidatorService, RecaptchaValidatorService>();


                services.AddTransient<ITransactionContextManager, TransactionContextManager>();
                services.AddTransient(typeof(ICacheManager<>), typeof(MemoryCacheManager<>));
                services.AddTransient<IHttpContextHelper, HttpContextHelper>();

                services.AddTransient<IUserContextModel, UserContextModel>();
                services.AddTransient(typeof(IUserContextManager<IUserContextModel>), typeof(UserContextManager));

                services.AddTransient<IJwtTokenHandler, JwtTokenHandler>();
                services.AddTransient<JwtOptions>();

                services.AddTransient<IUserContextService, UserContextService>();

                services.AddTransient<IRestIntegration, RestIntegration>();
                services.AddTransient<ISoapIntegration, SoapIntegration>();

                services.AddTransient<CustomJwtEventHandler>();

                services.AddTransient<IUserContextDAO, UserContextDAO>(x => new UserContextDAO(new MysqlDatabaseManager("DBConnection", x.GetService<IConfigManager>()), new MysqlQueryTemplate()));
                services.AddTransient<IDatabaseLogManagerDAO, DatabaseLogManagerDAO>(x => new DatabaseLogManagerDAO(new MysqlDatabaseManager("DBConnection", x.GetService<IConfigManager>()), new MysqlQueryTemplate()));

                services.AddScoped<CustomExceptionFilter>();

                //recaptchaValidatorService
                services.AddTransient<IRecaptchaValidatorService, RecaptchaValidatorService>();

                //askAuthenticationService
                services.AddTransient<IAskAuthenticationService, AskAuthenticationService>();
                services.AddTransient<IAskAuthenticationDAO, AskAuthenticationDAO>(x => new AskAuthenticationDAO(new MysqlDatabaseManager("DBConnection", x.GetService<IConfigManager>()), new MysqlQueryTemplate()));

                //askUserService
                services.AddTransient<IAskUserService, AskUserService>();
                services.AddTransient<IAskUserDAO, AskUserDAO>(x => new AskUserDAO(new MysqlDatabaseManager("DBConnection", x.GetService<IConfigManager>()), new MysqlQueryTemplate()));

                //AskBadge
                services.AddTransient<IAskBadgeService, AskBadgeService>();
                services.AddTransient<IAskBadgeDAO, AskBadgeDAO>(x => new AskBadgeDAO(new MysqlDatabaseManager("DBConnection", x.GetService<IConfigManager>()), new MysqlQueryTemplate()));

                //AskUserService
                services.AddTransient<IAskUserService, AskUserService>();
                services.AddTransient<IAskUserDAO, AskUserDAO>(x => new AskUserDAO(new MysqlDatabaseManager("DBConnection", x.GetService<IConfigManager>()), new MysqlQueryTemplate()));

                //AskQuestion
                services.AddTransient<IAskQuestionService, AskQuestionService>();
                services.AddTransient<IAskQuestionDAO, AskQuestionDAO>(x => new AskQuestionDAO(new MysqlDatabaseManager("DBConnection", x.GetService<IConfigManager>()), new MysqlQueryTemplate()));

                //AskAnswer
                services.AddTransient<IAskAnswerService, AskAnswerService>();
                services.AddTransient<IAskAnswerDAO, AskAnswerDAO>(x => new AskAnswerDAO(new MysqlDatabaseManager("DBConnection", x.GetService<IConfigManager>()), new MysqlQueryTemplate()));

                //AskQuestion
                services.AddTransient<IAskTagService, AskTagService>();
                services.AddTransient<IAskTagDAO, AskTagDAO>(x => new AskTagDAO(new MysqlDatabaseManager("DBConnection", x.GetService<IConfigManager>()), new MysqlQueryTemplate()));

                //AskComment
                services.AddTransient<IAskCommentService, AskCommentService>();
                services.AddTransient<IAskCommentDAO, AskCommentDAO>(x => new AskCommentDAO(new MysqlDatabaseManager("DBConnection", x.GetService<IConfigManager>()), new MysqlQueryTemplate()));

                services.AddElasticsearch(Configuration);

                ConfigHelper.LoadConfig(Configuration);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger, IApiVersionDescriptionProvider provider)
        {
            logger.LogTrace("Startup::Configure");
            logger.LogDebug($"Startup::Configure::Environment:{env.EnvironmentName}");
            try
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                else
                {
                    app.UseHsts();
                }

                app.UseCors();

                app.UseSession();


                app.UseRouting();
                app.UseAuthentication();
                app.UseAuthorization();

                app.UseMiddleware(typeof(SecurityMiddleware));

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
                app.UseRequestLocalization();

                //SWAGGER
                if (_appSettings.Swagger.Enabled)
                {
                    app.UseSwagger();
                    app.UseSwaggerUI(options =>
                    {
                        foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
                        {
                            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
