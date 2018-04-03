using System;
using System.Threading.Tasks;
using ContosoMaintenance.WebAPI.Services;
using ContosoMaintenance.WebAPI.Services.BlobStorage;
using ContosoMaintenance.WebAPI.Services.StorageQueue;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContosoMaintenance.WebAPI
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
            // Inject Configuration
            services.AddSingleton<IConfiguration>(Configuration);

            // Inject Blob Storage
            services.AddScoped<IAzureBlobStorage>(factory =>
            {
                return new AzureBlobStorage(new AzureBlobSettings(
                    Configuration["AzureStorage:StorageAccountName"],
                    Configuration["AzureStorage:Key"],
                    Configuration["AzureStorage:PhotosBlobContainerName"]));
            });

            // Inject Storage Queue
            services.AddScoped<IAzureStorageQueue>(factory =>
            {
                return new AzureStorageQueue(new AzureStorageQueueSettings(
                    Configuration["AzureStorage:StorageAccountName"],
                    Configuration["AzureStorage:Key"],
                    Configuration["AzureStorage:QueueName"]));
            });

            // Add Azure Active Directory B2C Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.Audience = Configuration["ActiveDirectory:ApplicationId"];
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = AuthenticationFailed
                };

                var authorityBase = $"https://login.microsoftonline.com/tfp/{Configuration["ActiveDirectory:Tenant"]}/";
                options.Authority = $"{authorityBase}{Configuration["ActiveDirectory:SignUpSignInPolicy"]}/v2.0/";
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
        }

        Task AuthenticationFailed(AuthenticationFailedContext arg)
        {
            Console.WriteLine(arg.Exception.Message);
            return Task.FromResult(0);
        }
    }
}
