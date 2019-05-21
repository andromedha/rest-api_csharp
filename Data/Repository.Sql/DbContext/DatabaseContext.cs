using System;
using System.Collections.Generic;
using System.Text;
using DataClasses;
using DataClasses.Enums;
using Microsoft.EntityFrameworkCore;

namespace Repository.Sql.DbContext
{
    public class DatabaseContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
          
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Person>()
                .Property(x => x.Color)
                .HasConversion(
                    x => (int)Enum.Parse(typeof(Colors), x.ToString()),
                    x => (Colors)x
                    );
        }


        public DbSet<Person> Persons { get; set; }
    }
}
