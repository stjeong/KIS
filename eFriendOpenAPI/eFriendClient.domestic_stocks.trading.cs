using Cysharp.Web;
using eFriendOpenAPI.Extension;
using eFriendOpenAPI.Packet;
using System.Net.Http.Json;

namespace eFriendOpenAPI;

partial class eFriendClient
{
    /// <summary>
    /// 주식현금매수주문
    /// </summary>
    /// <param name="종목코드">PDNO(6자리, ETN의 경우, Q로 시작 (EX. Q500001))</param>
    /// <param name="주문수량">ORD_QTY</param>
    /// <param name="주문구분">ORD_DVSN(00 : 지정가, 01 : 시장가, ...)</param>
    /// <param name="주문단가">ORD_UNPR</param>
    /// <returns></returns>
    public async Task<(주식주문현금DTO? rseult, string error)> 주식현금매수주문(string 종목코드, uint 주문수량, string 주문구분 = "01", uint 주문단가 = 0)
    {
        if (주문구분 == "00" && 주문단가 == 0)
        {
            throw new ArgumentException("[매수주문] 지정가 주문시 주문단가를 입력해야 합니다.");
        }

        string trId = _isVTS ? "VTTC0802U" : "TTTC0802U";
        return await 주식주문현금(trId, 종목코드, 주문구분, 주문수량, 주문단가);
    }

    /// <summary>
    /// 주식현금매도주문
    /// </summary>
    /// <param name="종목코드">PDNO(6자리, ETN의 경우, Q로 시작 (EX. Q500001))</param>
    /// <param name="주문수량">ORD_QTY</param>
    /// <param name="주문구분">ORD_DVSN(00 : 지정가, 01 : 시장가, ...)</param>
    /// <param name="주문단가">ORD_UNPR</param>
    /// <returns></returns>
    public async Task<(주식주문현금DTO? rseult, string error)> 주식현금매도주문(string 종목코드, uint 주문수량, string 주문구분 = "01", uint 주문단가 = 0)
    {
        if (주문구분 == "00" && 주문단가 == 0)
        {
            return (null, "[매도주문] 지정가 주문시 주문단가를 입력해야 합니다.");
        }

        string trId = _isVTS ? "VTTC0801U" : "TTTC0801U";
        return await 주식주문현금(trId, 종목코드, 주문구분, 주문수량, 주문단가);
    }

    // 주식주문(현금)[v1_국내주식-001]
    // https://apiportal.koreainvestment.com/apiservice/apiservice-domestic-stock
    async Task<(주식주문현금DTO? rseult, string error)> 주식주문현금(string trId, string 종목코드, string 주문구분, uint 주문수량, uint 주문단가)
    {
        if (CodeExists(종목코드) == false)
        {
            return (null, $"종목코드가 존재하지 않습니다. {종목코드}");
        }

        using var client = NewHttp(trId);

        주식주문현금Query query = new()
        {
            CANO = this._account.CANO,
            ACNT_PRDT_CD = this._account.ACNT_PRDT_CD,
            PDNO = 종목코드,
            ORD_DVSN = 주문구분,
            ORD_QTY = 주문수량.ToString(),
            ORD_UNPR = 주문단가.ToString(),
        };

        string url = "/uapi/domestic-stock/v1/trading/order-cash";

        var response = await client.PostJsonContent(url, query);
        var respBody = await response.Content.ReadFromJsonAsync<PacketResponse<주식주문현금DTO>>();

        if (response.IsSuccessStatusCode)
        {
            return (respBody?.output, respBody?.msg1 ?? "");
        }

        return (null, respBody?.msg1 ?? "");
    }

    /// <summary>
    /// 주식주문정정
    /// </summary>
    /// <param name="한국거래소전송주문조직번호">KRX_FWDG_ORD_ORGNO "" (Null 값 설정), 주문시 한국투자증권 시스템에서 지정된 영업점코드</param>
    /// <param name="원주문번호">ORGN_ODNO API output1의 odno(주문번호) 값 입력, 주문시 한국투자증권 시스템에서 채번된 주문번호</param>
    /// <param name="주문구분">ORD_DVSN (00 : 지정가, 01 : 시장가)</param>
    /// <param name="주문수량">ORD_QTY ([잔량전부 취소/정정주문] "0" 설정(QTY_ALL_ORD_YN= Y 설정 ), [잔량일부 취소/정정주문] 취소/정정 수량)</param>
    /// <param name="주문단가">ORD_UNPR ([정정] 정정주문 1주당 가격, [취소] "0" 설정)</param>
    /// <param name="잔량전부주문여부">QTY_ALL_ORD_YN ([정정/취소] Y : 잔량전부, N : 잔량일부)</param>
    /// <returns></returns>
    public async Task<(주식주문정정취소DTO? result, string error)> 주식주문정정(string 한국거래소전송주문조직번호, string 원주문번호, string 주문구분,
        uint 주문수량, uint 주문단가, string 잔량전부주문여부)
    {
        return await 주식주문정정취소(한국거래소전송주문조직번호, 원주문번호, 주문구분,
            정정취소구분코드: "01", 주문수량, 주문단가, 잔량전부주문여부);
    }

