using Microsoft.EntityFrameworkCore;
using QMS.DAL.Models;

namespace QMS.DAL.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasKey(e => new { e.UserID, e.RoleID });
            modelBuilder.Entity<Subscription>()
                         .Property(s => s.Status)
                         .HasConversion<string>();

            modelBuilder.Entity<UserIdentity>().HasKey(e => new { e.UserId, e.IdentityTypeID });
            modelBuilder.Entity<UserQueueNeededData>().HasKey(e => new {e.ServiceNeededDataID, e.UserQueueID});

        }

        #region DbSets
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<IdentityType> IdentityTypes { get; set; }
        public DbSet<Queue> Queues { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceNeededData> ServicesNeededData { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserIdentity> UserIdentities { get; set; }
        public DbSet<UserQueue> UserQueues { get; set; }
        public DbSet<UserQueueNeededData> UserQueueNeededData { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        #endregion
    }
}
