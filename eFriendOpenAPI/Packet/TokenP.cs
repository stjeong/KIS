using System.Text.Json.Serialization;

namespace ConsoleApp1.Packet;

public class TokenPRequest
{
    [JsonPropertyName("grant_type")]
    public string GrantType { get; set; } = "client_credentials";
    [JsonPropertyName("appkey")]
    public string AppKey { get; set; } = "";
    [JsonPropertyName("appsecret")]
    public string SecretKey { get; set; } = "";

    public OAuth2ApprovalRequest AsApprovalRequest()
    {
        return new OAuth2ApprovalRequest { GrantType = GrantType, AppKey = AppKey, SecretKey = SecretKey };
    }
}

public class AccessToken
{
    [JsonPropertyName("access_token")]
    public string Value { get; set; } = "";
    [JsonPropertyName("token_type")]
    public string Type { get; set; } = "";
    [JsonPropertyName("expires_in")]
    public decimal ExpiresIn { get; set; } = 0;

    public DateTime ExpiresAt { get; set; } = DateTime.MinValue;
}