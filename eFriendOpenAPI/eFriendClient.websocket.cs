using ConsoleApp1.Packet;
using eFriendOpenAPI.Packet;
using System.Buffers;
using System.Net.Http.Json;
using System.Net.WebSockets;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace eFriendOpenAPI;
partial class eFriendClient
{
    public event EventHandler<국내주식실시간체결가DTO[]>? 국내주식실시간체결가Arrived;

    public async Task<string> GetApprovalKey()
    {
        using var client = NewHttp();
        var response = await client.PostAsJsonAsync("/oauth2/Approval", _tokenPRequest.AsApprovalRequest());
        if (response.IsSuccessStatusCode)
        {
            var respBody = await response.Content.ReadFromJsonAsync<OAuth2ApprovalResponse>();
            return respBody?.ApprovalKey ?? "";
        }

        return "";
    }

    protected virtual void On국내주식실시간체결가Event(국내주식실시간체결가DTO[] e)
    {
        국내주식실시간체결가Arrived?.Invoke(this, e);
    }

    public async Task<bool> 국내주식실시간체결가(string 종목코드 /* tr_key */, bool register)
    {
        if (_webSocket == null)
        {
            return false;
        }

        string tr_type = register ? "1" : "2";
        string custtype = _isLegalPerson ? "B" : "P";
        국내주식실시간체결가Query query = new 국내주식실시간체결가Query(_approvalKey, custtype, tr_type, 종목코드);

        string jsonRequest = JsonSerializer.Serialize(query);
        byte[] buffer = Encoding.UTF8.GetBytes(jsonRequest);
        await _webSocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);

