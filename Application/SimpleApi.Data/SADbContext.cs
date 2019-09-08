using Microsoft.EntityFrameworkCore;
using SimpleApi.Data.Domain;
using System;
using System.Linq;

namespace SimpleApi.Data
{
    public class SADbContext : DbContext
    {
        public SADbContext(DbContextOptions<SADbContext> options)
            : base(options)
        {
        }
        public DbSet<UserInfo> UserInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.StartsWith(nameof(SimpleApi)))
                    .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IDbQueryItem))))
                    .ToList();
            types.ForEach((type) =>
            {
                modelBuilder.Query(type);
            });
        }
    }
}
