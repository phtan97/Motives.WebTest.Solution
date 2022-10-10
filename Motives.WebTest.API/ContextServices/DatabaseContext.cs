using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Motives.WebTest.API.ContextServices
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() { }
        public DatabaseContext(DbContextOptions options) : base(options)  
        {  
            
        }
        public virtual DbSet<TableUser> TableUsers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsbuilder)
        {
            if (!optionsbuilder.IsConfigured)
            {
                var absolutePath = AppContext.BaseDirectory;
                IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(absolutePath).AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true).Build();
                optionsbuilder.UseSqlServer(configuration.GetConnectionString("Motives"));
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)  
        {  
            base.OnModelCreating(builder);
            builder.Entity<TableUser>();
        }  
    }
}
