
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using session_service.Contracts.Proxies;
using session_service.Contracts.Repositories;
using session_service.Contracts.Services;
using session_service.Hubs;
using session_service.Proxies;
using session_service.Repositories;
using session_service.Services;
using session_service.Mappings;

namespace session_service
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
            services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder =>
                {
                    builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .SetIsOriginAllowed((host) => true)
                        .AllowCredentials();
                }));
            services.AddControllers();
            services.AddSignalR();
            
            services.AddAutoMapper(typeof(Maps));
            
            services.AddSingleton<ISessionRepository, DumbSessionRepo>();
            services.AddSingleton<IChatRepository, DumbChatRepo>();
            
            services.AddSingleton<ISessionService, SessionService>();
            services.AddSingleton<IChatService, ChatService>();
            services.AddSingleton<IUserService, UserService>();
            
            services.AddSingleton<IVideoConferencingServiceProxy, VideoConferencingServiceProxy>();
            services.AddSingleton<IScreensharingServiceProxy, ScreensharingServiceProxy>();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/ChatHub");
                endpoints.MapHub<ChatHubWithRecording>("/ChatHubWithRecording");
            });
        }
    }
}