        return true;
    }

    /// <summary>
    /// 국내주식 실시간 체결통보 수신 시에 (1) 주문·정정·취소·거부 접수 통보 와 (2) 체결 통보 가 모두 수신됩니다.
    /// </summary>
    /// <param name="tr_key">HTS ID</param>
    /// <param name="register">true : 등록, false : 해제</param>
    /// <returns></returns>
    public async Task<bool> 국내주식실시간체결통보(string tr_key, bool register)
    {
        if (_webSocket == null)
        {
            return false;
        }

        string tr_type = register ? "1" : "2";
        string custtype = _isLegalPerson ? "B" : "P";
        국내주식실시간체결통보Query query = new 국내주식실시간체결통보Query(_approvalKey, custtype, tr_type, tr_key, _isVTS);

        string jsonRequest = JsonSerializer.Serialize(query);
        byte[] buffer = Encoding.UTF8.GetBytes(jsonRequest);
        await _webSocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);

        return true;
    }

    public async Task<bool> ConnectWebSocketAsync()
    {
        if (string.IsNullOrEmpty(_approvalKey) == true)
        {
            string approvalKey = await GetApprovalKey();
            if (string.IsNullOrEmpty(approvalKey))
            {
                Console.WriteLine("Failed to get approval key");
                return false;
            }

            _approvalKey = approvalKey;
        }

        string url = (_isVTS) ? "ws://ops.koreainvestment.com:31000" : "ws://ops.koreainvestment.com:21000";
        var connectTimeout = new CancellationTokenSource();
        connectTimeout.CancelAfter(2000);

        _webSocket = new ClientWebSocket();
        _webSocket.Options.KeepAliveInterval = TimeSpan.FromSeconds(0);

        await _webSocket.ConnectAsync(new Uri(url), connectTimeout.Token);

        if (_webSocket.State != System.Net.WebSockets.WebSocketState.Open)
        {
            Console.WriteLine($"Failed to connect: {url}");
            return false;
        }

        _ = Task.Run(() => WebSocketProc());

        return true;
    }

    private async Task WebSocketProc()
    {
        if (_webSocket == null)
        {
            return;
        }

        string aesKey = "";
        string aesIV = "";

        while (_webSocket.State == System.Net.WebSockets.WebSocketState.Open)
        {
            byte[] rentBuffer = ArrayPool<byte>.Shared.Rent(16384);

            try
            {
                var buffer = new ArraySegment<byte>(rentBuffer);
                var result = await _webSocket.ReceiveAsync(buffer, CancellationToken.None);
                if (result.MessageType == System.Net.WebSockets.WebSocketMessageType.Close)
                {
                    await _webSocket.CloseAsync(System.Net.WebSockets.WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                }
                else
                {
                    byte[] encoded = buffer.Array ?? Array.Empty<byte>();

                    if (encoded.Length == 0)
                    {
                        continue;
                    }

                    string data = Encoding.UTF8.GetString(encoded, 0, encoded.Length);
                    string[] recvstr;
                    string trid0;
                    int data_cnt;

                    // https://github.com/koreainvestment/open-trading-api/blob/main/websocket/python/ws_domestic_overseas_all.py
                    switch (encoded[0])
                    {
                        case (byte)'0':
                            recvstr = data.Split('|');
                            trid0 = recvstr[1];

                            switch (trid0)
                            {
                                case 국내주식실시간체결가Query.TR_ID: // 주식 체결 데이터 처리
                                    data_cnt = int.Parse(recvstr[2]);
                                    stockspurchase_domestic(data_cnt, recvstr[3]);
                                    break;
                            }
                            break;

                        case (byte)'1':
                            recvstr = data.Split('|');
                            trid0 = recvstr[1];

                            switch (trid0)
                            {
                                case 국내주식실시간체결통보Query.TR_ID:
                                case 국내주식실시간체결통보Query.TR_VTS_ID:
                                    stocksigningnotice_domestic(recvstr[3], aesKey, aesIV);
                                    break;
                            }
                            break;

                        default:
                            WebSocketResponse? response = JsonSerializer.Deserialize<WebSocketResponse>(data);
                            string? tr_key = response?.header?.tr_key;
                            string? rt_cd = response?.body?.rt_cd;
                            string? tr_id = response?.header?.tr_id;
                            string? msg1 = response?.body?.msg1;

                            string? aes_key = response?.body?.output?.key;
                            string? aes_iv = response?.body?.output?.iv;

                            switch (response?.header?.tr_id)
                            {
                                case "PINGPONG":
                                    System.Diagnostics.Trace.WriteLine(data);
                                    break;

                                default:
                                    switch (rt_cd)
                                    {
                                        case "1":
                                            if (msg1 != "ALREADY IN SUBSCRIBE")
                                            {
                                                System.Diagnostics.Trace.WriteLine($"### ERROR RETURN CODE [{tr_key}][{rt_cd}] MSG [{msg1}]");
                                            }
                                            break;

                                        case "0":
                                            System.Diagnostics.Trace.WriteLine($"### RETURN CODE [{tr_key}][{rt_cd}] MSG [{msg1}]");
                                            switch (tr_id)
                                            {
                                                case 국내주식실시간체결통보Query.TR_ID:
                                                case 국내주식실시간체결통보Query.TR_VTS_ID:
                                                    aesKey = aes_key ?? "";
                                                    aesIV = aes_iv ?? "";
                                                    System.Diagnostics.Trace.WriteLine("### TRID [{trid}] KEY[{aesKey}] IV[{aesIV}]");
                                                    break;
                                            }
                                            break;
                                    }
                                    break;
                            }

                            break;
                    }
                }
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(rentBuffer);
            }
        }

        Console.WriteLine($"WebSocket: Closed");
    }

    public void stockspurchase_domestic(int data_cnt, string data)
    {
        string[] pValue = data.Split('^');
        국내주식실시간체결가DTO[] dtoArr = new 국내주식실시간체결가DTO[data_cnt];

        for (int cnt = 0; cnt < data_cnt; cnt++)
        {
            dtoArr[cnt] = 국내주식실시간체결가DTO.Parse(pValue);
            pValue = pValue[국내주식실시간체결가DTO.FieldCount..];
        }

        On국내주식실시간체결가Event(dtoArr);
    }

    public void stocksigningnotice_domestic(string data, string key, string iv)
    {
        // Aes.Decrypt()
    }
}
