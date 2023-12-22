﻿using ConsoleApp1.Packet;
using Cysharp.Web;
using eFriendOpenAPI.Extension;
using eFriendOpenAPI.Packet;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace eFriendOpenAPI;

public class eFriendClient
{
    static byte[] _entropy = { 0xfd, 0xed, 0x10, 0x20, 0xe9, 0x75 };
    static HttpClientHandler _sharedHandler = new HttpClientHandler() { MaxConnectionsPerServer = 1 };
    string _baseUrl;
    TokenPRequest _tokenPRequest;
    AccessToken? _accessToken;
    계좌번호 _account;
    public 계좌번호 Account { get => _account; }

    bool _isVTS = false;
    bool _isLegalPerson = false;
    string _legalPersonSecretKey;
    string _macAddress = "";
    string _legalPhoneNumber = "";
    string _legalPublicIP = "";
    string _hashKey = ""; // Not supported yet.
    string _legalUID = "";

    public bool DebugMode { get; set; } = false;

    public eFriendClient(bool isVTS, string appKey, string secretKey, string account, bool isLegalPerson = false,
        string legalPersonSecretKey = "", string macAddress = "", string legalPhoneNumber = "",
        string legalPublicIP = "", string legalUID = "")
    {
        _isVTS = isVTS;
        _isLegalPerson = isLegalPerson;
        _legalPersonSecretKey = legalPersonSecretKey;
        _macAddress = macAddress;
        _legalPhoneNumber = legalPhoneNumber;
        _legalPublicIP = legalPublicIP;
        _legalUID = legalUID;

        _baseUrl = _isVTS ? "https://openapivts.koreainvestment.com:29443"
                           : "https://openapi.koreainvestment.com:9443";

        _tokenPRequest = new TokenPRequest()
        {
            AppKey = appKey,
            SecretKey = secretKey
        };

        string text = account.Replace(" ", "").Replace("-", "");
        _account = new 계좌번호(text);
    }

    public HttpClient NewHttp(string trId = "", bool dumpHttp = false)
    {
        HttpClient client;

        if (dumpHttp)
        {
            client = new HttpClient(new LoggingHandler(_sharedHandler), false);
        }
        else
        {
            client = new HttpClient(_sharedHandler, false);
        }

        client.BaseAddress = new Uri(_baseUrl);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        if (string.IsNullOrEmpty(trId) == false)
        {
            client.DefaultRequestHeaders.Add("authorization", $"Bearer {_accessToken?.Value}");
            client.DefaultRequestHeaders.Add("appkey", _tokenPRequest.AppKey);
            client.DefaultRequestHeaders.Add("appsecret", _tokenPRequest.SecretKey);

            client.DefaultRequestHeaders.Add("tr_id", trId);
            client.DefaultRequestHeaders.Add("tr_cont", "");

            client.DefaultRequestHeaders.Add("custtype", _isLegalPerson ? "B" : "P");

            if (string.IsNullOrEmpty(_macAddress) == false)
            {
                client.DefaultRequestHeaders.Add("mac_addr", _macAddress);
            }

            if (string.IsNullOrEmpty(_hashKey) == false)
            {
                client.DefaultRequestHeaders.Add("hashkey", _hashKey);
            }

            if (_isLegalPerson)
            {
                client.DefaultRequestHeaders.Add("personalseckey", _legalPersonSecretKey);
                client.DefaultRequestHeaders.Add("seq_no", "01");
                client.DefaultRequestHeaders.Add("phone_number", _legalPhoneNumber);
                client.DefaultRequestHeaders.Add("ip_addr", _legalPublicIP);
                client.DefaultRequestHeaders.Add("gt_uid", _legalUID);
            }
        }

        return client; ;
    }

