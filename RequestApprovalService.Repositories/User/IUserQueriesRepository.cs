using System.Threading.Tasks;
using RequestApprovalService.Silverspoon.Repositories.Abstractions;

namespace RequestApprovalService.Repositories.User
{
    public interface IUserQueriesRepository : IRepositoryQueries<Domain.User>
    {
    }
}
