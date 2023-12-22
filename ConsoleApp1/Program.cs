using eFriendOpenAPI;
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
            var dto = await client.주식현재가시세("305720");
            Console.WriteLine(dto);
        }

        {
            Console.WriteLine($"[계좌번호: {client.Account}]");
            주식잔고조회DTO[]? array = await client.주식잔고조회();

            if (array == null)
            {
                Console.WriteLine($"\t(None)");
            }
            else
            {
                foreach (주식잔고조회DTO? dto in array)
                {
                    Console.WriteLine($"\t{dto}");
                }
            }
        }
    }

    private static (string appKey, string secretKey, string account) LoadKeyInfo(string keyFileName)
    {
        string[] keyInfo = File.ReadAllLines($@"D:\settings\{keyFileName}");
        return (keyInfo[0], keyInfo[1], keyInfo[2]);
    }
}
