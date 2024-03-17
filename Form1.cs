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
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Web;
using System.Threading.Tasks;


using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Trade_Auto : Form
    {
        //-----------------------------------공용 신호----------------------------------------

        static public string[] arrCondition;
        static public string[] account;

        //-----------------------------------------------Main------------------------------------------------
        public Trade_Auto()
        {
            InitializeComponent();

            //-------------------초기 동작-------------------

            //시간 동작
            timer1.Start(); //시간 표시 - 1000ms
            timer2.Start(); //보유 계좌 현황 - 1000ms
            timer3.Start(); //편입 종목 감시 - 200ms

            //테이블 초기 세팅
            initial_Table();

            //초기 실행(비동기)
            Run();


            //TR조회
            axKHOpenAPI1.OnReceiveTrData += onReceiveTrData;

            //----------------매매 동작-------------------
            axKHOpenAPI1.OnReceiveChejanData += onReceiveChejanData;

            //-------------------버튼-------------------
            Login_btn.Click += login_btn; //로그인
            Main_menu.Click += main_menu; //메인메뉴
            Trade_setting.Click += trade_setting; //설정창
            //매매정보
            //업데이트 및 동의사항
            //사용설명서

            update_interval.SelectedIndexChanged += acc_interval; //계좌 조회 인터벌 변경
            Stock_search_btn.Click += stock_search_btn; //종목조회

            Real_time_search_btn.Click += real_time_search_btn; //실시간 조건식 등록
            Real_time_stop_btn.Click += real_time_stop_btn; //조건식 실시간 전체 중단


            //----------------데이터 조회 동작-------------------
            axKHOpenAPI1.OnReceiveTrData += onReceiveTrData; //TR조회
            axKHOpenAPI1.OnReceiveTrCondition += onReceiveTrCondition; //매도 및 실시간 조건식 종목 정보 받기
            axKHOpenAPI1.OnReceiveRealCondition += onReceiveRealCondition; //실시간 조건식 편출입 종목 받기
            axKHOpenAPI1.OnReceiveRealData += onReceiveRealData; //실시간 조건식 시세 받기
            axKHOpenAPI1.OnReceiveChejanData += onReceiveChejanData; //매매 정보 받기
        }

        //-----------------------------------storage----------------------------------------

        //telegram용 초당 1회 전송 저장소
        private Queue<String> telegram_chat = new Queue<string>();

        //실시간 조건 검색 용 테이블(누적 저장)
        private DataTable dtCondStock = new DataTable();

        //실시간 계좌 보유 현황 용 테이블(누적 저장)
        private DataTable dtCondStock_hold = new DataTable();

        //------------------------------------------공용기능-------------------------------------------

        //timer1(1000ms) : 주기 고정
        private void ClockEvent(object sender, EventArgs e)
        {
            //시간표시
            timetimer.Text = DateTime.Now.ToString("yy MM-dd (ddd) HH:mm:ss");

            //Telegram 전송
            if (utility.Telegram_Allow && telegram_chat.Count > 0)
            {
                telegram_send(telegram_chat.Dequeue());
            }
        }

        //timer2(1000ms) : 실시간 잔고 조회(0346)  / 주기 변경 가능
        private void Reload_Timer(object sender, EventArgs e)
        {
            //계좌 보유 종목 갱신
            if (utility.load_check)
            {
                axKHOpenAPI1.SetInputValue("계좌번호", utility.setting_account_number);
                axKHOpenAPI1.SetInputValue("상장폐지조회구분", "0");
                axKHOpenAPI1.SetInputValue("비밀번호입력매체구분", "00");
                int result = axKHOpenAPI1.CommRqData("계좌평가현황요청", "OPW00004", 2, GetScreenNo());
                GetErrorMessage(result);
            }  
        }

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

        //telegram_chat
        private void telegram_message(string message)
        {
            string time = DateTime.Now.ToString("HH:mm:ss");
            string message_edtied = "[" + time + "] " + message;
            telegram_chat.Enqueue(message_edtied);
        }

        //telegram_send(초당 1개씩 전송)
        private async void telegram_send(string message)
        {
            string urlString = $"https://api.telegram.org/bot{utility.telegram_token}/sendMessage?chat_id={utility.telegram_user_id}&text={message}";

            WebRequest request = WebRequest.Create(urlString);
            request.Timeout = 60000; // 60초로 Timeout 설정

            //await은 비동기 작업이 완료될떄까지 기다린다.
            //using 문은 IDisposable 인터페이스를 구현한 객체의 리소스를 안전하게 해제하는 데 사용
            using (WebResponse response = await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string responseString = await reader.ReadToEndAsync();
            }
        }

        //-----------------------------------------initial-------------------------------------

        //초기 실행
        private async Task Run()
        {
            //기존 세팅 로드
            await utility.setting_load_auto();

            //초기세팅 반영
            await initial_allow();

            await Task.Run(() =>
            {
                //로그인
                axKHOpenAPI1.CommConnect();

                //로그인 상태 확인(ID,NAME,계좌번호,KEYBOARD,FIREWALL,조건식)
                axKHOpenAPI1.OnEventConnect += onEventConnect;

                //조건식 조회
                axKHOpenAPI1.OnReceiveConditionVer += onReceiveConditionVer;

            }); 


            //전체 종목 업데이트

            //기존 종목 테이블에 추가

        }

        //초기 설정 반영
        public async Task initial_allow()
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
            buy_condition.Text = utility.Fomula_list_buy_text;
            buy_condtion_method.Text = mode[utility.buy_set1] + " - " + hoo[utility.buy_set2];
            sell_condtion.Text = utility.Fomula_list_sell_text;
            sell_condtion_method.Text = mode[utility.sell_set1] + " - " + hoo[utility.sell_set2];

            //갱신 주기
            string[] ms = { "200", "400", "500", "1000", "2000", "5000" };
            update_interval.Items.AddRange(ms);

            //
            WriteLog("세팅 반영 완료\n");
            telegram_message("세팅 반영 완료\n");
        }

        //초기 Table 값 입력
        private void initial_Table()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("편입", typeof(string)); // '편입' '이탈'
            dataTable.Columns.Add("상태", typeof(string)); // '대기' '매수중 '매수완료' '매도중' '매도완료'
            dataTable.Columns.Add("종목코드", typeof(string));
            dataTable.Columns.Add("종목명", typeof(string));
            dataTable.Columns.Add("현재가", typeof(string)); // + - 부호를 통해 매수호가인지 매도 호가인지 현재가인지 파악한다.
            dataTable.Columns.Add("등락율", typeof(string));
            dataTable.Columns.Add("거래량", typeof(string));
            dataTable.Columns.Add("편입가", typeof(string));
            dataTable.Columns.Add("수익률", typeof(string));
            dataTable.Columns.Add("조건식", typeof(string));
            dataTable.Columns.Add("편입시간", typeof(string));
            dtCondStock = dataTable;
            dataGridView1.DataSource = dtCondStock;

            DataTable dataTable2 = new DataTable();
            dataTable2.Columns.Add("종목명", typeof(string));
            dataTable2.Columns.Add("현재가", typeof(string));
            dataTable2.Columns.Add("보유수량", typeof(string));
            dataTable2.Columns.Add("평균단가", typeof(string));
            dataTable2.Columns.Add("평가금액", typeof(string));
            dataTable2.Columns.Add("손익률", typeof(string));
            dataTable2.Columns.Add("손익금액", typeof(string));
            dataTable2.Columns.Add("매도수량", typeof(string));
            dtCondStock_hold = dataTable2;
            dataGridView2.DataSource = dtCondStock_hold;
        }

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
                telegram_message("로그인 성공\n");

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
                if (axKHOpenAPI1.GetConditionLoad() == 1)
                {
                    WriteLog("조건식 검색 성공\n");
                    telegram_message("조건식 검색 성공\n");
                }
                else
                {
                    WriteLog("조건식 검색 실패\n");
                    telegram_message("조건식 검색 실패\n");
                }

                //예수금 받아오기
                GetCashInfo(acc_text.Text.Trim());

            }
            else
            {
                switch (e.nErrCode)
                {
                    case 100:
                        WriteLog("사용자 정보교환 실패\n");
                        telegram_message("사용자 정보교환 실패\n");
                        break;
                    case 101:
                        WriteLog("서버접속 실패\n");
                        telegram_message("서버접속 실패\n");
                        break;
                    case 102:
                        WriteLog("버전처리 실패\n");
                        telegram_message("버전처리 실패\n");
                        break;
                }

            }

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

        //조건식 조회(조건식이 있어야 initial 작동 / initial을 통해 계좌를 받아와야 GetCashInfo)
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
            WriteLog("조건식 조회 성공\n");
            telegram_message("조건식 조회 성공\n");
            //실시간 조건 검색 시작
            auto_allow();
        }

        //초기 보유 종목 테이블 업데이트


        //한국투자증권API

        //--------------------------------TR TABLE--------------------------------------------

        //데이터 조회(예수금, 유가증권, 조건식, 일반 검색, 실시간 검색 등)
        private void onReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            //
            string[] name_split = e.sRQName.Split('/');
            string split_name = name_split[0];
            string condition_name = "";
            if (name_split.Length == 2)
            {
                condition_name = name_split[1];
            }


            //
            switch (split_name)
            {
                //예수금 데이터 조회
                case "예수금상세현황":
                    User_money.Text = string.Format("{0:#,##0}", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "예수금").Trim()));
                    WriteLog("예수금 조회 완료\n");
                    WriteLog("예수금 : " + User_money.Text + "\n");
                    telegram_message("예수금 조회 완료\n");
                    telegram_message("예수금 : " + User_money.Text + "\n");
                    break;

                //계좌 보유 현황 조회
                case "계좌평가현황요청":
                    DataTable dataTable2 = new DataTable();
                    dataTable2.Columns.Add("종목명", typeof(string));
                    dataTable2.Columns.Add("현재가", typeof(string));
                    dataTable2.Columns.Add("보유수량", typeof(string));
                    dataTable2.Columns.Add("평균단가", typeof(string));
                    dataTable2.Columns.Add("평가금액", typeof(string));
                    dataTable2.Columns.Add("손익률", typeof(string));
                    dataTable2.Columns.Add("손익금액", typeof(string));
                    dataTable2.Columns.Add("매도수량", typeof(string));
                    int count2 = axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName);
                    for (int i = 0; i < count2; i++)
                    {
                        dataTable2.Rows.Add(
                            axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목명").Trim(),
                            string.Format("{0:#,##0}", Convert.ToInt32(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "현재가").Trim())),
                            string.Format("{0:#,##0}", Convert.ToInt32(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "보유수량").Trim())),
                            string.Format("{0:#,##0}", Convert.ToInt32(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "평균단가").Trim())),
                            string.Format("{0:#,##0}", Convert.ToInt32(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "평가금액").Trim())),
                            string.Format("{0:#,##0.00}%", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "손익률").Trim())),
                            string.Format("{0:#,##0}", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "손익금액").Trim())),
                            string.Format("{0:#,##0}", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "매도수량").Trim()))
                        );
                    }
                    dtCondStock_hold = dataTable2;
                    dataGridView2.DataSource = dtCondStock_hold;
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
                    dataTable.Columns.Add("편입", typeof(string)); // '편입' '이탈'
                    dataTable.Columns.Add("상태", typeof(string)); // '대기' '매수중 '매수완료' '매도중' '매도완료'
                    dataTable.Columns.Add("종목코드", typeof(string));
                    dataTable.Columns.Add("종목명", typeof(string));
                    dataTable.Columns.Add("현재가", typeof(string)); // + - 부호를 통해 매수호가인지 매도 호가인지 현재가인지 파악한다.
                    dataTable.Columns.Add("등락율", typeof(string));
                    dataTable.Columns.Add("거래량", typeof(string));
                    dataTable.Columns.Add("편입가", typeof(string));
                    dataTable.Columns.Add("수익률", typeof(string));
                    dataTable.Columns.Add("조건식", typeof(string));
                    dataTable.Columns.Add("편입시간", typeof(string));
                    int count = axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName);
                    for (int i = 0; i < count; i++)
                    {
                        int current_price = Math.Abs(Convert.ToInt32(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "현재가").Trim()));
                        string time1 = DateTime.Now.ToString("HH:mm:ss");
                        string code = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목코드").Trim();
                        string code_name = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목명").Trim();
                        string condition1 = false ? "매수중": "대기"; //buy_check(code, time1)
                        //
                        WriteLog("[신규종목/초기/" + condition_name + "] : " + code + "-" + code_name + "\n");
                        telegram_message("[신규종목/초기/" + condition_name + "] : " + code + "-" + code_name + "\n");
                        //
                        dataTable.Rows.Add(
                            "편입",
                            condition1,
                            code,
                            code_name,
                            string.Format("{0:#,##0}", current_price),
                            string.Format("{0:#,##0.00}%", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "등락율").Trim())),
                            string.Format("{0:#,##0}", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "거래량").Trim())),
                            string.Format("{0:#,##0}", current_price),
                            "0.00%",
                            condition_name,
                            time1
                        );
                    }
                    dtCondStock = dataTable;
                    dataGridView1.DataSource = dtCondStock;
                    break;

                //실시간 조건 검색(상태(편입, 이탈, 매수, 매도), 종목코드, 종목명, 등락표시, 현재가, 등락율, 거래량, 편입가, 편입대비, 수익률, 편입시간, 매수조건식, 매도조건식) => 상태, 종목코드, 대비기호, 현재가. 등락율, 거래량
                case "조건실시간검색":
                    int current_price2 = Math.Abs(Convert.ToInt32(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "현재가").Trim()));
                    string time2 = DateTime.Now.ToString("HH:mm:ss");
                    string code2 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "종목코드").Trim();
                    string code_name2 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "종목명").Trim();
                    string condition2 = false ? "매수중" : "대기"; //buy_check(code2, time2) 
                    //
                    WriteLog("[신규종목/편입/" + condition_name + "] : " + code2 + "-" + code_name2 + "\n");
                    telegram_message("[신규종목/편입/" + condition_name + "] : " + code2 + "-" + code_name2 + "\n");
                    //
                    dtCondStock.Rows.Add(
                        "편입",
                        condition2,
                        code2,
                        code_name2,
                        string.Format("{0:#,##0}", current_price2),
                        string.Format("{0:#,##0.00}%", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "등락율").Trim())),
                        string.Format("{0:#,##0}", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "거래량").Trim())),
                        string.Format("{0:#,##0}", current_price2),
                        "0.00%",
                        condition_name,
                        time2
                    );
                    dataGridView1.DataSource = dtCondStock;
                    break;
            }
        }

        //---------------LOAD---------------------

        //초기 매매 설정
        private async Task auto_allow()
        {
            //자동 설정 여부
            if (utility.auto_trade_allow)
            {
                //자동 매수 조건식 설정 여부
                if (utility.buy_condition)
                {
                        real_time_search_btn(null, EventArgs.Empty);
                }
                else
                {
                    WriteLog("자동 조건식 매수 미설정\n");
                    telegram_message("자동 조건식 매수 미설정\n");
                }
                //자동 매도 조건식 설정 여부
                if (utility.sell_condition)
                {
                    WriteLog("실시간 조건식 매도 시작\n");
                    telegram_message("실시간 조건식 매도 시작\n");
                    real_time_search_btn(null, EventArgs.Empty);

                }
                else
                {
                    WriteLog("자동 조건식 매도 미설정\n");
                    telegram_message("자동 조건식 매도 미설정\n");
                }
            }
            else
            {
                WriteLog("자동 실행 미설정\n");
                telegram_message("자동 실행 미설정\n");
            }
        }

        //------------------------------기본 BUTTON 모음-------------------------------------

        //main menu 실행
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

        //매매내역 실행



        //업데이트 및 사용동의 실행



        //사용설명 실행


        //계좌 보유 현황 갱신 주기 설정
        private void acc_interval(object sender, EventArgs e)
        {
            timer2.Interval = Convert.ToInt32(update_interval.Text);
        }


        //종목 조회 실행
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

        //------------------------------실시간 실행 초기 시작 모음-------------------------------------

        //매도 전용 조건식 검색
        private void normal_search_btn(object sender, EventArgs e)
        {
            //검색된 조건식이 없을시
            if (string.IsNullOrEmpty(sell_condtion.Text)) return;

            //검색된 조건식이 있을시
            string[] condition = sell_condtion.Text.Split('^');
            var condInfo = conditionInfo.Find(f => f.Index == Convert.ToInt32(condition[0]));
            if (condInfo == null) return;

            //조건식에 대한 검색은 60초 마다 가능
            if (condInfo.LastRequestTime != null && condInfo.LastRequestTime >= DateTime.Now.AddSeconds(-60))
            {
                int second = 60 - (DateTime.Now - condInfo.LastRequestTime.Value).Seconds;
                WriteLog($"{second}초 후에 조회 가능합니다.\n");
                return;
            }

            condInfo.LastRequestTime = DateTime.Now;

            //종목 검색 요청
            //화면 번호, 조건식 이름, 조건식 번호, 조회 구분(0은 일반 검색, 1은 실시간 검색)
            int result = axKHOpenAPI1.SendCondition(GetScreenNo(), condition[1], Convert.ToInt32(condition[0]), 0);
            if (result == 1)
                WriteLog("조건식 일반 검색 성공\n");
            else
                WriteLog("조건식 일반 검색 실패\n");
        }

        //실시간 검색(조건식 로드 후 사용가능하다)
        private void real_time_search_btn(object sender, EventArgs e)
        {
            //실시간 검색이 시작되면 '일반 검색'이 불가능해 진다.
            Real_time_stop_btn.Enabled = true;
            Real_time_search_btn.Enabled = false;

            //조건식이 로딩되었는지
            if (string.IsNullOrEmpty(buy_condition.Text))
            {
                WriteLog("선택된 조건식이 없습니다.\n");
                telegram_message("선택된 조건식이 없습니다.\n");
                Real_time_stop_btn.Enabled = false;
                Real_time_search_btn.Enabled = true;
                return;
            }

            //검색된 조건식이 있을시
            string[] condition = buy_condition.Text.Split('^');
            var condInfo = conditionInfo.Find(f => f.Index == Convert.ToInt32(condition[0]));

            //로드된 조건식 목록에 설정된 조건식이 존재하지 않는 경우 이탈
            if (condInfo == null)
            {
                WriteLog("선택된 조건식이 조건색 리스트에 없습니다.\n");
                telegram_message("선택된 조건식이 조건색 리스트에 없습니다.\n");
                Real_time_stop_btn.Enabled = false;
                Real_time_search_btn.Enabled = true;
                return;
            }

            //조건식에 대한 검색은 60초 마다 가능
            if (condInfo.LastRequestTime != null && condInfo.LastRequestTime >= DateTime.Now.AddSeconds(-60))
            {
                int second = 60 - (DateTime.Now - condInfo.LastRequestTime.Value).Seconds;
                WriteLog($"{second}초 후에 조회 가능합니다.\n");
                Real_time_stop_btn.Enabled = false;
                Real_time_search_btn.Enabled = true;
                return;
            }
            
            //마지막 조건식 검색 시각 업데이트
            condInfo.LastRequestTime = DateTime.Now;

            WriteLog("실시간 조건식 매수 시작\n");
            telegram_message("실시간 조건식 매수 시작\n");

            //종목 검색 요청
            //화면 번호, 조건식 이름, 조건식 번호, 조회 구분(0은 일반 검색, 1은 실시간 검색)
            int result = axKHOpenAPI1.SendCondition(GetScreenNo(), condition[1], Convert.ToInt32(condition[0]), 1);
            if (result != 1)
            {
                WriteLog("실시간조건검색 : 실패\n");
                telegram_message("실시간조건검색 : 실패\n");
            }
        }

        //조건식 실시간 중단
        private void real_time_stop_btn(object sender, EventArgs e)
        {
            //실시간 중단이 선언되면 '실시간시작'과 '일반검색'이 가능해진다.
            Real_time_stop_btn.Enabled = false;
            Real_time_search_btn.Enabled = true;

            // 검색된 조건식이 없을시
            if (string.IsNullOrEmpty(buy_condition.Text)) return;

            //검색된 조건식이 있을시
            string[] condition = buy_condition.Text.Split('^');

            //실시간 중지
            WriteLog("실시간조건식검색 : 중단\n");
            telegram_message("실시간조건식검색 : 중단\n");
            axKHOpenAPI1.SendConditionStop(GetScreenNo(), condition[1], Convert.ToInt32(condition[0])); //조건검색 중지
            axKHOpenAPI1.SetRealRemove("ALL", "ALL"); //실시간 시세 중지
        }

        //-----------------------실시간 조건 검색------------------------------

        //조건식 초기 검색(일반, 실시간)
        private void onReceiveTrCondition(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrConditionEvent e)
        {
            WriteLog("실시간조건검색 : 시작\n");
            telegram_message("실시간조건검색 : 시작\n");
            string code = e.strCodeList.Trim();
            if (string.IsNullOrEmpty(code)) return;
            if (code.Length > 0) code = code.Remove(code.Length - 1);
            int codeCount = code.Split(';').Length;
            //
            //종목 데이터
            //종목코드 리스트, 연속조회여부(기본값0만존재), 종목코드 갯수, 종목(0 주식, 3 선물옵션), 사용자 구분명, 화면번호
            axKHOpenAPI1.CommKwRqData(code, 0, codeCount, 0, "조건일반검색/"+ e.strConditionName, GetScreenNo());
        }


        //실시간 종목 편입 이탈
        private void onReceiveRealCondition(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealConditionEvent e)
        {
            switch (e.strType)
            {
                //종목 편입
                case "I":
                    axKHOpenAPI1.SetInputValue("종목코드", e.sTrCode);
                    axKHOpenAPI1.CommRqData("조건실시간검색/" + e.strConditionName, "OPT10001", 0, GetScreenNo());
                    //실시간 항목 등록(대비기호, 현재가. 등락율, 거래량)
                    axKHOpenAPI1.SetRealReg(GetScreenNo(), e.sTrCode, "10;12;13", "1");
                    break;

                //종목 이탈
                case "D":
                    //종목 이탈
                    WriteLog("기존종목(이탈) : " + e.sTrCode + "\n");
                    telegram_message("기존종목(이탈) : " + e.sTrCode + "\n");
                    DataRow[] findRows = dtCondStock.Select($"종목코드 = {e.sTrCode}");
                    if (findRows.Length == 0) return;

                    findRows[0]["편입"] = "이탈";
                    dtCondStock.AcceptChanges();
                    dataGridView1.DataSource = dtCondStock;

                    //실시간 시세 중단(보유 중인 종목일 경우 미실시)
                    axKHOpenAPI1.SetRealRemove("ALL", e.sTrCode);
                    break;
            }
        }
        

        //실시간 시세(지속적 발생)(현재가. 등락율, 거래량, 수익률)
        private void onReceiveRealData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEvent e)
        {
            //시세 발생시간
            string time1 = DateTime.Now.ToString("HH:mm:ss");

            //종목 확인
            DataRow[] findRows = dtCondStock.Select($"종목코드 = {e.sRealKey}");

            //검출 종목이 아니거나 검출 후 시세 해지 못한 종목 => 불필요한듯
            if (findRows.Length == 0) return;

            //신규 값 받기
            string price = Regex.Replace(axKHOpenAPI1.GetCommRealData(e.sRealKey, 10).Trim(), @"[\+\-]", ""); //새로운 현재가
            string updown = axKHOpenAPI1.GetCommRealData(e.sRealKey, 12).Trim(); //새로운 등락율
            string amount = axKHOpenAPI1.GetCommRealData(e.sRealKey, 13).Trim(); //새로운 거래량
            string percent;

            //[우선] 수익률 계산
            if (!price.Equals(""))
            {
                double native_price = Convert.ToDouble(price);
                double native_percent = (native_price - Convert.ToDouble(findRows[0]["편입가"].ToString().Replace(",", ""))) / Convert.ToDouble(findRows[0]["편입가"].ToString().Replace(",", "")) * 100;
                percent = string.Format("{0:#,##0.00}%", Convert.ToDecimal(native_percent)); //새로운 수익률
                findRows[0]["수익률"] = percent;
            }
            else
            {
                percent = findRows[0]["수익률"].ToString(); //기존 수익률
                findRows[0]["수익률"] = percent;
            }

            //매도 확인
            sell_check(e.sRealKey, percent, time1);

            //신규 값 빈값 확인
            if (!price.Equals(""))
            {
                findRows[0]["현재가"] = string.Format("{0:#,##0}", Convert.ToInt32(price)); //새로운 현재가
            }
            if (!updown.Equals(""))
            {
                findRows[0]["등락율"] = string.Format("{0:#,##0.00}%", Convert.ToDecimal(updown)); //새로운 등락율
            }
            if (!amount.Equals(""))
            {
                findRows[0]["거래량"] = string.Format("{0:#,##0}", Convert.ToInt32(amount)); //새로운 거래량
            }

            //적용
            dtCondStock.AcceptChanges();
            dataGridView1.DataSource = dtCondStock;
        }

        //--------------편입 이후 매수 종목에 대한 감시(200ms)---------------------

        //timer3(200ms) : 09시 30분 이후 매수 시작인 것에 대하여 이전에 진입한 종목 중 편입 상태인 종목에 대한 매수
        private void Transfer_Timer(object sender, EventArgs e)
        {
            //검출 시간
            string time1 = DateTime.Now.ToString("HH:mm:ss");

            //특저 열 추출
            DataColumn columnEditColumn = dtCondStock.Columns["편입"];
            DataColumn columnStateColumn = dtCondStock.Columns["상태"];
            //AsEnumerable()은 DataTable의 행을 열거형으로 변환
            var filteredRows = dtCondStock.AsEnumerable()
                                        .Where(row => row.Field<string>(columnEditColumn) == "편입" &&
                                                      row.Field<string>(columnStateColumn) == "대기")
                                        .ToList();
            /*
            //검출 종목
            if (filteredRows.Count > 1)
            {
                foreach (DataRow row in filteredRows)
                {
                    buy_check(row.Field<string>("종목코드"), time1);
                }
            }
            */
        }

        //--------------실시간 매수---------------------

        //매수 가능한 상태인지 확인
        private bool buy_check(string code, string time)
        {
            //매수 시간 확인
            TimeSpan t_code = TimeSpan.ParseExact(time, "HH:mm:ss", null);
            TimeSpan t_start = TimeSpan.ParseExact(utility.buy_condition_start, "HH:mm:ss", null);
            TimeSpan t_end = TimeSpan.ParseExact(utility.sell_condition_start, "HH:mm:ss", null);

            if (t_code.CompareTo(t_start) < 0 || t_code.CompareTo(t_end) > 0)
            {
                // result가 0보다 작으면 time1 < time2
                // result가 0이면 time1 = time2
                // result가 0보다 크면 time1 > time2
                return false;
            }

            //상태 재확인

            //이전 종목 매수와의 TERM

            //보유 종목 수 확인

            //당일 매수 중복 확인

            //기존 보유 종목 확인

            //자동 시간전 검출 매수 확인

            //상태 확인


            //편입 차트 상태 '매수중' 변경

            //주문
            buy_order(code);

            return true;
        }

        //매수 주문
        private void buy_order(string code)
        {
            //일반에 대하여 주문 가능 개수 계산

            //시장가에 대하여 주문 가능 개수 계산

            //익절

            //익절TS

            //익절원

            //매수 주문

        }


        //--------------실시간 매도 및 청산---------------------

        //실시간 가격에 대해 조건 확인 및 청산 시간 확인
        private void sell_check(String code, String percent, string time)
        {
            //
        }

        //매도 주문
        private void sell_order(string code)
        {

        }

        //------------주문 상태 확인 및 정정---------------------
        private void onReceiveChejanData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveChejanDataEvent e)
        {
            //매수

            //매수 미체결

            //매도

            //매도 미체결

            //매도 종목 실시간 시세 해지

        }
    }
}
