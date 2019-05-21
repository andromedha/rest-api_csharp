using System;
using System.Text;
using DataClasses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonManagement;
using PersonManagement.Contracts;
using Repository.Contracts;
using Repository.Csv;
using Repository.Sql;
using Repository.Sql.DbContext;
using Uebung_RestAPI.ExceptionMiddleware;

namespace Uebung_RestAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(options =>
            {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });
            var configuration = new Configuration();
            Configuration.Bind("Configuration", configuration);
            if(configuration.CsvConfiguration == null || configuration.SqlConfiguration == null)
            {
                throw new Exception("An error occured while reading the section \"Configuration\" in appsettings.json .");
            }
            services.AddSingleton<CsvConfiguration>(configuration.CsvConfiguration);
            services.AddScoped<IPersonManager, PersonManager>();
            services.AddScoped<IRepository, CsvRepository>();
            //services.AddScoped<IRepository, SqlRepository>();
            services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer(BuildConnectionstring(configuration.SqlConfiguration), provideroptions => provideroptions.CommandTimeout(30)));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.ConfigureExceptionMiddleware();
            app.UseMvc();
            
        }

        private String BuildConnectionstring (SqlConfiguration configuration)
        {
            var builder = new StringBuilder();
            builder.AppendFormat("Server={0};", configuration.Server ?? throw new Exception());
            builder.AppendFormat("Database={0};", configuration.Database ?? throw new Exception());
            builder.AppendFormat("User ID={0};", configuration.User ?? throw new Exception());
            builder.AppendFormat("Password={0};", configuration.Password ?? throw new Exception());
            builder.AppendFormat("Trusted_Connection={0};", configuration.TrustedConnection.ToString());
            return builder.ToString();
        }
    }
}
