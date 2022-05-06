using Microsoft.Extensions.DependencyInjection;

namespace RequestApprovalService.Silverspoon.Wire.Abstractions
{
    public interface IWire
    {
        IServiceCollection Couple(IServiceCollection services, string configuration);
    }

}
