namespace WGL.Auth.Application.DTOs.Account
{
    public class UserLoginTokenResponse
    {
        public required UserResponse[] data { get; set; }
        public required Status status { get; set; }
    }

    public class Status
    {
        public string? type { get; set; }
        public int code { get; set; }
        public required string message { get; set; }
        public bool error { get; set; }
    }

    public class UserResponse
    {
        public required string status { get; set; }
        public required string session_token { get; set; }
        public object? return_to_url { get; set; }
        public string? expires_at { get; set; }
        public User? user { get; set; }
    }

    public class User
    {
        public string? username { get; set; }
        public string? lastname { get; set; }
        public string? email { get; set; }
        public string? firstname { get; set; }
        public int id { get; set; }
    }


}
