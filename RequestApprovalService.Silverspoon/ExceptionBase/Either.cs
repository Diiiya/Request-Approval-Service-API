using System;

namespace RequestApprovalService.Silverspoon.ExceptionBase
{
    public class Either<TRight>
    {
        private readonly bool _isLeft;
        private readonly EitherException _left;
        private readonly TRight _right;

        public Either(EitherException left)
        {
            this._left = left;
            this._isLeft = true;
        }

        public Either(TRight right)
        {
            this._right = right;
            this._isLeft = false;
        }

        public T Match<T>(Func<EitherException, T> left, Func<TRight, T> right)
        {
            return this._isLeft ? left(this._left) : right(this._right);
        }
    }

}
