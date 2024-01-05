using ConsoleApp1.Packet;
using eFriendOpenAPI.Packet;
using System.Net.Http.Json;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace eFriendOpenAPI;
partial class eFriendClient
{
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

        while (_webSocket.State == System.Net.WebSockets.WebSocketState.Open)
        {
            var buffer = new ArraySegment<byte>(new byte[1024]);
            var result = await _webSocket.ReceiveAsync(buffer, CancellationToken.None);
            if (result.MessageType == System.Net.WebSockets.WebSocketMessageType.Close)
            {
                await _webSocket.CloseAsync(System.Net.WebSockets.WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
            }
            else
            {
                byte[] encoded = buffer.Array ?? Array.Empty<byte>();
                string msg = Encoding.UTF8.GetString(encoded, 0, encoded.Length);
                Console.WriteLine($"[{DateTime.Now}] WebSocket: {msg}");
            }
        }

        Console.WriteLine($"WebSocket: Closed");
    }
}
