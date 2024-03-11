using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();

            //매수매도방식
            string[] mode = {"지정가","시장가"};
            string[] hoo = {"5호가","4호가","3호가","2호가","1호가","현재가","시장가","-1호가","-2호가","-3호가","-4호가","-5호가"};
            buy_set1.Items.AddRange(mode);
            buy_set2.Items.AddRange(hoo);
            sell_set1.Items.AddRange(mode);
            sell_set2.Items.AddRange(hoo);

            //조건식 로딩
            onReceiveConditionVer(Trade_Auto.account, Trade_Auto.arrCondition);

            //auto load
            setting_load_auto();

            //save & load
            save_button.Click += setting_save;
            setting_open.Click += setting_load;


            //TELEGRAM TEST
            telegram_test_button.Click += telegram_test;


        }

        private void setting_save(object sender, EventArgs e)
        {

        }
        private void setting_load(object sender, EventArgs e)
        {
            //다이얼로그 창 뜨고 선택
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String filepath = openFileDialog1.FileName;
                load_auto(filepath);
            }
        }
        private void setting_load_auto()
        {
            //windows server 2022 영문 기준 바탕화면에 파일을 해제했을 떄 기준으로 주소 변경
            String filepath = "C:\\Users\\krkr5\\OneDrive\\바탕 화면\\project\\kiwoom2\\setting.txt";
            load_auto(filepath);
        }
        private void load_auto(String filepath)
        {
            StreamReader reader = new StreamReader(filepath);
            //파일 주소 확인
            setting_name.Text = filepath;

            //자동실행
            String[] auto_trade_allow_tmp = reader.ReadLine().Split('/');
            auto_trade_allow.Checked = Convert.ToBoolean(auto_trade_allow_tmp[1]);

            //자동 운영 시간
            String[] time_tmp = reader.ReadLine().Split('/');
            market_start_time.Text = time_tmp[1];
            market_end_time.Text = time_tmp[2];

            //계좌 번호
            String[] account_tmp = reader.ReadLine().Split('/');
            setting_account_number.Text = account_tmp[1];

            //초기 자산
            String[] balance_tmp = reader.ReadLine().Split('/');
            initial_balance.Text = balance_tmp[1];

            //종목당매수금액 / 매수수량 / 매수비율
            String[] buy_per_price_tmp = reader.ReadLine().Split('/');
            buy_per_price.Checked = Convert.ToBoolean(buy_per_price_tmp[1]);
            buy_per_price_text.Text = buy_per_price_tmp[2];

            //매수수량
            String[] buy_per_amount_tmp = reader.ReadLine().Split('/');
            buy_per_amount.Checked = Convert.ToBoolean(buy_per_amount_tmp[1]);
            buy_per_amount_text.Text = buy_per_amount_tmp[2];

            //매수비율
            String[] buy_per_percemt_tmp = reader.ReadLine().Split('/');
            buy_per_percent.Checked = Convert.ToBoolean(buy_per_percemt_tmp[1]);
            buy_per_percent_text.Text = buy_per_percemt_tmp[2];

            //매수종목수
            String[] maxbuy_tmp = reader.ReadLine().Split('/');
            maxbuy.Text = maxbuy_tmp[1];

            //종목최소매수가
            String[] min_price_tmp = reader.ReadLine().Split('/');
            min_price.Text = min_price_tmp[1];

            //종목최대매수가
            String[] max_price_tmp = reader.ReadLine().Split('/');
            max_price.Text = max_price_tmp[1];

            //최대보유종목수
            String[] max_hold_tmp = reader.ReadLine().Split('/');
            max_hold.Checked = Convert.ToBoolean(max_hold_tmp[1]);
            max_hold_text.Text = max_hold_tmp[2];

            //당일중복매수금지
            String[] duplication_deny_tmp = reader.ReadLine().Split('/');
            duplication_deny.Checked = Convert.ToBoolean(duplication_deny_tmp[1]);

            //보유종목매수금지
            String[] hold_deny_tmp = reader.ReadLine().Split('/');
            hold_deny.Checked = Convert.ToBoolean(hold_deny_tmp[1]);

            //매수시간전검출매수금지
            String[] before_time_deny_tmp = reader.ReadLine().Split('/');
            before_time_deny.Checked = Convert.ToBoolean(before_time_deny_tmp[1]);

            //매수조건
            String[] buy_condition_tmp = reader.ReadLine().Split('/');
            buy_condition.Checked = Convert.ToBoolean(buy_condition_tmp[1]);
            buy_condition_start.Text = buy_condition_tmp[2];
            buy_condition_end.Text = buy_condition_tmp[3];
            Fomula_list_buy.SelectedIndex = Convert.ToInt32(buy_condition_tmp[4]);
            buy_and.Checked = Convert.ToBoolean(buy_condition_tmp[5]);

            //매도조건
            String[] sell_condition_tmp = reader.ReadLine().Split('/');
            sell_condition.Checked = Convert.ToBoolean(sell_condition_tmp[1]);
            sell_condition_start.Text = sell_condition_tmp[2];
            sell_condition_end.Text = sell_condition_tmp[3];
            Fomula_list_sell.SelectedIndex = Convert.ToInt32(sell_condition_tmp[4]);

            //익절
            String[] profit_percent_tmp = reader.ReadLine().Split('/');
            profit_percent.Checked = Convert.ToBoolean(profit_percent_tmp[1]);
            profit_percent_text.Text = profit_percent_tmp[2];

            //손절
            String[] loss_percent_tmp = reader.ReadLine().Split('/');
            loss_percent.Checked = Convert.ToBoolean(loss_percent_tmp[1]);
            loss_percent_text.Text = loss_percent_tmp[2];

            //익절TS
            String[] profit_ts_tmp = reader.ReadLine().Split('/');
            profit_ts.Checked = Convert.ToBoolean(profit_ts_tmp[1]);
            profit_ts_text.Text = profit_ts_tmp[2];

            //익절원
            String[] profit_won_tmp = reader.ReadLine().Split('/');
            profit_won.Checked = Convert.ToBoolean(profit_won_tmp[1]);
            profit_won_text.Text = profit_won_tmp[2];

            //손절원
            String[] loss_won_tmp = reader.ReadLine().Split('/');
            loss_won.Checked = Convert.ToBoolean(loss_won_tmp[1]);
            loss_won_text.Text = loss_won_tmp[2];

            //전체청산
            String[] clear_sell_tmp = reader.ReadLine().Split('/');
            clear_sell.Checked = Convert.ToBoolean(clear_sell_tmp[1]);
            clear_sell_start.Text = clear_sell_tmp[2];
            clear_sell_end.Text = clear_sell_tmp[3];
            clear_sell_market.Checked = Convert.ToBoolean(clear_sell_tmp[4]);

            //청산시장가
            String[] clear_sell_profit_tmp = reader.ReadLine().Split('/');
            clear_sell_profit.Checked = Convert.ToBoolean(clear_sell_profit_tmp[1]);
            clear_sell_profit_text.Text = clear_sell_profit_tmp[2];

            //청산익절
            String[] clear_sell_loss_tmp = reader.ReadLine().Split('/');
            clear_sell_loss.Checked = Convert.ToBoolean(clear_sell_loss_tmp[1]);
            clear_sell_loss_text.Text = clear_sell_loss_tmp[2];

            //동시호가익절
            String[] after_market_profit_tmp = reader.ReadLine().Split('/');
            after_market_profit.Checked = Convert.ToBoolean(after_market_profit_tmp[1]);

            //동시호가손절
            String[] after_market_loss_tmp = reader.ReadLine().Split('/');
            after_market_loss.Checked = Convert.ToBoolean(after_market_loss_tmp[1]);

            //종목매수텀
            String[] term_for_buy_tmp = reader.ReadLine().Split('/');
            term_for_buy.Checked = Convert.ToBoolean(term_for_buy_tmp[1]);
            term_for_buy_text.Text = term_for_buy_tmp[2];

            //미체결매수취소
            String[] term_for_non_buy_tmp = reader.ReadLine().Split('/');
            term_for_non_buy.Checked = Convert.ToBoolean(term_for_non_buy_tmp[1]);
            term_for_non_buy_text.Text = term_for_non_buy_tmp[2];

            //매수설정
            String[] buy_set_tmp = reader.ReadLine().Split('/');
            buy_set1.SelectedIndex = Convert.ToInt32(buy_set_tmp[1]);
            buy_set2.SelectedIndex = Convert.ToInt32(buy_set_tmp[2]);

            //매도설정
            String[] sell_set_tmp = reader.ReadLine().Split('/');
            sell_set1.SelectedIndex = Convert.ToInt32(sell_set_tmp[1]);
            sell_set2.SelectedIndex = Convert.ToInt32(sell_set_tmp[2]);

            //코스피지수
            String[] kospi_index_tmp = reader.ReadLine().Split('/');
            kospi_index.Checked = Convert.ToBoolean(kospi_index_tmp[1]);
            kospi_index_start.Text = kospi_index_tmp[2];
            kospi_index_end.Text = kospi_index_tmp[3];

            //코스닥지수
            String[] kosdak_index_tmp = reader.ReadLine().Split('/');
            kosdak_index.Checked = Convert.ToBoolean(kosdak_index_tmp[1]);
            kosdak_index_start.Text = kosdak_index_tmp[2];
            kosdak_index_end.Text = kosdak_index_tmp[3];

            //코스피선물
            String[] kospi_commodity_tmp = reader.ReadLine().Split('/');
            kospi_commodity.Checked = Convert.ToBoolean(kospi_commodity_tmp[1]);
            kospi_commodity_start.Text = kospi_commodity_tmp[2];
            kospi_commodity_end.Text = kospi_commodity_tmp[3];

            //코스닥선물
            String[] kosdak_commodity_tmp = reader.ReadLine().Split('/');
            kosdak_commodity.Checked = Convert.ToBoolean(kosdak_commodity_tmp[1]);
            kosdak_commodity_start.Text = kosdak_commodity_tmp[2];
            kosdak_commodity_end.Text = kosdak_commodity_tmp[3];

            //텔레그램ID
            String[] telegram_user_id_tmp = reader.ReadLine().Split('/');
            telegram_user_id.Text = telegram_user_id_tmp[1];

            //텔레그램TOKEN
            String[] telegram_token_tmp = reader.ReadLine().Split('/');
            telegram_token.Text = telegram_token_tmp[1];
        }

        //계좌 및 조건식 리스트 받아오기
        class ConditionInfo
        {
            public int Index { get; set; }
            public string Name { get; set; }
            public DateTime? LastRequestTime { get; set; }
        }

        private List<ConditionInfo> conditionInfo = new List<ConditionInfo>();

        public void onReceiveConditionVer(string[] user_account, string[] Condition)
        {
            //계좌 추가
            for (int i = 0; i < user_account.Length; i++)
            {
                account_list.Items.Add(user_account[i]);
            }

            //조건식 추가
            Fomula_list_buy.Items.AddRange(Condition);
            Fomula_list_sell.Items.AddRange(Condition);
        }

        private void telegram_test(object sender, EventArgs e)
        {
            string test_message = "TELEGRAM CONNECTION CHECK";
            string urlString = $"https://api.telegram.org/bot{telegram_token.Text}/sendMessage?chat_id={telegram_user_id.Text}&text={test_message}";
            WebRequest request = WebRequest.Create(urlString);
            Stream stream = request.GetResponse().GetResponseStream();
        }

        private void Setting_Load(object sender, EventArgs e)
        {

        }
    }
}
