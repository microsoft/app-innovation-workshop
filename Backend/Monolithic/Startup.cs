using ContosoMaintenance.WebAPI.Models;
using ContosoMaintenance.WebAPI.Services;
using ContosoMaintenance.WebAPI.Services.BlobStorage;
using ContosoMaintenance.WebAPI.Services.StorageQueue;
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
            
            services.AddScoped<IAzureBlobStorage>(factory =>
            {
                return new AzureBlobStorage(new AzureBlobSettings(
                    storageAccount: Configuration["AzureStorage:Blob_StorageAccount"],
                    storageKey: Configuration["AzureStorage:Blob_StorageKey"],
                    containerName: Configuration["AzureStorage:Blob_ContainerName"]));
            });

            services.AddScoped<IAzureStorageQueue>(factory =>
            {
                return new AzureStorageQueue(new AzureStorageQueueSetings(
                    Configuration["AzureStorage:Blob_StorageAccount"],
                    Configuration["AzureStorage:Blob_StorageKey"],
                    Configuration["AzureStorage:Queue_Name"]));
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

            app.UseMvc();
        }

        async void CreateDummyData()
        {
            var jobsData = new DocumentDBRepositoryBase<Job>();
            var jobCount = jobsData.GetItemsCount();
            if(jobCount == 0)
            {
                var dummyData = new DummyData.DummyData();
                var dummyJobs = dummyData.Jobs;
                foreach(var job in dummyJobs)
                {
                    await jobsData.CreateItemAsync(job);
                }
            }

            var customersData = new DocumentDBRepositoryBase<Customer>();
            var customersCount = customersData.GetItemsCount();
            if (customersCount == 0)
            {
                var dummyData = new DummyData.DummyData();
                var dummyCustomers = dummyData.Customers;
                foreach (var customer in dummyCustomers)
                {
                    await customersData.CreateItemAsync(customer);
                }
            }

            var employeesData = new DocumentDBRepositoryBase<Employee>();
            var employeesCount = employeesData.GetItemsCount();
            if (employeesCount == 0)
            {
                var dummyData = new DummyData.DummyData();
                var dummyEmployees = dummyData.Employees;
                foreach (var employee in dummyEmployees)
                {
                    await employeesData.CreateItemAsync(employee);
                }
            }

            var addressData = new DocumentDBRepositoryBase<Location>();
            var addressCount = addressData.GetItemsCount();
            if (addressCount == 0)
            {
                var dummyData = new DummyData.DummyData();
                var dummyAddress = dummyData.Addresses;
                foreach (var address in dummyAddress)
                {
                    await addressData.CreateItemAsync(address);
                }
            }

        }
    }
}
