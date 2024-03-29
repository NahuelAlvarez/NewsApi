using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NewsApi.Services;
using System.Text.Json.Serialization;

namespace NewsApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            //services.AddCors(options =>
            //{
            //    options.AddPolicy(name: "MyPolicy",
            //                policy =>
            //                {
            //                    policy.WithOrigins("https://newsapiapi.azure-api.net",
            //                        "http://localhost:26163",
            //                        "https://localhost:5001",
            //                        "http://localhost:5000",
            //                        "http://localhost:3000",
            //                        "https://newsproyectfrontend.azurewebsites.net/")
            //                            .AllowAnyMethod()
            //                            .AllowAnyHeader();
            //                });
            //});

            services.AddScoped<INewsServices, NewsServices>();

            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NewsApi", Version = "v1" });
            });

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               
            }
            app.UseCors(
                options => options.WithOrigins("https://newsapiapi.azure-api.net",
                                        "http://example.com",
                                        "http://localhost:26163",
                                        "https://localhost:5001",
                                        "http://localhost:5000",
                                        "http://localhost:3000",
                                        "https://newsproyectfrontend.azurewebsites.net/")
                                         .AllowAnyOrigin().AllowAnyHeader().AllowCredentials().AllowAnyMethod()
            );

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NewsApi v1"));

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
