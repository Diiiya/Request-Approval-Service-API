using Microsoft.EntityFrameworkCore;

namespace RequestApprovalService.Domain
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public virtual DbSet<Policy> Policies { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Share> Shares { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRequests> UserRequests { get; set; }
        public DbSet<UserPolicies> UserPolicies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasCharSet("latin1").UseCollation("latin1_swedish_ci");
            modelBuilder.Entity<Share>().HasKey(s => new { s.UserId, s.PolicyId });

            modelBuilder.Entity<UserRequests>().HasKey(ur => new { ur.UserId, ur.RequestId });
            modelBuilder.Entity<UserRequests>()
                .HasOne(ur => ur.User)
                .WithMany(r => r.Requests).IsRequired()
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRequests>()
                .HasOne(ur => ur.Request)
                .WithMany(u => u.Users).IsRequired()
                .HasForeignKey(ur => ur.RequestId);

            modelBuilder.Entity<UserPolicies>().HasKey(up => new { up.UserId, up.PolicyId });
            modelBuilder.Entity<UserPolicies>()
                .HasOne(up => up.User)
                .WithMany(p => p.Policies).IsRequired()
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserPolicies>()
                .HasOne(up => up.Policy)
                .WithMany(u => u.Users).IsRequired()
                .HasForeignKey(up => up.PolicyId);


            // TO DO!
            // Unique constraints for Names, Emails etc. ..

        }
    }
}
