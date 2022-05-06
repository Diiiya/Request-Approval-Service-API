using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RequestApprovalService.Domain
{
    public class Policy
    {
        //public Policy()
        //{
        //    this.Users = new HashSet<User>();
        //}

        [Required]
        [Key]
        public Guid PolicyId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Secret { get; set; }
        [Required]
        public int Threshold { get; set; }

        public virtual IEnumerable<UserPolicies> Users { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<Share> Shares { get; set; }
    }
}
