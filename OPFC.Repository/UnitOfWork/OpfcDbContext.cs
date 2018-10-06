using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OPFC.API.DTO;
using OPFC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Repositories.UnitOfWork
{
    /// <summary>
    /// The OPFC system DbContext
    /// </summary>
    public class OpfcDbContext : DbContext
    {

        /// <summary>
        /// The constructor
        /// </summary>
        public OpfcDbContext() : base() { }

        /// <summary>
        /// Configurations
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Config connection string here
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                         .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                         .AddJsonFile("appsettings.json")
                                         .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("OpfcDbConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        // Declare mapping models belows
        // Example:
        // public DbSet<[Model]> Model {get; set;}

        /// <summary>
        /// [dbo].[User]
        /// </summary>
        public DbSet<User> User { get; set; }

        /// <summary>
        /// [dbo].[UserRole]
        /// </summary>
        public DbSet<UserRole> UserRole { get; set; }

        /// <summary>
        /// [dbo].[Brand]
        /// </summary>
        public DbSet<Brand> Brand { get; set; }

        /// <summary>
        /// [dbo].[Photo]
        /// </summary>
        public DbSet<Photo> Photo { get; set; }

        /// <summary>
        /// [dbo].[Event]
        /// </summary>
        public DbSet<Event> Event { get; set; }

        /// <summary>
        /// [dbo].[EventType]
        /// </summary>
        public DbSet<EventType> EventType { get; set; }
    }
}
