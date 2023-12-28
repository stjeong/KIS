using eFriendOpenAPI.Extension;

namespace eFriendOpenAPI.Packet;

// open-trading-api/stocks_info/kis_kosdaq_code_mst.py
// https://github.com/koreainvestment/open-trading-api/blob/main/stocks_info/kis_kosdaq_code_mst.py
public class KOSDAQCode
{
        #pragma warning disable format
    public static int[] field_specs = [2, 1,
                                       4, 4, 4, 1, 1,
                                       1, 1, 1, 1, 1,
                                       1, 1, 1, 1, 1,
                                       1, 1, 1, 1, 1,
                                       1, 1, 1, 1, 9,
                                       5, 5, 1, 1, 1,
                                       2, 1, 1, 1, 2,
                                       2, 2, 3, 1, 3,
                                       12, 12, 8, 15, 21,
                                       2, 7, 1, 1, 1,
                                       1, 9, 9, 9, 5,
                                       9, 8, 9, 3, 1,
                                       1, 1
                                       ];

    public static string[] part2_columns = ["증권그룹구분코드","시가총액 규모 구분 코드 유가",
                                             "지수업종 대분류 코드","지수 업종 중분류 코드","지수업종 소분류 코드","벤처기업 여부 (Y/N)",
                                             "저유동성종목 여부","KRX 종목 여부","ETP 상품구분코드","KRX100 종목 여부 (Y/N)",
                                             "KRX 자동차 여부","KRX 반도체 여부","KRX 바이오 여부","KRX 은행 여부","기업인수목적회사여부",
                                             "KRX 에너지 화학 여부","KRX 철강 여부","단기과열종목구분코드","KRX 미디어 통신 여부",
                                             "KRX 건설 여부","(코스닥)투자주의환기종목여부","KRX 증권 구분","KRX 선박 구분",
                                             "KRX섹터지수 보험여부","KRX섹터지수 운송여부","KOSDAQ150지수여부 (Y,N)","주식 기준가",
                                             "정규 시장 매매 수량 단위","시간외 시장 매매 수량 단위","거래정지 여부","정리매매 여부",
                                             "관리 종목 여부","시장 경고 구분 코드","시장 경고위험 예고 여부","불성실 공시 여부",
                                             "우회 상장 여부","락구분 코드","액면가 변경 구분 코드","증자 구분 코드","증거금 비율",
                                             "신용주문 가능 여부","신용기간","전일 거래량","주식 액면가","주식 상장 일자","상장 주수(천)",
                                             "자본금","결산 월","공모 가격","우선주 구분 코드","공매도과열종목여부","이상급등종목여부",
                                             "KRX300 종목 여부 (Y/N)","매출액","영업이익","경상이익","단기순이익","ROE(자기자본이익률)",
                                             "기준년월","전일기준 시가총액 (억)","그룹사 코드","회사신용한도초과여부","담보대출가능여부","대주가능여부"
                                             ];
#pragma warning restore format

    public string 단축코드 { get; set; } = "";
    public string 표준코드 { get; set; } = "";
    public string 한글명 { get; set; } = "";

