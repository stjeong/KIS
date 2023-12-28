using System.Text.Json.Serialization;

namespace eFriendOpenAPI.Packet;

public class 주식주문정정취소Query
{
    [JsonPropertyName("CANO")]
    public string CANO { get; set; } = ""; // 종합계좌번호
    [JsonPropertyName("ACNT_PRDT_CD")]
    public string ACNT_PRDT_CD { get; set; } = ""; // 계좌상품코드
    [JsonPropertyName("KRX_FWDG_ORD_ORGNO")]
    public string KRX_FWDG_ORD_ORGNO { get; set; } = ""; // 한국거래소전송주문조직번호
    [JsonPropertyName("ORGN_ODNO")]
    public string ORGN_ODNO { get; set; } = ""; // 원주문번호
    [JsonPropertyName("ORD_DVSN")]
    public string ORD_DVSN { get; set; } = ""; // 주문구분
    [JsonPropertyName("RVSE_CNCL_DVSN_CD")]
    public string RVSE_CNCL_DVSN_CD { get; set; } = ""; // 정정취소구분코드
    [JsonPropertyName("ORD_QTY")]
    public string ORD_QTY { get; set; } = ""; // 주문수량
    [JsonPropertyName("ORD_UNPR")]
    public string ORD_UNPR { get; set; } = ""; // 주문단가
    [JsonPropertyName("QTY_ALL_ORD_YN")]
    public string QTY_ALL_ORD_YN { get; set; } = ""; // 잔량전부주문여부
    [JsonPropertyName("ALGO_NO")]
    public string ALGO_NO { get; set; } = ""; // 알고리즘번호 (미사용)
}

public class 주식주문정정취소DTO
{
    public string KRX_FWDG_ORD_ORGNO { get; set; } = ""; // 한국거래소전송주문조직번호 (주문시 한국투자증권 시스템에서 지정된 영업점코드)
    public string ODNO { get; set; } = ""; // 주문번호 (정정 주문시 한국투자증권 시스템에서 채번된 주문번호)
    public string ORD_TMD { get; set; } = ""; // 주문시각 (주문시각(시분초HHMMSS))

    public override string ToString()
    {
        return $"주문번호: {ODNO} at {ORD_TMD} (전송번호: {KRX_FWDG_ORD_ORGNO})";
    }
}
