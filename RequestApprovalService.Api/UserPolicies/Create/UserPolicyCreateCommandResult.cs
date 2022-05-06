namespace RequestApprovalService.Api.UserPolicies.Create
{
    public class UserPolicyCreateCommandResult
    {
        public int UsersCount { get; set; }
        public bool IsSuccess { get; internal set; }
    }
}
