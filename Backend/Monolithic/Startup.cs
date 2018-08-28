using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using ContosoMaintenance.WebAPI.Helpers;
using ContosoMaintenance.WebAPI.Services;
using ContosoMaintenance.WebAPI.Services.BlobStorage;
using ContosoMaintenance.WebAPI.Services.StorageQueue;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace ContosoMaintenance.WebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        // public Startup(IHostingEnvironment env)
        // {
        //     var builder = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        //        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        //        .AddEnvironmentVariables();

        //     Configuration = builder.Build();
        // }

        public Startup(IConfiguration configuration)
        {            
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Inject Configuration
            services.AddSingleton<IConfiguration>(Configuration);

            // Inject Blob Storage
            services.AddScoped<IAzureBlobStorage>(factory =>
            {
                try
                {
                    return new AzureBlobStorage(new AzureBlobSettings(
                        Configuration["AzureStorage:StorageAccountName"],
                        Configuration["AzureStorage:Key"],
                        Constants.PhotosBlobContainerName));
                }
                catch (ArgumentException)
                {
                    throw new ArgumentException("No Azure Storage connection found at specified URL. Please make sure to provide a valid Storage Cofiguration in the Application Settigs.");
                }
            });

            // Inject Storage Queue
            services.AddScoped<IAzureStorageQueue>(factory =>
            {
                return new AzureStorageQueue(new AzureStorageQueueSettings(
                    Configuration["AzureStorage:StorageAccountName"],
                    Configuration["AzureStorage:Key"],
                    Constants.QueueName));
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

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    // Pretty Print Swagger output JSON
                    options.SerializerSettings.Formatting = Formatting.Indented;
                });

            // Configure the Swagger generator
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("1.0", new Info { Title = "Contoso Maintenance API", Version = "1.0" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // As this is a demo, we always show the rich exception page.
            // Make sure to hide it in production environments in real-world scearios to 
            // hide your code from attackers.
            app.UseDeveloperExceptionPage();
                    
            // As this is a demo, we always show the rich exception page.
            // Make sure to hide it in production environments in real-world scearios to 
            // hide your code from attackers.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            // Activate Swagger and cofigure its UI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/1.0/swagger.json", "Contoso Maintenance API 1.0");
                c.RoutePrefix = string.Empty; // Makes Swagger UI the root page
            });

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
