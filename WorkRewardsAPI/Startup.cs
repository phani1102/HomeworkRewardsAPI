using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WorkRewards.Data;
using WorkRewards.Data.Interface;
using WorkRewards.Manager;
using WorkRewards.Manager.Interface;
using WorkRewardsAPI.Middleware;

namespace WorkRewardsAPI
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
            //   services.AddTokenAuthentication(Configuration);

            services.AddCors();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IUserData, UserData>();
            services.AddTransient<IDropdownManager, DropdownManager>();
            services.AddTransient<IDropdownData, DropdownData>();
            services.AddTransient<IRewardData, RewardData>();
            services.AddTransient<IRewardManager, RewardManager>();
            services.AddTransient<ITaskData, TaskData>();
            services.AddTransient<ITaskManager, TaskManager>();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Work Rewards Service API",
                    Version = "v1",
                    Description = "Work Rewards Services",
                });
            });
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
            // app.UseAuthentication();
            // app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Work Rewards Services"));
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
