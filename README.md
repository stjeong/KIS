# KIS (eFriend Expert)

![NuGet version](https://img.shields.io/nuget/v/eFriendOpenAPI)

C# - eFriend Expert OCX 예제를 .NET Core/5+ Console App에서 사용하는 방법
; https://www.sysnet.pe.kr/2/0/13482

## Usage instructions
Initialize Client object with appkey, appsecret and account

``` cs
// Install-Package eFriendOpenAPI

(string appKey, string secretKey, string account) = (...);
bool isVTS = false; // true: 모의 Domain, false: 실전 Domain

eFriendClient client = new eFriendClient(isVTS, appKey, secretKey, account);
```
***

## Call API

### 국내주식주문
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