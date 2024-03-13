using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Trade_Auto : Form
    {
        //---------------공용 신호-------------------
        static public string[] arrCondition;
        static public string[] account;

        //---------------Main---------------------
        public Trade_Auto()
        {
            InitializeComponent();

            //----초기동작----
            if (utility.setting_load_auto()) { WriteLog("저장된 세팅 로딩 완료\n"); }; //기존 세팅 로드
            axKHOpenAPI1.CommConnect(); //로그인
            axKHOpenAPI1.OnEventConnect += onEventConnect; //로그인 상태 확인(ID,NAME,계좌번호,KEYBOARD,FIREWALL,조건식) 및 조건식 조회
            axKHOpenAPI1.OnReceiveConditionVer += onReceiveConditionVer; //조건식 로드 및 기존 세팅 반영
            timer1.Start(); //시간 표시
            //[자동] 전체 종목 업데이트
            //if (utility.auto_trade_allow) { auto_allow(); }; //자동 세팅 반영
            //보유 종목 개수 확인

            //----공용동작----
            axKHOpenAPI1.OnReceiveTrData += onReceiveTrData; //TR조회

            //----시간 동작----
            timer1.Start(); //시간 표시 + 기타 초 마다 실행하는 함수 실행

            //----버튼----
            Login_btn.Click += login_btn; //로그인
            Trade_setting.Click += trade_setting; //설정창
            Stock_search_btn.Click += stock_search_btn; //종목조회
            Normal_search_btn.Click += normal_search_btn; //조건식 일반 검색
            axKHOpenAPI1.OnReceiveTrCondition += onReceiveTrCondition; //조건식 일반 검색 조건식 등록

            Real_time_search_btn.Click += real_time_search_btn; //조건식 실시간 검색
            axKHOpenAPI1.OnReceiveRealCondition += onReceiveRealCondition; //조건식 실시간 검색 조건식 등록
            axKHOpenAPI1.OnReceiveRealData += onReceiveRealData; //조건식 실시간 검색 시세 등록 편출입
            //실시간 시세 해지

            Real_time_stop_btn.Click += real_time_stop_btn; //조건식 실시간 전체 중단

            Main_menu.Click += main_menu; //메인메뉴

        }

        //---------------공용기능---------------------
        //화면번호
        private int _screenNo = 1001;
        private string GetScreenNo()
        {
            //화면번호 : 조회나 주문등 필요한 기능을 요청할때 이를 구별하기 위한 키값
            //0000(혹은 0)을 제외한 임의의 네자리 숫자
            //개수가 200개로 한정, 이 개수를 넘지 않도록 관리
            //200개를 넘는 경우 조회 결과나 주문 결과에 다른 데이터가 섞이거나 원하지 않는 결과가 나타날 수 있다.
            if (_screenNo < 1200)
                _screenNo++;
            else
                _screenNo = 1001;
            return _screenNo.ToString();
        }


        //CommRqData 에러 목록
        private void GetErrorMessage(int errorcode)
        {
            switch (errorcode)
            {
                case 0:
                    WriteLog("정상조회\n");
                    break;
                case 200:
                    WriteLog("시세과부화\n");
                    break;
                case 201:
                    WriteLog("조회전문작성 에러\n");
                    break;
            }
        }


        //로그창
        private void WriteLog(string message)
        {
            string time = DateTime.Now.ToString("HH:mm:ss");
            log_window.AppendText($@"{"[" + time + "] " + message}");
        }

        //telegram(초당 1개씩 전송)
        private void telegram_chat(string message)
        {
            string time = DateTime.Now.ToString("HH:mm:ss");
            string message_edtied = "[" + time + "] " + message;
            string urlString = $"https://api.telegram.org/bot{utility.telegram_token}/sendMessage?chat_id={utility.telegram_user_id}&text={message_edtied}";
            WebRequest request = WebRequest.Create(urlString);
            Stream stream = request.GetResponse().GetResponseStream();
        }

        //telegram용 초당 1회 전송 저장소
        private Queue<String> telegram_save = new Queue<string>();

        //실시간 조건 검색 용 테이블(누적 저장)
        private DataTable dtCondStock = new DataTable();

        //실시간 계좌 보유 현황 용 테이블(누적 저장)
        private DataTable dtCondStock_hold = new DataTable();

        //---------------초기 로그인---------------------

        //로그인
        private void login_btn(object sender, EventArgs e)
        {
            //CommConnect를 하는 경우 KHOpenAPI Control의 OnEventConnect 이벤트가 호출
            axKHOpenAPI1.CommConnect();
        }
        private void onEventConnect(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent e)
        {
            if (e.nErrCode == 0)
            {
                // 정상 처리
                WriteLog("로그인 성공\n");
                telegram_chat("로그인 성공\n");
                //"ACCOUNT_CNT" : 보유계좌 갯수
                //"ACCLIST" 또는 "ACCNO" : 구분자 ';', 보유계좌 목록                
                string 계좌목록 = axKHOpenAPI1.GetLoginInfo("ACCLIST").Trim();
                //계좌목록은 ';'문자로 분리된 문자열
                //분리된 계좌를 ComboBox에 추가 
                account = 계좌목록.Split(';');
                //사용자 id를 UserId 라벨에 추가
                string 사용자id = axKHOpenAPI1.GetLoginInfo("USER_ID");
                User_id.Text = 사용자id;
                //사용자 이름을 UserName 라벨에 추가
                string 사용자이름 = axKHOpenAPI1.GetLoginInfo("USER_NAME");
                User_name.Text = 사용자이름;
                //접속서버 구분(1 : 모의투자, 나머지: 실거래서버)
                string 접속서버구분 = axKHOpenAPI1.GetLoginInfo("GetServerGubun");
                if (접속서버구분.Equals("1"))
                {
                    User_connection.Text = "모의\n";
                }
                else
                {
                    User_connection.Text = "실제\n";
                }
                //"KEY_BSECGB" : 키보드 보안 해지여부(0 : 정상, 1 : 해지)
                string 키보드보안 = axKHOpenAPI1.GetLoginInfo("KEY_BSECGB");
                if (키보드보안.Equals("1"))
                {
                    Keyboard_wall.Text = "정상\n";
                }
                else
                {
                    Keyboard_wall.Text = "해지\n";
                }
                //"FIREW_SECGB" : 방화벽 설정여부(0 : 미설정, 1 : 설정, 2 : 해지)
                string 방화벽 = axKHOpenAPI1.GetLoginInfo("FIREW_SECGB");
                if (방화벽.Equals("0"))
                {
                    Fire_wall.Text = "미설정\n";
                }
                else if (방화벽.Equals("1"))
                {
                    Fire_wall.Text = "설정\n";
                }
                else
                {
                    Fire_wall.Text = "해지\n";
                }
                //조건식 검색
                WriteLog("조건식 검색ing\n");
                if(axKHOpenAPI1.GetConditionLoad() == 1)
                {
                    WriteLog("조건식 검색 성공\n");
                    telegram_chat("조건식 검색 성공\n");
                }
                else
                {
                    WriteLog("조건식 검색 실패\n");
                    telegram_chat("조건식 검색 실패\n");
                }

            }
            else
            {
                switch (e.nErrCode)
                {
                    case 100:
                        WriteLog("사용자 정보교환 실패\n");
                        break;
                    case 101:
                        WriteLog("서버접속 실패\n");
                        break;
                    case 102:
                        WriteLog("버전처리 실패\n");
                        break;
                }

            }

        }

        //---------------TR TABLE---------------------

        //데이터 조회(예수금, 유가증권, 조건식, 일반 검색, 실시간 검색 등)
        private void onReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            switch (e.sRQName)
            {
                //예수금 데이터 조회
                case "예수금상세현황":
                    User_money.Text = string.Format("{0:#,##0}", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "예수금").Trim()));
                    WriteLog("예수금 조회 완료\n");
                    break;
                //개별 증권 데이터 조회
                case "주식기본정보":
                    WriteLog("------------------------------------\n");
                    WriteLog(string.Format("종목코드: {0}\n", axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "종목코드").Trim()));
                    WriteLog(string.Format("종목명: {0}\n", axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "종목명").Trim()));
                    WriteLog(string.Format("연중최고: {0:#,##0}\n", Convert.ToInt32(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "연중최고").Trim())));
                    WriteLog(string.Format("연중최저: {0:#,##0}\n", Convert.ToInt32(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "연중최저").Trim())));
                    WriteLog(string.Format("PER: {0:#,##0.00}\n", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "PER").Trim())));
                    WriteLog(string.Format("EPS: {0:#,##0}\n", Convert.ToInt32(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "EPS").Trim())));
                    WriteLog(string.Format("ROE: {0:#,##0.00}\n", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "ROE").Trim())));
                    WriteLog(string.Format("PBR: {0:#,##0.00}\n", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "PBR").Trim())));
                    WriteLog(string.Format("EV: {0:#,##0.00}\n", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "EV").Trim())));
                    WriteLog(string.Format("BPS: {0:#,##0}\n", Convert.ToInt32(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "BPS").Trim())));
                    WriteLog(string.Format("신용비율: {0:#,##0.00}%\n", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "신용비율").Trim())));
                    WriteLog(string.Format("외인소진률: {0:#,##0.00}%\n", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "외인소진률").Trim())));
                    WriteLog(string.Format("거래량: {0:#,##0}\n", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "거래량").Trim())));
                    WriteLog("------------------------------------\n");
                    break;
                //일반 검색 데이터 조회
                case "조건일반검색":
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add("편입", typeof(string));
                    dataTable.Columns.Add("종목코드", typeof(string));
                    dataTable.Columns.Add("종목명", typeof(string));
                    dataTable.Columns.Add("현재가", typeof(string));
                    dataTable.Columns.Add("등락율", typeof(string));
                    dataTable.Columns.Add("거래량", typeof(string));
                    dataTable.Columns.Add("편입시간", typeof(string));
                    int count = axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName);
                    for (int i = 0; i < count; i++)
                    {
                        dataTable.Rows.Add(
                            "편입",
                            axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목코드").Trim(),
                            axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목명").Trim(),
                            string.Format("{0:#,##0}", Convert.ToInt32(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "현재가").Trim())),
                            string.Format("{0:#,##0.00}%", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "등락율").Trim())),
                            string.Format("{0:#,##0}", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "거래량").Trim())),
                            DateTime.Now
                        );
                    }
                    dtCondStock = dataTable;
                    dataGridView1.DataSource = dtCondStock;

                    //매수 감시

                    break;
                //실시간 조건 검색(상태(편입, 이탈, 매수, 매도), 종목코드, 종목명, 등락표시, 현재가, 등락율, 거래량, 편입가, 편입대비, 수익률, 편입시간, 매수조건식, 매도조건식) => 상태, 종목코드, 대비기호, 현재가. 등락율, 거래량
                case "조건실시간검색":
                    dtCondStock.Rows.Add(
                        "편입",
                        axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "종목코드").Trim(),
                        axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "종목명").Trim(),
                        string.Format("{0:#,##0}", Convert.ToInt32(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "현재가").Trim())),
                        string.Format("{0:#,##0.00}%", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "등락율").Trim())),
                        string.Format("{0:#,##0}", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "거래량").Trim())),
                        DateTime.Now
                    );
                    dataGridView1.DataSource = dtCondStock;

                    //매수 감시

                    break;
                case "계좌평가현황요청":
                    DataTable dataTable2 = new DataTable();
                    dataTable2.Columns.Add("종목코드", typeof(string));
                    dataTable2.Columns.Add("종목명", typeof(string));
                    dataTable2.Columns.Add("현재가", typeof(string));
                    dataTable2.Columns.Add("보유수량", typeof(string));
                    dataTable2.Columns.Add("평균단가", typeof(string));
                    dataTable2.Columns.Add("평가금액", typeof(string));
                    dataTable2.Columns.Add("손익률", typeof(string));
                    dataTable2.Columns.Add("손익금액", typeof(string));
                    dataTable2.Columns.Add("금일매도수량", typeof(string));
                    int count2 = axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName);
                    for (int i = 0; i < count2; i++)
                    {
                        dataTable2.Rows.Add(
                            axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목코드").Trim(),
                            axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목명").Trim(),
                            string.Format("{0:#,##0}", Convert.ToInt32(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "현재가").Trim())),
                            string.Format("{0:#,##0}", Convert.ToInt32(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "보유수량").Trim())),
                            string.Format("{0:#,##0}", Convert.ToInt32(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "평균단가").Trim())),
                            string.Format("{0:#,##0}", Convert.ToInt32(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "평가금액").Trim())),
                            string.Format("{0:#,##0.00}%", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "손익률").Trim())),
                            string.Format("{0:#,##0}", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "손익금액").Trim())),
                            string.Format("{0:#,##0}", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "금일매도수량").Trim()))
                        );
                    }
                    dtCondStock_hold = dataTable2;
                    dataGridView2.DataSource = dtCondStock_hold;

                    //매도 감시

                    break;
            }
        }

        //---------------로드---------------------

        //조건식 검색(조건식이 있어야 initial 작동 / initial을 통해 계좌를 받아와야 GetCashInfo)
        class ConditionInfo
        {
            public int Index { get; set; }
            public string Name { get; set; }
            public DateTime? LastRequestTime { get; set; }
        }

        private List<ConditionInfo> conditionInfo = new List<ConditionInfo>();

        private void onReceiveConditionVer(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveConditionVerEvent e)
        {
            if (e.lRet != 1) return;
            Fomula_list.Items.Clear();
            conditionInfo.Clear();
            //사용자 조건식을 조건식의 고유번호와 조건식 이름을 한 쌍으로 하는 문자열
            // ';' 구분
            arrCondition = axKHOpenAPI1.GetConditionNameList().Trim().Split(';');
            foreach (var cond in arrCondition)
            {
                if (string.IsNullOrEmpty(cond)) continue;
                // '^' 구분 ex) 001^조건식1
                var item = cond.Split('^');
                conditionInfo.Add(new ConditionInfo
                {
                    Index = Convert.ToInt32(item[0]),
                    Name = item[1]
                });
            }
            Fomula_list.Items.AddRange(arrCondition);
            buy_condition.Items.AddRange(arrCondition);
            sell_condtion.Items.AddRange(arrCondition);
            if (Fomula_list.Items.Count > 0)
            {
                Fomula_list.SelectedIndex = 0;
                //조건식 검색 후 초기 세팅을 시작한다.
                initial_allow();
            }
            WriteLog("조건식 조회 성공\n");
            //예수금 받아오기
            GetCashInfo(acc_text.Text.Trim());
            //telegram_chat("조건식 조회 성공\n");
        }

        //예수금 조회
        private void GetCashInfo(string acctNo)
        {
            //SetInputValue : 계좌번호, 비밀번호입력매체구분, 조회구분
            //비밀번호입력매체구 : 기본(00), 일반조회(2). 추정조회(3)
            //CommRqData(Request Name, TR CODE, 0, 화면 번호)
            axKHOpenAPI1.SetInputValue("계좌번호", acctNo);
            axKHOpenAPI1.SetInputValue("비밀번호입력매체구분", "00");
            axKHOpenAPI1.SetInputValue("조회구분", "2");
            //CommRqData(sRQName, sTRCode, nPreNext, sScreenNo)
            //CommRqData(임의 사용자 구분명, TR목록, 연속조회여부, 화면번호)
            //CommRqData를 하는 경우 KHOpenAPI Control의 OnReceiveTrData 이벤트가 호출
            int result = axKHOpenAPI1.CommRqData("예수금상세현황", "OPW00001", 0, GetScreenNo());
            GetErrorMessage(result);
        }

        //초기 설정 반영 & 즉시 반영
        public void initial_allow()
        {
            string[] mode = { "지정가", "시장가" };
            string[] hoo = { "5호가", "4호가", "3호가", "2호가", "1호가", "현재가", "시장가", "-1호가", "-2호가", "-3호가", "-4호가", "-5호가" };

            //초기 세팅
            acc_text.Text = utility.setting_account_number;
            total_money.Text = string.Format("{0:#,##0}", Convert.ToDecimal(utility.initial_balance));
            max_hoid.Text = utility.maxbuy_acc;
            operation_start.Text = utility.market_start_time;
            operation_stop.Text = utility.market_end_time;
            search_start.Text = utility.buy_condition_start;
            search_stop.Text = utility.buy_condition_end;
            clear_sell.Text = Convert.ToString(utility.clear_sell);
            clear_sell_time.Text = utility.clear_sell_start + "~";
            profit.Text = utility.profit_percent_text;
            loss.Text = utility.loss_percent_text;
            buy_condition.SelectedIndex = utility.Fomula_list_buy;
            buy_condtion_method.Text = mode[utility.buy_set1] + " - " + hoo[utility.buy_set2];
            sell_condtion.SelectedIndex = utility.Fomula_list_sell;
            sell_condtion_method.Text = mode[utility.sell_set1] + " - " + hoo[utility.sell_set2];
        }

        //초기 매매 설정
        private void auto_allow()
        {
            //1. 자동 설정 여부 확인

            //2. 1s 마다 작동 시간 확인

            //3. 실시간 조건식 등록
        }

        //---------------BUTTON 모음---------------------

        //main menu
        private void main_menu(object sender, EventArgs e)
        {

        }

        //설정창 실행
        private void trade_setting(object sender, EventArgs e)
        {
            if (!utility.load_check || account.Length == 0)
            {
                MessageBox.Show("로딩중입니다.");
                return;
            }
            Setting newform2 = new Setting();
            newform2.ShowDialog(); //form2 닫기 전까지 form1 제어 불가능

        }

        //종목 조회
        private void stock_search_btn(object sender, EventArgs e)
        {
            if (axKHOpenAPI1.GetConnectState() == 0)
            {
                MessageBox.Show("Open API 연결되어 있지 않습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(Stock_code.Text.Trim()))
            {
                MessageBox.Show("종목코드를 입력해주세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            WriteLog("[종목 조회]\n");
            SearchStockInfo(Stock_code.Text.Trim());

        }
        private void SearchStockInfo(string code)
        {
            axKHOpenAPI1.SetInputValue("종목코드", code);
            int result = axKHOpenAPI1.CommRqData("주식기본정보", "OPT10001", 0, GetScreenNo());
            GetErrorMessage(result);
        }

        //조건식 일반 검색
        private void normal_search_btn(object sender, EventArgs e)
        {
            //검색된 조건식이 없을시
            if (string.IsNullOrEmpty(Fomula_list.SelectedItem.ToString())) return;
            //검색된 조건식이 있을시
            string[] condition = Fomula_list.SelectedItem.ToString().Split('^');
            var condInfo = conditionInfo.Find(f => f.Index == Convert.ToInt32(condition[0]));
            if (condInfo == null) return;
            //조건식에 대한 검색은 60초 마다 가능
            if (condInfo.LastRequestTime != null && condInfo.LastRequestTime >= DateTime.Now.AddSeconds(-60))
            {
                int second = 60 - (DateTime.Now - condInfo.LastRequestTime.Value).Seconds;
                WriteLog($"{second}초 후에 조회 가능합니다.\n");
                return;
            }
            //
            WriteLog("[일반 검색]\n");
            //
            condInfo.LastRequestTime = DateTime.Now;
            //종목 검색 요청
            //화면 번호, 조건식 이름, 조건식 번호, 조회 구분(0은 일반 검색, 1은 실시간 검색)
            int result = axKHOpenAPI1.SendCondition(GetScreenNo(), condition[1], Convert.ToInt32(condition[0]), 0);
            if (result == 1)
                WriteLog("조건식 일반 검색 성공\n");
            else
                WriteLog("조건식 일반 검색 실패\n");
        }

        private void real_time_search_btn(object sender, EventArgs e)
        {
            //검색된 조건식이 없을시
            if (string.IsNullOrEmpty(Fomula_list.SelectedItem.ToString())) return;
            //검색된 조건식이 있을시
            string[] condition = Fomula_list.SelectedItem.ToString().Split('^');
            var condInfo = conditionInfo.Find(f => f.Index == Convert.ToInt32(condition[0]));
            if (condInfo == null) return;
            //조건식에 대한 검색은 60초 마다 가능
            if (condInfo.LastRequestTime != null && condInfo.LastRequestTime >= DateTime.Now.AddSeconds(-60))
            {
                int second = 60 - (DateTime.Now - condInfo.LastRequestTime.Value).Seconds;
                WriteLog($"{second}초 후에 조회 가능합니다.\n");
                return;
            }
            //
            WriteLog("[실시간 검색]\n");
            //
            condInfo.LastRequestTime = DateTime.Now;
            //종목 검색 요청
            //화면 번호, 조건식 이름, 조건식 번호, 조회 구분(0은 일반 검색, 1은 실시간 검색)
            int result = axKHOpenAPI1.SendCondition(GetScreenNo(), condition[1], Convert.ToInt32(condition[0]), 1);
            if (result == 1)
                WriteLog("조건식 실시간 검색 성공\n");
            else
                WriteLog("조건식 실시간 검색 실패\n");
        }

        //조건식 실시간 중단
        private void real_time_stop_btn(object sender, EventArgs e)
        {
            // 검색된 조건식이 없을시
            if (string.IsNullOrEmpty(Fomula_list.SelectedItem.ToString())) return;
            //검색된 조건식이 있을시
            string[] condition = Fomula_list.SelectedItem.ToString().Split('^');
            //실시간 조건검색 중지
            WriteLog("[조건식검색-전체중단]\n");
            axKHOpenAPI1.SendConditionStop(GetScreenNo(), condition[1], Convert.ToInt32(condition[0]));
            //실시간 시세 중단
            WriteLog("[실시간시세-전체중단]\n");
            axKHOpenAPI1.SetRealRemove("ALL", "ALL");
        }

        //--------------실시간 조건---------------------

        //조건식 초기 검색(일반, 실시간)
        private void onReceiveTrCondition(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrConditionEvent e)
        {
            WriteLog("[조건식검색-시작]\n");
            string code = e.strCodeList.Trim();
            if (string.IsNullOrEmpty(code)) return;
            if (code.Length > 0) code = code.Remove(code.Length - 1);
            int codeCount = code.Split(';').Length;
            //
            foreach(string single_code in code.Split(';'))
            {
                WriteLog("[신규종목-편입] : " + single_code + "\n");
            }
            //종목 데이터
            //종목코드 리스트, 연속조회여부(기본값0만존재), 종목코드 갯수, 종목(0 주식, 3 선물옵션), 사용자 구분명, 화면번호
            axKHOpenAPI1.CommKwRqData(code, 0, codeCount, 0, "조건일반검색", GetScreenNo());
        }


        //실시간 종목 편입 이탈
        private void onReceiveRealCondition(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealConditionEvent e)
        {
            switch (e.strType)
            {
                //종목 편입
                case "I":
                    //일시 항목 출력
                    WriteLog("[신규종목-편입] : " + e.sTrCode + "\n");
                    axKHOpenAPI1.SetInputValue("종목코드", e.sTrCode);
                    axKHOpenAPI1.CommRqData("조건실시간검색", "OPT10001", 0, GetScreenNo());
                    //실시간 항목 등록(대비기호, 현재가. 등락율, 거래량)
                    WriteLog("[실시간시세-등록] : " + e.sTrCode + "\n");
                    axKHOpenAPI1.SetRealReg(GetScreenNo(), e.sTrCode, "10;12;13", "1");
                    break;

                //종목 이탈
                case "D":
                    //종목 이탈
                    WriteLog("[기존종목-이탈] : " + e.sTrCode + "\n");
                    DataRow[] findRows = dtCondStock.Select($"종목코드 = {e.sTrCode}");
                    if (findRows.Length == 0) return;
                    //dtCondStock.Rows.Remove(findRows[0]);
                    findRows[0]["편입"] = "이탈";
                    dtCondStock.AcceptChanges();
                    dataGridView1.DataSource = dtCondStock;
                    //실시간 시세 중단(보유 중인 종목일 경우 미실시)
                    WriteLog("[실시간시세-해지] : " + e.sTrCode + "\n");
                    axKHOpenAPI1.SetRealRemove("ALL", e.sTrCode);
                    break;
            }
        }


        //실시간 시세 등록(지속적 발생)(대비기호, 현재가. 등락율, 거래량)
        private void onReceiveRealData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEvent e)
        {
            //FID값 업데이트
            DataRow[] findRows = dtCondStock.Select($"종목코드 = {e.sRealKey}");
            if (findRows.Length == 0) return;
            //findRows[0]["대비기호"] = axKHOpenAPI1.GetCommRealData(e.sRealKey, 25);
            String price = string.Format("{0:#,##0}", axKHOpenAPI1.GetCommRealData(e.sRealKey, 10).Trim()); //새로운 현재가
            String updown = string.Format("{0:#,##0.00}%", axKHOpenAPI1.GetCommRealData(e.sRealKey, 12).Trim()); //새로운 등락율
            String amount = string.Format("{0:#,##0}", axKHOpenAPI1.GetCommRealData(e.sRealKey, 13).Trim()); //새로운 거래량
            //
            if (!findRows[0]["현재가"].Equals(price))
            {
                findRows[0]["현재가"] = string.Format("{0:#,##0}", axKHOpenAPI1.GetCommRealData(e.sRealKey, 10).Trim());
                dtCondStock.AcceptChanges();
                dataGridView1.DataSource = dtCondStock;
            }
            if (!findRows[0]["등락율"].Equals(updown))
            {
                findRows[0]["등락율"] = string.Format("{0:#,##0.00}%", axKHOpenAPI1.GetCommRealData(e.sRealKey, 12).Trim());
                dtCondStock.AcceptChanges();
                dataGridView1.DataSource = dtCondStock;
            }
            if (!findRows[0]["거래량"].Equals(amount))
            {
                findRows[0]["거래량"] = string.Format("{0:#,##0}", axKHOpenAPI1.GetCommRealData(e.sRealKey, 13).Trim());
                dtCondStock.AcceptChanges();
                dataGridView1.DataSource = dtCondStock;
            }
            //dtCondStock.AcceptChanges();
            //dataGridView1.DataSource = dtCondStock;

            //매도 감시
        }

        //--------------실시간 매수---------------------

        //1. 1s 마다 dtCondStock에 편입 상태인 종목이 있는지 파악

        //2. 실시간 종목 편입에 대하여 확인

        //3. 매수 시간 확인

        //4. 매수 횟수 확인

        //5. 매수 보유 확인

        //6. 매수 중복 확인

        //7. 일반, 시장가에 대하여 주문 가능 개수 계산

        //8. 매수 주문

        //9. 매수 미체결 취소 확인

        //10. 매수 주문 확인


        //--------------실시간 매도 및 청산---------------------

        //1. 실시간 가격에 대해 확인

        //2. 1s 마다 청산 시간 확인

        //3. 매도 주문

        //4. 매도 종목 실시간 시세 해지

        //5. 매도 미체결 취소 확인

        //6. 매도 주문 확인

        //7. 매매내역 업데이트

        //8. 당일 손익 및 손익률 계산

        //9. 예수금 업데이트

        //---------------1s 마다 실행할 함수 지정---------------------
        private void ClockEvent(object sender, EventArgs e)
        {
            //시간 표시
            timetimer.Text = DateTime.Now.ToString("yy MM-dd (ddd) HH:mm:ss");
            //계좌 업데이트
            account_real();
        }

        //실시간 잔고 조회(0346)
        private void account_real()
        {
            axKHOpenAPI1.SetInputValue("계좌번호", utility.setting_account_number);
            axKHOpenAPI1.SetInputValue("상장폐지조회구분", "0");
            axKHOpenAPI1.SetInputValue("비밀번호입력매체구분", "00");
            int result = axKHOpenAPI1.CommRqData("계좌평가현황요청", "OPW00004", 0, GetScreenNo());
            GetErrorMessage(result);
        }

        //실시간 계좌 평가 손익과 손익률

        //매도 감시

        //---------------매매내역---------------------

        //1. 당일 손익 및 손익률 계산

        //2. 총수익 누계


        //---------------업데이트 내역---------------------



        //---------------동의사항---------------------



        //---------------특별기능1(한국투자증권API)---------------------



        //---------------불필요 기능---------------------

        private void Login_btn_Click(object sender, EventArgs e)
        {

        }

        private void Trade_Auto_Load(object sender, EventArgs e)
        {

        }

        private void total_money_Click(object sender, EventArgs e)
        {

        }

        private void label35_Click(object sender, EventArgs e)
        {

        }
    }
}
