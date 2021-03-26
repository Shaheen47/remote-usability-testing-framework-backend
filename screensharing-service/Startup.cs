using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using screensharing_service.Contracts.Repositories;
using screensharing_service.Contracts.Services;
using screensharing_service.Hubs;
using screensharing_service.Mappings;
using screensharing_service.Repositories;
using screensharing_service.Services;


namespace screensharing_service
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

            services.AddCors(options => options. AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed (_ => true)
                        .AllowCredentials();
                }));
            
            services.AddAutoMapper(typeof(Maps));
            
            services.AddControllers();
            services.AddSignalR(o =>
            {
                o.MaximumReceiveMessageSize = null;
            });
            services.AddSingleton<ISessionService, SessionService>();
            services.AddSingleton<IScreenMirroringRepository, DumbScreenMirroringRepository>();
            
            services.AddSingleton<IScreenEventsRecordingService, ScreenEventsRecordingService>();
            services.AddSingleton<IScreenEventsReplyService, ScreenEventsReplyService>();
            
            services.AddSingleton<ISessionRepository, SessionRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { 
                endpoints.MapControllers();
                endpoints.MapHub<ScreenMirroringHub>("/ScreenMirroringHub");
                endpoints.MapHub<ScreenMirroringHubWithRecording>("/ScreenMirroringHubWithRecording");
                endpoints.MapHub<ScreenMirroringReplyControllingHub>("/ScreenMirroringReplyControllingHub");
            });
            
            
        }
        
    }
}