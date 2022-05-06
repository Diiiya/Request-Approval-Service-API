using System;
using System.Collections.Generic;

namespace RequestApprovalService.Api.Share.Create
{
    public class ShareCreateCommandResult
    {
        //public int CreatedSharesCount { get; set; }
        public Dictionary<Guid, Decimal> UserShares { get; set; }
    }
}
