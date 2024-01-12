# KIS (eFriend Expert)

![NuGet version](https://img.shields.io/nuget/v/eFriendOpenAPI)

한국투자증권 KIS Developers OpenAPI의 C# 래퍼 버전 - eFriendOpenAPI NuGet 패키지 - https://www.sysnet.pe.kr/2/0/13498

## Usage instructions
Initialize Client object with appkey, appsecret and account

``` cs
// Install-Package eFriendOpenAPI

(string appKey, string secretKey, string account) = (...);
bool isVTS = false; // true: 모의 Domain, false: 실전 Domain

eFriendClient client = new eFriendClient(isVTS, appKey, secretKey, account);

// AccessToken 발급
if (await client.CheckAccessToken() == false)
{
    Console.WriteLine("Failed to get AccessToken");
    return;
}

// 웹소켓 연결
if (await client.ConnectWebSocketAsync() == false)
{
    Console.WriteLine("Failed to get connection for websocket");
    return;
}
```
***

## Call API

### 국내주식주문

#### 주식주문(현금)[v1_국내주식-001] (/uapi/domestic-stock/v1/trading/order-cash)

``` cs
string 종목코드 = "329200"; // 종목코드(6자리)
var 시세 = await client.주식현재가시세(종목코드);
uint 주문단가 = 시세?.하한가 ?? 10_000;

(주식주문현금DTO? order, string error) = await client.주식현금매수주문(종목코드, /*주문수량*/ 1, /*지정가*/ "00", 주문단가);
```
***

#### 주식주문(정정취소)[v1_국내주식-003] (/uapi/domestic-stock/v1/trading/order-rvsecncl)

``` cs
(주식주문현금DTO? order, string error) = await client.주식현금매수주문(종목코드, /*주문수량*/ 1, /*지정가*/ "00", 주문단가);

await Task.Delay(1000 * 5); // 5초 후 전량 취소

(var canceled, error) = await client.주식주문전량취소(order.KRX_FWDG_ORD_ORGNO, order.ODNO);
```
***

#### 주식잔고조회 (/uapi/domestic-stock/v1/trading/inquire-balance)

``` cs
var array = await client.주식잔고조회();

foreach (주식잔고조회DTO dto in array)
{
    Console.WriteLine($"\t{dto}");
}
```
***

### 국내주식시세
#### 주식현재가 시세 (/uapi/domestic-stock/v1/quotations/inquire-price)
``` cs
var dto = await client.주식현재가시세("305720");
Console.WriteLine(dto);
// 출력 결과:
// (최저가: 23235) ~ (현재가: 23235) ~ (최고가: 23895), 전일 대비: -465, 호가 단위: 5, (305720, ETF)
```
***

### 실시간시세(국내주식)
#### 국내주식 실시간체결가 (/tryitout/H0STCNT0)
``` cs

// 실시간 체결가 이벤트 핸들러 등록
client.국내주식실시간체결가Arrived += (sender, e) =>
{
    foreach (var item in e)
    {
        Console.WriteLine($"[실시간 체결] {item}");
    }
};

// 실시간 체결가 등록
await client.국내주식실시간체결가(종목코드, true); // true == 이벤트 등록

await Task.Delay(1000 * 5); // 5초 후 등록 해제

await client.국내주식실시간체결가(종목코드, false); // false == 이벤트 해제
```
***
