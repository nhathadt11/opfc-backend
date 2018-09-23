using Microsoft.EntityFrameworkCore;
using OPFC.Constants;
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
            optionsBuilder.UseSqlServer(ConnectionConst.CONNECTION_STRING);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        // Declare mapping models belows
        // Example:
        // public DbSet<[Model]> Model {get; set;}

        /// <summary>
        /// [dbo].[Users]
        /// </summary>
        public DbSet<User> Users { get; set; }
    }
}
