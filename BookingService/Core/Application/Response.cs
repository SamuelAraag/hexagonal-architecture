namespace Application
{
    public abstract class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ErrorCodes ErrorCode { get; set; }
    }

    public enum ErrorCodes
    {
        NotFound = 1,
        CouldNotStoreData = 2,
        InvalidPersonId = 3,
        MissingRequiredInformation = 4,
        InvalidEmail = 5
    }
}
