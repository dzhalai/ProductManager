using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace ProductManager
{
    using Models;

    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDbContext<AdventureWorksContext>(options => options.UseSqlServer(_configuration.GetConnectionString(Constants.Database.ConnectionStringName)));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(Constants.Swagger.Version, new Info { Title = Constants.Swagger.Name, Version = Constants.Swagger.Version });
            });

            ConfigureSerilog();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(Constants.Swagger.Url, Constants.Swagger.Name);
            });
        }

        private void ConfigureSerilog()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Error()
                .Enrich.FromLogContext()
                .WriteTo.File(
                    Constants.Serilog.Path,
                    fileSizeLimitBytes: Constants.Serilog.FileSizeLimitBytes,
                    rollOnFileSizeLimit: Constants.Serilog.RollOnFileSizeLimit,
                    shared: Constants.Serilog.Shared,
                    flushToDiskInterval: TimeSpan.FromSeconds(Constants.Serilog.FlushToDiskIntervalInSeconds))
                .CreateLogger();
        }
    }
}
