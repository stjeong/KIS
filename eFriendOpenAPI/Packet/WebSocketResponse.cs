using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFriendOpenAPI.Packet;

public class WebSocketResponseHeader
{
    public string tr_id { get; set; } = "";
    public string tr_key { get; set; } = "";
    public string encrypt { get; set; } = "";
}

public class WebSocketResponseBodyOutput
{
    public string iv { get; set; } = "";
    public string key { get; set; } = "";
}

public class WebSocketResponseBody
{
    public string rt_cd { get; set; } = "";
    public string msg_cd { get; set; } = "";
    public string msg1 { get; set; } = "";

    public WebSocketResponseBodyOutput? output = default;
}

public class WebSocketResponse
{
    public WebSocketResponseHeader? header = default;
    public WebSocketResponseBody? body = default;
}
