using Microsoft.EntityFrameworkCore;
using VehicleTrackingSystem.Entities;
namespace VehicleTrackingSystem.DataAccess.DbContext
{
    public class VehicleTrackingCommandsContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public VehicleTrackingCommandsContext(DbContextOptions<VehicleTrackingCommandsContext> options) : base(options)
        {
        }
        public DbSet<UserType> UserType { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<Device> Device { get; set; }
        public DbSet<Location> Location { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region UserType
            modelBuilder.Entity<UserType>().Property(b => b.Name).HasMaxLength(500);
            #endregion

            #region User
            modelBuilder.Entity<User>().Property(b => b.Name).HasMaxLength(500);
            modelBuilder.Entity<User>().Property(b => b.Email).HasMaxLength(50);
            modelBuilder.Entity<User>().Property(b => b.MobileNumber).HasMaxLength(10);
            #endregion

            #region Vehicle
            modelBuilder.Entity<Vehicle>().Property(b => b.Name).HasMaxLength(50);
            modelBuilder.Entity<Vehicle>().Property(b => b.Model).HasMaxLength(500);
            modelBuilder.Entity<Vehicle>().Property(b => b.Maker).HasMaxLength(100);
            modelBuilder.Entity<Vehicle>().Property(b => b.Year).HasMaxLength(4);
            modelBuilder.Entity<Vehicle>().Property(b => b.BodyType).HasMaxLength(10);
            #endregion

            #region Device
            modelBuilder.Entity<Device>().Property(b => b.ImeiNumber).HasMaxLength(16);
            #endregion

            #region Location
            modelBuilder.Entity<Location>().Property(b => b.Latitude).HasMaxLength(500);
            modelBuilder.Entity<Location>().Property(b => b.Longitude).HasMaxLength(500);
            #endregion

            #region Seed Value
            modelBuilder.Entity<UserType>().HasData(new { UserTypeId = 1, Name = "Admin" });
            modelBuilder.Entity<UserType>().HasData(new { UserTypeId = 2, Name = "User" });
            #endregion 
        }
    }
}
