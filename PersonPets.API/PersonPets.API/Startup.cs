using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PersonPets.API.ApiClients;
using PersonPets.API.ApiClients.Interfaces;
using PersonPets.API.Common;
using PersonPets.API.Common.Interfaces;
using PersonPets.API.Models;
using PersonPets.API.Services;
using PersonPets.API.Services.Interfaces;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace PersonPets.API
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AGL Test Api", Version = "v1" });
            });
            ConfigureCors(services);
            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "PersonPets.API");
                c.RoutePrefix = "";
            });

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IJsonSerializer, JsonSerializer>();
            services.AddScoped<IApiClient>(x => new ApiClient(Configuration.GetSection(Constants.HostUrl).Value.ToString(), new JsonSerializer()));
            services.AddScoped<IPeopleApiClient, PeopleApiClient>();

            services.AddScoped<IValidatorService<Person>, ValidatorService>();
            services.AddScoped<IPeopleService, PeopleService>();
        }

        protected void ConfigureCors(IServiceCollection services, string origin = null)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyMethod().AllowAnyHeader();
                    if (string.IsNullOrEmpty(origin))
                    {
                        builder.AllowAnyOrigin();
                    }
                    else
                    {
                        builder.WithOrigins(origin);
                    }
                });
            });
        }
    }
}
