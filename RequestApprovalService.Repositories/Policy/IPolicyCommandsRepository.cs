using RequestApprovalService.Silverspoon.Repositories.Abstractions;

namespace RequestApprovalService.Repositories.Policy
{
    public interface IPolicyCommandsRepository : IRepositoryAddCommands<Domain.Policy>
    {
    }
}
