using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp
{
    public class TrainingDbContext : DbContext
    {
        private string _connectionString;
        private string _assemblyName;
        public DbSet<Course> Courses { get; set; }

        public TrainingDbContext()
        {
            _connectionString = "Server = DESKTOP-1GDG61U\\SQLEXPRESS; Database = Test; User Id = demo; Password = 123456;";
            _assemblyName = Assembly.GetExecutingAssembly().FullName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(_connectionString,
                    m => m.MigrationsAssembly(_assemblyName));
            }

            base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
