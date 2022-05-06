using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RequestApprovalService.Domain
{
    public interface IDataContext
    {
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Share> Shares { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRequests> UserRequests { get; set; }
        public DbSet<UserPolicies> UserPolicies { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
