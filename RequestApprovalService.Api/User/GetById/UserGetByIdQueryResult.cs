
namespace RequestApprovalService.Api.User.GetById
{
    public class UserGetByIdQueryResult 
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
