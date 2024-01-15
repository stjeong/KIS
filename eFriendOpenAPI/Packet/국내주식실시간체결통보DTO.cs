using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFriendOpenAPI.Packet;

public class 국내주식실시간체결통보Item
{
    public string tr_id { get; set; } = "";
    public string tr_key { get; set; } = "";
}

public class 국내주식실시간체결통보Body
{
    public 국내주식실시간체결통보Item input { get; set; } = default!;
}

public class 국내주식실시간체결통보Query
{
    public const string TR_ID = "H0STCNI0";
    public const string TR_VTS_ID = "H0STCNI9";

    public WebSocketHeader header { get; set; }
    public 국내주식실시간체결통보Body body { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="approvalKey">실시간 (웹소켓) 접속키 발급 API(/oauth2/Approval)를 사용하여 발급받은 웹소켓 접속키</param>
    /// <param name="custtype">B : 법인, P : 개인</param>
    /// <param name="trType">1 : 등록, 2 : 해제</param>
    /// <param name="trKey">HTS ID</param>
    /// <param name="isVTS">모의: true, 실전: false</param>
    public 국내주식실시간체결통보Query(string approvalKey, string custtype, string trType, string trKey, bool isVTS)
    {
        header = new WebSocketHeader()
        {
            approval_key = approvalKey,
            custtype = custtype,
            tr_type = trType,
        };

        body = new 국내주식실시간체결통보Body()
        {
            input = new 국내주식실시간체결통보Item()
            {
                tr_id = (isVTS) ? TR_ID : TR_VTS_ID,
                tr_key = trKey
            }
        };
    }
}

public class 국내주식실시간체결통보DTO
{

}
