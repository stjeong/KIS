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
    public const string TR_ID = "H0STCNT0";

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
                tr_id = TR_ID,
                tr_key = trKey
            }
        };
    }
}

public class 국내주식실시간체결가DTO
{
    public string 유가증권단축종목코드 { get; set; } = "";
    public string 주식체결시간 { get; set; } = "";
    public string 주식현재가 { get; set; } = "";
    public string 전일대비부호 { get; set; } = "";
    /// <summary>
    /// 1 : 상한, 2 : 상승, 3 : 보합, 4 : 하한, 5 : 하락
    /// </summary>
    public string 전일대비 { get; set; } = "";
    public string 전일대비율 { get; set; } = "";
    public string 가중평균주식가격 { get; set; } = "";
    public string 주식시가 { get; set; } = "";
    public string 주식최고가 { get; set; } = "";
    public string 주식최저가 { get; set; } = "";
    public string 매도호가1 { get; set; } = "";
    public string 매수호가1 { get; set; } = "";
    public string 체결거래량 { get; set; } = "";
    public string 누적거래량 { get; set; } = "";
    public string 누적거래대금 { get; set; } = "";
    public string 매도체결건수 { get; set; } = "";
    public string 매수체결건수 { get; set; } = "";
    public string 순매수체결건수 { get; set; } = "";
    public string 체결강도 { get; set; } = "";
    public string 총매도수량 { get; set; } = "";
    public string 총매수수량 { get; set; } = "";
    /// <summary>
    /// 1:매수(+), 3:장전, 5:매도(-)
    /// </summary>
    public string 체결구분 { get; set; } = "";
    public string 매수비율 { get; set; } = "";
    public string 전일거래량대비등락율 { get; set; } = "";
    public string 시가시간 { get; set; } = "";
    /// <summary>
    /// 1 : 상한, 2 : 상승, 3 : 보합, 4 : 하한, 5 : 하락
    /// </summary>
    public string 시가대비구분 { get; set; } = "";
    public string 시가대비 { get; set; } = "";
    public string 최고가시간 { get; set; } = "";
    /// <summary>
    /// 1 : 상한, 2 : 상승, 3 : 보합, 4 : 하한, 5 : 하락
    /// </summary>
    public string 고가대비구분 { get; set; } = "";
    public string 고가대비 { get; set; } = "";
    public string 최저가시간 { get; set; } = "";
    /// <summary>
    /// 1 : 상한, 2 : 상승, 3 : 보합, 4 : 하한, 5 : 하락
    /// </summary>
    public string 저가대비구분 { get; set; } = "";
    public string 저가대비 { get; set; } = "";
    public string 영업일자 { get; set; } = "";
    /// <summary>
    /// (1) 첫 번째 비트 - 1 : 장개시전, 2 : 장중, 3 : 장종료후, 4 : 시간외단일가, 7 : 일반Buy-in, 8 : 당일Buy-in
    /// 
    /// (2) 두 번째 비트 - 0 : 보통, 1 : 종가, 2 : 대량, 3 : 바스켓, 7 : 정리매매, 8 : Buy-in
    /// </summary>
    public string 신장운영구분코드 { get; set; } = "";
    /// <summary>
    /// Y : 정지, N : 정상거래
    /// </summary>
    public string 거래정지여부 { get; set; } = "";
    public string 매도호가잔량 { get; set; } = "";
    public string 매수호가잔량 { get; set; } = "";
    public string 총매도호가잔량 { get; set; } = "";
    public string 총매수호가잔량 { get; set; } = "";
    public string 거래량회전율 { get; set; } = "";
    public string 전일동시간누적거래량 { get; set; } = "";
    public string 전일동시간누적거래량비율 { get; set; } = "";
    /// <summary>
    /// 0 : 장중, A : 장후예상, B : 장전예상, C : 9시이후의 예상가, VI발동, D : 시간외 단일가 예상
    /// </summary>
    public string 시간구분코드 { get; set; } = "";
    public string 임의종료구분코드 { get; set; } = "";
    public string 정적VI발동기준가 { get; set; } = "";

    public const int FieldCount = 46;

    public static 국내주식실시간체결가DTO Parse(string[] values)
    {
        국내주식실시간체결가DTO dto = new 국내주식실시간체결가DTO();

        if (values.Length != FieldCount)
        {
            throw new ArgumentException($"Invalid field count: {values.Length} != {FieldCount}");
        }

        dto.유가증권단축종목코드 = values[0];
        dto.주식체결시간 = values[1];
        dto.주식현재가 = values[2];
        dto.전일대비부호 = values[3];
        dto.전일대비 = values[4];
        dto.전일대비율 = values[5];
        dto.가중평균주식가격 = values[6];
        dto.주식시가 = values[7];
        dto.주식최고가 = values[8];
        dto.주식최저가 = values[9];
        dto.매도호가1 = values[10];
        dto.매수호가1 = values[11];
        dto.체결거래량 = values[12];
        dto.누적거래량 = values[13];
        dto.누적거래대금 = values[14];
        dto.매도체결건수 = values[15];
        dto.매수체결건수 = values[16];
        dto.순매수체결건수 = values[17];
        dto.체결강도 = values[18];
        dto.총매도수량 = values[19];
        dto.총매수수량 = values[20];
        dto.체결구분 = values[21];
        dto.매수비율 = values[22];
        dto.전일거래량대비등락율 = values[23];
        dto.시가시간 = values[24];
        dto.시가대비구분 = values[25];
        dto.시가대비 = values[26];
        dto.최고가시간 = values[27];
        dto.고가대비구분 = values[28];
        dto.고가대비 = values[29];
        dto.최저가시간 = values[30];
        dto.저가대비구분 = values[31];
        dto.저가대비 = values[32];
        dto.영업일자 = values[33];
        dto.신장운영구분코드 = values[34];
        dto.거래정지여부 = values[35];
        dto.매도호가잔량 = values[36];
        dto.매수호가잔량 = values[37];
        dto.총매도호가잔량 = values[38];
        dto.총매수호가잔량 = values[39];
        dto.거래량회전율 = values[40];
        dto.전일동시간누적거래량 = values[41];
        dto.전일동시간누적거래량비율 = values[42];
        dto.시간구분코드 = values[43];
        dto.임의종료구분코드 = values[44];
        dto.정적VI발동기준가 = values[45];

        return dto;
    }

    public override string ToString()
    {
        return $"{유가증권단축종목코드}, 현재가={주식현재가}, 체결거래량={체결거래량}, 체결강도={체결강도}";
    }
}
