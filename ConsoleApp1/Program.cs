using eFriendOpenAPI;
using eFriendOpenAPI.Extension;
using eFriendOpenAPI.Packet;

namespace ConsoleApp1;

internal class Program
{
    static async Task Main(string[] args)
    {
        (string appKey, string secretKey, string account) = LoadKeyInfo("kinvest.key.01.txt");

        bool isVTS = false; // true: 모의 Domain, false: 실전 Domain
        bool useWebSocket = true;

        eFriendClient client = new eFriendClient(isVTS, appKey, secretKey, account);

        client.국내주식실시간체결가Arrived += (sender, e) =>
        {
            foreach (var item in e)
            {
                Console.WriteLine($"[{DateTime.Now}] [실시간 체결] {item}");
            }
        };

        string tempDirectory = Path.Combine(Path.GetTempPath(), "eFriendOpenAPI");
        await client.LoadKospiMasterCode(tempDirectory);
        await client.LoadKosdaqiMasterCode(tempDirectory);

        if (isVTS == false)
        {
            Console.WriteLine("*** 주의 사항: 운영 접속입니다. ***");
        }
        else
        {
            Console.WriteLine("*** 모의 접속입니다. ***");
        }

        if (await client.CheckAccessToken() == false)
        {
            Console.WriteLine("Failed to get AccessToken");
            return;
        }

        if (useWebSocket)
        {
            // 웹소켓
            if (await client.ConnectWebSocketAsync() == false)
            {
                Console.WriteLine("Failed to get connection for websocket");
                return;
            } else
            {
                Console.WriteLine($"[{DateTime.Now}] websocket connected.");
            }
        }

        {
            Console.WriteLine($"[계좌번호: {client.Account}]");
            var array = await client.주식잔고조회();

            if (array == null)
            {
                Console.WriteLine($"\t(None)");
            }
            else
            {
                foreach (주식잔고조회DTO? dto in array)
                {
                    Console.Write($"\t{dto}");
                    var 시세 = await client.주식현재가시세(dto.pdno);
                    Console.WriteLine($", 현재가={시세?.stck_prpr.ToMoney():n0}");
                }
            }
        }

        bool opend = await client.휴장일();

        if (opend == false)
        {
            Console.WriteLine("오늘은 휴장일입니다.");
            return;
        }
        else
        {
            // 주식 주문 및 취소 (테스트를 위해, 하한가로 주문 후 전량 취소)
            string 종목코드 = "305720"; // 종목코드(6자리)
            string 종목이름 = client.GetCodeName(종목코드);

            var 시세 = await client.주식현재가시세(종목코드);
            uint 주문단가 = 시세?.하한가 ?? 10_000;

            (주식주문현금DTO? order, string error) = await client.주식현금매수주문(종목코드, /*주문수량*/ 1, /*지정가*/ "00", 주문단가);
            if (order == null)
            {
                Console.WriteLine($"[{DateTime.Now}] [매수실패: 종목: {종목코드} ({종목이름}), 가격: {주문단가}] {error}");
            }
            else
            {
                Console.WriteLine($"[{DateTime.Now}] [현금매수] {order}");

                await Task.Delay(1000 * 5); // 5초 후 전량 취소

                (var canceled, error) = await client.주식주문전량취소(order.KRX_FWDG_ORD_ORGNO, order.ODNO);
                Console.WriteLine($"[{DateTime.Now}] [전량취소] {canceled} {error}");
            }
        }

        if (useWebSocket)
        {
            string 종목코드 = "226490"; // 종목코드(6자리)
            string 종목이름 = client.GetCodeName(종목코드);
            Console.WriteLine($"[{DateTime.Now}] [실시간 체결 구독] {종목코드}({종목이름})");
            await client.국내주식실시간체결가(종목코드, true);

            await Task.Delay(1000 * 30); // 30초 후 실시간 해제
            await client.국내주식실시간체결가(종목코드, false);
            Console.WriteLine($"[{DateTime.Now}] [실시간 체결 해제] {종목코드}({종목이름})");
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadLine();
    }

    private static (string appKey, string secretKey, string account) LoadKeyInfo(string keyFileName)
    {
        string[] keyInfo = File.ReadAllLines($@"D:\settings\{keyFileName}");
        return (keyInfo[0], keyInfo[1], keyInfo[2]);
    }
}
