using eFriendOpenAPI.Extension;

namespace eFriendOpenAPI.Packet;

// open-trading-api/stocks_info/kis_kospi_code_mst.py
// https://github.com/koreainvestment/open-trading-api/blob/main/stocks_info/kis_kospi_code_mst.py
public class KOSPICode
{
    #pragma warning disable format
    public static int[] field_specs = [2, 1, 4, 4, 4,
                                       1, 1, 1, 1, 1,
                                       1, 1, 1, 1, 1,
                                       1, 1, 1, 1, 1,
                                       1, 1, 1, 1, 1,
                                       1, 1, 1, 1, 1,
                                       1, 9, 5, 5, 1,
                                       1, 1, 2, 1, 1,
                                       1, 2, 2, 2, 3,
                                       1, 3, 12, 12, 8,
                                       15, 21, 2, 7, 1,
                                       1, 1, 1, 1, 9,
                                       9, 9, 5, 9, 8,
                                       9, 3, 1, 1, 1
                                       ];

    public static string[] part2_columns = ["그룹코드", "시가총액규모", "지수업종대분류", "지수업종중분류", "지수업종소분류",
                                            "제조업", "저유동성", "지배구조지수종목", "KOSPI200섹터업종", "KOSPI100",
                                            "KOSPI50", "KRX", "ETP", "ELW발행", "KRX100",
                                            "KRX자동차", "KRX반도체", "KRX바이오", "KRX은행", "SPAC",
                                            "KRX에너지화학", "KRX철강", "단기과열", "KRX미디어통신", "KRX건설",
                                            "Non1", "KRX증권", "KRX선박", "KRX섹터_보험", "KRX섹터_운송",
                                            "SRI", "기준가", "매매수량단위", "시간외수량단위", "거래정지",
                                            "정리매매", "관리종목", "시장경고", "경고예고", "불성실공시",
                                            "우회상장", "락구분", "액면변경", "증자구분", "증거금비율",
                                            "신용가능", "신용기간", "전일거래량", "액면가", "상장일자",
                                            "상장주수", "자본금", "결산월", "공모가", "우선주",
                                            "공매도과열", "이상급등", "KRX300", "KOSPI", "매출액",
                                            "영업이익", "경상이익", "당기순이익", "ROE", "기준년월",
                                            "시가총액", "그룹사코드", "회사신용한도초과", "담보대출가능", "대주가능"
                                         ];
    #pragma warning restore format

    public string 단축코드 { get; set; } = "";
    public string 표준코드 { get; set; } = "";
    public string 한글명 { get; set; } = "";

    public string 그룹코드 { get; set; } = "";
    public string 시가총액규모 { get; set; } = "";
    public string 지수업종대분류 { get; set; } = "";
    public string 지수업종중분류 { get; set; } = "";
    public string 지수업종소분류 { get; set; } = "";
    public string 제조업 { get; set; } = "";
    public string 저유동성 { get; set; } = "";
    public string 지배구조지수종목 { get; set; } = "";
    public string KOSPI200섹터업종 { get; set; } = "";
    public string KOSPI100 { get; set; } = "";
    public string KOSPI50 { get; set; } = "";
    public string KRX { get; set; } = "";
    public string ETP { get; set; } = "";
    public string ELW발행 { get; set; } = "";
    public string KRX100 { get; set; } = "";
    public string KRX자동차 { get; set; } = "";
    public string KRX반도체 { get; set; } = "";
    public string KRX바이오 { get; set; } = "";
    public string KRX은행 { get; set; } = "";
    public string SPAC { get; set; } = "";
    public string KRX에너지화학 { get; set; } = "";
    public string KRX철강 { get; set; } = "";
    public string 단기과열 { get; set; } = "";
    public string KRX미디어통신 { get; set; } = "";
    public string KRX건설 { get; set; } = "";
    public string Non1 { get; set; } = "";
    public string KRX증권 { get; set; } = "";
    public string KRX선박 { get; set; } = "";
    public string KRX섹터_보험 { get; set; } = "";
    public string KRX섹터_운송 { get; set; } = "";
    public string SRI { get; set; } = "";
    public string 기준가 { get; set; } = "";
    public string 매매수량단위 { get; set; } = "";
    public string 시간외수량단위 { get; set; } = "";
    public string 거래정지 { get; set; } = "";
    public string 정리매매 { get; set; } = "";
    public string 관리종목 { get; set; } = "";
    public string 시장경고 { get; set; } = "";
    public string 경고예고 { get; set; } = "";
    public string 불성실공시 { get; set; } = "";
    public string 우회상장 { get; set; } = "";
    public string 락구분 { get; set; } = "";
    public string 액면변경 { get; set; } = "";
    public string 증자구분 { get; set; } = "";
    public string 증거금비율 { get; set; } = "";
    public string 신용가능 { get; set; } = "";
    public string 신용기간 { get; set; } = "";
    public string 전일거래량 { get; set; } = "";
    public string 액면가 { get; set; } = "";
    public string 상장일자 { get; set; } = "";
    public string 상장주수 { get; set; } = "";
    public string 자본금 { get; set; } = "";
    public string 결산월 { get; set; } = "";
    public string 공모가 { get; set; } = "";
    public string 우선주 { get; set; } = "";
    public string 공매도과열 { get; set; } = "";
    public string 이상급등 { get; set; } = "";
    public string KRX300 { get; set; } = "";
    public string KOSPI { get; set; } = "";
    public string 매출액 { get; set; } = "";
    public string 영업이익 { get; set; } = "";
    public string 경상이익 { get; set; } = "";
    public string 당기순이익 { get; set; } = "";
    public string ROE { get; set; } = "";
    public string 기준년월 { get; set; } = "";
    public string 시가총액 { get; set; } = "";
    public string 그룹사코드 { get; set; } = "";
    public string 회사신용한도초과 { get; set; } = "";
    public string 담보대출가능 { get; set; } = "";
    public string 대주가능 { get; set; } = "";

    public static KOSPICode ReadFromMSTFile(string line)
    {
        KOSPICode code = new KOSPICode();

        string rf1 = line[0..(line.Length - 228)];
        code.단축코드 = rf1[0..9].Trim();
        code.표준코드 = rf1[9..21].Trim();
        code.한글명 = rf1[21..].Trim();

        int fieldTotalLength = field_specs.Sum();
        string rf2 = line[^fieldTotalLength..];

        Type type = code.GetType();

        foreach (var field in field_specs.Zip(part2_columns, (value, name) => (value, name)))
        {
            string fValue = rf2[0..field.value].Trim();
            type.GetProperty(field.name)?.SetValue(code, fValue);

            rf2 = rf2[field.value..];
        }

        return code;
    }

    public override string ToString()
    {
        // 당기 순이익 = Quater별 순이익 합
        // ROE(자기자본수익률) = 당기 순이익 / 자기 자본 (Higher is better)

        decimal 기준가 = this.기준가.ToMoney();
        decimal ROE = this.ROE.ToMoney();
        decimal 당기순이익 = this.당기순이익.ToMoney();
        decimal 매출액 = this.매출액.ToMoney();
        decimal 시가총액 = this.시가총액.ToMoney();

        return $"{한글명} ({단축코드}), 기준가={기준가:n0}, ROE={ROE}, 당기순이익={당기순이익:n0}(억), 매출액={매출액:n0}(억), 시가총액={시가총액:n0}(억)";
    }
}
