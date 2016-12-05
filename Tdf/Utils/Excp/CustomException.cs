using System;

namespace Tdf.Utils.Excp
{
    public class CustomException : Exception
    {
        public CustomException(int errCode, string message)
                : base(message)
        {
            this.ErrCode = errCode;
        }

        public int ErrCode { get; }
    }
}
