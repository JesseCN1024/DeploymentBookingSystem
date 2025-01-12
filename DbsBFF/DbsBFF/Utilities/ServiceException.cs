using DbsEnvManagementService.Presentation.Constants;

namespace DbsEnvManagementService.Utilities
{
    public class ServiceException : Exception
    {
        public string ServiceMessage { get; set; }

        public ErrorCode ErrorCode { get; set; }

        public ServiceException()
        {
           
        }

        public ServiceException(ErrorCode errorCode, string seviceExceptionMessage, Exception innerException = null)
            : base(seviceExceptionMessage, innerException)
        {
            ErrorCode = errorCode;
            ServiceMessage = seviceExceptionMessage;
        }

        public ServiceException(ErrorCode errorCode, string message, string userFriendlyMessage, Exception innerException = null)
            : base(message, innerException)
        {
            ServiceMessage = userFriendlyMessage;
            ErrorCode = errorCode;
        }

        public ServiceException(string message, string userFriendlyMessage, Exception innerException = null)
            : base(message, innerException)
        {
            ServiceMessage = userFriendlyMessage;
        }
    }
}
