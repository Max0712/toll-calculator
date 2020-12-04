using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Toll_Calculator_API.DbModels
{
    public partial class tollContext : DbContext
    {
        public tollContext()
        {
        }

        public tollContext(DbContextOptions<tollContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<VehicleType> VehicleType { get; set; }
        public virtual DbSet<Vehicle> Vehicle { get; set; }
        public virtual DbSet<TollFee> TollFee { get; set; }
        public virtual DbSet<VehicleTollEvent> VehicleTollEvent { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Name=TollDatabase");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
