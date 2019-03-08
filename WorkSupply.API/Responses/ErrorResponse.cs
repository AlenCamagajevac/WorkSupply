namespace WorkSupply.API.Responses
{
    public class ErrorResponse
    {
        public ErrorCodes Code { get; set; }

        public string Reason { get; set; }
    }
}