    public string 구분코드 { get; set; } = ""; // 증권그룹
    public string 시가총액규모 { get; set; } = ""; // 구분코드유가
    public string 지수업종대분류 { get; set; } = ""; // 코드
    public string 지수업종중분류 { get; set; } = ""; // 코드
    public string 지수업종소분류 { get; set; } = ""; // 코드
    public string 벤처기업 { get; set; } = ""; // 여부 (Y/N)
    public string 저유동성 { get; set; } = ""; // 종목 여부 
    public string KRX { get; set; } = ""; // 종목 여부 
    public string ETP { get; set; } = ""; // 상품구분코드
    public string KRX100 { get; set; } = ""; // 종목 여부 (Y/N)
    public string KRX자동차 { get; set; } = ""; // 여부
    public string KRX반도체 { get; set; } = ""; // 여부
    public string KRX바이오 { get; set; } = ""; // 여부 
    public string KRX은행 { get; set; } = ""; // 여부
    public string 기업인수목적회사여부 { get; set; } = "";
    public string KRX에너지화학 { get; set; } = ""; // 여부
    public string KRX철강 { get; set; } = ""; // 여부
    public string 단기과열 { get; set; } = ""; //종목구분코드
    public string KRX미디어통신 { get; set; } = ""; // 여부
    public string KRX건설 { get; set; } = ""; // 여부
    public string 투자주의환기종목여부 { get; set; } = ""; // (코스닥) 
    public string KRX증권 { get; set; } = ""; // 구분
    public string KRX선박 { get; set; } = ""; // 구분
    public string KRX섹터지수보험 { get; set; } = ""; // 여부
    public string KRX섹터지수운송 { get; set; } = ""; // 여부
    public string KOSDAQ150지수 { get; set; } = ""; // 여부(Y, N)
    public string 기준가 { get; set; } = ""; // 주식 
    public string 매매수량단위 { get; set; } = ""; // 정규 시장 
    public string 시간외수량단위 { get; set; } = ""; // 시간외 시장 매매
    public string 거래정지 { get; set; } = ""; // 여부
    public string 정리매매 { get; set; } = ""; // 여부
    public string 관리종목 { get; set; } = ""; // 여부
    public string 시장경고 { get; set; } = ""; // 구분 코드
    public string 경고예고 { get; set; } = ""; // 시장 경고위험 예고 여부
    public string 불성실공시 { get; set; } = ""; // 여부
    public string 우회상장 { get; set; } = ""; // 여부
    public string 락구분 { get; set; } = ""; // 코드
    public string 액면변경 { get; set; } = ""; // 액면가 변경 구분 코드
    public string 증자구분 { get; set; } = ""; // 코드
    public string 증거금비율 { get; set; } = "";
    public string 신용가능 { get; set; } = ""; // 신용주문 가능 여부 
    public string 신용기간 { get; set; } = "";
    public string 전일거래량 { get; set; } = "";
    public string 액면가 { get; set; } = ""; // 주식 액면가
    public string 상장일자 { get; set; } = ""; // 주식 상장 일자
    public string 상장주수 { get; set; } = ""; // 단위: (천)
    public string 자본금 { get; set; } = "";
    public string 결산월 { get; set; } = "";
    public string 공모가 { get; set; } = ""; // 공모가격
    public string 우선주 { get; set; } = ""; // 구분 코드
    public string 공매도과열 { get; set; } = ""; // 종목여부
    public string 이상급등 { get; set; } = ""; // 종목 여부
    public string KRX300 { get; set; } = ""; // 종목 여부(Y/N)
    public string 매출액 { get; set; } = "";
    public string 영업이익 { get; set; } = "";
    public string 경상이익 { get; set; } = "";
    public string 당기순이익 { get; set; } = "";
    public string ROE { get; set; } = ""; // (자기자본이익률)
    public string 기준년월 { get; set; } = "";
    public string 시가총액 { get; set; } = ""; // 전일기준 단위: (억)
    public string 그룹사코드 { get; set; } = "";
    public string 회사신용한도초과 { get; set; } = ""; // 여부
    public string 담보대출가능 { get; set; } = ""; // 여부
    public string 대주가능 { get; set; } = ""; // 여부

    public static KOSDAQCode ReadFromMSTFile(string line)
    {
        KOSDAQCode code = new KOSDAQCode();

        string rf1 = line[0..(line.Length - 222)];
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
        decimal 경상이익 = this.경상이익.ToMoney();
        decimal 영업이익 = this.영업이익.ToMoney();
        decimal 매출액 = this.매출액.ToMoney();
        decimal 자본금 = this.자본금.ToMoney() / 100_000_000; // 1억

        return $"{한글명} ({단축코드}), ROE={경상이익:n0}(억), 영업이익={영업이익:n0}(억), 매출액={매출액:n0}(억), 시가총액={자본금:n0}(억)";
    }
}
