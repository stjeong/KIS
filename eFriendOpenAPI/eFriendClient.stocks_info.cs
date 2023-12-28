using eFriendOpenAPI.Packet;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFriendOpenAPI;
partial class eFriendClient
{
    // 코스닥주식종목코드(kosdaq_code.mst) 정제 파이썬 파일
    // https://github.com/koreainvestment/open-trading-api/blob/main/stocks_info/kis_kosdaq_code_mst.py
    public async Task<KOSDAQCode[]> LoadKosdaqiMasterCode(string baseDirectory)
    {
        string codeFileName = "kosdaq_code.mst";
        if (await CacheDownloadAsync(baseDirectory, codeFileName, TimeSpan.FromHours(23)) == false)
        {
            return Array.Empty<KOSDAQCode>();
        }

        // "%TEMP%\eFriendOpenAPI\kosdaq_code.mst"
        string mstFile = Path.Combine(baseDirectory, codeFileName);
        List<KOSDAQCode> list = new();

        foreach (string line in File.ReadAllLines(mstFile, Encoding.GetEncoding("euc-kr")))
        {
            list.Add(KOSDAQCode.ReadFromMSTFile(line));
        }

        return list.ToArray();
    }

    // 코스피주식종목코드(kospi_code.mst) 정제 파이썬 파일
    // https://github.com/koreainvestment/open-trading-api/blob/main/stocks_info/kis_kospi_code_mst.py
    public async Task<KOSPICode[]> LoadKospiMasterCode(string baseDirectory)
    {
        string codeFileName = "kospi_code.mst";
        if (await CacheDownloadAsync(baseDirectory, codeFileName, TimeSpan.FromHours(23)) == false)
        {
            return Array.Empty<KOSPICode>();
        }

        // "%TEMP%\eFriendOpenAPI\kospi_code.mst"
        string mstFile = Path.Combine(baseDirectory, codeFileName);
        List<KOSPICode> list = new();

        foreach (string line in File.ReadAllLines(mstFile, Encoding.GetEncoding("euc-kr")))
        {
            list.Add(KOSPICode.ReadFromMSTFile(line));
        }

        return list.ToArray();
    }

    private async Task<bool> CacheDownloadAsync(string baseDirectory, string codeFileName, TimeSpan expire)
    {
        string codeFilePath = Path.Combine(baseDirectory, codeFileName);
        if (File.Exists(codeFilePath))
        {
            FileInfo fileInfo = new FileInfo(codeFilePath);
            if (fileInfo.CreationTimeUtc.Add(expire) < DateTime.UtcNow)
            {
                File.Delete(codeFilePath);
            }
            else
            {
                return true;
            }
        }

        HttpClient client = new HttpClient();

        CancellationTokenSource tokenSource = new CancellationTokenSource();
        tokenSource.CancelAfter(TimeSpan.FromSeconds(10));

        string masterFile = $"https://new.real.download.dws.co.kr/common/master/{codeFileName}.zip";
        Stream? stream = await client.GetStreamAsync(masterFile, tokenSource.Token);

        if (stream == null)
        {
            return false;
        }

        using (stream)
        using (ZipArchive zipArchive = new ZipArchive(stream))
        {
            zipArchive.ExtractToDirectory(baseDirectory);
        }

        return true;
    }
}
