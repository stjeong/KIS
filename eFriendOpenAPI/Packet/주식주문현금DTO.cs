using System.Text.Json.Serialization;

namespace eFriendOpenAPI.Packet;

public class 주식주문현금Query
{
    [JsonPropertyName("CANO")]
    public string CANO { get; set; } = ""; // 종합계좌번호
    [JsonPropertyName("ACNT_PRDT_CD")]
    public string ACNT_PRDT_CD { get; set; } = ""; // 계좌상품코드
    [JsonPropertyName("PDNO")]
    public string PDNO { get; set; } = ""; // 종목코드(6자리), ETN의 경우, Q로 시작(EX.Q500001)
    [JsonPropertyName("ORD_DVSN")]
    public string ORD_DVSN { get; set; } = "";  // 00 : 지정가
                                                // 01 : 시장가
                                                // 02 : 조건부지정가
                                                // 03 : 최유리지정가
                                                // 04 : 최우선지정가
                                                // 05 : 장전 시간외 (08:20~08:40)
                                                // 06 : 장후 시간외 (15:30~16:00)
                                                // 07 : 시간외 단일가(16:00~18:00)
                                                // 08 : 자기주식
                                                // 09 : 자기주식S-Option
                                                // 10 : 자기주식금전신탁
                                                // 11 : IOC지정가 (즉시체결,잔량취소)
                                                // 12 : FOK지정가 (즉시체결,전량취소)
                                                // 13 : IOC시장가 (즉시체결,잔량취소)
                                                // 14 : FOK시장가 (즉시체결,전량취소)
                                                // 15 : IOC최유리 (즉시체결,잔량취소)
                                                // 16 : FOK최유리 (즉시체결,전량취소)

    [JsonPropertyName("ORD_QTY")]
    public string ORD_QTY { get; set; } = ""; // 주문수량 (주문주식수)
    [JsonPropertyName("ORD_UNPR")]
    public string ORD_UNPR { get; set; } = "0"; // 주문단가 
                                                // 주문단가가 없는주문은 상한가로 주문금액을 선정하고 이후 체결이되면 체결금액로 정산
                                                // 지정가 이외의 장전 시간외, 장후 시간외, 시장가 등 모든 주문구분의 경우 1주당 가격을 공란으로 비우지 않음 "0"으로 입력 권고
                                                // 1주당 가격
}

public class 주식주문현금DTO
{
    public string KRX_FWDG_ORD_ORGNO { get; set; } = ""; // 한국거래소전송주문조직번호   String Y	5	주문시 한국투자증권 시스템에서 지정된 영업점코드
    public string ODNO { get; set; } = ""; // 주문번호    String Y	10	주문시 한국투자증권 시스템에서 채번된 주문번호 
    public string ORD_TMD { get; set; } = ""; // 주문시각    String Y	6	주문시각(시분초HHMMSS)

    public override string ToString()
    {
        return $"주문번호: {ODNO} at {ORD_TMD} (전송번호: {KRX_FWDG_ORD_ORGNO})";
    }
}