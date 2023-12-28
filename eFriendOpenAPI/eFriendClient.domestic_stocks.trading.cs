using Cysharp.Web;
using eFriendOpenAPI.Packet;
using System.Net.Http.Json;

namespace eFriendOpenAPI;

partial class eFriendClient
{
    /// <summary>
    /// 주식현금매수주문
    /// </summary>
    /// <param name="PDNO">종목코드(6자리, ETN의 경우, Q로 시작 (EX. Q500001))</param>
    /// <param name="ORD_QTY">주문수량</param>
    /// <param name="ORD_DVSN">주문구분(00 : 지정가, 01 : 시장가, ...)</param>
    /// <param name="ORD_UNPR">주문단가</param>
    /// <returns></returns>
    public async Task<(주식주문현금DTO? rseult, string error)> 주식현금매수주문(string PDNO, uint ORD_QTY, string ORD_DVSN = "01", uint ORD_UNPR = 0)
    {
        if (ORD_DVSN == "00" && ORD_UNPR == 0)
        {
            throw new ArgumentException("[매수주문] 지정가 주문시 주문단가를 입력해야 합니다.");
        }

        string trId = _isVTS ? "VTTC0802U" : "TTTC0802U";
        return await 주식주문현금(trId, PDNO, ORD_DVSN, ORD_QTY, ORD_UNPR);
    }

    /// <summary>
    /// 주식현금매도주문
    /// </summary>
    /// <param name="PDNO">종목코드(6자리)</param>
    /// <param name="ORD_QTY">주문수량</param>
    /// <param name="ORD_DVSN">주문구분(00 : 지정가, 01 : 시장가, ...)</param>
    /// <param name="ORD_UNPR">주문단가</param>
    /// <returns></returns>
    public async Task<(주식주문현금DTO? rseult, string error)> 주식현금매도주문(string PDNO, uint ORD_QTY, string ORD_DVSN = "01", uint ORD_UNPR = 0)
    {
        if (ORD_DVSN == "00" && ORD_UNPR == 0)
        {
            throw new ArgumentException("[매도주문] 지정가 주문시 주문단가를 입력해야 합니다.");
        }

        string trId = _isVTS ? "VTTC0801U" : "TTTC0801U";
        return await 주식주문현금(trId, PDNO, ORD_DVSN, ORD_QTY, ORD_UNPR);
    }

    // 주식주문(현금)[v1_국내주식-001]
    // https://apiportal.koreainvestment.com/apiservice/apiservice-domestic-stock
    async Task<(주식주문현금DTO? rseult, string error)> 주식주문현금(string trId, string PDNO, string ORD_DVSN, uint ORD_QTY, uint ORD_UNPR)
    {
        using var client = NewHttp(trId);

        주식주문현금Query query = new()
        {
            CANO = this._account.CANO,
            ACNT_PRDT_CD = this._account.ACNT_PRDT_CD,
            PDNO = PDNO,
            ORD_DVSN = ORD_DVSN,
            ORD_QTY = ORD_QTY.ToString(),
            ORD_UNPR = ORD_UNPR.ToString(),
            ALGO_NO = ""
        };

        string url = "/uapi/domestic-stock/v1/trading/order-cash";

        var response = await client.PostAsJsonAsync(url, query);
        var respBody = await response.Content.ReadFromJsonAsync<PacketResponse<주식주문현금DTO>>();

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
