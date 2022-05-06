using System;

namespace RequestApprovalService.Silverspoon.ExceptionBase
{
    public class EitherException : Exception
    {
        protected EitherException(string message) : base(message)
        {

        }
    }

}
