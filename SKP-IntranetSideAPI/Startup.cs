using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SKP_IntranetSideAPI.Cruds;
using SKP_IntranetSideAPI.DB_Settings;

namespace SKP_IntranetSideAPI
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
            services.Configure<LoginDBSettings>(
                Configuration.GetSection(nameof(LoginDBSettings)));
            services.AddSingleton<ILoginDBSettings>(x =>
                x.GetRequiredService<IOptions<LoginDBSettings>>().Value);
            services.AddSingleton<LoginCrud>();

            services.Configure<UserDBSettings>(
               Configuration.GetSection(nameof(UserDBSettings)));
            services.AddSingleton<IUserDBSettings>(x =>
                x.GetRequiredService<IOptions<UserDBSettings>>().Value);
            services.AddSingleton<UserCrud>();

            services.Configure<SaltDBSettings>(
                Configuration.GetSection(nameof(SaltDBSettings)));
            services.AddSingleton<ISaltDBSettings>(x =>
                x.GetRequiredService<IOptions<SaltDBSettings>>().Value);
            services.AddSingleton<SaltCrud>();

            services.Configure<ProjectDBSettings>(
                Configuration.GetSection(nameof(ProjectDBSettings)));
            services.AddSingleton<IProjectDBSettings>(x =>
                x.GetRequiredService<IOptions<ProjectDBSettings>>().Value);
            services.AddSingleton<ProjectCrud>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });


            // services.AddMvc();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();


            app.UseCors("CorsPolicy");


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
