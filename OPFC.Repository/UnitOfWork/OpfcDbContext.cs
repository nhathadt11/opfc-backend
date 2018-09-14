using Microsoft.EntityFrameworkCore;
using OPFC.Constants;
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionConst.CONNECTION_STRING);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        // Declare mapping models belows
        // Example:
        // public DbSet<[Model]> Model {get; set;}

    }
}
