using eFriendOpenAPI.Extension;

namespace eFriendOpenAPI.Packet;

public class 주식잔고조회Query
{
    public string CANO { get; set; } = ""; // 종합계좌번호
    public string ACNT_PRDT_CD { get; set; } = ""; // 계좌상품코드
    public string AFHR_FLPR_YN { get; set; } = "N"; // 시간외단일가여부 (N : 기본값, Y : 시간외단일가)
    public string OFL_YN { get; set; } = "N"; // 오프라인여부 (공란(Default))
    public string INQR_DVSN { get; set; } = "02"; // 조회구분 (01 : 대출일별, 02 : 종목별)
    public string UNPR_DVSN { get; set; } = "01"; // 단가구분 (01 : 기본값)
    public string FUND_STTL_ICLD_YN { get; set; } = "N"; // 펀드결제분포함여부 (N : 포함하지 않음, Y : 포함)
    public string FNCG_AMT_AUTO_RDPT_YN { get; set; } = "N"; // 융자금액자동상환여부 (N : 기본값)
    public string PRCS_DVSN { get; set; } = "00"; // 처리구분 (00 : 전일매매포함, 01 : 전일매매미포함)
    public string CTX_AREA_FK100 { get; set; } = ""; // 연속조회검색조건100
    public string CTX_AREA_NK100 { get; set; } = ""; // 연속조회키100
}

public class 주식잔고조회DTO
{
    public string pdno { get; set; } = " "; // 상품번호    String Y	12	종목번호(뒷 6자리)
    public string prdt_name { get; set; } = " "; // 상품명 String Y	60	종목명
    public string trad_dvsn_name { get; set; } = " "; // 매매구분명   String Y	60	매수매도구분
    public string bfdy_buy_qty { get; set; } = " "; // 전일매수수량  String Y	10	
    public string bfdy_sll_qty { get; set; } = " "; // 전일매도수량  String Y	10	
    public string thdt_buyqty { get; set; } = " "; // 금일매수수량  String Y	10	
    public string thdt_sll_qty { get; set; } = " "; // 금일매도수량  String Y	10	
    public string hldg_qty { get; set; } = " "; // 보유수량    String Y	19	
    public string ord_psbl_qty { get; set; } = " "; // 주문가능수량  String Y	10	
    public string pchs_avg_pric { get; set; } = " "; // 매입평균가격  String Y	22	매입금액 / 보유수량
    public string pchs_amt { get; set; } = " "; // 매입금액    String Y	19	
    public string prpr { get; set; } = " "; // 현재가 String Y	19	
    public string evlu_amt { get; set; } = " "; // 평가금액    String Y	19	
    public string evlu_pfls_amt { get; set; } = " "; // 평가손익금액  String Y	19	평가금액 - 매입금액
    public string evlu_pfls_rt { get; set; } = " "; // 평가손익율   String Y	9	
    public string evlu_erng_rt { get; set; } = " "; // 평가수익율   String Y	31	
    public string loan_dt { get; set; } = " "; // 대출일자    String Y	8	INQR_DVSN(조회구분)을 01(대출일별) 로 설정해야 값이 나옴
    public string loan_amt { get; set; } = " "; // 대출금액    String Y	19	
    public string stln_slng_chgs { get; set; } = " "; // 대주매각대금  String Y	19	
    public string expd_dt { get; set; } = " "; // 만기일자    String Y	8	
    public string fltt_rt { get; set; } = " "; // 등락율 String Y	31	
    public string bfdy_cprs_icdc { get; set; } = " "; // 전일대비증감  String Y	19	
    public string item_mgna_rt_name { get; set; } = " "; // 종목증거금율명 String Y	20	
    public string grta_rt_name { get; set; } = " "; // 보증금율명   String Y	20	
    public string sbst_pric { get; set; } = " "; // 대용가격    String Y	19	증권매매의 위탁보증금으로서 현금 대신에 사용되는 유가증권 가격
    public string stck_loan_unpr { get; set; } = " "; // 주식대출단가  String Y	22	
                                                      // output2 응답상세2   Array Y
    public string dnca_tot_amt { get; set; } = " "; // 예수금총금액  String Y	19	예수금
    public string nxdy_excc_amt { get; set; } = " "; // 익일정산금액  String Y	19	D+1 예수금
    public string prvs_rcdl_excc_amt { get; set; } = " "; // 가수도정산금액 String Y	19	D+2 예수금
    public string cma_evlu_amt { get; set; } = " "; // CMA평가금액 String Y	19	
    public string bfdy_buy_amt { get; set; } = " "; // 전일매수금액  String Y	19	
    public string thdt_buy_amt { get; set; } = " "; // 금일매수금액  String Y	19	
    public string nxdy_auto_rdpt_amt { get; set; } = " "; // 익일자동상환금액    String Y	19	
    public string bfdy_sll_amt { get; set; } = " "; // 전일매도금액  String Y	19	
    public string thdt_sll_amt { get; set; } = " "; // 금일매도금액  String Y	19	
    public string d2_auto_rdpt_amt { get; set; } = " "; // D+2자동상환금액 String  Y	19	
    public string bfdy_tlex_amt { get; set; } = " "; // 전일제비용금액 String Y	19	
    public string thdt_tlex_amt { get; set; } = " "; // 금일제비용금액 String Y	19	
    public string tot_loan_amt { get; set; } = " "; // 총대출금액   String Y	19	
    public string scts_evlu_amt { get; set; } = " "; // 유가평가금액  String Y	19	
    public string tot_evlu_amt { get; set; } = " "; // 총평가금액   String Y	19	유가증권 평가금액 합계금액 + D+2 예수금
    public string nass_amt { get; set; } = " "; // 순자산금액   String Y	19	
    public string fncg_gld_auto_rdpt_yn { get; set; } = " "; // 융자금자동상환여부   String Y	1	보유현금에 대한 융자금만 차감여부
                                                             // 신용융자 매수체결 시점에서는 융자비율을 매매대금 100%로 계산 하였다가 수도결제일에 보증금에 해당하는 금액을 고객의 현금으로 충당하여 융자금을 감소시키는 업무
    public string pchs_amt_smtl_amt { get; set; } = " "; // 매입금액합계금액    String Y	19	
    public string evlu_amt_smtl_amt { get; set; } = " "; // 평가금액합계금액    String Y	19	유가증권 평가금액 합계금액
    public string evlu_pfls_smtl_amt { get; set; } = " "; // 평가손익합계금액    String Y	19	
    public string tot_stln_slng_chgs { get; set; } = " "; // 총대주매각대금 String Y	19	
    public string bfdy_tot_asst_evlu_amt { get; set; } = " "; // 전일총자산평가금액   String Y	19	
    public string asst_icdc_amt { get; set; } = " "; // 자산증감액   String Y	19	
    public string asst_icdc_erng_rt { get; set; } = " "; // 자산증감수익율 String Y	31	데이터 미제공

    public override string ToString()
    {
        return $"{prdt_name}({pdno}), 보유 {hldg_qty}, 매입평균가격 {pchs_avg_pric.ToMoney():n0}, 평가손익금액 {evlu_pfls_amt.ToMoney():n0}";
    }
}
