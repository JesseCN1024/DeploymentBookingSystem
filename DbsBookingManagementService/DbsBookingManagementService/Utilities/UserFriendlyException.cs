using DbsBookingManagementService.Presentation.Constants;

namespace DbsBookingManagementService.Utilities
{
    public class UserFriendlyException : Exception
    {
        public string UserFriendlyMessage { get; set; }

        public ErrorCode ErrorCode { get; set; }

        public UserFriendlyException()
        {
        }

        public UserFriendlyException(ErrorCode errorCode, string userFriendlyMessage, Exception innerException = null)
            : base(userFriendlyMessage, innerException)
        {
            ErrorCode = errorCode;
            UserFriendlyMessage = userFriendlyMessage;
        }

        public UserFriendlyException(ErrorCode errorCode, string message, string userFriendlyMessage, Exception innerException = null)
            : base(message, innerException)
        {
            UserFriendlyMessage = userFriendlyMessage;
            ErrorCode = errorCode;
        }

        public UserFriendlyException(string message, string userFriendlyMessage, Exception innerException = null)
            : base(message, innerException)
        {
            UserFriendlyMessage = userFriendlyMessage;
        }
    }
}
