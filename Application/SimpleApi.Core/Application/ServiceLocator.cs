using Microsoft.Extensions.DependencyInjection;
using System;

namespace SimpleApi.Core
{
    //服务定位器
    public static class ServiceLocator
    {
        public static IServiceProvider Instance { get; set; }
        public static T GetService<T>() where T : class
        {
            return Instance.GetService<T>();
        }

    }
}
