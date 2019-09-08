using AutoMapper.Attributes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleApi.Data;
using SimpleApi.Framework;
using SimpleApi.Services.User;
using SimpleApi.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterApp(Configuration);

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDistributedRedisCache(o =>
            {
                Configuration.GetSection("RedisCacheSetting").Bind(o);
            });

            services.AddSession(options =>
            {
                options.Cookie.Name = "PHPSESSION";
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddScoped<IUserService, UserService>();

            AutoMapper.Mapper.Initialize(config =>
            {
                typeof(Services.User.Dtos.UserInfoInputDto).Assembly.MapTypes(config);
            });

            services.AddMvcCore().AddJsonFormatters();

            services.AddMvc()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .ConfigureApiBehaviorOptions(options =>
            {
                //options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressInferBindingSourcesForParameters = true;
                //模型验证策略
                options.SuppressModelStateInvalidFilter = true;
                //options.SuppressMapClientErrors = true;
            })
            .AddJsonOptions(op =>
            {
                //骆驼命名法可使用DefaultContractResolver
                //op.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                //日期返回格式
                op.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm";
                //忽略null值返回
                //op.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                //忽略循环引用
                op.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                //忽略缺失的属性
                //op.SerializerSettings.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
            });

            services.BuildApp();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseFixHeadersMap();

            app.UseCookiePolicy();
            app.UseSession();

            loggerFactory.AddLog4Net("log4net.config");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseStaticFiles();
        }
    }
}
