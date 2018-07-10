using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using iBookmar.Repositories.Repositories;
using iBookmark.Helper.Database;
using iBookmark.Model.AggregatesModel.ContainerAggregate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System.Configuration;
using iBookmark.Infrastructure.Helpers;
using iBookmark.Helpers.Network;
using iBookmark.API.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using iBookmark.Domain.AggregatesModel.BookmarkAggregate;
using iBookmark.Infrastructure.Repositories;

namespace iBookmark.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

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
            if (HostingEnvironment.IsDevelopment())
            {
                services.AddCors(action => action.AddPolicy("AllowAnyOrigin",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            }

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

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "InsuTech V1");
            });
        }
    }
}
