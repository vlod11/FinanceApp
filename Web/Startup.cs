using AutoMapper;
using Common.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Services;
using Web.Extensions.StartupExtensions;
using Web.Helpers;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        private IConfiguration Configuration { get; }
        private IWebHostEnvironment WebHostEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var mongoDbName = Configuration["MongoDb:DatabaseName"];
            var mongoDbConnectionString = Configuration["MongoDb:ConnectionString"];
            var mongoConnectionUrl = new MongoUrl(mongoDbConnectionString);
            var mongoClientSettings = MongoClientSettings.FromUrl(mongoConnectionUrl);

            services.AddSingleton(x => new MongoClient(mongoClientSettings));
            services.AddSingleton(x => x.GetService<MongoClient>().GetDatabase(mongoDbName));

            services.AddTransient(typeof(SeedDatabase));

            services.AddRepositories();
            services.AddServices();
            services.AddControllers();

            services.AddSwagger();

            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddScoped<IDateService, DateService>();
            services.AddScoped<IServiceResultMapper, ServiceResultMapper>();

            services.AddApiVersioning(
                o =>
                {
                    o.AssumeDefaultVersionWhenUnspecified = true;
                    o.DefaultApiVersion = new ApiVersion(1, 0);
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IWebHostEnvironment env,
            SeedDatabase seedDatabase)
        {
            //TODO: add only friendly origins
            app.UseCors(options =>
            {
                options.AllowAnyMethod();
                options.AllowAnyHeader();
                options.AllowAnyOrigin();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI();

#if DEBUG
            seedDatabase.Seed();
#endif
            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}