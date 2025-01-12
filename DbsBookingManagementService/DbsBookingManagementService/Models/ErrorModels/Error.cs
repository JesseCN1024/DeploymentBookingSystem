namespace DbsBookingManagementService.Models.ErrorModels
{
    public class Error
    {
        public List<ErrorProperty> Properties = new List<ErrorProperty>();

        public string Code { get; set; }

        public string Message { get; set; }

        public Error(string code = null, string message = null)
        {
            Code = code;
            Message = message;
        }

        public void AddErrorProperty(ErrorProperty property)
        {
            Properties.Add(property);
        }
    }
}
