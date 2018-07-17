using iBookmar.Repositories.Repositories;
using iBookmark.API.Auth;
using iBookmark.API.Filters;
using iBookmark.API.Middlewares;
using iBookmark.API.Options;
using iBookmark.Domain.AggregatesModel.BookmarkAggregate;
using iBookmark.Helper.Database;
using iBookmark.Helper.Security;
using iBookmark.Helpers.Network;
using iBookmark.Infrastructure.Repositories;
using iBookmark.Model.AggregatesModel.ContainerAggregate;
using iBookmark.Model.AggregatesModel.UserAggregate;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Security.Claims;
using System.Text;

namespace iBookmark.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        private const string SecretKey = "needtogetthisfromenvironment";
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(option => option.Filters.Add(typeof(ModelStateValidationsFilterAttribute)));
            services.Configure<MvcOptions>(x => x.Filters.Add(new CorsAuthorizationFilterFactory("AllowAnyOrigin")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "iBookmark API - V1",
                        Version = "v1",
                        Contact = new Contact() { Email = "mrlokeshagarwal@gmail.com", Name = "Lokesh Agarwal", Url = "www.iBookmark.com" }
                    }
                 );

                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "iBookmark.API.xml");
                c.IncludeXmlComments(filePath);
            });
            var connectionString = Configuration.GetValue<string>("Data:BookmarkConnection:ConnectionString");
            Polly.IAsyncPolicy[] databaseAsyncPolicies = GetDatabaseAsyncPolicies();
            Polly.ISyncPolicy[] databaseSyncPolicies = GetDatabaseSyncPolicies();
            services.AddSingleton<IUserRepository>(
                repo => new UserSqlRepository(() =>
                {
                    var con = new PollySqlConnection(connectionString, databaseAsyncPolicies, databaseSyncPolicies);
                    con.Open();
                    return con;
                }
                )
            );

            services.AddSingleton<IContainerRepository>(
                repo => new ContainerSqlRepository(() =>
                    {
                        var con = new PollySqlConnection(connectionString, databaseAsyncPolicies, databaseSyncPolicies);
                        con.Open();
                        return con;
                    }
                )
            );

            services.AddSingleton<IBookmarkRepository>(
                repo => new BookmarkSqlRepository(() =>
                {
                    var con = new PollySqlConnection(connectionString, databaseAsyncPolicies, databaseSyncPolicies);
                    con.Open();
                    return con;
                }
                )
            );
            services.AddScoped<IJwtFactory, JwtFactory>();
            var saltKey = Configuration.GetValue<string>("Security:SaltKey");
            var keySize = Configuration.GetValue<int>("Security:KeySize");
            var passPhrase = Configuration.GetValue<string>("Security:PassPhrase");
            services.AddSingleton<IEncryptorDecryptor>(en => new RijndaelManagedEncryptorDecryptor(saltKey, keySize, passPhrase));
            if (HostingEnvironment.IsDevelopment())
            {
                services.AddCors(action => action.AddPolicy("AllowAnyOrigin",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            }

            #region JWT Token
            var JwtAppSettingsTokenOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            services.Configure<JwtIssuerOptions>(options => {
                options.Issuer = JwtAppSettingsTokenOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = JwtAppSettingsTokenOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = JwtAppSettingsTokenOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = false,
                ValidAudience = JwtAppSettingsTokenOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero,
            
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = JwtAppSettingsTokenOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AddContainer", policy => policy.RequireClaim(ClaimTypes.Role, "add:container"));
                options.AddPolicy("AddBookmark", policy => policy.RequireClaim(ClaimTypes.Role, "add:bookmark"));

            });
            #endregion

        }

        private Polly.IAsyncPolicy[] GetDatabaseAsyncPolicies()
        {
            var maxRetry = Configuration.GetValue<int>("Polly:MaxRetry");
            var pauseBetweenFailuresInMilliseconds = Configuration.GetValue<int>("Polly:PauseBetweenFailuresInMilliseconds");
            var timeoutInSeconds = Configuration.GetValue<int>("Polly:TimeoutInSeconds");
            var databasePolicies = PollyPolicyHelper.GetStandardDatabaseAsyncPolicies(maxRetry, pauseBetweenFailuresInMilliseconds, timeoutInSeconds);
            return databasePolicies;
        }

        private Polly.ISyncPolicy[] GetDatabaseSyncPolicies()
        {
            var maxRetry = Configuration.GetValue<int>("Polly:MaxRetry");
            var pauseBetweenFailuresInMilliseconds = Configuration.GetValue<int>("Polly:PauseBetweenFailuresInMilliseconds");
            var timeoutInSeconds = Configuration.GetValue<int>("Polly:TimeoutInSeconds");
            var databasePolicies = PollyPolicyHelper.GetStandardDatabaseSyncPolicies(maxRetry, pauseBetweenFailuresInMilliseconds, timeoutInSeconds);
            return databasePolicies;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseErrorHandlingMiddleWare();
            app.UseAuthentication();                      
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "InsuTech V1");
            });
            app.UseMvc();
        }
    }
}
