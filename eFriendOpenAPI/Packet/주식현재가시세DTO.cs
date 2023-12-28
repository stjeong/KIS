using eFriendOpenAPI.Extension;

namespace eFriendOpenAPI.Packet;

public class 주식현재가시세Query
{
    public string FID_COND_MRKT_DIV_CODE { get; set; } = "J"; // FID 조건 시장 분류 코드
    public string FID_INPUT_ISCD { get; set; } = ""; // FID 입력 종목코드
}

public class 주식현재가시세DTO
{
    public string iscd_stat_cls_code { get; set; } = ""; // 종목 상태 구분 코드 00 : 그외
                                                         // 51 : 관리종목
                                                         // 52 : 투자위험
                                                         // 53 : 투자경고
                                                         // 54 : 투자주의
                                                         // 55 : 신용가능
                                                         // 57 : 증거금 100%
                                                         // 58 : 거래정지
                                                         // 59 : 단기과열
    public string stringmarg_rate { get; set; } = ""; // 증거금 비율
    public string rprs_mrkt_kor_name { get; set; } = ""; //  대표 시장 한글 명 String  Y	40	
    public string new_hgpr_lwpr_cls_code { get; set; } = ""; //  신 고가 저가 구분 코드   String Y    10	조회하는 종목이 신고/신저에 도달했을 경우에만 조회됨
    public string bstp_kor_isnm { get; set; } = ""; //  업종 한글 종목명   String Y 40	
    public string temp_stop_yn { get; set; } = ""; //  임시 정지 여부    String Y  1	
    public string oprc_rang_cont_yn { get; set; } = ""; //  시가 범위 연장 여부 String  Y	1	
    public string clpr_rang_cont_yn { get; set; } = ""; //  종가 범위 연장 여부 String  Y	1	
    public string crdt_able_yn { get; set; } = ""; //  신용 가능 여부    String Y  1	
    public string grmn_rate_cls_code { get; set; } = ""; //  보증금 비율 구분 코드 String  Y	3	한국투자 증거금비율(marg_rate 참고)
                                                         // 40 : 20%, 30%, 40%
                                                         // 50 : 50%
                                                         // 60 : 60%
    public string elw_pblc_yn { get; set; } = ""; //  ELW 발행 여부   String Y   1	
    public string stck_prpr { get; set; } = ""; //  주식 현재가 String  Y	10	
    public string prdy_vrss { get; set; } = ""; //  전일 대비 String  Y	10	
    public string prdy_vrss_sign { get; set; } = ""; //  전일 대비 부호    String Y    1	1 : 상한
                                                     // 2 : 상승
                                                     // 3 : 보합
                                                     // 4 : 하한
                                                     // 5 : 하락
    public string prdy_ctrt { get; set; } = ""; // 전일 대비율 String  Y	10	
    public string acml_tr_pbmn { get; set; } = ""; // 누적 거래 대금    String Y  18	
    public string acml_vol { get; set; } = ""; // 누적 거래량 String  Y	18	
    public string prdy_vrss_vol_rate { get; set; } = ""; // 전일 대비 거래량 비율 String  Y	12	주식현재가 일자별 API 응답값 사용
    public string stck_oprc { get; set; } = "";  // 주식 시가 String  Y	10	
    public string stck_hgpr { get; set; } = "";  // 주식 최고가 String  Y	10	
    public string stck_lwpr { get; set; } = "";  // 주식 최저가 String  Y	10	
    public string stck_mxpr { get; set; } = "";  // 주식 상한가 String  Y	10	
    public string stck_llam { get; set; } = "";  // 주식 하한가 String  Y	10	
    public string stck_sdpr { get; set; } = ""; // 주식 기준가 String  Y	10	
    public string wghn_avrg_stck_prc { get; set; } = "";  //  가중 평균 주식 가격 String  Y	21	
    public string hts_frgn_ehrt { get; set; } = "";  // HTS 외국인 소진율 String Y 82	
    public string frgn_ntby_qty { get; set; } = "";  // 외국인 순매수 수량  String Y 12	
    public string pgtr_ntby_qty { get; set; } = "";  // 프로그램매매 순매수 수량   String Y 18	
    public string pvt_scnd_dmrs_prc { get; set; } = "";  // 피벗 2차 디저항 가격 String  Y	10	직원용 데이터
    public string pvt_frst_dmrs_prc { get; set; } = "";  // 피벗 1차 디저항 가격 String  Y	10	직원용 데이터
    public string pvt_pont_val { get; set; } = "";  // 피벗 포인트 값    String Y  10	직원용 데이터
    public string pvt_frst_dmsp_prc { get; set; } = "";  // 피벗 1차 디지지 가격 String  Y	10	직원용 데이터
    public string pvt_scnd_dmsp_prc { get; set; } = "";  // 피벗 2차 디지지 가격 String  Y	10	직원용 데이터
    public string dmrs_val { get; set; } = "";  // 디저항 값 String  Y	10	직원용 데이터
    public string dmsp_val { get; set; } = "";  // 디지지 값 String  Y	10	직원용 데이터
    public string cpfn { get; set; } = "";  // 자본금 String Y  22	
    public string rstc_wdth_prc { get; set; } = "";  // 제한 폭 가격 String Y 10	
    public string stck_fcam { get; set; } = "";  // 주식 액면가 String  Y	11	
    public string stck_sspr { get; set; } = "";  // 주식 대용가 String  Y	10	
    public string aspr_unit { get; set; } = "";  // 호가단위    String Y 10	
    public string hts_deal_qty_unit_val { get; set; } = "";  // HTS 매매 수량 단위 값  String Y 10	
    public string lstn_stcn { get; set; } = "";  // 상장 주수 String  Y	18	
    public string hts_avls { get; set; } = "";  // HTS 시가총액 String  Y	18	
    public string per { get; set; } = "";  // PER String Y   10	
    public string pbr { get; set; } = "";  // PBR String Y   10	
    public string stac_month { get; set; } = "";  // 결산 월 String  Y	2	
    public string vol_tnrt { get; set; } = "";  // 거래량 회전율 String  Y	10	
    public string eps { get; set; } = "";  // EPS String Y   13	
    public string bps { get; set; } = "";  // BPS String Y   13	
    public string d250_hgpr { get; set; } = "";  // 250일 최고가    String Y    10	
    public string d250_hgpr_date { get; set; } = "";  // 250일 최고가 일자 String  Y	8	
    public string d250_hgpr_vrss_prpr_rate { get; set; } = "";  // 250일 최고가 대비 현재가 비율 String  Y	12	
    public string d250_lwpr { get; set; } = "";  // 250일 최저가    String Y    10	
    public string d250_lwpr_date { get; set; } = "";  // 250일 최저가 일자 String  Y	8	
    public string d250_lwpr_vrss_prpr_rate { get; set; } = "";  //250일 최저가 대비 현재가 비율 String  Y	12	
    public string stck_dryy_hgpr { get; set; } = "";  // 주식 연중 최고가   String Y    10	
    public string dryy_hgpr_vrss_prpr_rate { get; set; } = "";  // 연중 최고가 대비 현재가 비율    String Y  12	
    public string dryy_hgpr_date { get; set; } = "";  // 연중 최고가 일자   String Y    8	
    public string stck_dryy_lwpr { get; set; } = "";  // 주식 연중 최저가   String Y    10	
    public string dryy_lwpr_vrss_prpr_rate { get; set; } = "";  // 연중 최저가 대비 현재가 비율    String Y  12	
    public string dryy_lwpr_date { get; set; } = "";  // 연중 최저가 일자   String Y    8	
    public string w52_hgpr { get; set; } = "";  // 52주일 최고가    String Y    10	
    public string w52_hgpr_vrss_prpr_ctrt { get; set; } = "";  // 52주일 최고가 대비 현재가 대비 String  Y	10	
    public string w52_hgpr_date { get; set; } = "";  // 52주일 최고가 일자 String  Y	8	
    public string w52_lwpr { get; set; } = "";  // 52주일 최저가    String Y    10	
    public string w52_lwpr_vrss_prpr_ctrt { get; set; } = "";  // 52주일 최저가 대비 현재가 대비 String  Y	10	
    public string w52_lwpr_date { get; set; } = "";  // 52주일 최저가 일자 String  Y	8	
    public string whol_loan_rmnd_rate { get; set; } = "";  // 전체 융자 잔고 비율 String  Y	12	
    public string ssts_yn { get; set; } = "";  // 공매도가능여부 String Y   1	
    public string stck_shrn_iscd { get; set; } = "";  // 주식 단축 종목코드  String Y    9	
    public string fcam_cnnm { get; set; } = "";  // 액면가 통화명 String  Y	20	
    public string cpfn_cnnm { get; set; } = "";  // 자본금 통화명 String  Y	20	외국주권은 억으로 떨어지며, 그 외에는 만으로 표시됨
    public string apprch_rate { get; set; } = "";  // 접근도 String Y   13	
    public string frgn_hldn_qty { get; set; } = "";  // 외국인 보유 수량   String Y 18	
    public string vi_cls_code { get; set; } = "";  // VI적용구분코드    String Y   1	
    public string ovtm_vi_cls_code { get; set; } = "";  // 시간외단일가VI적용구분코드  String Y  1	
    public string last_ssts_cntg_qty { get; set; } = "";  // 최종 공매도 체결 수량 String  Y	12	
    public string invt_caful_yn { get; set; } = "";  // 투자유의여부  String Y 1	Y/N
    public string mrkt_warn_cls_code { get; set; } = "";  // 시장경고코드  String Y    2	00 : 없음
                                                          // 01 : 투자주의
                                                          // 02 : 투자경고
                                                          // 03 : 투자위험
    public string short_over_yn { get; set; } = "";  // 단기과열여부  String Y 1	Y/N
    public string sltr_yn { get; set; } = "";  // 정리매매여부  String Y   1	Y/N

    public override string ToString()
    {
        return $"(최저가: {stck_lwpr.ToMoney():n0}) ~ (현재가: {stck_prpr.ToMoney():n0}) ~ (최고가: {stck_hgpr.ToMoney():n0}), 전일 대비: {prdy_vrss}, 호가 단위: {aspr_unit}, ({stck_shrn_iscd}, {rprs_mrkt_kor_name})";
    }
}
