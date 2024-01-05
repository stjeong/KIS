using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFriendOpenAPI.Packet;

public class WebSocketHeader
{
    public string approval_key { get; set; } = "";
    public string custtype { get; set; } = "P";
    public string tr_type { get; set; } = "";
    public string content_type { get; set; } = "utf-8";
}
