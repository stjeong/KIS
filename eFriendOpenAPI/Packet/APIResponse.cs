using System.Text.Json.Serialization;

namespace eFriendOpenAPI.Packet;

public class PacketResponse<T>
{
    [JsonPropertyName("rt_cd")]
    public string rt_cd { get; set; } = ""; // 성공 실패 여부	
    [JsonPropertyName("msg_cd")]
    public string msg_cd { get; set; } = ""; // 응답코드
    [JsonPropertyName("msg1")]
    public string msg1 { get; set; } = ""; // 응답메세지

    public T? output { get; set; }
}

public class ResponseChkHoliday<T>
{
    [JsonPropertyName("ctx_area_nk")]
    public string ctx_area_nk { get; set; } = "";
    [JsonPropertyName("ctx_area_fk")]
    public string ctx_area_fk { get; set; } = "";

    public T[]? output { get; set; }
}


public class PacketResponses<T>
{
    [JsonPropertyName("rt_cd")]
    public string rt_cd { get; set; } = ""; // 성공 실패 여부	
    [JsonPropertyName("msg_cd")]
    public string msg_cd { get; set; } = ""; // 응답코드
    [JsonPropertyName("msg1")]
    public string msg1 { get; set; } = ""; // 응답메세지

    public T[]? output1 { get; set; }
}