    /// <summary>
    /// 주식주문취소
    /// </summary>
    /// <param name="한국거래소전송주문조직번호">KRX_FWDG_ORD_ORGNO "" (Null 값 설정), 주문시 한국투자증권 시스템에서 지정된 영업점코드</param>
    /// <param name="원주문번호">ORGN_ODNO API output1의 odno(주문번호) 값 입력, 주문시 한국투자증권 시스템에서 채번된 주문번호</param>
    /// <param name="주문수량">ORD_QTY ([잔량전부 취소/정정주문] "0" 설정(QTY_ALL_ORD_YN= Y 설정 ), [잔량일부 취소/정정주문] 취소/정정 수량)</param>
    /// <param name="잔량전부주문여부">QTY_ALL_ORD_YN ([정정/취소] Y : 잔량전부, N : 잔량일부)</param>
    /// <returns></returns>
    public async Task<(주식주문정정취소DTO? result, string error)> 주식주문취소(string 한국거래소전송주문조직번호, string 원주문번호, 
        uint 주문수량, string 잔량전부주문여부 = "Y")
    {
        잔량전부주문여부 = (주문수량 == 0) ? "Y" : "N";

        return await 주식주문정정취소(한국거래소전송주문조직번호, 원주문번호,
            주문구분: "00", 정정취소구분코드: "02", 주문수량, 주문단가: 0, 잔량전부주문여부);
    }

    /// <summary>
    /// 주식주문전량취소
    /// </summary>
    /// <param name="한국거래소전송주문조직번호">KRX_FWDG_ORD_ORGNO "" (Null 값 설정), 주문시 한국투자증권 시스템에서 지정된 영업점코드</param>
    /// <param name="원주문번호">ORGN_ODNO API output1의 odno(주문번호) 값 입력, 주문시 한국투자증권 시스템에서 채번된 주문번호</param>
    /// <returns></returns>
    public async Task<(주식주문정정취소DTO? result, string error)> 주식주문전량취소(string 한국거래소전송주문조직번호, string 원주문번호)
    {
        return await 주식주문정정취소(한국거래소전송주문조직번호, 원주문번호,
            주문구분: "00", 정정취소구분코드: "02", 주문수량: 0, 주문단가: 0, 잔량전부주문여부: "Y");
    }

    /// <summary>
    /// 주식주문정정취소
    /// </summary>
    /// <param name="한국거래소전송주문조직번호">KRX_FWDG_ORD_ORGNO "" (Null 값 설정), 주문시 한국투자증권 시스템에서 지정된 영업점코드</param>
    /// <param name="원주문번호">ORGN_ODNO API output1의 odno(주문번호) 값 입력, 주문시 한국투자증권 시스템에서 채번된 주문번호</param>
    /// <param name="주문구분">ORD_DVSN (00 : 지정가, 01 : 시장가)</param>
    /// <param name="정정취소구분코드">RVSE_CNCL_DVSN_CD (정정 : 01, 취소 : 02)</param>
    /// <param name="주문수량">ORD_QTY ([잔량전부 취소/정정주문] "0" 설정(QTY_ALL_ORD_YN= Y 설정 ), [잔량일부 취소/정정주문] 취소/정정 수량)</param>
    /// <param name="주문단가">ORD_UNPR ([정정] 정정주문 1주당 가격, [취소] "0" 설정)</param>
    /// <param name="잔량전부주문여부">QTY_ALL_ORD_YN ([정정/취소] Y : 잔량전부, N : 잔량일부)</param>
    /// <returns></returns>
    public async Task<(주식주문정정취소DTO? result, string error)> 주식주문정정취소(
        string 한국거래소전송주문조직번호, string 원주문번호, string 주문구분,
        string 정정취소구분코드, uint 주문수량, uint 주문단가, string 잔량전부주문여부)
    {
        string trId = _isVTS ? "VTTC0803U" : "TTTC0803U";
        using var client = NewHttp(trId);

        주식주문정정취소Query query = new()
        {
            CANO = this._account.CANO,
            ACNT_PRDT_CD = this._account.ACNT_PRDT_CD,
            KRX_FWDG_ORD_ORGNO = 한국거래소전송주문조직번호,
            ORGN_ODNO = 원주문번호,
            ORD_DVSN = 주문구분,
            RVSE_CNCL_DVSN_CD = 정정취소구분코드,
            ORD_QTY = 주문수량.ToString(),
            ORD_UNPR = 주문단가.ToString(),
            QTY_ALL_ORD_YN = 잔량전부주문여부,
            ALGO_NO = ""
        };

        string url = "/uapi/domestic-stock/v1/trading/order-rvsecncl";

        var response = await client.PostJsonContent(url, query);
        var respBody = await response.Content.ReadFromJsonAsync<PacketResponse<주식주문정정취소DTO>>();

        if (response.IsSuccessStatusCode)
        {
            return (respBody?.output, respBody?.msg1 ?? "");
        }

        return (null, respBody?.msg1 ?? "");
    }

    // https://apiportal.koreainvestment.com/apiservice/apiservice-domestic-stock
    // 주식잔고조회[v1_국내주식-006]
    public async Task<주식잔고조회DTO[]> 주식잔고조회()
    {
        string trId = _isVTS ? "VTTC8434R" : "TTTC8434R";
        using var client = NewHttp(trId);

        주식잔고조회Query query = new()
        {
            CANO = this._account.CANO,
            ACNT_PRDT_CD = this._account.ACNT_PRDT_CD,
        };

        string url = "/uapi/domestic-stock/v1/trading/inquire-balance?" + WebSerializer.ToQueryString(query);

        var response = await client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var respBody = await response.Content.ReadFromJsonAsync<PacketResponses<주식잔고조회DTO>>();
            return respBody?.output1 ?? Array.Empty<주식잔고조회DTO>();
        }

        return Array.Empty<주식잔고조회DTO>();
    }

}
