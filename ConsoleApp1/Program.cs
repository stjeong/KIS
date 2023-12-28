using eFriendOpenAPI;
using eFriendOpenAPI.Packet;
using System.Diagnostics.CodeAnalysis;

namespace ConsoleApp1;

internal class Program
{
    [AllowNull]
    static KOSPICode[] s_kospiCodes;

    [AllowNull]
    static KOSDAQCode[] s_kosdaqCodes;

    static async Task Main(string[] args)
    {
        (string appKey, string secretKey, string account) = LoadKeyInfo("kinvest.key.01.txt");

        bool isVTS = false; // true: 모의 Domain, false: 실전 Domain

        eFriendClient client = new eFriendClient(isVTS, appKey, secretKey, account);
        client.DebugMode = true;

        string tempDirectory = Path.Combine(Path.GetTempPath(), "eFriendOpenAPI");
        s_kospiCodes = await client.LoadKospiMasterCode(tempDirectory);
        s_kosdaqCodes = await client.LoadKosdaqiMasterCode(tempDirectory);

        var 유한양행 = s_kospiCodes.First(x => x.한글명 == "유한양행");
        var 차백신연구소 = s_kosdaqCodes.First(x => x.한글명 == "차백신연구소");

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
            var array = await client.주식잔고조회();

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
