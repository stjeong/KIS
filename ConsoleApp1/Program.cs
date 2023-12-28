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

        eFriendClient client = new eFriendClient(isVTS, appKey, secretKey, account);
        client.DebugMode = true;

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

        // 주식 주문 및 취소
        {
            string pdno = "305720"; // 종목코드(6자리)
            (주식주문현금DTO? order, string error) = await client.주식현금매수주문(
                /*종목코드*/ pdno, /*주문수량*/ 1, /*지정가*/ "00", /*주문단가*/ 20_000);
            if (order == null)
            {
                Console.WriteLine($"[Failed] {error}");
            }
            else
            {
                Console.WriteLine($"[현금매수] {order}");

                await Task.Delay(1000 * 5);

                (var canceled, error) = await client.주식주문전량취소(order.KRX_FWDG_ORD_ORGNO, order.ODNO);
                Console.WriteLine($"[전량취소] {canceled} {error}");
            }
        }
    }

    private static (string appKey, string secretKey, string account) LoadKeyInfo(string keyFileName)
    {
        string[] keyInfo = File.ReadAllLines($@"D:\settings\{keyFileName}");
        return (keyInfo[0], keyInfo[1], keyInfo[2]);
    }
}
