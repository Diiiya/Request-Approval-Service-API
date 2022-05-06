
namespace RequestApprovalService.Api.Policy.GetById
{
    public class PolicyGetByIdQueryResult
    {
        public string Name { get; set; }

        public int Secret { get; set; }

        public int Threshold { get; set; }
        //public IEnumerable<Domain.UserPolicies> UserPolicies { get; set; }
    }
}
