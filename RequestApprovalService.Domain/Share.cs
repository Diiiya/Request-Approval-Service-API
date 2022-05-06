using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RequestApprovalService.Domain
{
    public class Share
    {
        [Required]
        [Key]
        public Guid ShareId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid PolicyId { get; set; }

        [Required]
        public Decimal X { get; set; }

        [Required]
        public Decimal Y { get; set; }
    }
}
