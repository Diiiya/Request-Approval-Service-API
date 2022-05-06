using RequestApprovalService.Silverspoon.Repositories.Abstractions;

namespace RequestApprovalService.Repositories.Request
{
    public interface IRequestCommandsRepository : IRepositoryAddCommands<Domain.Request>
    {
    }
}
