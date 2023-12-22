namespace eFriendOpenAPI.Packet;

public class PacketResponse<T>
{
    public string rt_cd { get; set; } = ""; // 성공 실패 여부	
    public string msg_cd { get; set; } = ""; // 응답코드
    public string msg1 { get; set; } = ""; // 응답메세지

    public T? output { get; set; }
}

public class PacketResponses<T>
{
    public string rt_cd { get; set; } = ""; // 성공 실패 여부	
    public string msg_cd { get; set; } = ""; // 응답코드
    public string msg1 { get; set; } = ""; // 응답메세지

    public T[]? output1 { get; set; }
}
