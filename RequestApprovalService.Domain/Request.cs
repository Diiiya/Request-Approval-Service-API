using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RequestApprovalService.Domain
{
    public class Request
    {
        //public Request()
        //{
        //    this.Users = new HashSet<User>();
        //}
        [Required]
        [Key]
        public Guid RequestId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid PolicyId { get; set; }
        [Required]
        public bool Approved { get; set; }
        public virtual IEnumerable<UserRequests> Users { get; set; }
    }
}