    // 주식현재가 시세[v1_국내주식-008]
    // https://apiportal.koreainvestment.com/apiservice/apiservice-domestic-stock-quotations
    public async Task<주식현재가시세DTO?> 주식현재가시세(string FID_INPUT_ISCD, string FID_COND_MRKT_DIV_CODE = "J")
    {
        using var client = NewHttp("FHKST01010100");

        주식현재가시세Query query = new()
        {
            FID_COND_MRKT_DIV_CODE = FID_COND_MRKT_DIV_CODE,
            FID_INPUT_ISCD = FID_INPUT_ISCD,
        };

        string url = "/uapi/domestic-stock/v1/quotations/inquire-price?" + WebSerializer.ToQueryString(query);

        var response = await client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var respBody = await response.Content.ReadFromJsonAsync<PacketResponse<주식현재가시세DTO>>();
            return respBody?.output;
        }

        return null;
    }

    // https://apiportal.koreainvestment.com/apiservice/apiservice-domestic-stock
    // 주식잔고조회[v1_국내주식-006]
    public async Task<주식잔고조회DTO[]?> 주식잔고조회()
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
            return respBody?.output1;
        }

        return null;
    }

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

    public async Task<bool> CheckAccessToken()
    {
        AccessToken? preToken = ReadTokenFromDiskCache();
        if (preToken != null)
        {
            if (DateTime.Now.AddHours(1) < preToken.ExpiresAt)
            {
                _accessToken = preToken;
                return true;
            }
        }

        using var client = NewHttp();
        var response = await client.PostAsJsonAsync("/oauth2/tokenP", _tokenPRequest);

        if (response.IsSuccessStatusCode)
        {
            var respBody = await response.Content.ReadFromJsonAsync<AccessToken>();
            if (respBody != null)
            {
                _accessToken = respBody;
                _accessToken.ExpiresAt = DateTime.Now.AddSeconds((double)respBody.ExpiresIn);
                WriteTokenToDiskCache(respBody);

                if (this.DebugMode)
                {
                    Console.WriteLine($"Token: {respBody.Value}");
                    Console.WriteLine($"Type: {respBody.Type}");
                    Console.WriteLine($"ExpiresIn: {respBody.ExpiresIn} ({respBody.ExpiresAt})");
                }
            }

            return respBody != null;
        }

        return default;
    }

    private AccessToken? ReadTokenFromDiskCache()
    {
        string keyFilePath = GetKeyFilePath();
        if (File.Exists(keyFilePath) == false)
        {
            return null;
        }

        byte[] encrypted = File.ReadAllBytes(keyFilePath);
        byte[] decrypted = encrypted;

        try
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                decrypted = ProtectedData.Unprotect(encrypted, _entropy, DataProtectionScope.CurrentUser);
            }
        }
        catch (CryptographicException)
        {
            return null;
        }

        string userData = Encoding.UTF8.GetString(decrypted);

        try
        {
            return JsonSerializer.Deserialize<AccessToken>(userData);
        }
        catch (JsonException)
        {
            return null;
        }
    }

    private void WriteTokenToDiskCache(AccessToken accessToken)
    {
        string jsonString = "";

        try
        {
            jsonString = JsonSerializer.Serialize(accessToken);
        }
        catch (JsonException)
        {
            return;
        }

        byte[] userData = Encoding.UTF8.GetBytes(jsonString);
        byte[] encrypted = userData;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            encrypted = ProtectedData.Protect(userData, _entropy, DataProtectionScope.CurrentUser);
        }

        File.WriteAllBytes(GetKeyFilePath(), encrypted);
    }

    string GetKeyFilePath()
    {
        string tempDir = Path.GetTempPath();
        string fileName = $@"hts.token.{CreateSHA512(_tokenPRequest.AppKey)[0..8]}";
        string tempFilePath = Path.Combine(tempDir, fileName);

        return tempFilePath;
    }

    public string CreateSHA512(string strData)
    {
        var message = Encoding.UTF8.GetBytes(strData);
        using (var alg = SHA512.Create())
        {
            var hashValue = alg.ComputeHash(message);
            return System.Convert.ToHexString(hashValue);
        }
    }
}