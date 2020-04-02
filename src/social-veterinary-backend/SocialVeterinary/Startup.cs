using SocialVeterinary.Api.Infrastructure;

namespace SocialVeterinary.Api
{
    using System.Reflection;

    using AutoMapper;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;

    using Newtonsoft.Json.Converters;

    using SocialVeterinary.Data;
    using SocialVeterinary.Domain.Interfaces;

    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options => { options.SerializerSettings.Converters.Add(new StringEnumConverter()); });

            services.AddTransient<IPetRepository, PetRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();

            services.AddAutoMapper(config =>
                {
                    config.AddProfile(new MapperProfile());
                }, new Assembly[0]);

            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                });

            // For test purposes only to simplify backend and frontend interactions
            services.AddCors(options =>
                {
                    options.AddPolicy(MyAllowSpecificOrigins,
                        builder =>
                            {
                                builder.WithOrigins("http://localhost:4200")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                            });
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(MyAllowSpecificOrigins);

            app.UseSwagger(c =>
                {
                    c.RouteTemplate = "api/swagger/{documentName}/swagger.json";
                });

            app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "Social Veterinary Api");
                    c.RoutePrefix = "api/swagger";
                });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
