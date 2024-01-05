using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFriendOpenAPI.Packet;

public class 국내주식실시간체결가Item
{
    public string tr_id { get; set; } = "";
    public string tr_key { get; set; } = "";
}

public class 국내주식실시간체결가Body
{
    public 국내주식실시간체결가Item input { get; set; } = default!;
}

internal class 국내주식실시간체결가Query
{
    public WebSocketHeader header { get; set; }
    public 국내주식실시간체결가Body body { get; set; }

    public 국내주식실시간체결가Query(string approvalKey, string custtype, string trType, string trKey)
    {
        header = new WebSocketHeader()
        {
            approval_key = approvalKey,
            custtype = custtype,
            tr_type = trType,
        };

        body = new 국내주식실시간체결가Body()
        {
            input = new 국내주식실시간체결가Item
            {
                tr_id = "H0STCNT0",
                tr_key = trKey
            }
        };
    }
}


internal class 국내주식실시간체결가DTO
{

}
