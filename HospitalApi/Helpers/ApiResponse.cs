namespace HospitalApi.Helpers
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }

        public ApiResponse(bool success, string? message, object? data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public static ApiResponse Ok(object? data, string? message = null)
        {
            return new ApiResponse(true, message, data);
        }

        public static ApiResponse Fail(string? message, object? data = null)
        {
            return new ApiResponse(false, message, data);
        }
    }
}
