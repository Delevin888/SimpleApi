using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SimpleApi.Data
{
    /// <summary>
    /// CodeFirst 生成数据库的配置
    /// </summary>
    public class SADbContextFactory : IDesignTimeDbContextFactory<SADbContext>
    {
        public SADbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SADbContext>();
            var appsettion = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            var setting = JsonConvert.DeserializeObject<AppSettingConfig>(appsettion);
            if (setting == null)
                throw new Exception("请配置数据库连接字符串");
            var connstr = setting.ConnectionStrings;
            if (!string.IsNullOrWhiteSpace(connstr.MSSQLConnectString))
            {
                builder.UseSqlServer(connstr.MSSQLConnectString);
            }
            else
            {
                throw new NotImplementedException("请配置MSSQLConnectString");
            }
            return new SADbContext(builder.Options);
        }
    }
    class AppSettingConfig
    {
        public DbConfig ConnectionStrings { get; set; }
    }
    class DbConfig
    {
        public string MSSQLConnectString { get; set; }
    }
}
