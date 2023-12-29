using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFriendOpenAPI.Packet;

public class 국내휴장일조회Query
{
    public string BASS_DT { get; set; } = "";
    public string CTX_AREA_NK { get; set; } = "";
    public string CTX_AREA_FK { get; set; } = "";
}

public class 국내휴장일조회DTO
{
    public string bass_dt { get; set; } = ""; // 기준일자    String Y	8	기준일자(YYYYMMDD)
    public string wday_dvsn_cd { get; set; } = ""; // 요일구분코드  String Y	2	01:일요일, 02:월요일, 03:화요일, 04:수요일, 05:목요일, 06:금요일, 07:토요일
    public string bzdy_yn { get; set; } = ""; // 영업일여부   String Y	1	Y/N 금융기관이 업무를 하는 날
    public string tr_day_yn { get; set; } = ""; // 거래일여부 String Y	1	Y/N 증권 업무가 가능한 날(입출금, 이체 등의 업무 포함)
    public string opnd_yn { get; set; } = ""; // 개장일여부   String Y	1	Y/N 주식시장이 개장되는 날
                                              // * 주문을 넣고자 할 경우 개장일여부(opnd_yn)를 사용
    public string sttl_day_yn { get; set; } = ""; // 결제일여부   String Y	1	Y/N 주식 거래에서 실제로 주식을 인수하고 돈을 지불하는 날

    public override string ToString()
    {
        return $"{bass_dt}, 개장일여부: {opnd_yn}";
    }
}