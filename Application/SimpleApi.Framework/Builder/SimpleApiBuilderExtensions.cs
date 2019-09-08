using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleApi.Core;
using SimpleApi.Data;
using SimpleApi.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleApi.Framework
{
    public static class SimpleApiBuilderExtensions
    {
        public static IServiceCollection RegisterApp(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddOptions();

            services.AddDbContext<SADbContext>(opt =>
            {
                var sqlServerConnstr = Configuration.GetConnectionString("MSSQLConnectString");
                //优先使用Sqlserver
                if (!string.IsNullOrEmpty(sqlServerConnstr))
                {
                    opt.UseSqlServer(sqlServerConnstr, sql => sql.UseRowNumberForPaging());
                }
                else
                {
                    throw new NotImplementedException("请配置数据库连接字符串");
                }
            }).AddUnitOfWork<SADbContext>();

            services.AddHttpContextAccessor();

            services.AddHttpClient();

            return services;
        }

        public static IServiceProvider BuildApp(this IServiceCollection services)
        {
            //构建容器
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ServiceLocator.Instance = serviceProvider;
            return serviceProvider;
        }

        /// <summary>
        /// header映射问题
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseFixHeadersMap(this IApplicationBuilder app)
        {
            var forwardingOptions = new ForwardedHeadersOptions()
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost
            };
            forwardingOptions.KnownNetworks.Clear();
            forwardingOptions.KnownProxies.Clear();
            app.UseForwardedHeaders(forwardingOptions);

            return app;
        }
    }
}
