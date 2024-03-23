﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace WindowsFormsApp1
{
    class utility
    {
        //check 변수
        public static string system_route = "C:\\Users\\krkr5\\OneDrive\\바탕 화면\\project\\password\\setting.txt";
        public static bool load_check = false;

        //global 변수
        public static bool auto_trade_allow; //자동실행 여부
        public static string market_start_time; //자동 운영 시간
        public static string market_end_time; //계좌 번호
        public static string setting_account_number; //계좌 번호
        public static string initial_balance; //초기 자산
        public static bool buy_per_price; 

        public static string buy_per_price_text;//종목당매수금액
        public static bool buy_per_amount;
        public static string buy_per_amount_text;//종목당매수수량
        public static bool buy_per_percent; 
        public static string buy_per_percent_text;//종목당매수비율
        public static string maxbuy; //종목당최대매수금액
        public static string maxbuy_acc;//최대매수종목수
        public static string min_price;//종목최소매수가
        public static string max_price;//종목최대매수가

        public static bool max_hold; 
        public static string max_hold_text;//최대보유종목수
        public static bool duplication_deny;//당일중복매수금지
        public static bool before_time_deny;//매수시간전검출매수금지
        public static bool hold_deny;//보유종목매수금지

        public static bool buy_condition; //매수조건
        public static string buy_condition_start;
        public static string buy_condition_end;
        public static string Fomula_list_buy_text;
        public static bool buy_OR;
        public static bool buy_AND;
        public static bool buy_INDEPENDENT;

        public static bool sell_condition; //매도조건
        public static string sell_condition_start;
        public static string sell_condition_end;
        public static int Fomula_list_sell;
        public static string Fomula_list_sell_text;

        public static bool profit_percent;
        public static string profit_percent_text; //익절
        public static bool profit_won;
        public static string profit_won_text; //익절원
        public static bool profit_ts;
        public static string profit_ts_text; //익절TS
        //익정동시호가
        //익절시간외단일가
        public static bool loss_percent;
        public static string loss_percent_text; //손절
        public static bool loss_won;
        public static string loss_won_text; //손절원
        //손절동시호가
        //손절시간외단일가
        //매매코스피연동
        //매매코스닥연동
        //매매코스피선물연동
        //매매코스닥선물연동

        public static bool clear_sell; //전체청산
        public static string clear_sell_start;
        public static string clear_sell_end;
        public static bool clear_sell_market; 
        public static bool clear_sell_profit; 
        public static string clear_sell_profit_text; //청산익절
        public static bool after_market_profit; //동시호가익절
        //청산익절시간외단일가
        public static bool clear_sell_loss;
        public static string clear_sell_loss_text;//청산손절
        public static bool after_market_loss;//동시호가손절
        //청산익절시간외단일가
        //청산코스피연동
        //청산코스닥연동
        //청산코스피선물연동
        //청산코스닥선물연동

        public static bool term_for_buy;
        public static string term_for_buy_text;//종목매수텀
        //종목매도텀
        //종목매도텀
        public static bool term_for_non_buy;
        public static string term_for_non_buy_text; //미체결매수취소
        //미체결매도취소

        public static int buy_set1; //매수설정
        public static int buy_set2;
        public static int sell_set1; //매도설정
        public static int sell_set2;

        public static bool kospi_index;
        public static string kospi_index_start;
        public static string kospi_index_end;
        public static bool kosdak_index;
        public static string kosdak_index_start;
        public static string kosdak_index_end;
        public static bool kospi_commodity;
        public static string kospi_commodity_start;
        public static string kospi_commodity_end;
        public static bool kosdak_commodity;
        public static string kosdak_commodity_start;
        public static string kosdak_commodity_end;

        public static bool KIS_Allow;
        public static string KIS_appkey;
        public static string KIS_appsecret;
        public static string KIS_amount;

        public static bool Telegram_Allow;
        public static string telegram_user_id;
        public static string telegram_token;

        //utility 목록
        public static async Task setting_load_auto()
        {
            //windows server 2022 영문 기준 바탕화면에 파일을 해제했을 떄 기준으로 주소 변경
            await auto_load(system_route);
            await Task.Run(() =>
            {
                load_check = true;
            });
        }
        public static async Task auto_load(string filepath)
        {
            StreamReader reader = new StreamReader(filepath);

            //자동실행
            String[] auto_trade_allow_tmp = reader.ReadLine().Split('/');
            auto_trade_allow = Convert.ToBoolean(auto_trade_allow_tmp[1]);

            //자동 운영 시간
            String[] time_tmp = reader.ReadLine().Split('/');
            market_start_time = time_tmp[1];
            market_end_time = time_tmp[2];

            //계좌 번호
            String[] account_tmp = reader.ReadLine().Split('/');
            setting_account_number = account_tmp[1];

            //초기 자산
            String[] balance_tmp = reader.ReadLine().Split('/');
            initial_balance = balance_tmp[1];

            //종목당매수금액
            String[] buy_per_price_tmp = reader.ReadLine().Split('/');
            buy_per_price = Convert.ToBoolean(buy_per_price_tmp[1]);
            buy_per_price_text = buy_per_price_tmp[2];

            //종목당매수수량
            String[] buy_per_amount_tmp = reader.ReadLine().Split('/');
            buy_per_amount = Convert.ToBoolean(buy_per_amount_tmp[1]);
            buy_per_amount_text = buy_per_amount_tmp[2];

            //종목당매수비율
            String[] buy_per_percemt_tmp = reader.ReadLine().Split('/');
            buy_per_percent = Convert.ToBoolean(buy_per_percemt_tmp[1]);
            buy_per_percent_text = buy_per_percemt_tmp[2];

            //종목당최대매수금액
            String[] maxbuy_tmp = reader.ReadLine().Split('/');
            maxbuy = maxbuy_tmp[1];

            //최대매수종목수
            String[] maxbuy_acc_tmp = reader.ReadLine().Split('/');
            maxbuy_acc = maxbuy_acc_tmp[1];

            //종목최소매수가
            String[] min_price_tmp = reader.ReadLine().Split('/');
            min_price = min_price_tmp[1];

            //종목최대매수가
            String[] max_price_tmp = reader.ReadLine().Split('/');
            max_price = max_price_tmp[1];

            //최대보유종목수
            String[] max_hold_tmp = reader.ReadLine().Split('/');
            max_hold = Convert.ToBoolean(max_hold_tmp[1]);
            max_hold_text = max_hold_tmp[2];

            //당일중복매수금지
            String[] duplication_deny_tmp = reader.ReadLine().Split('/');
            duplication_deny= Convert.ToBoolean(duplication_deny_tmp[1]);

            //매수시간전검출매수금지
            String[] before_time_deny_tmp = reader.ReadLine().Split('/');
            before_time_deny = Convert.ToBoolean(before_time_deny_tmp[1]);

            //보유종목매수금지
            String[] hold_deny_tmp = reader.ReadLine().Split('/');
            hold_deny = Convert.ToBoolean(hold_deny_tmp[1]);

            //매수조건
            String[] buy_condition_tmp = reader.ReadLine().Split('/');
            buy_condition = Convert.ToBoolean(buy_condition_tmp[1]);
            buy_condition_start = buy_condition_tmp[2];
            buy_condition_end = buy_condition_tmp[3];
            Fomula_list_buy_text = buy_condition_tmp[4];
            buy_OR = Convert.ToBoolean(buy_condition_tmp[5]);
            buy_AND = Convert.ToBoolean(buy_condition_tmp[6]);
            buy_INDEPENDENT = Convert.ToBoolean(buy_condition_tmp[7]);

            //매도조건
            String[] sell_condition_tmp = reader.ReadLine().Split('/');
            sell_condition = Convert.ToBoolean(sell_condition_tmp[1]);
            sell_condition_start = sell_condition_tmp[2];
            sell_condition_end = sell_condition_tmp[3];
            Fomula_list_sell= Convert.ToInt32(sell_condition_tmp[4]);
            Fomula_list_sell_text = sell_condition_tmp[5];

            //익절
            String[] profit_percent_tmp = reader.ReadLine().Split('/');
            profit_percent = Convert.ToBoolean(profit_percent_tmp[1]);
            profit_percent_text = profit_percent_tmp[2];

            //익절원
            String[] profit_won_tmp = reader.ReadLine().Split('/');
            profit_won = Convert.ToBoolean(profit_won_tmp[1]);
            profit_won_text = profit_won_tmp[2];

            //익절TS
            String[] profit_ts_tmp = reader.ReadLine().Split('/');
            profit_ts = Convert.ToBoolean(profit_ts_tmp[1]);
            profit_ts_text = profit_ts_tmp[2];

            //익정동시호가

            //익절시간외단일가

            //손절
            String[] loss_percent_tmp = reader.ReadLine().Split('/');
            loss_percent = Convert.ToBoolean(loss_percent_tmp[1]);
            loss_percent_text = loss_percent_tmp[2];

            //손절원
            String[] loss_won_tmp = reader.ReadLine().Split('/');
            loss_won = Convert.ToBoolean(loss_won_tmp[1]);
            loss_won_text = loss_won_tmp[2];

            //손절동시호가

            //손절시간외단일가

            //매매코스피연동(대기)

            //매매코스닥연동(대기)

            //매매코스피선물연동(대기)

            //매매코스닥선물연동(대기)

            //전체청산
            String[] clear_sell_tmp = reader.ReadLine().Split('/');
            clear_sell = Convert.ToBoolean(clear_sell_tmp[1]);
            clear_sell_start = clear_sell_tmp[2];
            clear_sell_end = clear_sell_tmp[3];
            clear_sell_market= Convert.ToBoolean(clear_sell_tmp[4]);

            //청산익절
            String[] clear_sell_profit_tmp = reader.ReadLine().Split('/');
            clear_sell_profit = Convert.ToBoolean(clear_sell_profit_tmp[1]);
            clear_sell_profit_text = clear_sell_profit_tmp[2];

            //청산익절동시호가
            String[] after_market_profit_tmp = reader.ReadLine().Split('/');
            after_market_profit = Convert.ToBoolean(after_market_profit_tmp[1]);

            //청산익절시간외단일가

            //청산손절
            String[] clear_sell_loss_tmp = reader.ReadLine().Split('/');
            clear_sell_loss= Convert.ToBoolean(clear_sell_loss_tmp[1]);
            clear_sell_loss_text = clear_sell_loss_tmp[2];

            //청산손절동시호가
            String[] after_market_loss_tmp = reader.ReadLine().Split('/');
            after_market_loss = Convert.ToBoolean(after_market_loss_tmp[1]);

            //청산익절시간외단일가

            //청산코스피연동(대기)

            //청산코스닥연동(대기)

            //청산코스피선물연동(대기)

            //청산코스닥선물연동(대기)

            //종목매수텀(대기)
            String[] term_for_buy_tmp = reader.ReadLine().Split('/');
            term_for_buy = Convert.ToBoolean(term_for_buy_tmp[1]);
            term_for_buy_text = term_for_buy_tmp[2];

            //종목매도텀(대기)

            //미체결매수취소(대기)
            String[] term_for_non_buy_tmp = reader.ReadLine().Split('/');
            term_for_non_buy = Convert.ToBoolean(term_for_non_buy_tmp[1]);
            term_for_non_buy_text = term_for_non_buy_tmp[2];

            //미체결매도취소(대기)

            //매수설정
            String[] buy_set_tmp = reader.ReadLine().Split('/');
            buy_set1 = Convert.ToInt32(buy_set_tmp[1]);
            buy_set2 = Convert.ToInt32(buy_set_tmp[2]);

            //매도설정
            String[] sell_set_tmp = reader.ReadLine().Split('/');
            sell_set1 = Convert.ToInt32(sell_set_tmp[1]);
            sell_set2 = Convert.ToInt32(sell_set_tmp[2]);

            //코스피지수
            String[] kospi_index_tmp = reader.ReadLine().Split('/');
            kospi_index = Convert.ToBoolean(kospi_index_tmp[1]);
            kospi_index_start = kospi_index_tmp[2];
            kospi_index_end = kospi_index_tmp[3];

            //코스닥지수
            String[] kosdak_index_tmp = reader.ReadLine().Split('/');
            kosdak_index = Convert.ToBoolean(kosdak_index_tmp[1]);
            kosdak_index_start = kosdak_index_tmp[2];
            kosdak_index_end = kosdak_index_tmp[3];

            //코스피선물
            String[] kospi_commodity_tmp = reader.ReadLine().Split('/');
            kospi_commodity = Convert.ToBoolean(kospi_commodity_tmp[1]);
            kospi_commodity_start = kospi_commodity_tmp[2];
            kospi_commodity_end = kospi_commodity_tmp[3];

            //코스닥선물
            String[] kosdak_commodity_tmp = reader.ReadLine().Split('/');
            kosdak_commodity = Convert.ToBoolean(kosdak_commodity_tmp[1]);
            kosdak_commodity_start = kosdak_commodity_tmp[2];
            kosdak_commodity_end = kosdak_commodity_tmp[3];

            //한국투자증권KIS_Allow
            String[] KIS_Allow_tmp = reader.ReadLine().Split('/');
            KIS_Allow = Convert.ToBoolean(KIS_Allow_tmp[1]);

            //한국투자증권appkey
            String[] KIS_appkey_tmp = reader.ReadLine().Split('/');
            KIS_appkey = KIS_appkey_tmp[1];

            //한국투자증권appsecret
            String[] KIS_appsecret_tmp = reader.ReadLine().Split('/');
            KIS_appsecret = KIS_appsecret_tmp[1];

            //한국투자증권KIS_amount
            String[] KIS_amount_tmp = reader.ReadLine().Split('/');
            KIS_amount = KIS_amount_tmp[1];

            //텔레그램Telegram_Allow
            String[] Telegram_Allow_tmp = reader.ReadLine().Split('/');
            Telegram_Allow = Convert.ToBoolean(Telegram_Allow_tmp[1]);

            //텔레그램ID
            String[] telegram_user_id_tmp = reader.ReadLine().Split('/');
            telegram_user_id = telegram_user_id_tmp[1];

            //텔레그램TOKEN
            String[] telegram_token_tmp = reader.ReadLine().Split('/');
            telegram_token = telegram_token_tmp[1];

            reader.Close();
        }
    }
}
