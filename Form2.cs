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

            //초기값세팅
            setting_load_auto();

            //save & load
            save_button.Click += setting_save;
            setting_open.Click += setting_load;

            //즉시반영
            setting_allowed.Click += setting_allow;

            //TELEGRAM TEST
            telegram_test_button.Click += telegram_test;


        }

        //초기 자동 실행
        private async Task setting_load_auto()
        {
            //조건식 로딩
            onReceiveConditionVer(Trade_Auto.account, Trade_Auto.arrCondition);

            //매도매수 목록 배치
            mode_hoo();

            match(utility.system_route);

        }

        private void mode_hoo()
        {
            //매수매도방식
            string[] mode = { "지정가", "시장가" };
            string[] hoo = { "5호가", "4호가", "3호가", "2호가", "1호가", "현재가", "시장가", "-1호가", "-2호가", "-3호가", "-4호가", "-5호가" };
            buy_set1.Items.AddRange(mode);
            buy_set2.Items.AddRange(hoo);
            sell_set1.Items.AddRange(mode);
            sell_set2.Items.AddRange(hoo);
        } 

        //settubg  저장
        private void setting_save(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "파일 저장 경로 지정하세요";
            saveFileDialog.Filter = "텍스트 파일 (*.txt)|*.txt";
            //
            if (!String.IsNullOrEmpty(account_list.Text))
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //임시저장
                    List<String> tmp = new List<String>();

                    tmp.Add("자동실행/" + Convert.ToString(auto_trade_allow.Checked));
                    tmp.Add("자동운영시간/" + market_start_time.Text + "/" + market_end_time.Text);
                    tmp.Add("계좌번호/" + account_list.Text);
                    tmp.Add("초기자산/" + initial_balance.Text);
                    tmp.Add("종목당매수금액/" + Convert.ToString(buy_per_price.Checked) + "/" + buy_per_price_text.Text);
                    tmp.Add("종목당매수수량/" + Convert.ToString(buy_per_amount.Checked) + "/" + buy_per_amount_text.Text);
                    tmp.Add("종목당매수비율/" + Convert.ToString(buy_per_percent.Checked) + "/" + buy_per_percent_text.Text);
                    tmp.Add("종목당최대매수금액/" + maxbuy.Text);
                    tmp.Add("최대매수종목수/" + maxbuy_acc.Text);
                    tmp.Add("종목최소매수가/" + min_price.Text);
                    tmp.Add("종목최대매수가/" + max_price.Text);
                    tmp.Add("최대보유종목수/" + Convert.ToString(max_hold.Checked) + "/" + max_hold_text.Text);
                    tmp.Add("당일중복매수금지/" + Convert.ToString(duplication_deny.Checked));
                    tmp.Add("보유종목매수금지/" + Convert.ToString(hold_deny.Checked));
                    tmp.Add("매수시간전검출매수금지/" + Convert.ToString(before_time_deny.Checked));
                    tmp.Add("매수조건/" + Convert.ToString(buy_condition.Checked) + "/" + buy_condition_start.Text + "/" + buy_condition_end.Text + "/" + Convert.ToString(Fomula_list_buy.SelectedIndex) + "/" + Fomula_list_buy.Text + "/" + Convert.ToString(buy_and.Checked));
                    tmp.Add("매도조건/" + Convert.ToString(sell_condition.Checked) + "/" + sell_condition_start.Text + "/" + sell_condition_end.Text + "/" + Convert.ToString(Fomula_list_sell.SelectedIndex) + "/" + Fomula_list_sell.Text);
                    tmp.Add("익절/" + Convert.ToString(profit_percent.Checked) + "/" + profit_percent_text.Text);
                    tmp.Add("손절/" + Convert.ToString(loss_percent.Checked) + "/" + loss_percent_text.Text);
                    tmp.Add("익절TS/" + Convert.ToString(profit_ts.Checked) + "/" + profit_ts_text.Text);
                    tmp.Add("익절원/" + Convert.ToString(profit_won.Checked) + "/" + profit_won_text.Text);
                    tmp.Add("손절원/" + Convert.ToString(loss_won.Checked) + "/" + loss_won_text.Text);
                    tmp.Add("전체청산/" + Convert.ToString(clear_sell.Checked) + "/" + clear_sell_start.Text + "/" + clear_sell_end.Text + "/" + Convert.ToString(clear_sell_market.Checked));
                    tmp.Add("청산익절/" + Convert.ToString(clear_sell_profit.Checked) + "/" + clear_sell_profit_text.Text);
                    tmp.Add("청산손절/" + Convert.ToString(clear_sell_loss.Checked) + "/" + clear_sell_loss_text.Text);
                    tmp.Add("동시호가익절/" + Convert.ToString(after_market_profit.Checked));
                    tmp.Add("동시호가손절/" + Convert.ToString(after_market_loss.Checked));
                    tmp.Add("종목매수텀/" + Convert.ToString(term_for_buy.Checked) + "/" + term_for_buy_text.Text);
                    tmp.Add("미체결매수취소/" + Convert.ToString(term_for_non_buy.Checked) + "/" + term_for_non_buy_text.Text);
                    tmp.Add("매수설정/" + Convert.ToString(buy_set1.SelectedIndex) + "/" + Convert.ToString(buy_set2.SelectedIndex));
                    tmp.Add("매도설정/" + Convert.ToString(sell_set1.SelectedIndex) + "/" + Convert.ToString(sell_set2.SelectedIndex));
                    tmp.Add("코스피지수/" + Convert.ToString(kospi_index.Checked) + "/" + kospi_index_start.Text + "/" + kospi_index_end.Text);
                    tmp.Add("코스닥지수/" + Convert.ToString(kosdak_index.Checked) + "/" + kosdak_index_start.Text + "/" + kosdak_index_end.Text);
                    tmp.Add("코스피선물/" + Convert.ToString(kospi_commodity.Checked) + "/" + kospi_commodity_start.Text + "/" + kospi_commodity_end.Text);
                    tmp.Add("코스닥선물/" + Convert.ToString(kosdak_commodity.Checked) + "/" + kosdak_commodity_start.Text + "/" + kosdak_commodity_end.Text);
                    tmp.Add("KIS_Allow/" + Convert.ToString(KIS_Allow.Checked));
                    tmp.Add("KIS_appkey/" + appkey.Text);
                    tmp.Add("KIS_appsecret/" + appsecret.Text);
                    tmp.Add("KIS_amount/" + kis_amount.Text);
                    tmp.Add("Telegram_Allow/" + Convert.ToString(Telegram_Allow.Checked));
                    tmp.Add("텔레그램ID/" + telegram_user_id.Text);
                    tmp.Add("텔레그램token/" + telegram_token.Text);

                    //텍스트 합치기
                    string textToSave = string.Join("\r\n", tmp);

                    // 사용자가 선택한 파일 경로
                    string filePath = saveFileDialog.FileName;

                    //파일에 텍스트 저장
                    System.IO.File.WriteAllText(filePath, textToSave);
                    MessageBox.Show("파일이 저장되었습니다: " + filePath);
                }
            }
            else
            {
                MessageBox.Show("계좌번호를설정해주세요");
            }
        }

        //setting 열기
        private void setting_load(object sender, EventArgs e)
        {
            //다이얼로그 창 뜨고 선택
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String filepath = openFileDialog1.FileName;
                match(filepath);
            }
        }

        //즉시 반영
        private void setting_allow(object sender, EventArgs e)
        {
            setting_allow_after();
        }

        private async Task setting_allow_after()
        {
            await Task.Run(() =>
            {
                utility.system_route = setting_name.Text;
            });
            await utility.setting_load_auto();
            await Task.Run(() =>
            {
                Trade_Auto trade_auto1 = Application.OpenForms.OfType<Trade_Auto>().FirstOrDefault();
                MessageBox.Show("반영이 완료되었습니다.");
            });
        }

        //매칭
        private void match(string filepath)
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

            //종목당매수금액
            String[] buy_per_price_tmp = reader.ReadLine().Split('/');
            buy_per_price.Checked = Convert.ToBoolean(buy_per_price_tmp[1]);
            buy_per_price_text.Text = buy_per_price_tmp[2];

            //종목당매수수량
            String[] buy_per_amount_tmp = reader.ReadLine().Split('/');
            buy_per_amount.Checked = Convert.ToBoolean(buy_per_amount_tmp[1]);
            buy_per_amount_text.Text = buy_per_amount_tmp[2];

            //종목당매수비율
            String[] buy_per_percemt_tmp = reader.ReadLine().Split('/');
            buy_per_percent.Checked = Convert.ToBoolean(buy_per_percemt_tmp[1]);
            buy_per_percent_text.Text = buy_per_percemt_tmp[2];

            //종목당최대매수금액
            String[] maxbuy_tmp = reader.ReadLine().Split('/');
            maxbuy.Text = maxbuy_tmp[1];

            //최대매수종목수
            String[] maxbuy_acc_tmp = reader.ReadLine().Split('/');
            maxbuy_acc.Text = maxbuy_acc_tmp[1];

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
            Fomula_list_buy.Text = buy_condition_tmp[5];
            buy_and.Checked = Convert.ToBoolean(buy_condition_tmp[6]);

            //매도조건
            String[] sell_condition_tmp = reader.ReadLine().Split('/');
            sell_condition.Checked = Convert.ToBoolean(sell_condition_tmp[1]);
            sell_condition_start.Text = sell_condition_tmp[2];
            sell_condition_end.Text = sell_condition_tmp[3];
            Fomula_list_sell.SelectedIndex = Convert.ToInt32(sell_condition_tmp[4]);
            Fomula_list_sell.Text = sell_condition_tmp[5];

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

            //청산익절
            String[] clear_sell_profit_tmp = reader.ReadLine().Split('/');
            clear_sell_profit.Checked = Convert.ToBoolean(clear_sell_profit_tmp[1]);
            clear_sell_profit_text.Text = clear_sell_profit_tmp[2];

            //청산손절
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

            //한국투자증권KIS_Allow
            String[] KIS_Allow_tmp = reader.ReadLine().Split('/');
            KIS_Allow.Checked = Convert.ToBoolean(KIS_Allow_tmp[1]);

            //한국투자증권appkey
            String[] KIS_appkey_tmp = reader.ReadLine().Split('/');
            appkey.Text = KIS_appkey_tmp[1];

            //한국투자증권appsecret
            String[] KIS_appsecret_tmp = reader.ReadLine().Split('/');
            appsecret.Text = KIS_appsecret_tmp[1];

            //한국투자증권appsecret
            String[] KIS_amount_tmp = reader.ReadLine().Split('/');
            kis_amount.Text = KIS_amount_tmp[1];

            //텔레그램Telegram_Allow
            String[] Telegram_Allow_tmp = reader.ReadLine().Split('/');
            Telegram_Allow.Checked = Convert.ToBoolean(Telegram_Allow_tmp[1]);

            //텔레그램ID
            String[] telegram_user_id_tmp = reader.ReadLine().Split('/');
            telegram_user_id.Text = telegram_user_id_tmp[1];

            //텔레그램TOKEN
            String[] telegram_token_tmp = reader.ReadLine().Split('/');
            telegram_token.Text = telegram_token_tmp[1];

            reader.Close();
        }

        //계좌 및 조건식 리스트 받아오기
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

        //Telegram 테스트
        private void telegram_test(object sender, EventArgs e)
        {
            string test_message = "TELEGRAM CONNECTION CHECK";
            string urlString = $"https://api.telegram.org/bot{telegram_token.Text}/sendMessage?chat_id={telegram_user_id.Text}&text={test_message}";
            WebRequest request = WebRequest.Create(urlString);
            Stream stream = request.GetResponse().GetResponseStream();
        }

        //---------------불필요 기능---------------------

        private void Setting_Load(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void groupBox10_Enter(object sender, EventArgs e)
        {

        }
    }
}
