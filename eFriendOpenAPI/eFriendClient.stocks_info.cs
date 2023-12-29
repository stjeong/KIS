using eFriendOpenAPI.Packet;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eFriendOpenAPI;
partial class eFriendClient
{
    [AllowNull]
    static Dictionary<string, KOSPICode> s_kospiCodes;

    [AllowNull]
    static Dictionary<string, KOSDAQCode> s_kosdaqCodes;

    public bool CodeExists(string code)
    {
        if (s_kospiCodes.ContainsKey(code) == true)
        {
            return true;
        }

        return s_kosdaqCodes.ContainsKey(code);
    }

    // 코스닥주식종목코드(kosdaq_code.mst) 정제 파이썬 파일
    // https://github.com/koreainvestment/open-trading-api/blob/main/stocks_info/kis_kosdaq_code_mst.py
    public async Task<Dictionary<string, KOSDAQCode>> LoadKosdaqiMasterCode(string baseDirectory)
    {
        Dictionary<string, KOSDAQCode> codes = new();

        string codeFileName = "kosdaq_code.mst";
        if (await CacheDownloadAsync(baseDirectory, codeFileName, TimeSpan.FromHours(23)) == false)
        {
            return codes;
        }

        // "%TEMP%\eFriendOpenAPI\kosdaq_code.mst"
        string mstFile = Path.Combine(baseDirectory, codeFileName);

        foreach (string line in File.ReadAllLines(mstFile, Encoding.GetEncoding("ks_c_5601-1987")))
        {
            var item = KOSDAQCode.ReadFromMSTFile(line);
            codes[item.단축코드] = item;
        }

        s_kosdaqCodes = codes;
        return codes;
    }

    // 코스피주식종목코드(kospi_code.mst) 정제 파이썬 파일
    // https://github.com/koreainvestment/open-trading-api/blob/main/stocks_info/kis_kospi_code_mst.py
    public async Task<Dictionary<string, KOSPICode>> LoadKospiMasterCode(string baseDirectory)
    {
        Dictionary<string, KOSPICode> codes = new();

        string codeFileName = "kospi_code.mst";
        if (await CacheDownloadAsync(baseDirectory, codeFileName, TimeSpan.FromHours(23)) == false)
        {
            return codes;
        }

        // "%TEMP%\eFriendOpenAPI\kospi_code.mst"
        string mstFile = Path.Combine(baseDirectory, codeFileName);

        foreach (string line in File.ReadAllLines(mstFile, Encoding.GetEncoding("ks_c_5601-1987")))
        {
            var item = KOSPICode.ReadFromMSTFile(line);
            codes[item.단축코드] = item;
        }

        s_kospiCodes = codes;
        return codes;
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
