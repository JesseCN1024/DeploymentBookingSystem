using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DbsUsersManagementService.Models.ErrorModels
{
    public class ErrorResponse
    {
        public IEnumerable<Error> Errors { get; set; } = new List<Error>();


        public ErrorResponse()
        {
        }

        public ErrorResponse(IEnumerable<Error> errors)
        {
            Errors = errors;
        }
    }
}
