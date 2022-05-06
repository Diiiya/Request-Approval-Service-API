using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RequestApprovalService.Domain;
using RequestApprovalService.Persistence.Policy;
using RequestApprovalService.Persistence.Request;
using RequestApprovalService.Persistence.Share;
using RequestApprovalService.Persistence.User;
using RequestApprovalService.Persistence.UserPolicies;
using RequestApprovalService.Persistence.UserRequests;
using RequestApprovalService.Repositories.Policy;
using RequestApprovalService.Repositories.Request;
using RequestApprovalService.Repositories.Share;
using RequestApprovalService.Repositories.User;
using RequestApprovalService.Repositories.UserPolicies;
using RequestApprovalService.Repositories.UserRequests;
using RequestApprovalService.Silverspoon.Wire.Abstractions;

namespace RequestApprovalService.Wire
{
    public class CommandsAndQueriesWire : IWire
    {
        public IServiceCollection Couple(IServiceCollection services, string configuration)
        {
            services.AddTransient<IDataContext, DataContext>();
            services.AddDbContext<DataContext>(options =>
                options.UseMySql(configuration, ServerVersion.Parse("5.7.29-mysql")));

            // Policy
            services.AddTransient<IContextPolicyCommandsRepository, ContextPolicyCommandsRepository>();
            services.AddTransient<IContextPolicyQueriesRepository, ContextPolicyQueriesRepository>();

            // Request
            services.AddTransient<IContextRequestCommandsRepository, ContextRequestCommandsRepository>();
            services.AddTransient<IContextRequestQueriesRepository, ContextRequestQueriesRepository>();

            // Share
            services.AddTransient<IContextShareCommandsRepository, ContextShareCommandsRepository>();
            services.AddTransient<IContextShareQueriesRepository, ContextShareQueriesRepository>();

            // User
            services.AddTransient<IContextUserQueriesRepository, ContextUserQueriesRepository>();

            // User Policies
            services.AddTransient<IContextUserPoliciesCommandsRepository, ContextUserPoliciesCommandsRepository>();
            services.AddTransient<IContextUserPoliciesQueriesRepository, ContextUserPoliciesQueriesRepository>();

            // User Requests
            services.AddTransient<IContextUserRequestsCommandsRepository, ContextUserRequestsCommandsRepository>();
            services.AddTransient<IContextUserRequestsQueriesRepository, ContextUserRequestsQueriesRepository>();

            return services;
        }
    }
}
