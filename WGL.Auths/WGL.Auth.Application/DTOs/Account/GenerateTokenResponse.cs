namespace WGL.Auth.Application.DTOs.Account
{
    public class GenerateTokenResponse
    {
        public string? access_token { get; set; }
        public DateTime created_at { get; set; }
        public int expires_in { get; set; }
        public string? refresh_token { get; set; }
        public string? token_type { get; set; }
        public int account_id { get; set; }
    }
}
