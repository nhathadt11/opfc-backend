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
            modelBuilder.Entity<MenuTag>()
                .HasKey(mt => new { mt.MenuId, mt.TagId });

            modelBuilder.Entity<MenuCategory>()
                .HasKey(mt => new { mt.MenuId, mt.CategoryId });

            modelBuilder.Entity<EventCategory>()
                .HasKey(mt => new { mt.EventId, mt.CategoryId });
            
            modelBuilder.Entity<MenuEventType>()
                .HasKey(mt => new { mt.MenuId, mt.EventTypeId });

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

        /// <summary>
        /// [dbo].[MenuMeal]
        /// </summary>
        public DbSet<MenuMeal> MenuMeal { get; set; }

        /// <summary>
        /// [dbo].[OrderLine]
        /// </summary>
        public DbSet<OrderLine> OrderLine { get; set; }
        
        /// <summary>
        /// [dbo].[MenuEventType]
        /// </summary>
        public DbSet<MenuEventType> MenuEventType { get; set; }

        /// <summary>
        /// [dbo].[District]
        /// </summary>
        public DbSet<District> District { get; set; }

        /// <summary>
        /// [dbo].[City]
        /// </summary>
        public DbSet<City> City { get; set; }

        /// <summary>
        /// [dbo].[Rating]
        /// </summary>
        public DbSet<Rating> Rating { get; set; }

        /// <summary>
        /// [dbo].[Tag]
        /// </summary>
        public DbSet<Tag> Tag { get; set; }

        /// <summary>
        /// [dbo].[MenuTag]
        /// </summary>
        public DbSet<MenuTag> MenuTag { get; set; }

        /// [dbo].[PrivateRating]
        /// </summary>
        public DbSet<PrivateRating> PrivateRating { get; set; }

        /// <summary>
        /// [dbo].[Category]
        /// </summary>
        public DbSet<Category> Category { get; set; } 

        /// <summary>
        /// [dbo].[MenuCategory]
        /// </summary>
        public DbSet<MenuCategory> MenuCategory { get; set; }

        /// <summary>
        /// [dbo].[EventCategory]
        /// </summary>
        public DbSet<EventCategory> EventCategory { get; set; }
    }
}
