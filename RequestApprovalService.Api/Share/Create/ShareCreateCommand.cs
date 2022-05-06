using System;
using System.Collections.Generic;
using RequestApprovalService.Silverspoon.Mediator;
using RequestApprovalService.Silverspoon.Shamir;

namespace RequestApprovalService.Api.Share.Create
{
    public class ShareCreateCommand : BaseCommand, IHandleableRequest<ShareCreateCommand, ShareCreateCommandHandler, ShareCreateCommandResult>
    {
        public List<Guid> UserIds { get; set; }
        public Guid PolicyId { get; set; }

        public List<Point> Shares { get; set; }
        //public int X { get; set; }
        //public int Y { get; set; }

        public ShareCreateCommand() : base(typeof(ShareCreateCommand))
        {
            
        }
    }
}
