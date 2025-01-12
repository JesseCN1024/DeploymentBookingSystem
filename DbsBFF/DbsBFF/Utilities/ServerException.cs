using System;

namespace DbsEnvManagementService.Utilities
{
    public class ServerException : Exception
    {
        public ServerException(ServerErrorCode errorCode, string message, Exception? innerException = null) : base(message, innerException)
        {
            this.ErrorCode = errorCode;
        }

        public ServerErrorCode ErrorCode { get; set; }
    }


       
    public enum ServerErrorCode
    {
        Unknown,
        NotFound,
        InvalidOperation,
        ServiceUnavailable
    }
}
