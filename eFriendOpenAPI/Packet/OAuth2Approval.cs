using System.Text.Json.Serialization;

namespace ConsoleApp1.Packet;

public class OAuth2ApprovalRequest
{
    [JsonPropertyName("grant_type")]
    public string GrantType { get; set; } = "client_credentials";
    [JsonPropertyName("appkey")]
    public string AppKey { get; set; } = "";
    [JsonPropertyName("secretkey")]
    public string SecretKey { get; set; } = "";
}

public class OAuth2ApprovalResponse
{
    [JsonPropertyName("approval_key")]
    public string ApprovalKey { get; set; } = "";
}
