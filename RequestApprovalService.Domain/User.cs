using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RequestApprovalService.Domain
{
    public class User
    {
        //public User()
        //{
        //    this.Policies = new HashSet<Policy>();
        //    this.Requests = new HashSet<Request>();
        //}
        [Required]
        [Key]
        public Guid UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool IsAdmin { get; set; }

        public virtual IEnumerable<UserPolicies>? Policies { get; set; }
        public virtual IEnumerable<UserRequests> Requests { get; set; }
        public virtual ICollection<Share> Shares { get; set; }
    }
}
