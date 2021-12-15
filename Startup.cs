using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Prometheus;
using Prometheus.DotNetRuntime;

namespace docker_observabilidade
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Collector = CreateCollector();
        }

        public IConfiguration Configuration { get; }

        public static IDisposable Collector;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services
                .AddSwaggerGen(c =>
                {
                    c
                        .SwaggerDoc("v1",
                        new OpenApiInfo {
                            Title = "docker_observabilidade",
                            Version = "v1"
                        });
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app
                    .UseSwaggerUI(c =>
                        c
                            .SwaggerEndpoint("/swagger/v1/swagger.json",
                            "docker_observabilidade v1"));
            }

            app.UseHttpMetrics();
            app.UseMetricServer();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }

        public static IDisposable CreateCollector()
        {
            DotNetRuntimeStatsBuilder.Builder builder =
                DotNetRuntimeStatsBuilder.Default();

            builder =
                DotNetRuntimeStatsBuilder
                    .Customize()
                    .WithContentionStats(CaptureLevel.Informational)
                    .WithGcStats(CaptureLevel.Verbose)
                    .WithThreadPoolStats(CaptureLevel.Informational)
                    .WithExceptionStats(CaptureLevel.Errors)
                    .WithJitStats();

            builder.RecycleCollectorsEvery(new TimeSpan(0, 20, 0));

            return builder.StartCollecting();
        } 
    }
}
