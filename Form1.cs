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
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Timers;
using Newtonsoft.Json.Linq;
//
using System.Threading;
using System.IO.Pipes;

namespace WindowsFormsApp1
{
    public partial class Trade_Auto : Form
    {
        //-----------------------------------공용 신호----------------------------------------

        static public string[] arrCondition = { };
        static public string[] account;

        //-----------------------------------인증 관련 신호----------------------------------------

        public static string Authentication = "1ab2c3d4e5f6g7h8i9"; //인증코드에 백슬래시 및 쉼표 불가능
        public static bool Authentication_Check = true; //미인증(false) / 인증(true)
        private int sample_balance = 500000; //500,000원(미인증 매매 금액 제한)

        //Delay
        private int delay1 = 300;

        //-----------------------------------storage----------------------------------------

        //매매로그 맟 전체로그 저장
        private List<string> log_trade = new List<string>();
        private List<string> log_full = new List<string>();

        //실시간 조건 검색 용 테이블(누적 저장)
        private DataTable dtCondStock = new DataTable();

        //실시간 계좌 보유 현황 용 테이블(누적 저장)
        private DataTable dtCondStock_hold = new DataTable();

        //
        private DataTable dtCondStock_Transaction = new DataTable();

        //-----------------------------------lock---------------------------------------- 

        //Lock1
        private readonly object index_write = new object();

        //Lock2
        private readonly object buy_lock = new object();
        private readonly object sell_lock = new object();

        //Lock3
        private readonly object table1 = new object();
        private readonly object table2 = new object();
        private readonly object table3 = new object();

        private List<Tuple<string, string>> waiting_Codes = new List<Tuple<string, string>>();
        private static Dictionary<string, bool> buy_runningCodes = new Dictionary<string, bool>();
        private static Dictionary<string, bool> sell_runningCodes = new Dictionary<string, bool>();

        //------------------------------기본 BUTTON 모음-------------------------------------

        //main menu 실행
        private void main_menu(object sender, EventArgs e)
        {
            MessageBox.Show("준비중입니다.");
        }

        //update
        private void login_btn(object sender, EventArgs e)
        {
            //CommConnect를 하는 경우 KHOpenAPI Control의 OnEventConnect 이벤트가 호출
            axKHOpenAPI1.CommConnect();
        }

        //설정창 실행
        private void trade_setting(object sender, EventArgs e)
        {
            if (!utility.load_check)
            {
                MessageBox.Show("초기 세팅 반영중");
                return;
            }
            if (login_check != 0)
            {
                MessageBox.Show("로그인 완료 후 조건식 로딩");
            }
            Setting newform2 = new Setting(this);
            newform2.ShowDialog(); //form2 닫기 전까지 form1 제어 불가능
        }

        //매매내역 확인
        private void Porfoilo_btn_Click(object sender, EventArgs e)
        {
            if (!utility.load_check)
            {
                MessageBox.Show("초기 세팅 반영중");
                return;
            }
            if (login_check != 0)
            {
                MessageBox.Show("로그인 중입니다.");
                return;
            }
            if (arrCondition.Length == 0)
            {
                MessageBox.Show("조건식 로딩중");
                return;
            }
            Transaction newform2 = new Transaction();
            newform2.ShowDialog(); //form2 닫기 전까지 form1 UI 제어 불가능
        }

        //전체로그 확인
        private void Log_btn_Click(object sender, EventArgs e)
        {
            if (!utility.load_check)
            {
                MessageBox.Show("초기 세팅 반영중");
                return;
            }
            if (login_check != 0)
            {
                MessageBox.Show("로그인 중입니다.");
                return;
            }
            if (arrCondition.Length == 0)
            {
                MessageBox.Show("조건식 로딩중");
                return;
            }
            Log newform2 = new Log();
            newform2.ShowDialog(); //form2 닫기 전까지 form1 UI 제어 불가능
        }

        //업데이트 및 동의사항 확인
        private void Update_agree_btn_Click(object sender, EventArgs e)
        {
            if (!utility.load_check)
            {
                MessageBox.Show("초기 세팅 반영중");
                return;
            }
            if (login_check != 0)
            {
                MessageBox.Show("로그인 중입니다.");
                return;
            }
            if (arrCondition.Length == 0)
            {
                MessageBox.Show("조건식 로딩중");
                return;
            }
            Update newform2 = new Update(this);
            newform2.ShowDialog(); //form2 닫기 전까지 form1 UI 제어 불가능
        }

        //종목 조회 실행
        private void stock_search_btn(object sender, EventArgs e)
        {
            if (!utility.load_check)
            {
                MessageBox.Show("초기 세팅 반영중");
                return;
            }
            if (login_check != 0)
            {
                MessageBox.Show("로그인 중입니다.");
                return;
            }
            if (arrCondition.Length == 0)
            {
                MessageBox.Show("조건식 로딩중");
                return;
            }
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

            WriteLog_System("[종목 조회]\n");
            axKHOpenAPI1.SetInputValue("종목코드", Stock_code.Text.Trim());
            int result = axKHOpenAPI1.CommRqData("주식기본정보", "OPT10001", 0, GetScreenNo());
            GetErrorMessage(result);

        }

        private void real_time_search_btn(object sender, EventArgs e)
        {
            if (!utility.load_check)
            {
                MessageBox.Show("초기 세팅 반영중");
                return;
            }
            if (login_check != 0)
            {
                MessageBox.Show("로그인 중입니다.");
                return;
            }
            if (arrCondition.Length == 0)
            {
                MessageBox.Show("조건식 로딩중");
                return;
            }
            //
            lock (table1)
            {
                dtCondStock.Clear();
                gridView1_refresh();
            }

            System.Threading.Thread.Sleep(delay1);

            //예수금 + 계좌 보유 현황 + 차트 반영
            lock (table2)
            {
                dtCondStock_hold.Clear();
            }
            Account_before("초기");

            System.Threading.Thread.Sleep(delay1);

            //체결내역업데이트(주문번호)
            lock (table3)
            {
                dtCondStock_Transaction.Clear();
            }
            Transaction_Detail("", "");

            System.Threading.Thread.Sleep(delay1);

            //당일 손익 + 당일 손일률 + 당일 수수료 업데이트
            today_profit_tax_load("");

            System.Threading.Thread.Sleep(delay1);

            //실시간 조건 검색 시작
            auto_allow(true);
        }

        private void real_time_stop_btn(object sender, EventArgs e)
        {
            if (!utility.load_check)
            {
                MessageBox.Show("초기 세팅 반영중");
                return;
            }
            if (login_check != 0)
            {
                MessageBox.Show("로그인 중입니다.");
                return;
            }
            if (arrCondition.Length == 0)
            {
                MessageBox.Show("조건식 로딩중");
                return;
            }
            //
            real_time_stop(true);
        }

        //전체 청산 버튼
        private void All_clear_btn_Click(object sender, EventArgs e)
        {
            if (!utility.load_check)
            {
                MessageBox.Show("초기 세팅 반영중");
                return;
            }
            if (login_check != 0)
            {
                MessageBox.Show("로그인 중입니다.");
                return;
            }
            if (arrCondition.Length == 0)
            {
                MessageBox.Show("조건식 로딩중");
                return;
            }

            lock (table1)
            {
                if (dtCondStock.Rows.Count > 0)
                {
                    foreach (DataRow row in dtCondStock.Rows)
                    {
                        if (row["상태"].ToString() == "매수완료")
                        {
                            sell_order(row.Field<string>("현재가"), "청산매도/일반", row.Field<string>("주문번호"), row.Field<string>("수익률"));
                        }

                        System.Threading.Thread.Sleep(delay1 + 500);
                    }
                }
                else
                {
                    WriteLog_Order("전체청산 종목 없음\n");
                }
            }
        }

        //수익 종목 청산 버튼
        private void Profit_clear_btn_Click(object sender, EventArgs e)
        {
            if (!utility.load_check)
            {
                MessageBox.Show("초기 세팅 반영중");
                return;
            }
            if (login_check != 0)
            {
                MessageBox.Show("로그인 중입니다.");
                return;
            }
            if (arrCondition.Length == 0)
            {
                MessageBox.Show("조건식 로딩중");
                return;
            }

            lock (table1)
            {
                if (dtCondStock.Rows.Count > 0)
                {
                    foreach (DataRow row in dtCondStock.Rows)
                    {
                        double percent_edit = double.Parse(row.Field<string>("수익률").TrimEnd('%'));
                        if (row["상태"].ToString() == "매수완료" && percent_edit >= 0)
                        {
                            sell_order(row.Field<string>("현재가"), "청산매도/수익", row.Field<string>("주문번호"), row.Field<string>("수익률"));
                        }

                        System.Threading.Thread.Sleep(delay1 + 500);
                    }
                }
                else
                {
                    WriteLog_Order("수익청산 종목 없음\n");
                }
            }
        }

        //손실 종목 청산 버튼
        private void Loss_clear_btn_Click(object sender, EventArgs e)
        {
            if (!utility.load_check)
            {
                MessageBox.Show("초기 세팅 반영중");
                return;
            }
            if (login_check != 0)
            {
                MessageBox.Show("로그인 중입니다.");
                return;
            }
            if (arrCondition.Length == 0)
            {
                MessageBox.Show("조건식 로딩중");
                return;
            }

            lock (table1)
            {
                if (dtCondStock.Rows.Count > 0)
                {
                    foreach (DataRow row in dtCondStock.Rows)
                    {
                        double percent_edit = double.Parse(row.Field<string>("수익률").TrimEnd('%'));
                        if (row["상태"].ToString() == "매수완료" && percent_edit < 0)
                        {
                            sell_order(row.Field<string>("현재가"), "청산매도/손실", row.Field<string>("주문번호"), row.Field<string>("수익률"));
                        }

                        System.Threading.Thread.Sleep(delay1 + 500);
                    }
                }
                else
                {
                    WriteLog_Order("손실청산 종목 없음\n");
                }
            }
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            //계좌보유현황업데이트
            lock (table2)
            {
                dtCondStock_hold.Clear();
            }

            Account_before("");

            System.Threading.Thread.Sleep(delay1);

            //체결내역업데이트(주문번호)
            lock(table3)
            {
                dtCondStock_Transaction.Clear();
            }
            Transaction_Detail("", "");

            System.Threading.Thread.Sleep(delay1);

            //당일 손익 + 당일 손일률 + 당일 수수료 업데이트
            today_profit_tax_load("");
        }

        private void Match_Click(object sender, EventArgs e)
        {
            //매매내역
            lock (table3)
            {
                dtCondStock_Transaction.Clear();

            }
            Transaction_Detail("", "");

            WriteLog_System("데이터 매칭 시작\n");

            System.Threading.Thread.Sleep(delay1);
            lock (table1)
            {
                DataRow[] findRows = dtCondStock.AsEnumerable().Where(row => row.Field<string>("상태") == "매수중" && row.Field<string>("보유수량").Split('/')[0] == row.Field<string>("보유수량").Split('/')[1]).ToArray();
                if (findRows.Any())
                {
                    for (int i = 0; i < findRows.Length; i++)
                    {
                        lock (table3)
                        {
                            DataRow[] findRows2 = dtCondStock_Transaction.AsEnumerable().Where(row => row.Field<string>("주문번호") == findRows[i]["주문번호"].ToString() && row.Field<string>("체결단가") != "0").ToArray();
                            if (findRows2.Any())
                            {
                                findRows[i]["상태"] = "매수완료";
                                findRows[i]["편입상태"] = "실매입";
                                findRows[i]["편입가"] = findRows2[0]["체결단가"];
                            }
                        }

                    }
                    //
                    gridView1_refresh();
                }
            }

            System.Threading.Thread.Sleep(delay1);

            lock (table1)
            {
                DataRow[] findRows3 = dtCondStock.AsEnumerable().Where(row => row.Field<string>("상태") == "매도중" && row.Field<string>("보유수량") == "0/0").ToArray();
                if (findRows3.Any())
                {
                    for (int i = 0; i < findRows3.Length; i++)
                    {
                        lock (table3)
                        {
                            DataRow[] findRows4 = dtCondStock_Transaction.AsEnumerable().Where(row => row.Field<string>("주문번호") == findRows3[i]["주문번호"].ToString() && row.Field<string>("체결단가") != "0").ToArray();
                            if (findRows4.Any())
                            {
                                findRows3[i]["상태"] = "매도완료";
                                findRows3[i]["매도가"] = findRows4[0]["체결단가"];
                            }
                        }

                    }
                    //
                    gridView1_refresh();
                }
            }

            WriteLog_System("데이터 매칭 종료\n");
        }

        private void Select_cancel_Click(object sender, EventArgs e)
        {
            lock (table1)
            {
                DataRow[] findRows = dtCondStock.AsEnumerable().Where(row => row.Field<string>("상태") == "매수중" && row.Field<bool>("선택")).ToArray();

                if (findRows.Any())
                {
                    //
                    for (int i = 0; i < findRows.Length; i++)
                    {
                        //string trade_type, string order_number, string gubun, string code_name, string code, string order_acc
                        order_close("매수", findRows[i]["주문번호"].ToString(), findRows[i]["종목명"].ToString(), findRows[i]["종목코드"].ToString(), findRows[i]["보유수량"].ToString().Split('/')[1]);
                        System.Threading.Thread.Sleep(750);
                    }
                }
            }

            System.Threading.Thread.Sleep(delay1);

            lock (table1)
            {
                DataRow[] findRows2 = dtCondStock.AsEnumerable().Where(row => row.Field<string>("상태") == "매도중" && row.Field<bool>("선택")).ToArray();

                if (findRows2.Any())
                {
                    //
                    for (int i = 0; i < findRows2.Length; i++)
                    {
                        order_close("매도", findRows2[i]["주문번호"].ToString(), findRows2[i]["종목명"].ToString(), findRows2[i]["종목코드"].ToString(), findRows2[i]["보유수량"].ToString().Split('/')[1]);
                        System.Threading.Thread.Sleep(750);
                    }
                }
            }
        }

        //------------------------------------------로그-------------------------------------------

        //로그창(System)
        private async void WriteLog_System(string message)
        {
            string time = DateTime.Now.ToString("HH:mm:ss:fff");
            string logMessage = $"[{time}] {message}";
            string fullLogMessage = $"[{time}][System] : {message}";

            // UI 스레드에서 log_window 컨트롤에 접근
            if (log_window.InvokeRequired)
            {
                log_window.Invoke(new Action(() =>
                {
                    log_window.AppendText(logMessage);
                }));
            }
            else
            {
                log_window.AppendText(logMessage);
            }

            // 비동기적으로 로그를 리스트에 추가
            await Task.Run(() => log_full.Add(fullLogMessage));
        }

        //로그창(Order)
        private async void WriteLog_Order(string message)
        {
            string time = DateTime.Now.ToString("HH:mm:ss:fff");
            string logMessage = $"[{time}] {message}";
            string fullLogMessage = $"[{time}][Order] : {message}";
            string tradeLogMessage = $"[{time}][Order] : {message}";

            // UI 스레드에서 log_window3 컨트롤에 접근
            if (log_window3.InvokeRequired)
            {
                log_window3.Invoke(new Action(() =>
                {
                    log_window3.AppendText(logMessage);
                }));
            }
            else
            {
                log_window3.AppendText(logMessage);
            }

            // 비동기적으로 로그를 리스트에 추가
            await Task.Run(() =>
            {
                log_full.Add(fullLogMessage);
                log_trade.Add(tradeLogMessage);
            });
        }

        //로그창(Stock)
        private async void WriteLog_Stock(string message)
        {
            string time = DateTime.Now.ToString("HH:mm:ss:fff");
            string logMessage = $"[{time}] {message}";
            string fullLogMessage = $"[{time}][Stock] : {message}";

            // UI 스레드에서 log_window2 컨트롤에 접근
            if (log_window2.InvokeRequired)
            {
                log_window2.Invoke(new Action(() =>
                {
                    log_window2.AppendText(logMessage);
                }));
            }
            else
            {
                log_window2.AppendText(logMessage);
            }

            // 비동기적으로 로그를 리스트에 추가
            await Task.Run(() =>
            {
                log_full.Add(fullLogMessage);
            });
        }

        //telegram_chat
        private async void telegram_message(string message)
        {
            if (!utility.Telegram_Allow) return;
            if (telegram_stop) return;
            //
            string time = DateTime.Now.ToString("HH:mm:ss");
            string message_edtied = "[" + time + "] " + message;

            //4000자 검증
            string[] lines = message_edtied.Split(new[] { "\n" }, StringSplitOptions.None);
            StringBuilder currentMessage = new StringBuilder();

            foreach (string line in lines)
            {
                if (currentMessage.Length + line.Length + 1 > 4000)
                {
                    // 현재 메시지가 최대 길이를 초과하는 경우 전송하고 새 메시지 시작
                    telegram_send(currentMessage.ToString());
                    currentMessage.Clear();
                }

                // 현재 줄을 메시지에 추가
                if (currentMessage.Length > 0)
                {
                    currentMessage.Append("\n");
                }
                currentMessage.Append(line);
            }

            // 마지막 메시지 전송
            if (currentMessage.Length > 0)
            {
                telegram_send(currentMessage.ToString());
            }
        }

        private bool telegram_stop = false;

        //telegram_send(초당 1개씩 전송)
        private async void telegram_send(string message)
        {
            string urlString = $"https://api.telegram.org/bot{utility.telegram_token}/sendMessage?chat_id={utility.telegram_user_id}&text={message}";

            bool success = false;

            while (!success)
            {
                try
                {
                    WebRequest request = WebRequest.Create(urlString);
                    request.Timeout = 60000; // 60초로 Timeout 설정

                    //await은 비동기 작업이 완료될떄까지 기다린다.
                    //using 문은 IDisposable 인터페이스를 구현한 객체의 리소스를 안전하게 해제하는 데 사용
                    using (WebResponse response = await request.GetResponseAsync())
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string responseString = await reader.ReadToEndAsync();
                        success = true;
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Response is HttpWebResponse response && response.StatusCode == (HttpStatusCode)429)
                    {
                        WriteLog_System($"FLOOD_WAIT: Waiting for 30s...");
                        await Task.Delay(30000);
                    }
                    else
                    {
                        WriteLog_System("Telegram 전송 오류 발생 : " + ex.Message);
                        telegram_stop = true;
                        WriteLog_System("Telegram 전송 중단\n");
                    }
                }
            }
        }

        public static int update_id = 0;
        private DateTime time_start = DateTime.Now;

        //Telegram 메시지 수신
        private async Task Telegram_Receive()
        {
            //string apiUrl = $"https://api.telegram.org/bot{utility.telegram_token}/getUpdates";  

            while (true)
            {
                try
                {
                    string requestUrl = $"https://api.telegram.org/bot{utility.telegram_token}/getUpdates" + (update_id == 0 ? "" : $"?offset={update_id + 1}");
                    WebRequest request = WebRequest.Create(requestUrl);
                    using (WebResponse response = await request.GetResponseAsync())
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string response_message = await reader.ReadToEndAsync();
                        JObject jsonData = JObject.Parse(response_message);
                        JArray resultArray = (JArray)jsonData["result"];
                        //
                        if (resultArray.Count > 0)
                        {
                            foreach (var result in resultArray)
                            {
                                string message = Convert.ToString(result["message"]["text"]);
                                int current_message_number = Convert.ToInt32(result["update_id"]);
                                //
                                long unixTimestamp = Convert.ToInt64(result["message"]["date"]);
                                DateTime dateTime = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).DateTime;
                                DateTime localDateTime = dateTime.ToLocalTime();
                                //
                                if (current_message_number > update_id && localDateTime >= time_start)
                                {
                                    if (!utility.load_check)
                                    {
                                        telegram_message($"[TELEGRAM] : 초기 세팅 반영중\n");
                                        continue;
                                    }
                                    if (login_check != 0)
                                    {
                                        telegram_message($"[TELEGRAM] : 로그인 진행중\n");
                                        continue;
                                    }
                                    if (arrCondition.Length == 0)
                                    {
                                        telegram_message($"[TELEGRAM] : 조건식 로딩중\n");
                                        continue;
                                    }
                                    //
                                    WriteLog_System($"[TELEGRAM] : {message} / {current_message_number}\n"); // 수신된 메시지 확인
                                    telegram_function(message);
                                    update_id = current_message_number;
                                }
                            }
                        }
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Response is HttpWebResponse httpResponse && httpResponse.StatusCode == HttpStatusCode.Conflict)
                    {
                        // 409 충돌 오류 처리
                        WriteLog_Order($"[TELEGRAM/ERROR] 409 Conflict: {ex.Message}\n");
                    }
                    else
                    {
                        WriteLog_Order($"[TELEGRAM/ERROR] : {ex.Message}\n");
                    }
                }

                // 일정한 간격으로 API를 호출하여 새로운 메시지 확인
                await Task.Delay(1000); // 1초마다 확인
            }
            /*           
            {"ok":true,"result":
                [{"update_id":000000000,
                  "message":
                    {"message_id":22222,
                    "from":{"id":34566778,"is_bot":false,"first_name":"Sy","last_name":"CH","username":"k456","language_code":"ko"}
                    ,"chat":{"id":69sdfg,"first_name":"Ssdfg","last_name":"CsdfgI","username":"ksdfg28","type":"private"}
                    ,"date":1717078874,
                    "text":"Hello"
                    }
                }]
            }
            */
        }

        //FORM CLOSED 후 LOG 저장
        //Process.Kill()에서 비정상 작동할 가능성 높음
        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {

            string formattedDate = DateTime.Now.ToString("yyyyMMdd");

            // 저장할 파일 경로
            string filePath = $@"C:\Auto_Trade_Kiwoom\Log\{formattedDate}_full.txt";
            string filePath2 = $@"C:\Auto_Trade_Kiwoom\Log_Trade\{formattedDate}_trade.txt";
            string filePath3 = "C:\\Auto_Trade_Kiwoom\\Setting\\setting.txt";

            // StreamWriter를 사용하여 파일 저장
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.Write(String.Join("", log_full));
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("파일 저장 중 오류 발생: " + ex.Message);
            }


            // StreamWriter를 사용하여 파일 저장
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath2, true))
                {
                    writer.Write(String.Join("", log_trade));
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("파일 저장 중 오류 발생: " + ex.Message);
            }


            //Telegram Message Last Number
            try
            {
                if (!File.Exists(filePath3))
                {
                    MessageBox.Show("세이브 파일이 존재하지 않습니다.");
                    return;
                }

                // 파일의 모든 줄을 읽어오기
                var lines = File.ReadAllLines(filePath3).ToList();

                // 파일이 비어 있지 않은지 확인
                if (lines.Any())
                {
                    lines[lines.Count - 3] = "Telegram_Last_Chat_update_id/" + Convert.ToString(update_id);
                    lines[lines.Count - 2] = "GridView1_Refresh_Time/" + Convert.ToString(UI_UPDATE.Text);
                    lines[lines.Count - 1] = "Auth/" + Convert.ToString(Authentication);
                    File.WriteAllLines(filePath3, lines);
                }
                else
                {
                    MessageBox.Show("파일 형식 오류 : 새로운 세이브 파일 다운로드 요망");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("파일 저장 중 오류 발생: " + ex.Message);
            }
        }

        public static string UI_Refresh_interval;

        private void UI_UPDATE_TextChanged(object sender, EventArgs e)
        {
            UI_Refresh_interval = UI_UPDATE.Text;
        }

        //------------------------------------------공용기능-------------------------------------------

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
                    WriteLog_System("정상조회\n");
                    break;
                case 200:
                    WriteLog_System("시세과부화\n");
                    break;
                case 201:
                    WriteLog_System("조회전문작성 에러\n");
                    break;
            }
        }

        //-----------------------------------------------Main------------------------------------------------

        public Trade_Auto()
        {
            InitializeComponent();

            //-------------------초기 동작-------------------

            //기존 세팅 로드
            utility.setting_load_auto();

            //메인 시간 동작
            timer1.Start(); //시간 표시 - 1000ms

            //----------종료_동작---------
            this.FormClosed += new FormClosedEventHandler(Form_FormClosed);

            //-------------------버튼-------------------
            Login_btn.Click += login_btn; //로그인
            Main_menu.Click += main_menu; //메인메뉴
            Trade_setting.Click += trade_setting; //설정창
            porfoilo_btn.Click += Porfoilo_btn_Click;//매매정보
            Log_btn.Click += Log_btn_Click;//매매정보
            update_agree_btn.Click += Update_agree_btn_Click;//업데이트 및 동의사항

            Stock_search_btn.Click += stock_search_btn; //종목조회

            Real_time_search_btn.Click += real_time_search_btn; //실시간 조건식 등록
            Real_time_stop_btn.Click += real_time_stop_btn; //조건식 실시간 전체 중단

            All_clear_btn.Click += All_clear_btn_Click;
            profit_clear_btn.Click += Profit_clear_btn_Click;
            loss_clear_btn.Click += Loss_clear_btn_Click;

            Refresh.Click += Refresh_Click;
            Match_btn.Click += Match_Click;
            select_cancel.Click += Select_cancel_Click;
            UI_UPDATE.TextChanged += UI_UPDATE_TextChanged;

            //-------------------로그인 이벤트 동작-------------------
            axKHOpenAPI1.OnEventConnect += onEventConnect; //로그인 상태 확인(ID,NAME,계좌번호,KEYBOARD,FIREWALL,조건식)
            axKHOpenAPI1.OnReceiveConditionVer += onReceiveConditionVer; //조건식 조회

            //----------------데이터 조회 이벤트 동작-------------------
            axKHOpenAPI1.OnReceiveTrData += onReceiveTrData; //TR조회
            axKHOpenAPI1.OnReceiveTrCondition += onReceiveTrCondition; //매도 및 실시간 조건식 종목 정보 받기
            axKHOpenAPI1.OnReceiveRealCondition += onReceiveRealCondition; //실시간 조건식 편출입 종목 받기
            axKHOpenAPI1.OnReceiveRealData += onReceiveRealData; //실시간 조건식 시세 받기
            axKHOpenAPI1.OnReceiveChejanData += onReceiveChejanData; //매매 정보 받기
        }

        //------------------------------------------Main_Timer-------------------------------------------

        private bool isRunned = false;
        private bool isRunned2 = false;
        private bool isRunned3 = false;

        private bool initial_process_complete = false;

        private bool first_index = false;
        private bool second_index = false;

        private DateTime index1 = DateTime.Parse("08:59:00");
        private DateTime index2 = DateTime.Parse("09:00:00");

        //timer1(1000ms) : 주기 고정
        private async void ClockEvent(object sender, EventArgs e)
        {
            //시간표시
            timetimer.Text = DateTime.Now.ToString("yy MM-dd (ddd) HH:mm:ss");

            if (utility.load_check && !isRunned3)
            {
                isRunned3 = true;
                /*
                var response = WindowsFormsApp1.Update.SendAuthCodeAsync("");
                if (response.ToString().StartsWith("ALLOW")) 
                {
                    Authentication_Check = true;
                    WriteLog_System($"인증 : 유효기간({response.ToString().Split(',')[1]})\n");
                    telegram_message($"인증 : 유효기간({response.ToString().Split(',')[1]})\n");
                }
                else
                {
                    WriteLog_System("미인증 : 50만원 제한\n");
                    telegram_message("미인증 : 50만원 제한\n");
                }
                */
                isRunned = false;
            }

            if (!isRunned)
            {
                isRunned = true;
                //테이블 초기 세팅
                await initial_Table();

                //초기 설정 반영
                await initial_allow(false);

                if (utility.Telegram_Allow)
                {
                    Telegram_Receive();
                }

                //로그인
                await Task.Run(() =>
                {
                    axKHOpenAPI1.CommConnect();
                });
            }

            //운영시간 확인
            DateTime t_now = DateTime.Now;
            DateTime t_start = DateTime.Parse(utility.market_start_time);
            DateTime t_end = DateTime.Parse(utility.market_end_time);

            if (initial_process_complete)
            {
                if (!isRunned2 && t_now >= t_start && t_now <= t_end)
                {
                    isRunned2 = true;

                    //실시간 조건 검색 시작
                    auto_allow(false);
                }
                else if (isRunned2 && t_now > t_end)
                {
                    isRunned2 = false;
                    real_time_stop(true);
                }

                //인덱스전송
                //DateTime index1 = DateTime.Parse("08:59:00");
                //DateTime index2 = DateTime.Parse("09:00:00");

                if (!first_index && index1 <= t_now)
                {
                    first_index = true;
                    WriteLog_System($"[INDEX/08:59:00] : {Foreign_Commdity.Text}/{kospi_index.Text}/{kosdaq_index.Text}/{dow_index.Text}/{sp_index.Text}/{nasdaq_index.Text}\n");
                    telegram_message($"[INDEX/08:59:00] : {Foreign_Commdity.Text}/{kospi_index.Text}/{kosdaq_index.Text}/{dow_index.Text}/{sp_index.Text}/{nasdaq_index.Text}\n");
                }

                if (!second_index && index2 <= t_now)
                {
                    second_index = true;
                    WriteLog_System($"[INDEX/09:00:00] : {Foreign_Commdity.Text}/{kospi_index.Text}/{kosdaq_index.Text}/{dow_index.Text}/{sp_index.Text}/{nasdaq_index.Text}\n");
                    telegram_message($"[INDEX/09:00:00] : {Foreign_Commdity.Text}/{kospi_index.Text}/{kosdaq_index.Text}/{dow_index.Text}/{sp_index.Text}/{nasdaq_index.Text}\n");
                }
            }
        }

        //-----------------------------------------initial-------------------------------------

        //초기 Table 값 입력
        private async Task initial_Table()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("선택", typeof(bool));
            dataTable.Columns.Add("편입", typeof(string)); // '편입' '이탈'
            dataTable.Columns.Add("상태", typeof(string)); // '대기' '매수중 '매수완료' '매도중' '매도완료'
            dataTable.Columns.Add("종목코드", typeof(string));
            dataTable.Columns.Add("종목명", typeof(string));
            dataTable.Columns.Add("현재가", typeof(string)); // + - 부호를 통해 매수호가인지 매도 호가인지 현재가인지 파악한다.
            dataTable.Columns.Add("등락율", typeof(string));
            dataTable.Columns.Add("거래량", typeof(string));
            dataTable.Columns.Add("편입상태", typeof(string));
            dataTable.Columns.Add("편입가", typeof(string));
            dataTable.Columns.Add("매도가", typeof(string));
            dataTable.Columns.Add("수익률", typeof(string));
            dataTable.Columns.Add("보유수량", typeof(string)); //보유수량
            dataTable.Columns.Add("조건식", typeof(string));
            dataTable.Columns.Add("편입시각", typeof(string));
            dataTable.Columns.Add("이탈시각", typeof(string));
            dataTable.Columns.Add("매수시각", typeof(string));
            dataTable.Columns.Add("매도시각", typeof(string));
            dataTable.Columns.Add("주문번호", typeof(string));
            dataTable.Columns.Add("상한가", typeof(string)); //상한가 => 시장가 계산용
            dataTable.Columns.Add("편입최고", typeof(string)); //당일최고
            dataTable.Columns.Add("매매진입", typeof(string)); //매매진입시각
            dtCondStock = dataTable;

            dataGridView1.DefaultCellStyle.Font = new Font("굴림", 8F, FontStyle.Regular);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("굴림", 8F, FontStyle.Bold);

            DataTable dataTable2 = new DataTable();
            dataTable2.Columns.Add("종목코드", typeof(string)); //고정
            dataTable2.Columns.Add("종목명", typeof(string)); //고정
            dataTable2.Columns.Add("현재가", typeof(string)); //실시간 변경
            dataTable2.Columns.Add("보유수량", typeof(string)); //고정
            dataTable2.Columns.Add("평균단가", typeof(string)); //고정
            dataTable2.Columns.Add("평가금액", typeof(string));
            dataTable2.Columns.Add("수익률", typeof(string)); //실시간 변경
            dataTable2.Columns.Add("손익금액", typeof(string));
            dataTable2.Columns.Add("매도수량", typeof(string)); //고정
            dtCondStock_hold = dataTable2;

            dataGridView2.DefaultCellStyle.Font = new Font("굴림", 8F, FontStyle.Regular);
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("굴림", 8F, FontStyle.Bold);

            DataTable dataTable3 = new DataTable();
            dataTable3.Columns.Add("종목번호", typeof(string));
            dataTable3.Columns.Add("종목명", typeof(string));
            dataTable3.Columns.Add("주문시간", typeof(string));
            dataTable3.Columns.Add("주문번호", typeof(string));
            dataTable3.Columns.Add("매매구분", typeof(string));
            dataTable3.Columns.Add("주문구분", typeof(string));
            dataTable3.Columns.Add("주문수량", typeof(string));
            dataTable3.Columns.Add("체결수량", typeof(string));
            dataTable3.Columns.Add("체결단가", typeof(string));
            dtCondStock_Transaction = dataTable3;
            dataGridView3.DataSource = dtCondStock_Transaction;

            dataGridView3.DefaultCellStyle.Font = new Font("굴림", 8F, FontStyle.Regular);
            dataGridView3.ColumnHeadersDefaultCellStyle.Font = new Font("굴림", 8F, FontStyle.Bold);

            InitializeDataGridView();
            dataGridView1.CurrentCellDirtyStateChanged += DataGridView1_CurrentCellDirtyStateChanged;
            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;

        }

        private BindingSource bindingSource;
        private BindingSource bindingSource2;

        //데이터 바인딩(속도, 변경, 고급 기능 등)
        private void InitializeDataGridView()
        {
            bindingSource = new BindingSource();
            bindingSource.DataSource = dtCondStock;

            dataGridView1.DataSource = bindingSource;

            // Set the bool column to display as a checkbox
            dataGridView1.Columns["선택"].ReadOnly = false;
            dataGridView1.Columns["선택"].Width = 50;
            dataGridView1.Columns["편입"].Width = 50;
            dataGridView1.Columns["상태"].Width = 50;
            dataGridView1.Columns["거래량"].Width = 80;

            bindingSource2 = new BindingSource();
            bindingSource2.DataSource = dtCondStock_hold;

            dataGridView2.DataSource = bindingSource2;
        }

        //셀 값이 변경될 때마다 이를 즉시 커밋하여 DataTable에 반영
        private void DataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.InvokeRequired)
            {
                dataGridView1.Invoke((MethodInvoker)delegate
                {
                    if (dataGridView1.IsCurrentCellDirty)
                    {
                        dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }
                });
            }
            else
            {
                if (dataGridView1.IsCurrentCellDirty)
                {
                    dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
        }

        //셀 값이 변경된 후 이를 DataTable에 반영
        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            lock (table1)
            {
                if (e.ColumnIndex == dataGridView1.Columns["선택"].Index)
                {
                    bool isChecked = (bool)dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                    dtCondStock.Rows[e.RowIndex]["선택"] = isChecked;
                    //
                    gridView1_refresh();
                }
            }
        }

        private bool ui_timer = false;

        private void gridView1_refresh()
        {
            if (UI_UPDATE.Text.Trim().Equals("실시간"))
            {
                // 현재 스크롤 위치 저장
                int firstDisplayedRowIndex = dataGridView1.FirstDisplayedScrollingRowIndex;

                //
                if (dataGridView1.InvokeRequired)
                {
                    dataGridView1.Invoke((MethodInvoker)delegate
                    {
                        bindingSource.ResetBindings(false);
                        // 스크롤 위치 복원
                        if (firstDisplayedRowIndex >= 0 && firstDisplayedRowIndex < dataGridView1.Rows.Count && firstDisplayedRowIndex != dataGridView1.FirstDisplayedScrollingRowIndex)
                        {
                            dataGridView1.FirstDisplayedScrollingRowIndex = firstDisplayedRowIndex;
                        }
                    });
                }
                else
                {
                    bindingSource.ResetBindings(false);
                    // 스크롤 위치 복원
                    if (firstDisplayedRowIndex >= 0 && firstDisplayedRowIndex < dataGridView1.Rows.Count && firstDisplayedRowIndex != dataGridView1.FirstDisplayedScrollingRowIndex)
                    {
                        dataGridView1.FirstDisplayedScrollingRowIndex = firstDisplayedRowIndex;
                    }
                }

                if (Ui_timer != null)
                {
                    Ui_timer.Stop();
                    Ui_timer.Dispose();
                    Ui_timer = null;
                }
            }
            else if (!UI_UPDATE.Text.Trim().Equals("실시간") && !ui_timer)
            {
                ui_timer = true;
                UI_timer();
            }
        }

        private System.Timers.Timer Ui_timer;

        private void UI_timer()
        {
            Ui_timer = new System.Timers.Timer(Convert.ToInt32(UI_UPDATE.Text.Replace("ms", "")));
            Ui_timer.Elapsed += (sender, e) =>
            {
                // 현재 스크롤 위치 저장
                int firstDisplayedRowIndex = dataGridView1.FirstDisplayedScrollingRowIndex;

                //
                if (dataGridView1.InvokeRequired)
                {
                    dataGridView1.Invoke((MethodInvoker)delegate
                    {
                        bindingSource.ResetBindings(false);
                    });
                }
                else
                {
                    bindingSource.ResetBindings(false);
                }

                // 스크롤 위치 복원
                if (firstDisplayedRowIndex >= 0 && firstDisplayedRowIndex < dataGridView1.Rows.Count && firstDisplayedRowIndex != dataGridView1.FirstDisplayedScrollingRowIndex)
                {
                    dataGridView1.FirstDisplayedScrollingRowIndex = firstDisplayedRowIndex;
                }
            };
            Ui_timer.AutoReset = false;
            Ui_timer.Start();
        }

        //초기 설정 변수
        private string sell_condtion_method_after;

        //초기 설정 반영
        public async Task initial_allow(bool check)
        {
            string[] mode = { "지정가", "시장가" };
            string[] hoo = { "5호가", "4호가", "3호가", "2호가", "1호가", "현재가", "시장가", "-1호가", "-2호가", "-3호가", "-4호가", "-5호가" };
            string[] hoo2 = { "5호가", "4호가", "3호가", "2호가", "1호가", "현재가", "시장가", "-1호가", "-2호가", "-3호가", "-4호가", "-5호가" };
            string[] ui_range = { "실시간", "100ms", "300ms", "500ms", "700ms", "1000ms" };
            //
            UI_UPDATE.Items.AddRange(ui_range);
            UI_UPDATE.SelectedItem = utility.GridView1_Refresh_Time;
            UI_Refresh_interval = utility.GridView1_Refresh_Time;

            //초기 세팅
            acc_text.Text = utility.setting_account_number;
            total_money.Text = string.Format("{0:#,##0}", Convert.ToDecimal(utility.initial_balance));
            Current_User_money.Text = "0";
            if (utility.buy_INDEPENDENT)
            {
                maxbuy_acc.Text = string.Concat(Enumerable.Repeat("0/", utility.Fomula_list_buy_text.Split(',').Length)) + utility.maxbuy_acc;
            }
            else
            {
                maxbuy_acc.Text = "0/" + utility.maxbuy_acc;
            }
            User_id.Text = "-";
            operation_start.Text = utility.market_start_time;
            operation_stop.Text = utility.market_end_time;
            search_start.Text = utility.buy_condition_start;
            search_stop.Text = utility.buy_condition_end;
            clear_sell.Text = Convert.ToString(utility.clear_sell);
            clear_sell_time.Text = utility.clear_sell_start;
            profit.Text = utility.profit_percent_text;
            loss.Text = utility.loss_percent_text;
            buy_condition.Text = utility.Fomula_list_buy_text;
            buy_condtion_method.Text = mode[utility.buy_set1] + "/" + hoo[utility.buy_set2];
            sell_condtion.Text = utility.Fomula_list_sell_text;
            sell_condtion_method.Text = mode[utility.sell_set1] + "/" + hoo[utility.sell_set2];
            sell_condtion_method_after = mode[utility.sell_set1_after] + "/" + hoo2[utility.sell_set2_after];

            //초기세팅2
            all_profit.Text = "0";
            all_profit_percent.Text = "00.00%";
            if (!check)
            {
                User_money.Text = "0";
            }
            today_profit_percent_tax.Text = "00.00%";
            today_profit_tax.Text = "0";
            today_profit_percent.Text = "00.00%";
            today_profit.Text = "0";

            Foreign_Commdity.Text = "미수신";
            kospi_index.Text = "미수신";
            kosdaq_index.Text = "미수신";
            dow_index.Text = "미수신";
            sp_index.Text = "미수신";
            nasdaq_index.Text = "미수신";

            //초기세팅3
            if (utility.buy_OR)
            {
                trading_mode.Text = "OR_모드";
            }
            else if (utility.buy_AND)
            {
                trading_mode.Text = "AND_모드";
            }
            else
            {
                trading_mode.Text = "독립_모드";
            }


            //KIS
            KIS_RUN.Text = Convert.ToString(utility.KIS_Allow); //사용여부
            KIS_Independent.Text = Convert.ToString(utility.KIS_Independent);
            KIS_Account_Number.Text = utility.KIS_Account;
            KIS_N.Text = utility.KIS_amount; //N등분
            KIS_ACCOUNT.Text = "0";//예수금
            KIS_Profit.Text = "0";

            //
            update_id = utility.Telegram_last_chat_update_id;

            //
            if (Authentication_Check)
            {
                Authentic.Text = "인증";
            }
            else
            {
                Authentic.Text = "미인증";
            }

            //
            WriteLog_System("세팅 반영 완료\n");
            telegram_message("세팅 반영 완료\n");
        }

        //------------------------------------Login---------------------------------

        public int login_check = 1;

        private void onEventConnect(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent e)
        {
            login_check = e.nErrCode;
            //
            if (login_check == 0)
            {
                // 정상 처리
                WriteLog_System("로그인 성공\n");
                telegram_message("로그인 성공\n");
                initial_process(false);
            }
            else
            {
                switch (login_check)
                {
                    case 100:
                        WriteLog_System("사용자 정보교환 실패\n");
                        telegram_message("사용자 정보교환 실패\n");
                        break;
                    case 101:
                        WriteLog_System("서버접속 실패\n");
                        telegram_message("서버접속 실패\n");
                        break;
                    case 102:
                        WriteLog_System("버전처리 실패\n");
                        telegram_message("버전처리 실패\n");
                        break;
                }
            }
        }

        //------------------------------------Login이후 동작---------------------------------

        //고정 예수금 업데이트
        private bool user_money_before = true;

        public async Task initial_process(bool check)
        {
            if (check)
            {
                lock (table1) { 
                    dtCondStock.Clear();
                    gridView1_refresh();
                }
                //
                lock (table2)
                {
                    dtCondStock_hold.Clear();
                }
                //
                lock (table3)
                {
                    dtCondStock_Transaction.Clear();
                }
            }

            //한번만 실행시켜주면 됨
            if (!check)
            {
                timer3.Start(); //체결 내역 업데이트 - 200ms
            }

            //접속서버 구분(1 : 모의투자, 나머지: 실거래서버)
            string 접속서버구분 = axKHOpenAPI1.GetLoginInfo("GetServerGubun");
            if (접속서버구분.Equals("1"))
            {
                User_connection.Text = "모의\n";
                WriteLog_System("모의투자 연결\n");
                telegram_message("모의투자 연결\n");
            }
            else
            {
                User_connection.Text = "실전\n";
                WriteLog_System("실전투자 연결\n");
                telegram_message("실전투자 연결\n");
            }

            System.Threading.Thread.Sleep(delay1);

            //사용자 id를 UserId 라벨에 추가
            string 사용자id = axKHOpenAPI1.GetLoginInfo("USER_ID");
            User_id.Text = 사용자id;

            System.Threading.Thread.Sleep(delay1);

            //사용자 이름을 UserName 라벨에 추가
            string 사용자이름 = axKHOpenAPI1.GetLoginInfo("USER_NAME");
            User_name.Text = 사용자이름;

            System.Threading.Thread.Sleep(delay1);

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

            System.Threading.Thread.Sleep(delay1);

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

            System.Threading.Thread.Sleep(delay1);

            //"ACCOUNT_CNT" : 보유계좌 갯수
            //"ACCLIST" 또는 "ACCNO" : 구분자 ';', 보유계좌 목록                
            string 계좌목록 = axKHOpenAPI1.GetLoginInfo("ACCLIST").Trim();
            account = 계좌목록.Split(';');
            if (!account.Contains(utility.setting_account_number))
            {
                WriteLog_System("계좌번호 재설정 요청 및 초기화 설정\n");
                acc_text.Text = account[0];
            }

            System.Threading.Thread.Sleep(delay1);

            //예수금 + 계좌 보유 현황 
            Account_before("초기");

            System.Threading.Thread.Sleep(delay1);

            //당일 손익 받기
            today_profit_tax_load("");

            System.Threading.Thread.Sleep(delay1);

            //매매내역 업데이트
            Transaction_Detail("", "");

            System.Threading.Thread.Sleep(delay1);

            //지수
            Index_load();

            System.Threading.Thread.Sleep(delay1);

            //조건식 검색(계좌불필요) => 계좌 보유 현황 확인 => 당일 손익 받기 => 초기 보유 종목 테이블 업데이트 => 실시간 조건 검색 시작
            if (axKHOpenAPI1.GetConditionLoad() == 1)
            {
                WriteLog_System("조건식 검색 성공\n");
            }
            else
            {
                WriteLog_System("조건식 검색 실패\n");
            }

            /*
            if (utility.TradingView_Webhook)
            {
                _ = Task.Run(() => TradingVIew_Listener_Start());
            }
            */
        }

        //------------------------------------Login이후 동작 함수 목록--------------------------------- 

        //계좌 보유 현황 확인 => 매매내역 업데이트 => 초기 보유 종목 테이블 업데이트 => 실시간 조건 검색 시작
        private void Account_before(string code)
        {
            axKHOpenAPI1.SetInputValue("계좌번호", acc_text.Text);
            axKHOpenAPI1.SetInputValue("상장폐지조회구분", "0");
            axKHOpenAPI1.SetInputValue("비밀번호입력매체구분", "00");
            axKHOpenAPI1.CommRqData("계좌평가현황요청/" + code, "OPW00004", 0, GetScreenNo());
        }

        //초기 보유 종목 테이블 업데이트
        private void Hold_Update()
        {
            if (dtCondStock_hold.Rows.Count == 0)
            {
                WriteLog_Stock("기존 보유 종목 없음\n");
                telegram_message("기존 보유 종목 없음.\n");
                if (utility.max_hold)
                {
                    //최대 보유 종목 에 대한 계산
                    max_hoid.Text = "0/" + utility.max_hold_text;
                }
                else
                {
                    max_hoid.Text = "0/10";
                }
                return;
            }

            //
            WriteLog_Stock("기존 보유 종목 있음\n");
            telegram_message("기존 보유 종목 있음\n");

            foreach (DataRow row in dtCondStock_hold.Rows)
            {
                string Code = row["종목코드"].ToString();
                WriteLog_System($"[기존보유/{Code}] : 호출\n");
                if (dtCondStock.Rows.Count > 20)
                {
                    WriteLog_Stock($"[신규편입불가/{Code}/전일] : 최대 감시 종목(20개) 초과 \n");
                    break;
                }
                //
                // 각 항목 처리
                axKHOpenAPI1.SetInputValue("종목코드", Code);
                axKHOpenAPI1.CommRqData("기존보유/" + row["보유수량"].ToString(), "OPT10001", 0, GetScreenNo());
                //
                System.Threading.Thread.Sleep(delay1);
            }

            //
            if (utility.max_hold)
            {
                //최대 보유 종목 에 대한 계산
                max_hoid.Text = dtCondStock_hold.Rows.Count + "/" + utility.max_hold_text;
            }
            else
            {
                max_hoid.Text = dtCondStock_hold.Rows.Count + "/10";
            }
            return;
        }

        //당일 손익 + 당일 손일률 + 당일 수수료
        private void today_profit_tax_load(string load_type)
        {
            axKHOpenAPI1.SetInputValue("계좌번호", acc_text.Text);
            axKHOpenAPI1.SetInputValue("기준일자", "");
            axKHOpenAPI1.SetInputValue("단주구분", "2");
            axKHOpenAPI1.SetInputValue("현금신용구분", "0");
            int result = axKHOpenAPI1.CommRqData("당일매매일지요청/" + load_type, "OPT10170", 0, GetScreenNo());
        }

        //체결내역업데이트(주문번호)
        private void Transaction_Detail(string order_number, string cancel_type)
        {
            axKHOpenAPI1.SetInputValue("주문일자", DateTime.Now.ToString("yyyyMMdd"));
            axKHOpenAPI1.SetInputValue("계좌번호", acc_text.Text);
            axKHOpenAPI1.SetInputValue("비밀번호입력매체구분", "00");
            axKHOpenAPI1.SetInputValue("조회구분", "2");
            axKHOpenAPI1.SetInputValue("주식채권구분", "0");
            axKHOpenAPI1.SetInputValue("매도수구분", "0");
            axKHOpenAPI1.SetInputValue("종목코드", "");//종목코드
            axKHOpenAPI1.SetInputValue("시작주문번호", "");//시작주문번호
            int result = axKHOpenAPI1.CommRqData("계좌별주문체결내역상세요청/" + order_number + "/" + cancel_type, "OPW00007", 0, GetScreenNo());
        }

        //------------------------------------인덱스 목록 받기--------------------------------- 

        //지수업데이트
        private async void Index_load()
        {
            US_INDEX();

            System.Threading.Thread.Sleep(delay1);

            if (utility.kospi_commodity || utility.kosdak_commodity)
            {
                Initial_kor_index();
            }

            //외국인 선물 누적
            if (utility.Foreign)
            {
                await Task.Run(() => KOR_FOREIGN_COMMUNICATION());
            }
        }

        private bool index_buy = false;
        private bool index_clear = false;

        private bool index_run = false;

        private bool index_stop = false;
        private bool index_skip = false;

        private async Task US_INDEX()
        {
            string dowUrl = "https://query1.finance.yahoo.com/v8/finance/chart/^DJI"; //.DJI
            string sp500Url = "https://query1.finance.yahoo.com/v8/finance/chart/^GSPC"; //SPX
            string nasdaqUrl = "https://query1.finance.yahoo.com/v8/finance/chart/^IXIC"; //COMP

            //다우존스
            if (utility.dow_index)
            {
                double tmp5 = await GetStockIndex(dowUrl, "DOW");

                if (tmp5 == -999)
                {
                    WriteLog_System($"[수신오류] DOW30 : 인터넷 접속 및 웹사이트 접속 차단 확인\n");
                    telegram_message($"[수신오류] DOW30 : 인터넷 접속 및 웹사이트 접속 차단 확인\n");
                }
                else
                {
                    dow_index.Text = tmp5.ToString();
                    //
                    if (index_skip) WriteLog_System("[DOW30/SKIP] : 미국 전영업일 휴무\n");

                    if (!index_skip && utility.buy_condition_index)
                    {
                        if (utility.type3_selection)
                        {
                            double start = Convert.ToDouble(utility.type3_start);
                            double end = Convert.ToDouble(utility.type3_end);
                            //
                            if (tmp5 < start || end < tmp5)
                            {
                                lock (index_write)
                                {
                                    index_buy = true;
                                }

                                WriteLog_System($"[BUY/이탈] DOW30 RANGE\n");
                                WriteLog_System($"START({start}) <=  NOW({tmp5}) <= END({end})\n");
                                WriteLog_System("Trade Stop\n");

                                telegram_message($"[BUY/이탈] DOW30 RANGE\n");
                                telegram_message($"START({start}) <=  NOW({tmp5}) <= END({end})\n");
                                telegram_message("Trade Stop\n");
                            }
                        }
                    }

                    if (!index_skip && utility.clear_index)
                    {
                        if (utility.type3_selection_all)
                        {
                            double start = Convert.ToDouble(utility.type3_start_all);
                            double end = Convert.ToDouble(utility.type3_end_all);

                            if (tmp5 < start || end < tmp5)
                            {
                                lock (index_write)
                                {
                                    index_clear = true;
                                }

                                WriteLog_System($"[CLEAR/이탈] DOW30 INDEX RANGE\n");
                                WriteLog_System($"START({start}) <=  NOW({tmp5}) <= END({end})\n");
                                WriteLog_System("Trade Stop\n");

                                telegram_message($"[CLEAR/이탈] DOW30 INDEX RANGE\n");
                                telegram_message($"START({start}) <=  NOW({tmp5}) <= END({end})\n");
                                telegram_message("Trade Stop\n");
                            }
                        }
                    }
                }
            }

            //
            await Task.Delay(2245);

            //S&P500
            if (utility.sp_index)
            {
                double tmp5 = await GetStockIndex(sp500Url, "S&P");

                if (tmp5 == -999)
                {
                    WriteLog_System($"[수신오류] S&P500  : 인터넷 접속 및 웹사이트 접속 차단 확인\n");
                    telegram_message($"[수신오류] S&P500 : 인터넷 접속 및 웹사이트 접속 차단 확인\n");
                    return;
                }
                else
                {
                    sp_index.Text = tmp5.ToString();
                    //
                    if (index_skip) WriteLog_System("[S&P500/SKIP] : 미국 전영업일 휴무\n");

                    if (!index_skip && utility.buy_condition_index)
                    {
                        if (utility.type4_selection)
                        {
                            double start = Convert.ToDouble(utility.type4_start);
                            double end = Convert.ToDouble(utility.type4_end);
                            if (tmp5 < start || end < tmp5)
                            {
                                lock (index_write)
                                {
                                    index_buy = true;
                                }

                                WriteLog_System($"[BUY/이탈] S&P500 RANGE\n");
                                WriteLog_System($"START({start}) <=  NOW({tmp5}) <= END({end})\n");
                                WriteLog_System("Trade Stop\n");

                                telegram_message($"[BUY/이탈] S&P500 RANGE\n");
                                telegram_message($"START({start}) <=  NOW({tmp5}) <= END({end})\n");
                                telegram_message("Trade Stop\n");
                            }
                        }
                    }

                    if (!index_skip && utility.clear_index)
                    {
                        if (utility.type4_selection_all)
                        {
                            double start = Convert.ToDouble(utility.type4_start_all);
                            double end = Convert.ToDouble(utility.type4_end_all);
                            if (tmp5 < start || end < tmp5)
                            {
                                lock (index_write)
                                {
                                    index_clear = true;

                                }

                                WriteLog_System($"[CLEAR/이탈] S&P500 RANGE\n");
                                WriteLog_System($"START({start}) <=  NOW({tmp5}) <= END({end})\n");
                                WriteLog_System("Trade Stop\n");

                                telegram_message($"[CLEAR/이탈] S&P500 RANGE\n");
                                telegram_message($"START({start}) <=  NOW({tmp5}) <= END({end})\n");
                                telegram_message("Trade Stop\n");
                            }
                        }
                    }

                }
            }

            //
            await Task.Delay(2174);

            //NASDAQ100
            if (utility.nasdaq_index)
            {
                double tmp5 = await GetStockIndex(nasdaqUrl, "NASDAQ");

                if (tmp5 == -999)
                {
                    WriteLog_System($"[수신오류] NASDAQ100  : 인터넷 접속 및 웹사이트 접속 차단 확인\n");
                    telegram_message($"[수신오류] NASDAQ100: 인터넷 접속 및 웹사이트 접속 차단 확인\n");
                    return;
                }
                else
                {
                    nasdaq_index.Text = tmp5.ToString();
                    //
                    if (index_skip) WriteLog_System("[NASDAQ100/SKIP] : 미국 전영업일 휴무\n");

                    if (!index_skip && utility.buy_condition_index)
                    {
                        if (utility.type5_selection)
                        {
                            double start = Convert.ToDouble(utility.type5_start);
                            double end = Convert.ToDouble(utility.type5_end);
                            if (tmp5 < start || end < tmp5)
                            {
                                lock (index_write)
                                {
                                    index_buy = true;
                                }

                                WriteLog_System($"[BUY/이탈] NASDAQ RANGE\n");
                                WriteLog_System($"START({start}) <=  NOW({tmp5}) <= END({end})\n");
                                WriteLog_System("Trade Stop\n");

                                telegram_message($"[BUY/이탈] NASDAQ RANGE\n");
                                telegram_message($"START({start}) <=  NOW({tmp5}) <= END({end})\n");
                                telegram_message("Trade Stop\n");
                            }
                        }
                    }

                    if (!index_skip && utility.clear_index)
                    {
                        if (utility.type5_selection_all)
                        {
                            double start = Convert.ToDouble(utility.type5_start_all);
                            double end = Convert.ToDouble(utility.type5_end_all);
                            if (tmp5 < start || end < tmp5)
                            {
                                lock (index_write)
                                {
                                    index_clear = true;
                                }

                                WriteLog_System($"[CLEAR/이탈] NASDAQ INDEX RANGE\n");
                                WriteLog_System($"START({start}) <=  NOW({tmp5}) <= END({end})\n");
                                WriteLog_System("Trade Stop\n");

                                telegram_message($"[CLEAR/이탈] NASDAQ INDEX RANGE\n");
                                telegram_message($"START({start}) <=  NOW({tmp5}) <= END({end})\n");
                                telegram_message("Trade Stop\n");
                            }
                        }
                    }
                }
            }
        }

        private int delayMilliseconds = 60102; //1분
        private int Max_Retry = 4;
        private string[] userAgents = new string[]
        {
         "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3",
         "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:76.0) Gecko/20100101 Firefox/76.0",
         "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_2) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0.4 Safari/605.1.15",
         "Mozilla/5.0 (Linux; Android 10; SM-G975F) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.106 Mobile Safari/537.36"
        };

        //HTTPS PARSING
        private async Task<double> GetStockIndex(string url, string symbol)
        {
            using (HttpClient client = new HttpClient())
            {
                for (int i = 0; i < Max_Retry; i++)
                {
                    try
                    {
                        client.DefaultRequestHeaders.Clear(); // 이전 헤더를 제거
                        client.DefaultRequestHeaders.Add("User-Agent", userAgents[i]);

                        HttpResponseMessage response = await client.GetAsync(url);

                        if (response.IsSuccessStatusCode)
                        {
                            string responseData = await response.Content.ReadAsStringAsync();
                            JObject jsonData = JObject.Parse(responseData);

                            // Navigate the JSON structure to get the closing price
                            double closePrice = Convert.ToDouble(jsonData["chart"]["result"][0]["meta"]["regularMarketPrice"]);
                            double chartPreviousClose = Convert.ToDouble(jsonData["chart"]["result"][0]["meta"]["chartPreviousClose"]);
                            long utc_time = Convert.ToInt64(jsonData["chart"]["result"][0]["meta"]["regularMarketTime"]);
                            int offset = Convert.ToInt32(jsonData["chart"]["result"][0]["meta"]["gmtoffset"]);

                            if (!index_run) index_stop_skip(utc_time, offset);

                            return Math.Round((closePrice - chartPreviousClose) / chartPreviousClose * 100, 2);
                        }
                        else if ((int)response.StatusCode == 429)
                        {
                            if (response.Headers.Contains("Retry-After"))
                            {
                                string retryAfter = response.Headers.GetValues("Retry-After").FirstOrDefault();
                                if (int.TryParse(retryAfter, out int retryAfterSeconds))
                                {
                                    delayMilliseconds = retryAfterSeconds * 1000;
                                    WriteLog_System($"과다요청(retry) : {delayMilliseconds / 1000}초 지연\n");
                                }
                                else
                                {
                                    delayMilliseconds += 30000;
                                    WriteLog_System($"과다요청 : {delayMilliseconds / 1000}초 지연\n");
                                }
                            }
                            else
                            {
                                delayMilliseconds += 30000;
                                WriteLog_System($"과다요청 : {delayMilliseconds / 1000}초 지연\n");
                            }

                            await Task.Delay(delayMilliseconds); // 지연 후 재시도
                        }
                        else
                        {
                            WriteLog_System($"Error fetching data for {symbol}: {response.StatusCode}\n");
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        WriteLog_System($"Error fetching data for {symbol}: {ex.Message}\n");
                        break;
                    }
                }
                return -999;
            }
        }

        //전일 휴무 확인(UTC)
        private void index_stop_skip(long Unixdate, int GMToffset)
        {
            if (utility.Foreign_Stop || utility.Foreign_Skip)
            {
                if (!Thread.CurrentThread.CurrentCulture.Name.Equals("ko-KR"))
                {
                    WriteLog_System("시스템 언어 한국어 변경 요망\n");
                    return;
                }

                //
                index_run = true;

                //UTC 시각 변환
                DateTime givenData_edited = DateTimeOffset.FromUnixTimeSeconds(Unixdate + GMToffset).UtcDateTime;

                //날짜 추출
                string today_week = DateTime.Now.ToString("ddd");

                //현재날짜(시간 부분 제외)
                DateTime currentDate = DateTime.Now.Date;

                //표기
                WriteLog_System($"EDT시각{givenData_edited.ToString()} / KOR시각{currentDate.ToString()}\n");

                //날짜 차이 계산
                TimeSpan difference = currentDate.Date - givenData_edited.Date;

                //월요일
                if (today_week.Equals("월") && Math.Abs(difference.Days) > 3)
                {
                    if (utility.Foreign_Stop) index_stop = true;
                    if (utility.Foreign_Skip) index_skip = true;
                    WriteLog_System("[미국장 전영업일 휴무] : 매수 중단(조건식 탐색은 실행)\n");
                    telegram_message("[미국장 전영업일 휴무] : 매수 중단(조건식 탐색은 실행)\n");
                }
                else if (!today_week.Equals("월") && Math.Abs(difference.Days) > 1)
                {
                    if (utility.Foreign_Stop) index_stop = true;
                    if (utility.Foreign_Skip) index_skip = true;
                    WriteLog_System("[미국장 전영업일 휴무] : 매수 중단(조건식 탐색은 실행)\n");
                    telegram_message("[미국장 전영업일 휴무] : 매수 중단(조건식 탐색은 실행)\n");
                }
            }
        }

        private string index_time = DateTime.Now.ToString("yyyyMMdd");
        private int[] items = { 0, 1, 4, 5 }; //날짜,시간,저가,종가
        private List<string> sCode1 = new List<string>();
        private List<string> sKCode1 = new List<string>();

        private void Initial_kor_index()
        {
            //지수선물 종목코드 리스트를 ';'로 구분해서 전달
            string[] tmp = axKHOpenAPI1.GetFutureList().Split(';');

            foreach (string c in tmp)
            {
                if (c.StartsWith("101V"))
                {
                    sCode1.Add(c);
                    WriteLog_System("코스피선물월물 : " + c + "\n");
                    continue;
                }
                if (c.StartsWith("106V"))
                {
                    sKCode1.Add(c);
                    WriteLog_System("코스닥선물월물 : " + c + "\n");
                    continue;
                }
            }
            //101V
            //106V

            System.Threading.Thread.Sleep(delay1);

            Index_timer();
        }

        private System.Timers.Timer minuteTimer;

        private void Index_timer()
        {
            // 현재 시간을 기준으로 다음 분의 첫 번째 초까지의 시간을 계산
            DateTime now = DateTime.Now;

            // 다음 분의 00초를 계산
            DateTime nextMinute = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0).AddMinutes(1);
            double intervalToNextMinute = (nextMinute - now).TotalMilliseconds;

            // 첫 번째 타이머를 설정하여 다음 분 00초에 실행
            minuteTimer = new System.Timers.Timer(intervalToNextMinute);
            minuteTimer.Elapsed += (sender, e) =>
            {
                // 타이머를 중지하고 해제
                minuteTimer.Stop();
                minuteTimer.Dispose();

                // 매 1분마다 실행되는 타이머 설정
                StartMinuteTimer();

                // 특정 함수 호출
                KOR_INDEX();
            };
            minuteTimer.AutoReset = false;
            minuteTimer.Start();

            // 특정 함수 호출
            KOR_INDEX();
        }

        private void StartMinuteTimer()
        {
            minuteTimer = new System.Timers.Timer(60000); // 1분 = 60,000 밀리초
            minuteTimer.Elapsed += OnTimedEvent;
            minuteTimer.AutoReset = true;
            minuteTimer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            KOR_INDEX();
        }

        private double[] kospi_index_series = new double[3];
        private double[] kosdaq_index_series = new double[3];

        private void KOR_INDEX()
        {
            //KOSPI 200 FUTURES
            if (utility.kospi_commodity)
            {
                axKHOpenAPI1.SetInputValue("종목코드", sCode1.First());
                axKHOpenAPI1.CommRqData("KOSPI200_INDEX", "opt50001", 0, GetScreenNo());
            }

            System.Threading.Thread.Sleep(delay1);

            //KOSDAK 150 FUTURES
            if (utility.kosdak_commodity)
            {
                axKHOpenAPI1.SetInputValue("종목코드", sKCode1.First());
                axKHOpenAPI1.CommRqData("KOSDAK150_INDEX", "opt50001", 0, GetScreenNo());
            }
        }

        //크레온 프로그램과 연동하여 값 수신하도록 구성
        private async Task KOR_FOREIGN_COMMUNICATION()
        {
            using (var client = new NamedPipeClientStream(".", "testpipe", PipeDirection.InOut, PipeOptions.Asynchronous))
            {
                try
                {
                    while (true)
                    {
                        WriteLog_System("[Foreign Commodity Receiving] : waiting for connection...\n");

                        await client.ConnectAsync();

                        WriteLog_System("[Foreign Commodity Receiving] : connected to server\n");

                        using (var reader = new StreamReader(client))
                        {
                            // 서버로부터 주기적으로 전송되는 메시지 읽기
                            while (client.IsConnected)
                            {
                                try
                                {
                                    Task<string> messageTask = reader.ReadLineAsync();
                                    if (await Task.WhenAny(messageTask, Task.Delay(TimeSpan.FromSeconds(30))) == messageTask)
                                    {
                                        string message = messageTask.Result;

                                        if (message != null)
                                        {
                                            // UI 스레드 처리
                                            if (Foreign_Commdity.InvokeRequired)
                                            {
                                                Foreign_Commdity.Invoke(new Action(() => Foreign_Commdity.Text = message));
                                            }
                                            else
                                            {
                                                Foreign_Commdity.Text = message;
                                            }

                                            double current = Convert.ToDouble(message);

                                            if (utility.buy_condition_index)
                                            {
                                                if (utility.type0_selection && !index_buy)
                                                {
                                                    double start = Convert.ToDouble(utility.type0_start);
                                                    double end = Convert.ToDouble(utility.type0_end);
                                                    if (current < start || end < current)
                                                    {
                                                        lock (index_write)
                                                        {
                                                            index_buy = true;
                                                        }

                                                        WriteLog_System($"[BUY/이탈] FOREIGN RANGE : START({start}) <=  NOW({current}) <= END({end})\n");
                                                        WriteLog_System("Trade Stop\n");
                                                        telegram_message($"[BUY/이탈] FOREIGN RANGE : START({start}) <=  NOW({current}) <= END({end})\n");
                                                        telegram_message("Trade Stop\n");
                                                    }
                                                }
                                            }

                                            if (utility.clear_index)
                                            {
                                                if (utility.type0_selection_all && !index_clear)
                                                {
                                                    double start = Convert.ToDouble(utility.type0_start_all);
                                                    double end = Convert.ToDouble(utility.type0_end_all);
                                                    if (current < start || end < current)
                                                    {
                                                        lock (index_write)
                                                        {
                                                            index_clear = true;
                                                        }

                                                        WriteLog_System($"[CLEAR/이탈] FOREIGN RANGE : START({start}) <=  NOW({current}) <= END({end})\n");
                                                        WriteLog_System("Trade Stop\n");
                                                        telegram_message($"[CLEAR/이탈] FOREIGN RANGE : START({start}) <=  NOW({current}) <= END({end})\n");
                                                        telegram_message("Trade Stop\n");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        WriteLog_System("[Foreign Commodity Receiving] : Disconnected From Client\n");
                                        await KOR_FOREIGN_COMMUNICATION(); // 파이프가 끊어지면 다시 연결 시도
                                        return;
                                    }

                                    await Task.Delay(10000); // 10초 대기
                                }
                                catch (IOException ex)
                                {
                                    WriteLog_System($"[Foreign Commodity Receiving] : Error - {ex.Message}\n");
                                    await KOR_FOREIGN_COMMUNICATION(); // 파이프가 끊어지면 다시 연결 시도
                                    return;
                                }
                                catch (Exception ex)
                                {
                                    WriteLog_System($"[Foreign Commodity Receiving] : Error - {ex.Message}\n");
                                    return;
                                }
                            }
                        }
                    }
                }
                catch (UnauthorizedAccessException ex)
                {
                    WriteLog_System($"[Foreign Commodity Receiving] : Unauthorized Access - {ex.Message}\n");
                    await Task.Delay(5000); // 재시도 전에 5초 대기
                    await KOR_FOREIGN_COMMUNICATION(); // 예외 발생 시 다시 연결 시도
                }
                catch (Exception ex)
                {
                    WriteLog_System($"[Foreign Commodity Receiving] : Error - {ex.Message}\n");
                    await Task.Delay(5000); // 재시도 전에 5초 대기
                    await KOR_FOREIGN_COMMUNICATION(); // 예외 발생 시 다시 연결 시도
                }
            }
        }

        //------------------------------------조건식 수신---------------------------------

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
            if (e.lRet != 1)
            {
                WriteLog_System("조건식 로드 실패\n");
                return;
            }

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

            WriteLog_System("조건식 조회 성공\n");

            initial_process_complete = true;
        }

        //------------------------------실시간 실행 초기 점검-------------------------------------

        //초기 매매 설정
        public void auto_allow(bool skip)
        {
            if (skip)
            {
                WriteLog_System("수동 실행 : 인덱스 중단, 외국 영업일 중단 무시\n");
                telegram_message("수동 실행 : 인덱스 중단, 외국 영업일 중단 무시\n");

                index_stop = false;
                //
                lock (index_write)
                {
                    index_buy = false;
                    index_clear = false;
                }
            }

            //계좌 없으면 이탈
            if (!account.Contains(utility.setting_account_number))
            {
                WriteLog_System("계좌번호 재설정 요청\n");
                telegram_message("계좌번호 재설정 요청\n");
                WriteLog_System("자동 매매 정지\n");
                telegram_message("자동 매매 정지\n");
                return;
            }

            int condition_length = utility.Fomula_list_buy_text.Split(',').Length;

            //조건식 없으면 이탈
            if (utility.buy_condition)
            {
                if (condition_length == 0)
                {
                    WriteLog_System("설정된 매수 조건식 없음\n");
                    telegram_message("설정된 매수 조건식 없음\n");
                    WriteLog_System("자동 매매 정지\n");
                    telegram_message("자동 매매 정지\n");
                    return;
                }

                //AND 모드에서는 조건식이 2개
                if (utility.buy_AND && condition_length != 2)
                {
                    WriteLog_System("AND 모드 조건식 2개 필요\n");
                    telegram_message("AND 모드 조건식 2개 필요\n");
                    WriteLog_System("자동 매매 정지\n");
                    telegram_message("자동 매매 정지\n");
                    return;
                }

                //Independent 모드에서는 조건식이 2개
                if (utility.buy_INDEPENDENT && condition_length != 2)
                {
                    WriteLog_System("Independent 모드 조건식 2개 필요\n");
                    telegram_message("IndependentL 모드 조건식 2개 필요\n");
                    WriteLog_System("자동 매매 정지\n");
                    telegram_message("자동 매매 정지\n");
                    return;
                }
            }

            int condition_length2 = utility.Fomula_list_sell_text == "9999" ? 0 : 1;

            //조건식 없으면 이탈
            if (utility.sell_condition && condition_length2 == 0)
            {
                WriteLog_System("설정된 매도 조건식 없음\n");
                telegram_message("설정된 매도 조건식 없음\n");
                WriteLog_System("자동 매매 정지\n");
                telegram_message("자동 매매 정지\n");
                return;
            }

            //자동 설정 여부
            if (!utility.auto_trade_allow && !skip)
            {
                WriteLog_System("자동 매매 실행 미설정\n");
                telegram_message("자동 매매 실행 미설정\n");
                return;
            }

            if (!index_stop)
            {
                timer2.Start();
            }

            //자동 매수 조건식 설정 여부
            if (utility.buy_condition)
            {
                if (!index_stop)
                {
                    timer2.Start();
                }

                real_time_search(null, EventArgs.Empty);

                WriteLog_System("실시간 조건식 매수 시작\n");
                telegram_message("실시간 조건식 매수 시작\n");
            }
            else
            {
                WriteLog_System("자동 조건식 매수 미설정\n");
                telegram_message("자동 조건식 매수 미설정\n");
            }

            System.Threading.Thread.Sleep(250);

            //자동 매도 조건식 설정 여부
            if (utility.sell_condition)
            {
                if (!index_stop)
                {
                    timer2.Start();
                }

                real_time_search_sell(null, EventArgs.Empty);

                WriteLog_System("실시간 조건식 매도 시작\n");
                telegram_message("실시간 조건식 매도 시작\n");
            }
            else
            {
                WriteLog_System("자동 조건식 매도 미설정\n");
                telegram_message("자동 조건식 매도 미설정\n");
            }
        }

        //매도 전용 조건식 검색
        private void real_time_search_sell(object sender, EventArgs e)
        {
            //실시간 검색이 시작되면 '일반 검색'이 불가능해 진다.
            Real_time_stop_btn.Enabled = true;
            Real_time_search_btn.Enabled = false;

            //조건식이 로딩되었는지
            if (string.IsNullOrEmpty(utility.Fomula_list_buy_text))
            {
                WriteLog_System("매도 조건식 선택 요청\n");
                telegram_message("매도 조건식 선택 요청\n");
                WriteLog_System("실시간 매매 중단\n");
                telegram_message("실시간 매매 중단\n");
                Real_time_stop_btn.Enabled = false;
                Real_time_search_btn.Enabled = true;
                return;
            }

            //검색된 조건식이 있을시
            string[] condition = utility.Fomula_list_sell_text.Split('^');
            var condInfo = conditionInfo.Find(f => f.Index == Convert.ToInt32(condition[0]) && f.Name.Equals(condition[1]));

            //로드된 조건식 목록에 설정된 조건식이 존재하지 않는 경우 이탈
            if (condInfo == null)
            {
                WriteLog_System("[실시간매도조건식/미존재/" + utility.Fomula_list_sell_text + "] : HTS 조건식 리스트 미포함\n");
                telegram_message("[실시간매도조건식/미존재/" + utility.Fomula_list_sell_text + "] : HTS 조건식 리스트 미포함\n");
                Real_time_stop_btn.Enabled = false;
                Real_time_search_btn.Enabled = true;
                return;
            }

            //조건식에 대한 검색은 60초 마다 가능
            if (condInfo.LastRequestTime != null && condInfo.LastRequestTime >= DateTime.Now.AddSeconds(-60))
            {
                int second = 60 - (DateTime.Now - condInfo.LastRequestTime.Value).Seconds;
                WriteLog_System($"{second}초 후에 조회 가능합니다.\n");
                Real_time_stop_btn.Enabled = false;
                Real_time_search_btn.Enabled = true;
                return;
            }

            //마지막 조건식 검색 시각 업데이트
            condInfo.LastRequestTime = DateTime.Now;

            //종목 검색 요청
            //화면 번호, 조건식 이름, 조건식 번호, 조회 구분(0은 일반 검색, 1은 실시간 검색)

            System.Threading.Thread.Sleep(delay1);

            int result = axKHOpenAPI1.SendCondition(GetScreenNo(), condition[1], Convert.ToInt32(condition[0]), 1);
            if (result != 1)
            {
                WriteLog_System("[실시간매도조건식/등록실패/" + utility.Fomula_list_sell_text + "] : 고유번호 및 이름 확인\n");
                telegram_message("[실시간매도조건식/등록실패/" + utility.Fomula_list_sell_text + "] : 고유번호 및 이름 확인\n");
            }
        }

        //실시간 검색(조건식 로드 후 사용가능하다)
        private void real_time_search(object sender, EventArgs e)
        {
            //실시간 검색이 시작되면 '일반 검색'이 불가능해 진다.
            Real_time_stop_btn.Enabled = true;
            Real_time_search_btn.Enabled = false;

            //조건식이 로딩되었는지
            if (string.IsNullOrEmpty(utility.Fomula_list_buy_text))
            {
                WriteLog_System("조건식 선택 요청\n");
                telegram_message("조건식 선택 요청\n");
                WriteLog_System("실시간 매매 중단\n");
                telegram_message("실시간 매매 중단\n");
                Real_time_stop_btn.Enabled = false;
                Real_time_search_btn.Enabled = true;
                return;
            }

            foreach (string Fomula in utility.Fomula_list_buy_text.Split(','))
            {
                //검색된 조건식이 있을시
                string[] condition = Fomula.Split('^');
                var condInfo = conditionInfo.Find(f => f.Index == Convert.ToInt32(condition[0]) && f.Name.Equals(condition[1]));

                //로드된 조건식 목록에 설정된 조건식이 존재하지 않는 경우 이탈
                if (condInfo == null)
                {
                    WriteLog_System("[실시간조건식/미존재/" + Fomula + "] : HTS 조건식 리스트 미포함\n");
                    telegram_message("[실시간조건식/미존재/" + Fomula + "] : HTS 조건식 리스트 미포함\n");
                    Real_time_stop_btn.Enabled = false;
                    Real_time_search_btn.Enabled = true;
                    continue;
                }

                //조건식에 대한 검색은 60초 마다 가능
                if (condInfo.LastRequestTime != null && condInfo.LastRequestTime >= DateTime.Now.AddSeconds(-60))
                {
                    int second = 60 - (DateTime.Now - condInfo.LastRequestTime.Value).Seconds;
                    WriteLog_System($"{second}초 후에 조회 가능합니다.\n");
                    Real_time_stop_btn.Enabled = false;
                    Real_time_search_btn.Enabled = true;
                    return;
                }

                //마지막 조건식 검색 시각 업데이트
                condInfo.LastRequestTime = DateTime.Now;

                //종목 검색 요청
                //화면 번호, 조건식 이름, 조건식 번호, 조회 구분(0은 일반 검색, 1은 실시간 검색)

                int result = axKHOpenAPI1.SendCondition(GetScreenNo(), condition[1], Convert.ToInt32(condition[0]), 1);
                if (result != 1)
                {
                    WriteLog_System("[실시간조건식/등록실패/" + Fomula + "] : 고유번호 및 이름 확인\n");
                    telegram_message("[실시간조건식/등록실패/" + Fomula + "] : 고유번호 및 이름 확인\n");
                }

                System.Threading.Thread.Sleep(delay1);
            }
        }


        //#############################이전까지 반드시 동기화 작동 / Datatable 반복 수정문 및 최적화 기준점########################


        //-----------------------실시간 조건 검색------------------------------

        //조건식 초기 검색(일반)
        private void onReceiveTrCondition(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrConditionEvent e)
        {
            string code = e.strCodeList.Trim();

            //매도 조건식일 경우
            if (utility.Fomula_list_sell_text.Split('^')[1] == e.strConditionName) return;

            if (string.IsNullOrEmpty(code))
            {
                WriteLog_Stock("[실시간조건식/시작/" + e.strConditionName + "] : 초기 검색 종목 없음\n");
                telegram_message("[실시간조건식/시작/" + e.strConditionName + "] : 초기 검색 종목 없음\n");
                return;
            }

            //
            WriteLog_System("[실시간조건식/시작/" + e.strConditionName + "] : 초기 검색 종목 존재\n");
            telegram_message("[실시간조건식/시작/" + e.strConditionName + "] : 초기 검색 종목 존재\n");

            if (code.Length > 0) code = code.Remove(code.Length - 1);
            int codeCount = code.Split(';').Length;
            //
            //종목 데이터
            //종목코드 리스트, 연속조회여부(기본값0만존재), 종목코드 갯수, 종목(0 주식, 3 선물옵션), 사용자 구분명, 화면번호
            int error = axKHOpenAPI1.CommKwRqData(code, 0, codeCount, 0, "조건일반검색/" + e.strConditionName, GetScreenNo());
            WriteLog_System("[실시간조건식/시작/" + e.strConditionName + "] : 조회결과(" + error + ")\n");
        }

        //--------------------------------TR TABLE--------------------------------------------  

        //데이터 조회(예수금, 유가증권, 조건식, 일반 검색, 실시간 검색 등)
        private void onReceiveTrData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveTrDataEvent e)
        {
            //
            string[] name_split = e.sRQName.Split('/');
            string split_name = name_split[0];
            string condition_nameORcode = "";
            if (name_split.Length >= 2)
            {
                condition_nameORcode = name_split[1];
            }

            /*
             * 주식기본정보 => 개별 증권 데이터 조회
             * 계좌평가현황요청   => 계좌 보유 현황 및 예수금
             * 기존보유 => 기존 보유 종목 업데이트
             * 당일매매일지요청 => 평가 손익 받기(세전, 세후)
             * 계좌별주문체결내역상세요청 => 체결내역확인 및 채결가 업데이트
             * KOSPI200_INDEX
             * KOSDAK150_INDEX
             * 조건일반검색 => 초기 복수 검색 항목 추가
             * 조건실시간검색 => 실시간 편입 종목 추가
             * 조건실시간검색_수동 => HTS 매수 종목 추가
            */

            switch (split_name)
            {
                //개별 증권 데이터 조회
                case "주식기본정보":
                    WriteLog_Stock("------------------------------------\n");
                    WriteLog_Stock(string.Format("종목코드: {0}\n", axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "종목코드").Trim()));
                    WriteLog_Stock(string.Format("종목명: {0}\n", axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "종목명").Trim()));
                    WriteLog_Stock(string.Format("연중최고: {0:#,##0}\n", Convert.ToInt32(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "연중최고").Trim())));
                    WriteLog_Stock(string.Format("연중최저: {0:#,##0}\n", Convert.ToInt32(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "연중최저").Trim())));
                    WriteLog_Stock(string.Format("PER: {0:#,##0.00}\n", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "PER").Trim())));
                    WriteLog_Stock(string.Format("EPS: {0:#,##0}\n", Convert.ToInt32(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "EPS").Trim())));
                    WriteLog_Stock(string.Format("ROE: {0:#,##0.00}\n", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "ROE").Trim())));
                    WriteLog_Stock(string.Format("PBR: {0:#,##0.00}\n", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "PBR").Trim())));
                    WriteLog_Stock(string.Format("EV: {0:#,##0.00}\n", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "EV").Trim())));
                    WriteLog_Stock(string.Format("BPS: {0:#,##0}\n", Convert.ToInt32(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "BPS").Trim())));
                    WriteLog_Stock(string.Format("신용비율: {0:#,##0.00}%\n", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "신용비율").Trim())));
                    WriteLog_Stock(string.Format("외인소진률: {0:#,##0.00}%\n", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "외인소진률").Trim())));
                    WriteLog_Stock(string.Format("거래량: {0:#,##0}\n", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "거래량").Trim())));
                    WriteLog_Stock("------------------------------------\n");
                    break;

                //계좌 보유 현황 및 예수금
                case "계좌평가현황요청":

                    //예수금 업데이트
                    string tmp = string.Format("{0:#,##0}", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "D+2추정예수금").Trim()));

                    //장전 얘수금 1회용
                    if (user_money_before)
                    {
                        User_money.Text = tmp;
                        user_money_before = false;
                    }

                    //변동 예수금
                    Current_User_money.Text = tmp;

                    all_profit.Text = string.Format("{0:#,##0}", Convert.ToDecimal(Convert.ToInt32(Current_User_money.Text.Replace(",", "")) - Convert.ToInt32(total_money.Text.Replace(",", "")))); //수익
                    all_profit_percent.Text = string.Format("{0:#,##0.00}%", Convert.ToDecimal(Convert.ToDouble(all_profit.Text.Replace(",", "")) / Convert.ToDouble(total_money.Text.Replace(",", "")) * 100)); //수익률

                    WriteLog_System("[D+2예수금] : " + tmp + "\n");
                    telegram_message("[D+2예수금] : " + tmp + "\n");

                    //
                    int count2 = axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName);

                    lock (table2)
                    {
                        for (int i = 0; i < count2; i++)
                        {
                            string code = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목코드").Trim().Replace("A", "");
                            string average_price = string.Format("{0:#,##0}", Convert.ToInt32(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "평균단가").Trim()));
                            //
                            dtCondStock_hold.Rows.Add(
                                code,
                                axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목명").Trim(),
                                string.Format("{0:#,##0}", Convert.ToInt32(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "현재가").Trim())),
                                string.Format("{0:#,##0}", Convert.ToInt32(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "보유수량").Trim())),
                                average_price,
                                string.Format("{0:#,##0}", Convert.ToInt32(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "평가금액").Trim())),
                                string.Format("{0:#,##0.00}%", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "손익율").Trim()) / 10000),
                                string.Format("{0:#,##0}", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "손익금액").Trim())),
                                string.Format("{0:#,##0}", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "금일매도수량").Trim()))
                            );
                        }

                        if (dataGridView2.InvokeRequired)
                        {
                            dataGridView2.Invoke((MethodInvoker)delegate
                            {
                                bindingSource2.ResetBindings(false);
                            });
                        }
                        else
                        {
                            bindingSource2.ResetBindings(false);
                        }
                    }

                    System.Threading.Thread.Sleep(delay1);

                    //기존 보유 종목 차트 업데이트
                    if (condition_nameORcode.Equals("초기"))
                    {
                        Hold_Update();
                    }

                    break;

                //기존보유
                case "기존보유":
                    if (string.IsNullOrEmpty(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "현재가").Trim()))
                    {
                        WriteLog_System("기존 보유 종목 업데이트 실패\n");
                        break;
                    }
                    //
                    int current_price3 = Math.Abs(Convert.ToInt32(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "현재가").Trim()));
                    string time3 = DateTime.Now.ToString("HH:mm:ss");
                    string code3 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "종목코드").Trim();
                    string code_name3 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "종목명").Trim();
                    string high3 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "상한가").Trim();
                    string average_price2 = "";
                    //
                    lock (table2)
                    {
                        DataRow[] findRows = dtCondStock_hold.Select($"종목코드 = {code3}");
                        average_price2 = findRows[0]["평균단가"].ToString();
                    }
                    //
                    WriteLog_Stock("[기존종목/편입] : " + code3 + "-" + code_name3 + "\n");
                    telegram_message("[기존종목/편입] : " + code3 + "-" + code_name3 + "\n");
                    //
                    dtCondStock.Rows.Add(
                            false,
                            "편입",
                            "매수완료",
                            code3,
                            code_name3,
                            string.Format("{0:#,##0}", current_price3),
                            string.Format("{0:#,##0.00}%", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "등락율").Trim())),
                            string.Format("{0:#,##0}", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "거래량").Trim())),
                            "실매입",
                            average_price2,
                            "-",
                            "0.00%",
                            condition_nameORcode + "/" + condition_nameORcode,
                            "전일보유",
                            time3,
                            "-",
                            "-",
                            "-",
                            code3,
                            string.Format("{0:#,##0}", Convert.ToDecimal(high3)),
                            average_price2,
                            "-"
                        );
                    //
                    gridView1_refresh();

                    //실시간 항목 등록(대비기호, 현재가. 등락율, 거래량)
                    axKHOpenAPI1.SetRealReg(GetScreenNo(), e.sTrCode, "10;12;13", "1");                 

                    break;

                //평가 손익 받기(세전, 세후)
                case "당일매매일지요청":
                    //실질매수 : 0.015% / 실질매도 : 0.015% + 0.18%
                    //모의매수 : 0.35% / 실질매도 : 0.35% + 0.25%
                    int sum_profit_tax = Convert.ToInt32(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "총손익금액").Trim().Replace(",", "")); //세후손익
                    int sum_tax = Convert.ToInt32(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "총수수료_세금").Trim().Replace(",", "")); //세금

                    today_profit.Text = string.Format("{0:#,##0}", Convert.ToDecimal(sum_profit_tax + sum_tax)); // 당일 손익
                    today_tax.Text = string.Format("{0:#,##0}", Convert.ToDecimal(sum_tax)); // 당일 세금
                    today_profit_tax.Text = string.Format("{0:#,##0}", Convert.ToDecimal(sum_profit_tax)); // 당일 세후 손익
                    today_profit_percent.Text = string.Format("{0:#,##0.00}%", Convert.ToDecimal(Convert.ToDouble(sum_profit_tax + sum_tax) / Convert.ToDouble(User_money.Text.Replace(",", "")) * 100)); // 당일 손익률
                    today_profit_percent_tax.Text = string.Format("{0:#,##0.00}%", Convert.ToDecimal(Convert.ToDouble(sum_profit_tax) / Convert.ToDouble(User_money.Text.Replace(",", "")) * 100)); // 당일 세후 손익률

                    if (condition_nameORcode.Equals("매도"))
                    {
                        WriteLog_System("[누적세전손익] : " + today_profit.Text + " / [누적세전손익률] : " + today_profit_percent.Text + "\n");
                        WriteLog_System("[누적세후손익] : " + today_profit_tax.Text + " / [누적세후손익률] : " + today_profit_percent_tax.Text + "\n");
                        telegram_message("[누적세전손익] : " + today_profit.Text + " / [누적세전손익률] : " + today_profit_percent.Text + "\n");
                        telegram_message("[누적세후손익] : " + today_profit_tax.Text + " / [누적세후손익률] : " + today_profit_percent_tax.Text + "\n");
                    }
                    break;

                //체결내역확인 및 채결가 업데이트
                case "계좌별주문체결내역상세요청":

                    int count3 = axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName);

                    if(count3 == 0 && condition_nameORcode != "")
                    {
                        WriteLog_System($"[채결내역수신/{condition_nameORcode}/{count3}] : 실패 재시도\n");
                        //
                        System.Threading.Thread.Sleep(delay1 + 200);
                        //
                        Transaction_Detail(condition_nameORcode, name_split[2]);
                        break;
                    }
                    
                    WriteLog_System($"[채결내역수신/{condition_nameORcode}/{count3}] : 성공\n");

                    for (int i = 0; i < count3; i++)
                    {
                        string transaction_number = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "주문번호").Trim();
                        string average_price = string.Format("{0:#,##0}", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "체결단가").Trim().TrimStart('0') == "" ? "0" : axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "체결단가").Trim().TrimStart('0')));
                        string gubun = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "주문구분").Trim();
                        string code = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목번호").Trim().Replace("A", "");
                        string code_name = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목명").Trim();
                        string order_sum = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "체결수량").Trim().TrimStart('0');

                        if (name_split[2].Equals("매수취소"))
                        {
                            lock (table1)
                            {
                                DataRow[] findRows2 = dtCondStock.AsEnumerable().Where(row => row.Field<string>("주문번호") == condition_nameORcode).ToArray();

                                if (findRows2.Any())
                                {
                                    if (order_sum == "0")
                                    {
                                        findRows2[0]["보유수량"] = $"{order_sum}/{order_sum}";
                                        if (utility.buy_AND)
                                        {
                                            findRows2[0]["상태"] = "주문";
                                        }
                                        else
                                        {
                                            findRows2[0]["상태"] = "대기";
                                        }
                                        //보유 수량 업데이트
                                        string[] hold_status = max_hoid.Text.Split('/');
                                        int hold = Convert.ToInt32(hold_status[0]);
                                        int hold_max = Convert.ToInt32(hold_status[1]);
                                        max_hoid.Text = $"{hold - 1}/{hold_max}";
                                    }
                                    else
                                    {
                                        findRows2[0]["보유수량"] = $"{order_sum}/{order_sum}";
                                    }
                                    gridView1_refresh();
                                }
                            }
                        }
                        else if (name_split[2].Equals("매도취소"))
                        {
                            lock (table1)
                            {
                                DataRow[] findRows2 = dtCondStock.AsEnumerable().Where(row => row.Field<string>("주문번호") == condition_nameORcode).ToArray();

                                if (findRows2.Any())
                                {
                                    if (order_sum.Equals("0"))
                                    {
                                        findRows2[0]["보유수량"] = $"{order_sum}/{order_sum}";
                                        if (!utility.duplication_deny)
                                        {
                                            findRows2[0]["상태"] = "대기";
                                        }
                                        else
                                        {
                                            findRows2[0]["상태"] = "매도완료";
                                            //
                                            //모든 화면에서 "code"종목 실시간 해지
                                            axKHOpenAPI1.SetRealRemove("ALL", code);
                                        }
                                        //보유 수량 업데이트
                                        string[] hold_status = max_hoid.Text.Split('/');
                                        int hold = Convert.ToInt32(hold_status[0]);
                                        int hold_max = Convert.ToInt32(hold_status[1]);
                                        max_hoid.Text = $"{hold - 1}/{hold_max}";
                                    }
                                    else
                                    {
                                        findRows2[0]["보유수량"] = $"{order_sum}/{order_sum}";
                                        findRows2[0]["상태"] = "매수완료";
                                    }
                                    gridView1_refresh();
                                }
                            }
                        }
                        //매수완료 후 실제 편입가 업데이트
                        else if (transaction_number.Equals(condition_nameORcode))
                        {
                            lock (table1)
                            {
                                var findRows2 = dtCondStock.AsEnumerable().Where(row => row.Field<string>("주문번호") == condition_nameORcode);

                                if (findRows2.Any())
                                {
                                    DataRow row = findRows2.First();
                                    if (gubun.StartsWith("현금매수")) //현금매수 K
                                    {
                                        row["편입상태"] = "실매입";
                                        row["편입가"] = average_price;
                                        //
                                        if (utility.profit_ts)
                                        {
                                            row["상태"] = "TS매수완료";
                                            row["편입최고"] = average_price;
                                        }
                                        else
                                        {
                                            row["상태"] = "매수완료";
                                        }
                                        gridView1_refresh();
                                        //Message
                                        WriteLog_Order($"[매수주문/정상완료/01] : {code_name}({code}) {order_sum}개 {average_price}원\n");
                                        telegram_message($"[매수주문/정상완료/01] : {code_name}({code}) {order_sum}개 {average_price}원\n");
                                    }
                                    else
                                    {
                                        row["매도가"] = average_price;
                                        gridView1_refresh();
                                        //
                                        //Message
                                        WriteLog_Order($"[매도주문/정상완료/01] : {code_name}({code}) {order_sum}개 {average_price}원\n");
                                        telegram_message($"[매도주문/정상완료/01] : {code_name}({code}) {order_sum}개 {average_price}원\n");
                                    }

                                }
                            }
                        }

                        lock (table3)
                        {
                            dtCondStock_Transaction.Rows.Add(
                            code,
                            code_name,
                            axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "주문시간").Trim(),
                            transaction_number,
                            axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "매매구분").Trim(),
                            gubun,
                            axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "주문수량").Trim().TrimStart('0'),
                            order_sum,
                            average_price
                            );
                        }
                    }
                    /*
                    dtCondStock_Transaction = dataTable3;
                    dataGridView3.DataSource = dtCondStock_Transaction;
                    */
                    //
                    //
                    if (dataGridView3.InvokeRequired)
                    {
                        dataGridView3.Invoke((MethodInvoker)delegate
                        {
                            dataGridView3.DataSource = dtCondStock_Transaction;
                            dataGridView3.Refresh();
                        });
                    }
                    else
                    {
                        dataGridView3.DataSource = dtCondStock_Transaction;
                        dataGridView3.Refresh();
                    }
                    break;

                //KOSPI200 인덱스 처리
                case "KOSPI200_INDEX":

                    string tmp3 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "대비기호").Trim().Replace("-", "").Replace("-", "");//대비기호
                    double tmp4 = Convert.ToDouble(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "전일대비").Trim().Replace("-", ""));//전일대비
                    double tmp5 = Convert.ToDouble(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "현재가").Trim().Replace("-", ""));//현재가
                    double tmp6 = Convert.ToDouble(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "저가").Trim().Replace("-", ""));//금일저가
                    double tmp7 = Convert.ToDouble(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "고가").Trim().Replace("-", ""));//금일고가
                    double tmp8 = 0;

                    if (tmp3 == "2")
                    {
                        tmp8 = tmp5 - tmp4;
                    }
                    else
                    {
                        tmp8 = tmp5 + tmp4;
                    }

                    //8시 45분전에 수신시 혹은 최초 수신시 0값이 나오는 경우가 있음
                    if (tmp5 == 0 || tmp6 == 0 || tmp7 == 0 || tmp8 == 0)
                    {
                        WriteLog_System($"[수신오류] KOSPI200 : 전일종가({tmp8}), 종가({tmp5}), 저가({tmp6}), 고가({tmp7})\n");
                        telegram_message($"[수신오류] KOSPI200 : 60초 뒤 재시도\n");
                        return;
                    }

                    //저가,종가,고가
                    kospi_index_series[0] = Math.Round((tmp6 - tmp8) / tmp8 * 100, 2); //저가
                    kospi_index_series[1] = Math.Round((tmp5 - tmp8) / tmp8 * 100, 2); //종가
                    kospi_index_series[2] = Math.Round((tmp7 - tmp8) / tmp8 * 100, 2); //고가

                    //KOSPI_INDEX_SERIES 입력전 실행되는 경우가 발생
                    //this.Invoke((MethodInvoker)delegate

                    kospi_index.Text = String.Format($"L({kospi_index_series[0]})/H({kospi_index_series[2]})");
                    //WriteLog_System($"{tmp}/{tmp1}/{tmp3}/{tmp4.ToString()}/{tmp5.ToString()}/{tmp6.ToString()}/{tmp7.ToString()}\n");

                    if (utility.buy_condition_index)
                    {
                        if (utility.type1_selection && !index_buy)
                        {
                            double start = Convert.ToDouble(utility.type1_start);
                            double end = Convert.ToDouble(utility.type1_end);
                            if (kospi_index_series[0] < start || end < kospi_index_series[2])
                            {
                                WriteLog_System($"[Buy/이탈] KOSPI200 RANGE\n");
                                WriteLog_System($"SET_LOW({start}) <= LOW({kospi_index_series[0]})\n");
                                WriteLog_System($"HIGH({kospi_index_series[2]}) <= SET_HIGH({end})\n");
                                WriteLog_System("Trade Stop\n");

                                telegram_message($"[Buy/이탈] KOSPI200 RANGE\n");
                                telegram_message($"SET_LOW({start}) <= LOW({kospi_index_series[0]})\n");
                                telegram_message($"HIGH({kospi_index_series[2]}) <= SET_HIGH({end})\n");
                                telegram_message("Trade Stop\n");

                                lock (index_write)
                                {
                                    index_buy = true;
                                }
                            }
                        }
                    }

                    if (utility.clear_index)
                    {
                        if (utility.type1_selection_all && !index_clear)
                        {
                            double start = Convert.ToDouble(utility.type1_start_all);
                            double end = Convert.ToDouble(utility.type1_end_all);
                            if (kospi_index_series[0] < start || end < kospi_index_series[2])
                            {
                                WriteLog_System($"[CLEAR/이탈] KOSPI200 RANGE\n");
                                WriteLog_System($"SET_LOW({start}) <= LOW({kospi_index_series[0]})\n");
                                WriteLog_System($"HIGH({kospi_index_series[2]}) <= SET_HIGH({end})\n");
                                WriteLog_System("Trade Stop\n");

                                telegram_message($"[CLEAR/이탈] KOSPI200 RANGE\n");
                                telegram_message($"SET_LOW({start}) <= LOW({kospi_index_series[0]})\n");
                                telegram_message($"HIGH({kospi_index_series[2]}) <= SET_HIGH({end})\n");
                                telegram_message("Trade Stop\n");

                                lock (index_write)
                                {
                                    index_clear = true;
                                }
                            }
                        }
                    }

                    break;

                //KOSDAK150 인덱스 처리
                case "KOSDAK150_INDEX":

                    string tmp3_KOSDAK = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "대비기호").Trim().Replace("-", "");//대비기호
                    double tmp4_KOSDAK = Convert.ToDouble(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "전일대비").Trim().Replace("-", ""));//전일대비
                    double tmp5_KOSDAK = Convert.ToDouble(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "현재가").Trim().Replace("-", ""));//현재가
                    double tmp6_KOSDAK = Convert.ToDouble(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "저가").Trim().Replace("-", ""));//금일저가
                    double tmp7_KOSDAK = Convert.ToDouble(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "고가").Trim().Replace("-", ""));//금일고가
                    double tmp8_KOSDAK = 0;

                    //WriteLog_Order($"{tmp3_KOSDAK} // {tmp4_KOSDAK.ToString()} // {tmp5_KOSDAK.ToString()} // {tmp6_KOSDAK.ToString()}");

                    //저가,종가,고가
                    if (tmp3_KOSDAK == "2")
                    {
                        tmp8_KOSDAK = tmp5_KOSDAK - tmp4_KOSDAK;
                    }
                    else
                    {
                        tmp8_KOSDAK = tmp5_KOSDAK + tmp4_KOSDAK;
                    }

                    //8시 45분전에 수신시 혹은 최초 수신시 0값이 나오는 경우가 있음
                    if (tmp5_KOSDAK == 0 || tmp6_KOSDAK == 0 || tmp7_KOSDAK == 0 || tmp8_KOSDAK == 0)
                    {
                        WriteLog_System($"[수신오류] KOSDAK150 : 전일종가({tmp8_KOSDAK}), 종가({tmp5_KOSDAK}), 저가({tmp6_KOSDAK}), 고가({tmp7_KOSDAK})\n");
                        telegram_message($"[수신오류] KOSDAK150 : 60초 뒤 재시도\n");
                        return;
                    }

                    //저가,종가,고가
                    kosdaq_index_series[0] = Math.Round((tmp6_KOSDAK - tmp8_KOSDAK) / tmp8_KOSDAK * 100, 2); //저가
                    kosdaq_index_series[1] = Math.Round((tmp5_KOSDAK - tmp8_KOSDAK) / tmp8_KOSDAK * 100, 2); //종가
                    kosdaq_index_series[2] = Math.Round((tmp7_KOSDAK - tmp8_KOSDAK) / tmp8_KOSDAK * 100, 2); //고가

                    //KOSDAKINDEX_SERIES 입력전 실행되는 경우가 발생
                    //this.Invoke((MethodInvoker)delegate

                    kosdaq_index.Text = String.Format($"L({kosdaq_index_series[0]})/H({kosdaq_index_series[2]})");
                    //WriteLog_System($"{tmp}/{tmp1}/{tmp3}/{tmp4.ToString()}/{tmp5.ToString()}/{tmp6.ToString()}/{tmp7.ToString()}\n");

                    if (utility.buy_condition_index)
                    {
                        if (utility.type2_selection && !index_buy)
                        {
                            double start = Convert.ToDouble(utility.type2_start);
                            double end = Convert.ToDouble(utility.type2_end);
                            if (kosdaq_index_series[0] < start || end < kosdaq_index_series[2])
                            {
                                WriteLog_System($"[Buy/이탈] KOSDAK150 RANGE\n");
                                WriteLog_System($"SET_LOW({start}) <= LOW({kosdaq_index_series[0]})\n");
                                WriteLog_System($"HIGH({kosdaq_index_series[2]}) <= SET_HIGH({end})\n");
                                WriteLog_System("Trade Stop\n");

                                telegram_message($"[Buy/이탈] KOSDAK150 RANGE\n");
                                telegram_message($"SET_LOW({start}) <= LOW({kosdaq_index_series[0]})\n");
                                telegram_message($"HIGH({kosdaq_index_series[2]}) <= SET_HIGH({end})\n");
                                telegram_message("Trade Stop\n");

                                lock (index_write)
                                {
                                    index_buy = true;
                                }
                            }
                        }
                    }

                    if (utility.clear_index)
                    {
                        if (utility.type2_selection_all && !index_clear)
                        {
                            double start = Convert.ToDouble(utility.type2_start_all);
                            double end = Convert.ToDouble(utility.type2_end_all);
                            if (kosdaq_index_series[0] < start || end < kosdaq_index_series[2])
                            {
                                WriteLog_System($"[Clear/이탈] KOSDAK150 RANGE\n");
                                WriteLog_System($"SET_LOW({start}) <= LOW({kosdaq_index_series[0]})\n");
                                WriteLog_System($"HIGH({kosdaq_index_series[2]}) <= SET_HIGH({end})\n");
                                WriteLog_System("Trade Stop\n");

                                telegram_message($"[Clear/이탈] KOSDAK150 RANGE\n");
                                telegram_message($"SET_LOW({start}) <= LOW({kosdaq_index_series[0]})\n");
                                telegram_message($"HIGH({kosdaq_index_series[2]}) <= SET_HIGH({end})\n");
                                telegram_message("Trade Stop\n");

                                lock (index_write)
                                {
                                    index_clear = true;
                                }
                            }
                        }
                    }

                    break;

                //실시간 조건 검색 초기 종목 검색
                case "조건일반검색":

                    int count = axKHOpenAPI1.GetRepeatCnt(e.sTrCode, e.sRQName);
                    string time1 = DateTime.Now.ToString("HH:mm:ss");

                    for (int i = 0; i < count; i++)
                    {
                        string code = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목코드").Trim();
                        string code_name = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "종목명").Trim();
                        int current_price = Math.Abs(Convert.ToInt32(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "현재가").Trim()));
                        string high1 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "상한가").Trim();
                        string now_hold1 = "0";
                        string Status = utility.buy_AND ? "호출" : "대기";

                        DataRow[] findRows_check = null;

                        lock (table1)
                        {
                            findRows_check = dtCondStock.Select($"종목코드 = '{code}'");

                            if (findRows_check.Any() && findRows_check[0]["조건식"].Equals("전일보유"))
                            {
                                WriteLog_Stock($"[전일보유/{condition_nameORcode}/편입실패] : {code_name}({code}) \n");
                                continue;
                            } 
                        }

                        lock (table2)
                        {

                            DataRow[] findRows_check2 = dtCondStock_hold.Select($"종목코드 = '{code}'");

                            if (findRows_check2.Any() && !findRows_check.Any())
                            {
                                string average_price3 = findRows_check2[0]["평균단가"].ToString();
                                string hold = findRows_check2[0]["보유수량"].ToString();
                                //
                                dtCondStock.Rows.Add(
                                    false,
                                    "편입",
                                    "매수완료",
                                    code,
                                    code_name,
                                    string.Format("{0:#,##0}", current_price),
                                    string.Format("{0:#,##0.00}%", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "등락율").Trim())),
                                    string.Format("{0:#,##0}", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "거래량").Trim())),
                                    "실매입",
                                    average_price3,
                                    "-",
                                    "0.00%",
                                    hold + "/" + hold,
                                    "전일보유",
                                    time1,
                                    "-",
                                    "-",
                                    "-",
                                    code,
                                    string.Format("{0:#,##0}", Convert.ToDecimal(high1)),
                                    average_price3,
                                    "-"
                                );
                                gridView1_refresh();
                                WriteLog_Stock($"[전일보유/{condition_nameORcode}/편입실패] : {code_name}({code}) 상태 수정\n");
                                continue;
                            }
                        }

                        //최소 및 최대 매수가 확인
                        if (current_price < Convert.ToInt32(utility.min_price) || current_price > Convert.ToInt32(utility.max_price))
                        {
                            WriteLog_Stock($"[{condition_nameORcode}/편입실패] : {code_name}({code}) 가격 최소 및 최대 범위 이탈\n");
                            continue;
                        }

                        bool buy_and_check = false;

                        if (findRows_check.Any() && utility.buy_OR)
                        {
                            WriteLog_Stock($"[신규편입/초기/{condition_nameORcode}] :  {code_name}({code}) OR 모드 중복\n");
                            continue;
                        }

                        if (findRows_check.Any() && utility.buy_AND)
                        {
                            WriteLog_Stock($"[신규편입/초기/{condition_nameORcode}] :  {code_name}({code}) AND 모드 중복\n");
                            buy_and_check = true;
                        }

                        if (!buy_and_check)
                        {
                            if (dtCondStock.Rows.Count > 20)
                            {
                                WriteLog_Stock($"[신규편입불가/{condition_nameORcode}/{code}/초기] : 최대 감시 종목(20개) 초과 \n");
                                continue;
                            }

                            //운영시간 확인
                            DateTime t_now = DateTime.Now;
                            DateTime t_end = DateTime.Parse(utility.buy_condition_end);
                            if (t_now > t_end)
                            {
                                WriteLog_Stock($"[신규편입/초기/{condition_nameORcode}] :  {code_name}({code})\n매수 시간 이후 종목은 차트에 포함하지 않습니다.\n");
                                continue;
                            }
                            WriteLog_Stock($"[신규편입/초기/{condition_nameORcode}] :  {code_name}({code})\n");
                        }

                        if (!utility.buy_AND && !buy_and_check)
                        {
                            lock (buy_lock)
                            {
                                if (!buy_runningCodes.ContainsKey(code))
                                {
                                    buy_runningCodes[code] = true;
                                    Status = buy_check(code, code_name, string.Format("{0:#,##0}", current_price), time1, high1, false, condition_nameORcode);
                                    buy_runningCodes.Remove(code);
                                }
                            }
                        }
                        else if (utility.buy_AND && buy_and_check)
                        {
                            lock (buy_lock)
                            {
                                if (!buy_runningCodes.ContainsKey(code) && !utility.buy_AND)
                                {
                                    buy_runningCodes[code] = true;
                                    Status = buy_check(code, code_name, string.Format("{0:#,##0}", current_price), time1, high1, true, condition_nameORcode);
                                    buy_runningCodes.Remove(code);
                                    //
                                    return;
                                }
                            }
                        }

                        //
                        if (Status.StartsWith("매수중"))
                        {
                            now_hold1 = Status.Split('/')[1];
                            Status = "매수중";
                        }

                        //초기검색 항목 점검
                        if (utility.buy_AND && !buy_and_check)
                        {
                            Status = "호출";
                        }

                        lock (table1)
                        {
                            //
                            dtCondStock.Rows.Add(
                            false,
                            "편입",
                            Status,
                            code,
                            code_name,
                            string.Format("{0:#,##0}", current_price),
                            string.Format("{0:#,##0.00}%", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "등락율").Trim())),
                            string.Format("{0:#,##0}", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, i, "거래량").Trim())),
                            "진입가",
                            string.Format("{0:#,##0}", current_price),
                            "-",
                            "00.00%",
                            "0/" + now_hold1,
                            condition_nameORcode,
                            time1,
                            "-",
                            "-",
                            "-",
                            "-",
                            string.Format("{0:#,##0}", Convert.ToDecimal(high1)),
                            string.Format("{0:#,##0}", current_price),
                            "-"
                            );
                            //
                            gridView1_refresh();
                        }
                        //실시간 항목 등록(대비기호, 현재가. 등락율, 거래량)
                        axKHOpenAPI1.SetRealReg(GetScreenNo(), e.sTrCode, "10;12;13", "1");
                        
                    }
                    /*
                    //OR 및 AND 모드에서는 중복제거 => 초기 종목 검색시 중복 제거 필수
                    if (!utility.buy_INDEPENDENT)
                    {
                        RemoveDuplicateRows(dtCondStock, utility.buy_AND);
                    }
                    */

                    //

                    break;

                //실시간 조건 검색 편출입 종목 검색
                case "조건실시간검색":
                    int current_price2 = Math.Abs(Convert.ToInt32(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "현재가").Trim()));
                    string code2 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "종목코드").Trim();
                    string code_name2 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "종목명").Trim();
                    string time2 = DateTime.Now.ToString("HH:mm:ss");
                    string high2 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "상한가").Trim();
                    string now_hold2 = "0";
                    string Status2 = utility.buy_AND ? "호출" : "대기";

                    //DataRow[] findRows_check2 = dtCondStock.Select($"종목코드 = '{code2}'");

                    //최소 및 최대 매수가 확인
                    if (current_price2 < Convert.ToInt32(utility.min_price) || current_price2 > Convert.ToInt32(utility.max_price))
                    {
                        WriteLog_Stock($"[{condition_nameORcode}/편입실패] : {code_name2}({code2}) 가격 최소 및 최대 범위 이탈\n");
                        return;
                    }

                    //운영시간 확인
                    DateTime t_now2 = DateTime.Now;
                    DateTime t_end2 = DateTime.Parse(utility.buy_condition_end);
                    if (t_now2 > t_end2)
                    {
                        WriteLog_Stock($"[신규편입/초기/{condition_nameORcode}] :  {code_name2}({code2})\n매수 시간 이후 종목은 차트에 포함하지 않습니다.\n");
                        return;
                    }
                    WriteLog_Stock($"[신규종목/편입/{condition_nameORcode}] : {code_name2}({code2})\n");

                    if (!utility.buy_AND)
                    {
                        lock (buy_lock)
                        {
                            if (!buy_runningCodes.ContainsKey(code2))
                            {
                                buy_runningCodes[code2] = true;
                                Status2 = buy_check(code2, code_name2, string.Format("{0:#,##0}", current_price2), time2, high2, false, condition_nameORcode);
                                buy_runningCodes.Remove(code2);
                            }
                        }
                    }

                    //
                    if (Status2.StartsWith("매수중"))
                    {
                        now_hold2 = Status2.Split('/')[1];
                        Status2 = "매수중";
                    }

                    lock (table1)
                    {
                        dtCondStock.Rows.Add(
                        false,
                        "편입",
                        Status2,
                        code2,
                        code_name2,
                        string.Format("{0:#,##0}", current_price2),
                        string.Format("{0:#,##0.00}%", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "등락율").Trim())),
                        string.Format("{0:#,##0}", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "거래량").Trim())),
                        "진입가",
                        string.Format("{0:#,##0}", current_price2),
                        "-",
                        "00.00%",
                        "0/" + now_hold2,
                        condition_nameORcode,
                        time2,
                        "-",
                        "-",
                        "-",
                        "-",
                        string.Format("{0:#,##0}", Convert.ToDecimal(high2)),
                        string.Format("{0:#,##0}", current_price2),
                        "-"
                        );
                        //
                        gridView1_refresh();
                    }

                    //실시간 항목 등록(대비기호, 현재가. 등락율, 거래량)
                    axKHOpenAPI1.SetRealReg(GetScreenNo(), e.sTrCode, "10;12;13", "1");
                   
                    break;

                //HTS 및 MTS 매매 종목 편입
                case "조건실시간검색_수동":
                    int current_price4 = Math.Abs(Convert.ToInt32(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "현재가").Trim()));
                    string time4 = DateTime.Now.ToString("HH:mm:ss");
                    string code4 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "종목코드").Trim();
                    string code_name4 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "종목명").Trim();
                    string high4 = axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "상한가").Trim();
                    string average_price4 = string.Format("{0:#,##0}", Convert.ToDecimal(current_price4));

                    WriteLog_Stock("[HTS_수동/편입] : " + code4 + "-" + code_name4 + "\n");
                    telegram_message("[HTS_수동/편입] : " + code4 + "-" + code_name4 + "\n");

                    lock (table1)
                    {
                        dtCondStock.Rows.Add(
                        false,
                        "편입",
                        "매수완료",
                        code4,
                        code_name4,
                        string.Format("{0:#,##0}", current_price4),
                        string.Format("{0:#,##0.00}%", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "등락율").Trim())),
                        string.Format("{0:#,##0}", Convert.ToDecimal(axKHOpenAPI1.GetCommData(e.sTrCode, e.sRQName, 0, "거래량").Trim())),
                        "실매입",
                        average_price4,
                        "-",
                        "0.00%",
                        name_split[2] + "/" + name_split[2],
                        "HTS보유",
                        time4,
                        "-",
                        time4,
                        "-",
                        condition_nameORcode,
                        string.Format("{0:#,##0}", Convert.ToDecimal(high4)),
                        average_price4,
                        "-"
                        );
                        //
                        gridView1_refresh();
                    }

                    //체결내역업데이트(주문번호)
                    lock (table3)
                    {
                        dtCondStock_Transaction.Clear();
                    }
                    Transaction_Detail(condition_nameORcode, "");
                    
                    //실시간 항목 등록(대비기호, 현재가. 등락율, 거래량)
                    axKHOpenAPI1.SetRealReg(GetScreenNo(), e.sTrCode, "10;12;13", "1");
                    
                    break;

            }
        }

        /*
        //중복제거
        public void RemoveDuplicateRows(DataTable dtCondStock, bool utilityBuyAnd)
        {
            //시간
            string time1 = DateTime.Now.ToString("HH:mm:ss");

            // 열 인덱스 가져오기
            int columnIndex = dtCondStock.Columns["종목명"].Ordinal;
            int statusColumnIndex = dtCondStock.Columns["상태"].Ordinal;
            int codeColumnIndex = dtCondStock.Columns["종목코드"].Ordinal;
            int currentPriceColumnIndex = dtCondStock.Columns["현재가"].Ordinal;
            int highPriceColumnIndex = dtCondStock.Columns["상한가"].Ordinal;
            int conditionColumnIndex = dtCondStock.Columns["조건식"].Ordinal;

            // 중복 행 제거를 위한 HashSet 생성
            HashSet<string> uniqueValues = new HashSet<string>();

            // 제거할 행의 인덱스 리스트
            List<int> rowsToRemove = new List<int>();

            lock (table1)
            {
                // 행을 역순으로 순회하면서 중복 행 확인
                for (int i = dtCondStock.Rows.Count - 1; i >= 0; i--)
                {
                    string currentValue = dtCondStock.Rows[i][columnIndex].ToString();

                    // 현재 값이 HashSet에 없으면 추가
                    if (!uniqueValues.Contains(currentValue))
                    {
                        uniqueValues.Add(currentValue);
                    }
                    // 현재 값이 이미 있으면 제거할 행 리스트에 추가
                    else
                    {
                        rowsToRemove.Add(i);

                        // utility.buy_AND가 True 상태이면 buy_check 함수 실행
                        if (utilityBuyAnd)
                        {
                            lock (buy_lock)
                            {
                                string code = dtCondStock.Rows[i][codeColumnIndex].ToString();
                                string code_name = currentValue;
                                string current_price = string.Format("{0:#,##0}", dtCondStock.Rows[i][currentPriceColumnIndex]);
                                string high1 = dtCondStock.Rows[i][highPriceColumnIndex].ToString();
                                string condition = dtCondStock.Rows[i][conditionColumnIndex].ToString();

                                if (!buy_runningCodes.ContainsKey(code))
                                {
                                    string buyCheckResult = buy_check(code, code_name, current_price, time1, high1, false, condition);
                                    if (buyCheckResult == "매수중")
                                    {
                                        dtCondStock.Rows[i][statusColumnIndex] = "매수중";
                                        dtCondStock.Rows[i]["보유수량"] = "0/" + buyCheckResult.Split('/')[1];
                                    }
                                    else
                                    {
                                        dtCondStock.Rows[i][statusColumnIndex] = "주문";
                                    }
                                }
                            }
                        }
                    }
                }
            }

            lock (table1)
            {
                // 제거할 행 목록에 따라 역순으로 행 제거
                foreach (int rowIndex in rowsToRemove)
                {
                    dtCondStock.Rows.RemoveAt(rowIndex);
                }
            }
        }
        */

        //--------------------------------실시간 시세 처리--------------------------------------------

        //실시간 시세(지속적 발생 / (현재가. 등락율, 거래량, 수익률)
        private void onReceiveRealData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealDataEvent e)
        {
            // 신규 값 받기
            string price = Regex.Replace(axKHOpenAPI1.GetCommRealData(e.sRealKey, 10).Trim(), @"[\+\-]", ""); // 새로운 현재가
            string amount = axKHOpenAPI1.GetCommRealData(e.sRealKey, 13).Trim(); // 새로운 거래량

            if (price.Equals("") || amount.Equals("")) return;

            // 값을 병렬로 업데이트
            Task updateDataAndCheckForSellTask = Task.Run(() => UpdateDataAndCheckForSell(e.sRealKey, price, amount));
            //Task updateDataTableHoldTask = Task.Run(() => UpdateDataTableHold(e.sRealKey, price, amount));
        }

        private void UpdateDataAndCheckForSell(string stockCode, string price, string amount)
        {
            lock (table1)
            {
                DataRow[] findRows = dtCondStock.Select($"종목코드 = '{stockCode}'");

                if (findRows.Length != 0)
                {
                    Parallel.ForEach(findRows, row =>
                    {
                        // 필요값 사전 추출
                        string buy_price = row["편입가"].ToString().Replace(",", "");
                        string status = row["상태"].ToString();
                        string in_high = row["편입최고"].ToString().Replace(",", "");
                        string order_number = row["주문번호"].ToString();
                        string hold = row["보유수량"].ToString().Split('/')[0];

                        // 값 계산
                        double native_price = Convert.ToDouble(price);
                        double native_percent = (native_price - Convert.ToDouble(buy_price)) / Convert.ToDouble(buy_price) * 100;
                        string percent = string.Format("{0:#,##0.00}%", Convert.ToDecimal(native_percent)); // 새로운 수익률

                        // 값 반영
                        row["현재가"] = string.Format("{0:#,##0}", Convert.ToInt32(price)); // 새로운 현재가
                        row["거래량"] = string.Format("{0:#,##0}", Convert.ToInt32(amount)); // 새로운 거래량
                        row["수익률"] = percent;

                        // TS 계산 및 반영
                        if (status == "매수완료" && status == "TS매수완료" && Convert.ToInt32(in_high) < Convert.ToInt32(price))
                        {
                            row["편입최고"] = string.Format("{0:#,##0}", Convert.ToInt32(price)); // 새로운 현재가
                            if (status == "TS매수완료" && native_percent >= double.Parse(utility.profit_ts_text))
                            {
                                row["상태"] = "매수완료";
                            }
                        }

                        // 매도 확인
                        if (status.Equals("매수완료"))
                        {
                            lock (sell_lock)
                            {
                                if (!sell_runningCodes.ContainsKey(order_number))
                                {
                                    sell_runningCodes[order_number] = true;
                                    if (utility.profit_ts)
                                    {
                                        if (Convert.ToInt32(in_high) > Convert.ToInt32(price))
                                        {
                                            double down_percent_real = (Convert.ToDouble(price) - Convert.ToDouble(in_high)) / Convert.ToDouble(in_high) * 100;
                                            sell_check_price(string.Format("{0:#,##0}", Convert.ToInt32(price)), percent, Convert.ToInt32(hold), Convert.ToInt32(buy_price), order_number, down_percent_real);
                                        }
                                    }
                                    else
                                    {
                                        sell_check_price(string.Format("{0:#,##0}", Convert.ToInt32(price)), percent, Convert.ToInt32(hold), Convert.ToInt32(buy_price), order_number, 0);
                                    }
                                    sell_runningCodes.Remove(order_number);
                                }
                            }
                        }
                    });

                    gridView1_refresh();
                }
            }
        }

        private void UpdateDataTableHold(string stockCode, string price, string amount)
        {
            lock (table2)
            {
                DataRow[] findRows2 = dtCondStock_hold.Select($"종목코드 = '{stockCode}'");

                if (findRows2.Length != 0)
                {
                    Parallel.ForEach(findRows2, row =>
                    {
                        row["현재가"] = string.Format("{0:#,##0}", Convert.ToInt32(price)); // 새로운 현재가
                        row["평가금액"] = string.Format("{0:#,##0}", Convert.ToInt32(price) * Convert.ToInt32(row["보유수량"].ToString().Replace(",", "")));
                        //
                        double native_price = Convert.ToDouble(price);
                        double buy_price = Convert.ToDouble(row["평균단가"].ToString().Replace(",", ""));
                        double native_percent = (native_price - buy_price) / buy_price * 100;
                        string percent = string.Format("{0:#,##0.00}%", Convert.ToDecimal(native_percent)); // 새로운 수익률
                                                                                                            //
                        row["수익률"] = percent;
                        row["손익금액"] = string.Format("{0:#,##0}", Convert.ToInt32(Convert.ToInt32(row["평가금액"].ToString().Replace(",", "")) * Convert.ToDouble(percent.Replace("%", "")) / 100));
                    });
                }

                // 모든 작업이 완료되었을 때 UI 업데이트
                if (dataGridView2.InvokeRequired)
                {
                    dataGridView2.Invoke((MethodInvoker)delegate
                    {
                        bindingSource2.ResetBindings(false);
                    });
                }
                else
                {
                    bindingSource2.ResetBindings(false);
                }
            }
        }

        //-----------------------종목 편출입------------------------------

        //실시간 종목 편입 이탈
        private void onReceiveRealCondition(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveRealConditionEvent e)
        {
            if (conditionInfo.Count() == 0)
            {
                WriteLog_System("조건식 로딩후 재실행\n");
                telegram_message("조건식 로딩후 재실행\n");
                real_time_stop_btn(this, EventArgs.Empty);
                return;
            }
            //

            //
            switch (e.strType)
            {
                //종목 편입
                case "I":

                    lock (table1)
                    {
                        DataRow[] findRows1 = dtCondStock.Select($"종목코드 = {e.sTrCode}");
                        string time1 = DateTime.Now.ToString("HH:mm:ss");

                        //매도 조건식일 경우
                        if (utility.sell_condition && utility.Fomula_list_sell_text.Split('^')[1] == e.strConditionName)
                        {
                            if (findRows1.Any())
                            {
                                for (int i = 0; i < findRows1.Length; i++)
                                {
                                    if (findRows1[i]["상태"].Equals("매수완료"))
                                    {
                                        lock (sell_lock)
                                        {
                                            if (!sell_runningCodes.ContainsKey(findRows1[i]["주문번호"].ToString()))
                                            {
                                                sell_runningCodes[findRows1[i]["주문번호"].ToString()] = true;
                                                sell_check_condition(e.sTrCode, findRows1[0]["현재가"].ToString(), findRows1[0]["수익률"].ToString(), time1, findRows1[0]["주문번호"].ToString());
                                                sell_runningCodes.Remove(findRows1[i]["주문번호"].ToString());
                                            }
                                        }
                                    }
                                }
                            }
                            return;
                        }

                        //신규종목
                        if (!findRows1.Any())
                        {
                            if (dtCondStock.Rows.Count > 20)
                            {
                                WriteLog_Stock($"[신규편입불가/{e.strConditionName}/{e.sTrCode}] : 최대 감시 종목(20개) 초과 \n");
                                return;
                            }
                            //
                            if (!waiting_Codes.Contains(Tuple.Create(e.sTrCode, e.strConditionName)))
                            {
                                waiting_Codes.Add(Tuple.Create(e.sTrCode, e.strConditionName));
                                axKHOpenAPI1.SetInputValue("종목코드", e.sTrCode);
                                axKHOpenAPI1.CommRqData("조건실시간검색/" + e.strConditionName, "OPT10001", 0, GetScreenNo());
                                waiting_Codes.Remove(Tuple.Create(e.sTrCode, e.strConditionName));
                            }
                        }
                        //INDEPENDENT의 경우 조건식이 다르면 편입한다.
                        else if (utility.buy_INDEPENDENT)
                        {
                            bool isentry = false;
                            bool issingle = false;

                            if (findRows1.Length == 2)
                            {
                                for (int i = 0; i < findRows1.Length; i++)
                                {
                                    if (e.strConditionName.Equals(findRows1[i]["조건식"]) && findRows1[i]["편입"].Equals("이탈") && findRows1[i]["상태"].Equals("대기"))
                                    {
                                        findRows1[i]["편입"] = "편입";
                                        findRows1[i]["편입시각"] = DateTime.Now.ToString("HH:mm:ss");
                                        isentry = true;
                                    }
                                }
                                gridView1_refresh();
                            }
                            else if (findRows1.Length == 1)
                            {
                                if (findRows1[0]["조건식"].Equals("전일보유"))
                                {
                                    WriteLog_Stock($"[기존종목/INDEPENDENT편입/{e.strConditionName}] : {findRows1[0]["종목명"]}({e.sTrCode}) 전일 보유 종목 \n");
                                }
                                else if (e.strConditionName.Equals(findRows1[0]["조건식"]))
                                {
                                    if (findRows1[0]["편입"].Equals("이탈") && findRows1[0]["상태"].Equals("대기"))
                                    {
                                        findRows1[0]["편입"] = "편입";
                                        findRows1[0]["편입시각"] = DateTime.Now.ToString("HH:mm:ss");
                                        //
                                        gridView1_refresh();
                                        //
                                        isentry = true;
                                    }
                                }
                                else
                                {
                                    issingle = true;
                                }
                            }

                            if (isentry)
                            {
                                WriteLog_Stock($"[기존종목/INDEPENDENT편입/{e.strConditionName}] : {findRows1[0]["종목명"]}({e.sTrCode})\n");

                                //정렬
                                dtCondStock = dtCondStock.AsEnumerable().OrderBy(row => row.Field<string>("편입시각")).CopyToDataTable();
                                bindingSource.DataSource = dtCondStock;
                                //
                                gridView1_refresh();
                                //
                                return;
                            }

                            if (issingle)
                            {

                                if (dtCondStock.Rows.Count > 20)
                                {
                                    WriteLog_Stock($"[신규편입불가/{e.strConditionName}/{e.sTrCode}] : 최대 감시 종목(20개) 초과 \n");
                                    return;
                                }
                                //
                                if (!waiting_Codes.Contains(Tuple.Create(e.sTrCode, e.strConditionName)))
                                {
                                    waiting_Codes.Add(Tuple.Create(e.sTrCode, e.strConditionName));
                                    axKHOpenAPI1.SetInputValue("종목코드", e.sTrCode);
                                    axKHOpenAPI1.CommRqData("조건실시간검색/" + e.strConditionName, "OPT10001", 0, GetScreenNo());
                                    waiting_Codes.Remove(Tuple.Create(e.sTrCode, e.strConditionName));
                                }
                            }

                        }
                        //기존에 포함됬던 종목
                        else
                        {
                            //OR 경우 종목당 한번만 포함된다.
                            if (utility.buy_OR && findRows1[0]["편입"].Equals("이탈") && findRows1[0]["상태"].Equals("대기"))
                            {
                                findRows1[0]["편입"] = "편입";
                                findRows1[0]["편입시각"] = DateTime.Now.ToString("HH:mm:ss");

                                WriteLog_Stock("[기존종목/재편입] : " + e.sTrCode + " - " + findRows1[0]["종목명"] + " - " + e.strConditionName + "\n");

                                //정렬
                                dtCondStock = dtCondStock.AsEnumerable().OrderBy(row => row.Field<string>("편입시각")).CopyToDataTable();
                                bindingSource.DataSource = dtCondStock;
                                //
                                gridView1_refresh();

                                return;
                            }

                            //AND의 경우 종목당 한번만 포함된다.
                            if (utility.buy_AND && findRows1[0]["편입"].Equals("이탈") && findRows1[0]["상태"].Equals("호출"))
                            {
                                findRows1[0]["편입"] = "편입";
                                findRows1[0]["편입시각"] = DateTime.Now.ToString("HH:mm:ss");
                                findRows1[0]["조건식"] = e.strConditionName.Trim();

                                WriteLog_Stock("[기존종목/AND재편입] : " + e.sTrCode + " - " + findRows1[0]["종목명"] + " - " + e.strConditionName + "\n");

                                //정렬
                                dtCondStock = dtCondStock.AsEnumerable().OrderBy(row => row.Field<string>("편입시각")).CopyToDataTable();
                                bindingSource.DataSource = dtCondStock;
                                //
                                gridView1_refresh();

                                return;
                            }

                            //AND의 경우 포함된 종목이 한번 더 발견되어야 매수를 시작할 수 있다.
                            if (utility.buy_AND && findRows1[0]["편입"].Equals("이탈") && findRows1[0]["상태"].Equals("호출"))
                            {
                                string code = findRows1[0]["종목코드"].ToString();
                                string code_name = findRows1[0]["종목명"].ToString();
                                string current_price = findRows1[0]["현재가"].ToString();
                                string high1 = findRows1[0]["상한가"].ToString();

                                findRows1[0]["편입"] = "편입";

                                //정렬
                                dtCondStock = dtCondStock.AsEnumerable().OrderBy(row => row.Field<string>("편입시각")).CopyToDataTable();
                                bindingSource.DataSource = dtCondStock;
                                //
                                gridView1_refresh();

                                WriteLog_Stock("[기존종목/AND완전재편입] : " + e.sTrCode + " - " + findRows1[0]["종목명"] + " - " + e.strConditionName + "\n");

                                if (!buy_runningCodes.ContainsKey(code))
                                {
                                    buy_runningCodes[code] = true;
                                    buy_check(code, code_name, current_price, time1, high1, false, e.strConditionName);
                                    buy_runningCodes.Remove(code);
                                }

                                return;
                            }
                        }

                        WriteLog_Stock($"[기존종목/편입/{e.strConditionName}] : {findRows1[0]["종목명"]}({e.sTrCode}) 재편입 대상 없음\n");
                    }

                    break;

                //종목 이탈
                case "D":

                    lock (table1)
                    {
                        //검출된 종목이 이미 이탈했다면(기본적으로 I D가 번갈아가면서 발생하므로 그럴릴 없음? 있는듯?)
                        DataRow[] findRows = dtCondStock.Select($"종목코드 = {e.sTrCode}");

                        if (findRows.Length == 0)
                        {
                            WriteLog_Stock($"[기존종목/이탈/{e.strConditionName}] : {e.sTrCode} 이탈 대상 없음\n");
                            return;
                        }

                        //매도 조건식일 경우
                        if (utility.sell_condition && utility.Fomula_list_sell_text.Split('^')[1] == e.strConditionName) return;

                        if (utility.buy_OR && findRows[0]["편입"].Equals("편입") && findRows[0]["상태"].Equals("대기"))
                        {
                            findRows[0]["편입"] = "이탈";
                            findRows[0]["이탈시각"] = DateTime.Now.ToString("HH:mm:ss");
                            //
                            WriteLog_Stock($"[기존종목/OR이탈/{e.strConditionName}] : {findRows[0]["종목명"]}({e.sTrCode})\n");
                            //
                            if (findRows[0]["상태"].Equals("매도완료") & findRows.Length == 1)
                            {
                                axKHOpenAPI1.SetRealRemove("ALL", e.sTrCode);
                            }
                            //
                            gridView1_refresh();
                        }
                        else if (utility.buy_AND)
                        {
                            if (findRows[0]["편입"].Equals("편입") && findRows[0]["상태"].Equals("호출"))
                            {
                                findRows[0]["편입"] = "이탈";
                                findRows[0]["이탈시각"] = DateTime.Now.ToString("HH:mm:ss");
                                WriteLog_Stock($"[기존종목/AND이탈/{e.strConditionName}] : {findRows[0]["종목명"]}({e.sTrCode}) 완전이탈 \n");
                                //
                                gridView1_refresh();
                            }
                            else if (findRows[0]["편입"].Equals("편입") && findRows[0]["상태"].Equals("대기"))
                            {
                                findRows[0]["상태"] = "호출";
                                WriteLog_Stock($"[기존종목/AND이탈/{e.strConditionName}] : {findRows[0]["종목명"]}({e.sTrCode}) 부분이탈\n");
                                //
                                gridView1_refresh();
                            }
                        }
                        else if (utility.buy_INDEPENDENT)
                        {
                            for (int i = 0; i < findRows.Length; i++)
                            {
                                if (e.strConditionName.Equals(findRows[i]["조건식"]) && findRows[i]["편입"].Equals("편입") && findRows[i]["상태"].Equals("대기"))
                                {
                                    findRows[i]["편입"] = "이탈";
                                    findRows[i]["이탈시각"] = DateTime.Now.ToString("HH:mm:ss");
                                    WriteLog_Stock($"[기존종목/INDEPENDENT이탈/{e.strConditionName}] : {findRows[i]["종목명"]}({e.sTrCode})\n");
                                    //
                                    if (findRows[i]["상태"].Equals("매도완료") & findRows.Length == 1)
                                    {
                                        axKHOpenAPI1.SetRealRemove("ALL", e.sTrCode);
                                    }
                                    //
                                    gridView1_refresh();

                                    break;
                                }
                            }
                        }

                        WriteLog_Stock($"[기존종목/이탈/{e.strConditionName}] : {findRows[0]["종목명"]}({e.sTrCode}) 재이탈 대상 없음\n");
                    }

                    break;
            }
        }

        //--------------편입 이후 종목에 대한 매수 매도 감시(200ms)---------------------

        //timer3(200ms) : 09시 30분 이후 매수 시작인 것에 대하여 이전에 진입한 종목 중 편입 상태인 종목에 대한 매수
        private void Transfer_Timer(object sender, EventArgs e)
        {
            order_cancel_check();

            //편입 상태 이면서 대기 종목인 녀석에 대한 검증
            if (!index_buy)
            {
                account_check_buy();
            }

            // 지수연동청산
            if (index_clear)
            {
                account_check_sell();
            }

            //매도 완료 종목에 대한 청산 검증
            if (utility.clear_sell || utility.clear_sell_mode)
            {
                //청산 매도 시간 확인
                TimeSpan t_code = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss"));
                TimeSpan t_start = TimeSpan.Parse(utility.clear_sell_start);
                TimeSpan t_end = TimeSpan.Parse(utility.clear_sell_end);

                if (t_code.CompareTo(t_start) < 0 || t_code.CompareTo(t_end) > 0) return;

                account_check_sell();
            }

        }

        //이전 매수 종목 매수 확인
        private void account_check_buy()
        {
            string time = DateTime.Now.ToString("HH:mm:ss");

            lock (table1)
            {
                //특저 열 추출
                DataColumn columnEditColumn = dtCondStock.Columns["편입"];
                DataColumn columnStateColumn = dtCondStock.Columns["상태"];

                //AsEnumerable()은 DataTable의 행을 열거형으로 변환
                var filteredRows = dtCondStock.AsEnumerable()
                                            .Where(row => row.Field<string>(columnEditColumn) == "편입" &&
                                                          row.Field<string>(columnStateColumn) == "대기" || row.Field<string>(columnStateColumn) == "주문")
                                            .ToList();

                //검출 종목에 대한 확인
                if (filteredRows.Count > 0)
                {
                    foreach (DataRow row in filteredRows)
                    {
                        //자동 시간전 검출 매수 확인
                        TimeSpan t_code = TimeSpan.Parse(row.Field<string>("편입시각"));
                        TimeSpan t_start = TimeSpan.Parse(utility.buy_condition_start);
                        if (utility.before_time_deny)
                        {
                            if (t_code.CompareTo(t_start) < 0) continue;
                            // result가 0보다 작으면 time1 < time2
                            // result가 0이면 time1 = time2
                            // result가 0보다 크면 time1 > time2
                        }

                        //중복 
                        lock (buy_lock)
                        {
                            string code = row.Field<string>("종목코드");

                            if (!buy_runningCodes.ContainsKey(code))
                            {
                                buy_runningCodes[code] = true;
                                buy_check(code, row.Field<string>("종목명"), row.Field<string>("현재가").Replace(",", ""), time, row.Field<string>("상한가"), true, row.Field<string>("조건식"));
                                buy_runningCodes.Remove(code);
                            }
                        }

                        System.Threading.Thread.Sleep(delay1);
                    }
                }
            }
        }

        //자동 취소 확인
        private void order_cancel_check()
        {
            if (utility.term_for_non_buy)
            {
                lock (table1)
                {
                    DataRow[] findRows = dtCondStock.AsEnumerable().Where(row => row.Field<string>("상태") == "매수중").ToArray();

                    if (findRows.Any())
                    {
                        TimeSpan t_now = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss"));
                        //
                        for (int i = 0; i < findRows.Length; i++)
                        {
                            TimeSpan t_last = TimeSpan.Parse(findRows[i]["매매진입"].ToString());
                            //
                            if (t_now - t_last >= TimeSpan.FromMilliseconds(Convert.ToInt32(utility.term_for_non_buy_text)))
                            {
                                //string trade_type, string order_number, string gubun, string code_name, string code, string order_acc
                                order_close("매수", findRows[i]["주문번호"].ToString(), findRows[i]["종목명"].ToString(), findRows[i]["종목코드"].ToString(), findRows[i]["보유수량"].ToString().Split('/')[1]);
                            }

                            System.Threading.Thread.Sleep(delay1);
                        }
                    }
                }
            }

            if (utility.term_for_non_buy)
            {
                lock (table1)
                {
                    DataRow[] findRows = dtCondStock.AsEnumerable().Where(row => row.Field<string>("상태") == "매도중").ToArray();

                    if (findRows.Any())
                    {
                        TimeSpan t_now = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss"));
                        //
                        for (int i = 0; i < findRows.Length; i++)
                        {
                            TimeSpan t_last = TimeSpan.Parse(findRows[i]["매매진입"].ToString());
                            //
                            if (t_now - t_last >= TimeSpan.FromMilliseconds(Convert.ToInt32(utility.term_for_non_buy_text)))
                            {
                                order_close("매도", findRows[i]["주문번호"].ToString(), findRows[i]["종목명"].ToString(), findRows[i]["종목코드"].ToString(), findRows[i]["보유수량"].ToString().Split('/')[1]);
                            }

                            System.Threading.Thread.Sleep(delay1);
                        }
                    }
                }
            }
        }

        //청산 확인
        private void account_check_sell()
        {
            if (utility.clear_sell)
            {
                lock (table1)
                {
                    //특저 열 추출
                    DataColumn columnStateColumn = dtCondStock.Columns["상태"];

                    //AsEnumerable()은 DataTable의 행을 열거형으로 변환
                    var filteredRows = dtCondStock.AsEnumerable().Where(row => row.Field<string>(columnStateColumn) == "매수완료").ToList();

                    //검출 종목에 대한 확인
                    if (filteredRows.Count > 0)
                    {
                        foreach (DataRow row in filteredRows)
                        {
                            lock (sell_lock)
                            {
                                string order_num = row.Field<string>("주문번호"); ;
                                if (!sell_runningCodes.ContainsKey(order_num))
                                {
                                    sell_runningCodes[order_num] = true;
                                    sell_order("Nan", "청산매도/시간", order_num, row.Field<string>("수익률"));
                                    sell_runningCodes.Remove(order_num);
                                }
                            }

                            System.Threading.Thread.Sleep(delay1);
                        }
                    }
                }

            }
            else if (utility.clear_sell_mode)
            {
                if (!utility.clear_sell_profit && !utility.clear_sell_loss)
                {
                    WriteLog_System("청산 모드 선택 요청\n");
                    telegram_message("청산 모드 선택 요청\n");
                    return;
                }

                lock (table1)
                {
                    //특저 열 추출
                    DataColumn columnStateColumn = dtCondStock.Columns["상태"];

                    //AsEnumerable()은 DataTable의 행을 열거형으로 변환
                    var filteredRows = dtCondStock.AsEnumerable().Where(row => row.Field<string>(columnStateColumn) == "매수완료").ToList();

                    //검출 종목에 대한 확인
                    if (filteredRows.Count > 0)
                    {
                        foreach (DataRow row in filteredRows)
                        {
                            lock (sell_lock)
                            {
                                string order_num = row.Field<string>("주문번호"); ;
                                if (!sell_runningCodes.ContainsKey(order_num))
                                {
                                    sell_runningCodes[order_num] = true;
                                    //
                                    double percent_edit = double.Parse(row.Field<string>("수익률").Replace("%", ""));
                                    double profit = double.Parse(utility.clear_sell_profit_text);
                                    double loss = double.Parse(utility.clear_sell_loss_text);
                                    if (utility.clear_sell_profit && percent_edit >= profit)
                                    {
                                        sell_order("Nan", "청산매도/수익", order_num, row.Field<string>("수익률"));
                                    }
                                    //
                                    if (utility.clear_sell_loss && percent_edit <= -loss)
                                    {
                                        sell_order("Nan", "청산매도/손실", order_num, row.Field<string>("수익률"));
                                    }
                                    //
                                    sell_runningCodes.Remove(order_num);
                                }
                            }

                            System.Threading.Thread.Sleep(delay1);
                        }
                    }
                }
            }
        }

        //--------------실시간 매수 조건 확인 및 매수 주문---------------------

        private string last_buy_time = "08:59:59";

        //매수 가능한 상태인지 확인
        private string buy_check(string code, string code_name, string price, string time, string high, bool check, string condition_name)
        {
            //지수 확인
            if (index_buy)
            {
                return "대기";
            }

            if (index_stop)
            {
                return "대기";
            }

            //매수 시간 확인
            TimeSpan t_code = TimeSpan.Parse(time);
            TimeSpan t_start = TimeSpan.Parse(utility.buy_condition_start);
            TimeSpan t_end = TimeSpan.Parse(utility.buy_condition_end);

            if (t_code.CompareTo(t_start) < 0 || t_code.CompareTo(t_end) > 0)
            {
                // result가 0보다 작으면 time1 < time2
                // result가 0이면 time1 = time2
                // result가 0보다 크면 time1 > time2
                return "대기";
            }

            //보유 종목 수 확인
            string[] hold_status = max_hoid.Text.Split('/');
            int hold = Convert.ToInt32(hold_status[0]);
            int hold_max = Convert.ToInt32(hold_status[1]);
            if (hold >= hold_max) return "대기";

            //매매 횟수 확인
            if (utility.buy_INDEPENDENT)
            {
                string[] trade_status = maxbuy_acc.Text.Split('/');
                string[] condition_num = utility.Fomula_list_buy_text.Split(',');
                for (int i = 0; i < condition_num.Length; i++)
                {
                    if (condition_num[i].Split('^')[1].Equals(condition_name))
                    {
                        if (Convert.ToInt32(trade_status[i]) >= Convert.ToInt32(trade_status[trade_status.Length - 1]))
                        {
                            return "대기";
                        }
                        break;
                    }
                }
            }
            else
            {
                string[] trade_status = maxbuy_acc.Text.Split('/');
                int trade_status_already = Convert.ToInt32(trade_status[0]);
                int trade_status_limit = Convert.ToInt32(trade_status[1]);
                if (trade_status_already >= trade_status_limit) return "대기";
            }

            //보유 종목 매수 확인
            if (utility.hold_deny)
            {
                lock (table2)
                {
                    DataRow[] findRows = dtCondStock_hold.Select($"종목코드 = {code}");
                    if (findRows.Any())
                    {
                        return "대기";
                    }
                }
            }

            //최소 주문간 간격 750ms
            if (utility.term_for_buy)
            {
                TimeSpan t_now = TimeSpan.Parse(time);
                TimeSpan t_last = TimeSpan.Parse(last_buy_time);

                if (t_now - t_last < TimeSpan.FromMilliseconds(Convert.ToInt32(utility.term_for_buy_text)))
                {
                    //WriteLog_Order($"[매수간격] 설정({utility.term_for_buy_text}), 현재({(t_now - t_last2).ToString()})\n");
                    return "대기";
                }
                last_buy_time = t_now.ToString();
            }
            else
            {
                TimeSpan t_now = TimeSpan.Parse(time);
                TimeSpan t_last = TimeSpan.Parse(last_buy_time);

                if (t_now - t_last < TimeSpan.FromMilliseconds(delay1))
                {
                    //WriteLog_Order($"[매수간격] 설정({utility.term_for_buy_text}), 현재({(t_now - t_last2).ToString()})\n");
                    return "대기";
                }
                last_buy_time = t_now.ToString();
            }

            //매수 주문(1초에 5회)
            //주문 방식 구분
            string[] order_method = buy_condtion_method.Text.Split('/');

            //시장가 주문
            if (order_method[0].Equals("시장가"))
            {

                //시장가에 대하여 주문 가능 개수 계산 => 기억해야 함 / 종목당매수금액 / 종목당매수수량 / 종목당매수비율 / 종목당최대매수금액
                //User_money.Text;
                int order_acc_market = buy_order_cal(Convert.ToInt32(high.Replace(",", "")));

                if (order_acc_market == 0)
                {
                    WriteLog_Order($"[매수주문/시장가/주문실패] : " + code_name + "(" + code + ") " + "예수금 부족 0개 주문\n");
                    telegram_message($"[매수주문/시장가/주문실패] : " + code_name + "(" + code + ") " + "예수금 부족 0개 주문\n");

                    if (check)
                    {
                        lock(table1)
                        {
                            DataRow[] findRows = dtCondStock.AsEnumerable().Where(row2 => row2.Field<string>("종목코드") == code && row2.Field<string>("조건식") == condition_name).ToArray();

                            findRows[0]["상태"] = "부족";

                            gridView1_refresh();
                        }
                    }

                    return "부족";
                }

                WriteLog_Order($"[매수주문/시장가/주문접수/{condition_name}] : {code_name}({code}) {order_acc_market} 개\n");
                telegram_message($"[매수주문/시장가/주문접수/{condition_name}] : {code_name}({code}) {order_acc_market} 개\n");

                //오류를 대비하여 사전에 입력

                // 보유 수량 업데이트
                string[] hold_status_update = max_hoid.Text.Split('/');
                int hold_update = Convert.ToInt32(hold_status_update[0]);
                int hold_max_update = Convert.ToInt32(hold_status_update[1]);
                max_hoid.Text = (hold_update + 1) + "/" + hold_max_update;

                string time2 = DateTime.Now.ToString("HH:mm:ss");

                if (check)
                {
                    lock (table1)
                    {
                        //편입 차트 상태 '매수중' 변경
                        DataRow[] findRows = dtCondStock.AsEnumerable().Where(row2 => row2.Field<string>("종목코드") == code && row2.Field<string>("조건식") == condition_name).ToArray();

                        findRows[0]["상태"] = "매수중";
                        findRows[0]["보유수량"] = "0/" + order_acc_market;
                        findRows[0]["매매진입"] = time2;
                        gridView1_refresh();
                    }
                }


                //매매 횟수업데이트
                if (utility.buy_INDEPENDENT)
                {
                    string[] trade_status = maxbuy_acc.Text.Split('/');
                    string[] condition_num = utility.Fomula_list_buy_text.Split(',');
                    for (int i = 0; i < condition_num.Length; i++)
                    {
                        if (condition_num[i].Split('^')[1].Equals(condition_name))
                        {
                            trade_status[i] = Convert.ToString(Convert.ToInt32(trade_status[i]) + 1);
                            maxbuy_acc.Text = String.Join("/", trade_status);
                            break;
                        }
                    }
                }
                else
                {
                    string[] trade_status_update = maxbuy_acc.Text.Split('/');
                    int trade_status_already_update = Convert.ToInt32(trade_status_update[0]);
                    int trade_status_limit_update = Convert.ToInt32(trade_status_update[1]);
                    maxbuy_acc.Text = trade_status_already_update + 1 + "/" + trade_status_limit_update;
                }

                int error = axKHOpenAPI1.SendOrder("시장가매수", GetScreenNo(), utility.setting_account_number, 1, code, order_acc_market, 0, "03", "");

                if (error == 0)
                {
                    //
                    WriteLog_Order($"[매수주문/시장가/주문성공/{condition_name}] : {code_name}({code}) {order_acc_market} 개\n");
                    telegram_message($"[매수주문/시장가/주문성공/{condition_name}] : {code_name}({code}) {order_acc_market} 개\n");

                    return "매수중/" + order_acc_market;

                }
                else if (error == -308)
                {
                    WriteLog_Order($"[매수주문/시장가/주문실패/{condition_name}] : {code_name}({code}) 초당 5회 이상 주문 블가)\n");
                    telegram_message($"[매수주문/시장가/주문실패/{condition_name}] : {code_name}({code}) 초당 5회 이상 주문 블가)\n");

                    //보유 수량 업데이트
                    string[] hold_status_update2 = max_hoid.Text.Split('/');
                    int hold_update2 = Convert.ToInt32(hold_status_update2[0]);
                    int hold_max_update2 = Convert.ToInt32(hold_status_update2[1]);
                    max_hoid.Text = (hold_update2 - 1) + "/" + hold_max_update2;

                    if (check)
                    {
                        lock (table1)
                        {
                            //편입 차트 상태 '매수중' 변경
                            DataRow[] findRows = dtCondStock.AsEnumerable().Where(row2 => row2.Field<string>("종목코드") == code && row2.Field<string>("조건식") == condition_name).ToArray();

                            findRows[0]["상태"] = "대기";
                            findRows[0]["보유수량"] = "0/0";
                            findRows[0]["매매진입"] = "-";
                            gridView1_refresh();
                        }
                    }

                    //매매 횟수업데이트
                    if (utility.buy_INDEPENDENT)
                    {
                        string[] trade_status = maxbuy_acc.Text.Split('/');
                        string[] condition_num = utility.Fomula_list_buy_text.Split(',');
                        for (int i = 0; i < condition_num.Length; i++)
                        {
                            if (condition_num[i].Split('^')[1].Equals(condition_name))
                            {
                                trade_status[i] = Convert.ToString(Convert.ToInt32(trade_status[i]) - 1);
                                maxbuy_acc.Text = String.Join("/", trade_status);
                                break;
                            }
                        }
                    }
                    else
                    {
                        string[] trade_status_update = maxbuy_acc.Text.Split('/');
                        int trade_status_already_update = Convert.ToInt32(trade_status_update[0]);
                        int trade_status_limit_update = Convert.ToInt32(trade_status_update[1]);
                        maxbuy_acc.Text = trade_status_already_update - 1 + "/" + trade_status_limit_update;
                    }

                    return "대기";
                }
                else
                {
                    WriteLog_Order($"[매수주문/시장가/주문실패/{condition_name}] : {code_name}({code}) 에러코드(" + error + "\n");
                    telegram_message($"[매수주문/시장가/주문실패/{condition_name}] : {code_name}({code}) 에러코드(" + error + "\n");

                    //보유 수량 업데이트
                    string[] hold_status_update2 = max_hoid.Text.Split('/');
                    int hold_update2 = Convert.ToInt32(hold_status_update2[0]);
                    int hold_max_update2 = Convert.ToInt32(hold_status_update2[1]);
                    max_hoid.Text = (hold_update2 - 1) + "/" + hold_max_update2;

                    if (check)
                    {
                        lock (table1)
                        {
                            //편입 차트 상태 '매수중' 변경
                            DataRow[] findRows = dtCondStock.AsEnumerable().Where(row2 => row2.Field<string>("종목코드") == code && row2.Field<string>("조건식") == condition_name).ToArray();

                            findRows[0]["상태"] = "대기";
                            findRows[0]["보유수량"] = "0/0";
                            findRows[0]["매매진입"] = "-";
                            gridView1_refresh();
                        }
                    }

                    //매매 횟수업데이트
                    if (utility.buy_INDEPENDENT)
                    {
                        string[] trade_status = maxbuy_acc.Text.Split('/');
                        string[] condition_num = utility.Fomula_list_buy_text.Split(',');
                        for (int i = 0; i < condition_num.Length; i++)
                        {
                            if (condition_num[i].Split('^')[1].Equals(condition_name))
                            {
                                trade_status[i] = Convert.ToString(Convert.ToInt32(trade_status[i]) - 1);
                                maxbuy_acc.Text = String.Join("/", trade_status);
                                break;
                            }
                        }
                    }
                    else
                    {
                        string[] trade_status_update = maxbuy_acc.Text.Split('/');
                        int trade_status_already_update = Convert.ToInt32(trade_status_update[0]);
                        int trade_status_limit_update = Convert.ToInt32(trade_status_update[1]);
                        maxbuy_acc.Text = trade_status_already_update - 1 + "/" + trade_status_limit_update;
                    }

                    return "대기";
                }
            }
            //지정가 주문
            else
            {
                //지정가 계산
                int edited_price_hoga = hoga_cal(Convert.ToInt32(price.Replace(",", "")), order_method[1].Equals("현재가") ? 0 : Convert.ToInt32(order_method[1].Replace("호가", "")), Convert.ToInt32(high.Replace(",", "")));

                //지정가에 대하여 주문 가능 개수 계산
                int order_acc = buy_order_cal(edited_price_hoga);

                if (order_acc == 0)
                {
                    WriteLog_Order($"[매수주문/지정가/주문실패] : " + code_name + "(" + code + ") " + "예수금 부족 0개 주문\n");
                    telegram_message($"[매수주문/지정가/주문실패] : " + code_name + "(" + code + ") " + "예수금 부족 0개 주문\n");

                    if (check)
                    {
                        lock (table1)
                        {
                            DataRow[] findRows = dtCondStock.AsEnumerable().Where(row2 => row2.Field<string>("종목코드") == code && row2.Field<string>("조건식") == condition_name).ToArray();

                            findRows[0]["상태"] = "부족";

                            gridView1_refresh();
                        }
                    }

                    return "부족";
                }              

                WriteLog_Order($"[매수주문/지정가/주문접수/{condition_name}] : {code_name}({code}) {order_acc}개 현재가({price}) 주문가({edited_price_hoga})원 주문방식({order_method[1]}\n");
                telegram_message($"[매수주문/지정가/주문접수/{condition_name}] : {code_name}({code}) {order_acc}개 현재가({price}) 주문가({edited_price_hoga})원 주문방식({order_method[1]}\n");

                //보유 수량 업데이트
                string[] hold_status_update = max_hoid.Text.Split('/');
                int hold_update = Convert.ToInt32(hold_status_update[0]);
                int hold_max_update = Convert.ToInt32(hold_status_update[1]);
                max_hoid.Text = (hold_update + 1) + "/" + hold_max_update;

                string time2 = DateTime.Now.ToString("HH:mm:ss");

                //기존에 포함된 종목이면 따로 변경해줘야 함
                if (check)
                {
                    lock (table1)
                    {
                        //편입 차트 상태 '매수중' 변경
                        DataRow[] findRows = dtCondStock.Select($"종목코드 = {code}");

                        findRows[0]["상태"] = "매수중";
                        findRows[0]["보유수량"] = "0/" + order_acc;
                        findRows[0]["매매진입"] = time2;
                        gridView1_refresh();
                    }
                }

                //매매 횟수업데이트
                if (utility.buy_INDEPENDENT)
                {
                    string[] trade_status = maxbuy_acc.Text.Split('/');
                    string[] condition_num = utility.Fomula_list_buy_text.Split(',');
                    for (int i = 0; i < condition_num.Length; i++)
                    {
                        if (condition_num[i].Split('^')[1].Equals(condition_name))
                        {
                            trade_status[i] = Convert.ToString(Convert.ToInt32(trade_status[i]) + 1);
                            maxbuy_acc.Text = String.Join("/", trade_status);
                            break;
                        }
                    }
                }
                else
                {
                    string[] trade_status_update = maxbuy_acc.Text.Split('/');
                    int trade_status_already_update = Convert.ToInt32(trade_status_update[0]);
                    int trade_status_limit_update = Convert.ToInt32(trade_status_update[1]);
                    maxbuy_acc.Text = trade_status_already_update + 1 + "/" + trade_status_limit_update;
                }

                int error = axKHOpenAPI1.SendOrder("지정가매수", GetScreenNo(), utility.setting_account_number, 1, code, order_acc, edited_price_hoga, "00", "");

                if (error == 0)
                {
                    //
                    WriteLog_Order($"[매수주문/지정가/접수성공/{condition_name}] : {code_name}({code}) {order_acc}개 현재가({price}) 주문가({edited_price_hoga})원 주문방식({order_method[1]}\n");
                    telegram_message($"[매수주문/지정가/접수성공/{condition_name}] : {code_name}({code}) {order_acc}개 현재가({price}) 주문가({edited_price_hoga})원 주문방식({order_method[1]}\n");

                    return "매수중/" + order_acc;

                }
                else if (error == -308)
                {
                    WriteLog_Order($"[매수주문/지정가/주문실패/{condition_name}] : {code_name}({code}) 초당 5회 이상 주문 불가)\n");
                    telegram_message($"[매수주문/지정가/주문실패/{condition_name}] : {code_name}({code}) 초당 5회 이상 주문 불가)\n");

                    //보유 수량 업데이트
                    string[] hold_status_update2 = max_hoid.Text.Split('/');
                    int hold_update2 = Convert.ToInt32(hold_status_update2[0]);
                    int hold_max_update2 = Convert.ToInt32(hold_status_update2[1]);
                    max_hoid.Text = (hold_update2 - 1) + "/" + hold_max_update2;

                    if (check)
                    {
                        lock (table1)
                        {
                            //편입 차트 상태 '매수중' 변경
                            DataRow[] findRows = dtCondStock.AsEnumerable().Where(row2 => row2.Field<string>("종목코드") == code && row2.Field<string>("조건식") == condition_name).ToArray();

                            findRows[0]["상태"] = "대기";
                            findRows[0]["보유수량"] = "0/0";
                            findRows[0]["매매진입"] = time2;
                            gridView1_refresh();
                        }
                    }

                    //매매 횟수업데이트
                    if (utility.buy_INDEPENDENT)
                    {
                        string[] trade_status = maxbuy_acc.Text.Split('/');
                        string[] condition_num = utility.Fomula_list_buy_text.Split(',');
                        for (int i = 0; i < condition_num.Length; i++)
                        {
                            if (condition_num[i].Split('^')[1].Equals(condition_name))
                            {
                                trade_status[i] = Convert.ToString(Convert.ToInt32(trade_status[i]) - 1);
                                maxbuy_acc.Text = String.Join("/", trade_status);
                                break;
                            }
                        }
                    }
                    else
                    {
                        string[] trade_status_update = maxbuy_acc.Text.Split('/');
                        int trade_status_already_update = Convert.ToInt32(trade_status_update[0]);
                        int trade_status_limit_update = Convert.ToInt32(trade_status_update[1]);
                        maxbuy_acc.Text = trade_status_already_update - 1 + "/" + trade_status_limit_update;
                    }

                    return "대기";
                }
                else
                {
                    WriteLog_Order($"[매수주문/지정가/주문실패/{condition_name}] : {code_name}({code}) 에러코드(" + error + "\n");
                    telegram_message($"[매수주문/지정가/주문실패/{condition_name}] : {code_name}({code}) 에러코드(" + error + "\n");

                    //보유 수량 업데이트
                    string[] hold_status_update2 = max_hoid.Text.Split('/');
                    int hold_update2 = Convert.ToInt32(hold_status_update2[0]);
                    int hold_max_update2 = Convert.ToInt32(hold_status_update2[1]);
                    max_hoid.Text = (hold_update2 - 1) + "/" + hold_max_update2;

                    if (check)
                    {
                        lock (table1)
                        {
                            //편입 차트 상태 '매수중' 변경
                            DataRow[] findRows = dtCondStock.AsEnumerable().Where(row2 => row2.Field<string>("종목코드") == code && row2.Field<string>("조건식") == condition_name).ToArray();

                            findRows[0]["상태"] = "대기";
                            findRows[0]["보유수량"] = "0/0";
                            findRows[0]["매매진입"] = time2;
                            gridView1_refresh();
                        }
                    }

                    //매매 횟수업데이트
                    if (utility.buy_INDEPENDENT)
                    {
                        string[] trade_status = maxbuy_acc.Text.Split('/');
                        string[] condition_num = utility.Fomula_list_buy_text.Split(',');
                        for (int i = 0; i < condition_num.Length; i++)
                        {
                            if (condition_num[i].Split('^')[1].Equals(condition_name))
                            {
                                trade_status[i] = Convert.ToString(Convert.ToInt32(trade_status[i]) - 1);
                                maxbuy_acc.Text = String.Join("/", trade_status);
                                break;
                            }
                        }
                    }
                    else
                    {
                        string[] trade_status_update = maxbuy_acc.Text.Split('/');
                        int trade_status_already_update = Convert.ToInt32(trade_status_update[0]);
                        int trade_status_limit_update = Convert.ToInt32(trade_status_update[1]);
                        maxbuy_acc.Text = trade_status_already_update - 1 + "/" + trade_status_limit_update;
                    }

                    return "대기";
                }
            }
        }

        //매수 주문 수량 계산
        private int buy_order_cal(int price)
        {
            //
            int current_balance = Convert.ToInt32(User_money.Text.Replace(",", ""));
            //
            if (!Authentication_Check && current_balance > sample_balance)
            {
                current_balance = sample_balance;
            }

            //
            int max_buy = Convert.ToInt32(utility.maxbuy);
            //
            if (utility.buy_per_percent)
            {
                //매수비율
                int ratio = Convert.ToInt32(utility.buy_per_percent_text);

                // 예수금 활용 비율 계산 (0.XX 형태로 변환)
                double buy_Percent = ratio / 100.0;

                // 주문 가능 금액 계산 (예수금 * 활용 비율)
                double order_Amount = current_balance * buy_Percent;

                // 상한가 기준 최대 주문 가능 수량 계산 (내림)
                int quantity = (int)Math.Floor(order_Amount / (double)price);

                // 실제 주문 금액 계산
                double actual_Order_Amount = quantity * price;

                //종목당 최대 매수 금액 비교
                if (actual_Order_Amount > (double)max_buy)
                {
                    quantity = (int)Math.Floor((double)max_buy / price);
                }
                // 실제 주문 금액이 주문 가능 금액을 초과하는 경우 수량 조정
                else if (actual_Order_Amount > (double)order_Amount)
                {
                    quantity--;
                }

                return quantity;
            }
            else if (utility.buy_per_amount)
            {
                //매수개수
                int max_amount = Convert.ToInt32(utility.buy_per_amount_text);

                // 상한가 기준 최대 주문 가능 금액 계산
                double max_Order_Amount = max_amount * price;

                // 예수금 - 최대 주문 가능 금액 - 종목당최대주문금액  중 작은 값으로 실제 주문 가능 금액 결정
                double order_Amount = Math.Min(Math.Min(current_balance, (int)max_Order_Amount), max_buy);

                // 실제 주문 가능 수량 계산 (내림)
                return (int)Math.Floor(order_Amount / price);
            }
            else
            {
                //매수금액
                int max_amount = Convert.ToInt32(utility.buy_per_price_text);

                // 예수금과 - 최대 주문 가능 금액 - 종목당최대주문금액 중 작은 값으로 실제 주문 가능 금액 결정
                double order_Amount = Math.Min(Math.Min(current_balance, max_amount), max_buy);

                // 실제 주문 가능 수량 계산 (내림)
                return (int)Math.Floor(order_Amount / price);
            }
        }

        //--------------실시간 매도 조건 확인---------------------  

        //조건식 매도
        private void sell_check_condition(string code, string price, string percent, string time, string order_num)
        {
            TimeSpan t_code = TimeSpan.Parse(time);
            TimeSpan t_start = TimeSpan.Parse(utility.sell_condition_start);
            TimeSpan t_end = TimeSpan.Parse(utility.sell_condition_end);

            if (t_code.CompareTo(t_start) < 0 || t_code.CompareTo(t_end) > 0)
            {
                WriteLog_Order("[조건식매도/매도시간이탈] : " + code + " - " + "조건식 매도 시간이 아닙니다." + "\n");
                return;
            }

            sell_order(price, "조건식매도", order_num, percent);
        }

        //실시간 가격 매도
        private void sell_check_price(string price, string percent, int hold, int buy_price, string order_num, double down_percent)
        {

            //익절
            if (utility.profit_percent)
            {
                double percent_edit = double.Parse(percent.Replace("%", ""));
                double profit = double.Parse(utility.profit_percent_text);
                if (percent_edit >= profit)
                {
                    sell_order(price, "익절매도", order_num, percent);
                    return;
                }
            }

            //익절원
            if (utility.profit_won)
            {
                int profit_amount = Convert.ToInt32(utility.profit_won_text);
                if ((hold * buy_price * double.Parse(percent.Replace("%", "")) / 100) >= profit_amount)
                {
                    sell_order(price, "익절원", order_num, percent);
                    return;
                }
            }

            //익절TS
            if (utility.profit_ts)
            {
                if (Math.Abs(down_percent) >= double.Parse(utility.profit_ts_text2))
                {
                    sell_order(price, "익절TS", order_num, percent);
                    return;
                }
            }

            //손절
            if (utility.loss_percent)
            {
                double percent_edit = double.Parse(percent.TrimEnd('%'));
                double loss = double.Parse(utility.loss_percent_text);
                if (percent_edit <= -loss)
                {
                    sell_order(price, "손절매도", order_num, percent);
                    return;
                }
            }

            //손절원
            if (utility.loss_won)
            {
                int loss_amount = Convert.ToInt32(utility.loss_won_text);
                if ((hold * buy_price * double.Parse(percent.Replace("%", "")) / 100) <= -loss_amount)
                {
                    sell_order(price, "익절원", order_num, percent);
                    return;
                }
            }
        }

        //--------------실시간 매도 주문---------------------  

        //매도 주문(1초에 5회)
        private void sell_order(string price, string sell_message, string order_num, string percent)
        {
            lock (table1)
            {
                var findRows = dtCondStock.AsEnumerable().Where(row2 => row2.Field<string>("주문번호") == order_num);

                if (!findRows.Any()) return;

                DataRow row = findRows.First();

                string start_price = row["편입가"].ToString();
                string code = row["종목코드"].ToString();
                string code_name = row["종목명"].ToString();

                //보유수량계산
                string[] tmp = row["보유수량"].ToString().Split('/');
                int order_acc = Convert.ToInt32(tmp[0].Replace(",", ""));

                //주문 방식 구분
                string[] order_method = buy_condtion_method.Text.Split('/');

                //주문시간 확인(0정규장, 1시간외종가, 2시간외단일가
                int market_time = 0;

                TimeSpan t_now = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss"));

                //주문간 간격
                if (utility.term_for_sell)
                {
                    TimeSpan t_last2 = TimeSpan.Parse(last_buy_time);

                    if (t_now - t_last2 < TimeSpan.FromMilliseconds(Convert.ToInt32(utility.term_for_sell_text)))
                    {
                        //WriteLog_Order($"[매도간격] 설정({utility.term_for_sell_text}), 현재({(t_now - t_last2).ToString()})\n");
                        return;
                    }
                    last_buy_time = t_now.ToString();
                }
                else
                {
                    TimeSpan t_last2 = TimeSpan.Parse(last_buy_time);

                    if (t_now - t_last2 < TimeSpan.FromMilliseconds(delay1))
                    {
                        //WriteLog_Order($"[매도간격] 설정({utility.term_for_sell_text}), 현재({(t_now - t_last2).ToString()})\n");
                        return;
                    }
                    last_buy_time = t_now.ToString();
                }

                TimeSpan t_time0 = TimeSpan.Parse("15:30:00");
                TimeSpan t_time1 = TimeSpan.Parse("15:40:00");
                TimeSpan t_time2 = TimeSpan.Parse("16:00:00");
                TimeSpan t_time3 = TimeSpan.Parse("18:00:00");

                // result가 0보다 작으면 time1 < time2
                // result가 0이면 time1 = time2
                // result가 0보다 크면 time1 > time2
                if (t_time0.CompareTo(t_now) <= 0 && t_now.CompareTo(t_time1) < 0)
                {
                    WriteLog_Order($"[{sell_message}/주문접수] : {code_name}({code}) {order_acc}개 {percent} 정규장 종료\n");
                    return;
                }
                else if (t_time1.CompareTo(t_now) <= 0 && t_now.CompareTo(t_time2) < 0)
                {
                    market_time = 1;
                }
                else if (t_time2.CompareTo(t_now) <= 0 && t_now.CompareTo(t_time3) < 0)
                {
                    market_time = 2;
                }
                else if (t_now.CompareTo(t_time3) >= 0)
                {
                    WriteLog_Order($"[{sell_message}/주문접수] : {code_name}({code}) {order_acc}개 {percent} 시간외단일가 종료\n");
                    return;
                }

                WriteLog_Order($"[{sell_message}/주문접수] : {code_name}({code}) {order_acc}개 {percent}\n");
                telegram_message($"[{sell_message}/주문접수] : {code_name}({code}) {order_acc}개 {percent}\n");

                string time2 = DateTime.Now.ToString("HH:mm:ss");

                //시간외종가
                if (market_time == 1)
                {
                    if (sell_message.Equals("청산매도/일반") || sell_message.Equals("청산매도/수익") && !utility.clear_sell_profit_after1)
                    {
                        return;
                    }
                    else if (sell_message.Equals("청산매도/손실") && !utility.clear_sell_loss_after1)
                    {
                        return;
                    }
                    else if (sell_message.Equals("익절매도") || sell_message.Equals("익절원") || sell_message.Equals("익절TS") && !utility.profit_after1)
                    {
                        return;
                    }
                    else if (sell_message.Equals(" 손절매도") || sell_message.Equals("손절원") && !utility.loss_after1)
                    {
                        return;
                    }

                    row["상태"] = "매도중";
                    row["매매진입"] = time2;
                    gridView1_refresh();

                    int error = axKHOpenAPI1.SendOrder("시간외종가", GetScreenNo(), utility.setting_account_number, 2, code, order_acc, 0, "81", "");

                    if (error == 0)
                    {
                        WriteLog_Order($"[{sell_message}/시간외종가/주문성공] : {code_name}({code}) {order_acc}개\n");
                        WriteLog_Order($"[{sell_message}/시간외종가/주문상세] : 편입가 {start_price}원, 현재가 {price}원, 수익 {percent}\n");
                        telegram_message($"[{sell_message}/시간외종가//주문성공] : {code_name}({code}) {order_acc}개 {percent}\n");
                        telegram_message($"[{sell_message}/시간외종가/주문상세] : 편입가 {start_price}원, 현재가 {price}원, 수익 {percent}\n");
                    }
                    else if (error == -308)
                    {
                        //편입 차트 상태 '매수완료' 변경
                        row["상태"] = "매수완료";
                        gridView1_refresh();

                        WriteLog_Order($"[{sell_message}/시간외종가//주문실패] : {code_name}({code}) 초당 5회 이상 주문 불가\n");
                        telegram_message($"[{sell_message}/시간외종가//주문실패] : {code_name}({code}) 초당 5회 이상 주문 불가\n");
                    }
                    else
                    {
                        //편입 차트 상태 '매수완료' 변경
                        row["상태"] = "매수완료";
                        gridView1_refresh();

                        WriteLog_Order($"[{sell_message}/시간외종가//주문실패] : {code_name}({code}) 에러코드({error})\n");
                        telegram_message($"[{sell_message}/시간외종가//주문실패] : {code_name}({code}) 에러코드({error})\n");
                    }
                }
                //시간외단일가
                else if (market_time == 2)
                {
                    if (sell_message.Equals("청산매도/일반") || sell_message.Equals("청산매도/수익") && !utility.clear_sell_profit_after2)
                    {
                        return;
                    }
                    else if (sell_message.Equals("청산매도/손실") && !utility.clear_sell_loss_after2)
                    {
                        return;
                    }
                    else if (sell_message.Equals("익절매도") || sell_message.Equals("익절원") || sell_message.Equals("익절TS") && !utility.profit_after2)
                    {
                        return;
                    }
                    else if (sell_message.Equals(" 손절매도") || sell_message.Equals("손절원") && !utility.loss_after2)
                    {
                        return;
                    }

                    //
                    order_method = sell_condtion_method_after.Split('/');
                    //
                    int edited_price_hoga = hoga_cal(Convert.ToInt32(price.Replace(",", "")), order_method[1].Equals("현재가") ? 0 : Convert.ToInt32(order_method[1].Replace("호가", "")), Convert.ToInt32(row["상한가"].ToString().Replace("호가", "")));

                    row["상태"] = "매도중";
                    row["매매진입"] = time2;
                    gridView1_refresh();

                    int error = axKHOpenAPI1.SendOrder("시간외단일가", GetScreenNo(), utility.setting_account_number, 2, code, order_acc, edited_price_hoga, "62", "");

                    if (error == 0)
                    {
                        WriteLog_Order($"[{sell_message}/시간외단일가/주문성공] : {code_name}({code}) {order_acc}개 수익 {percent}\\n");
                        WriteLog_Order($"[{sell_message}/시간외단일가/주문상세] : 편입가 {start_price}원, 현재가({price}) 주문가({edited_price_hoga})원 주문방식({order_method[1]}, 수익 {percent}\n");
                        telegram_message($"[{sell_message}/시간외단일가//주문성공] : {code_name}({code}) {order_acc}개 수익 {percent}\\n");
                        telegram_message($"[{sell_message}/시간외단일가/주문상세] : 편입가 {start_price}원, 현재가({price}) 주문가({edited_price_hoga})원 주문방식({order_method[1]}, 수익 {percent}\n");
                    }
                    else if (error == -308)
                    {
                        //편입 차트 상태 '매수완료' 변경
                        row["상태"] = "매수완료";
                        gridView1_refresh();

                        WriteLog_Order($"[{sell_message}/시간외단일가//주문실패] : {code_name}({code}) 초당 5회 이상 주문 불가\n");
                        telegram_message($"[{sell_message}/시간외단일가//주문실패] : {code_name}({code}) 초당 5회 이상 주문 불가\n");
                    }
                    else
                    {
                        //편입 차트 상태 '매수완료' 변경
                        row["상태"] = "매수완료";
                        gridView1_refresh();

                        WriteLog_Order($"[{sell_message}/시간외단일가//주문실패] : {code_name}({code}) 에러코드({error})\n");
                        telegram_message($"[{sell_message}/시간외단일가//주문실패] : {code_name}({code}) 에러코드({error})\n");
                    }

                }
                //시장가 주문 + 청산주문
                else if (sell_message.Split('/')[0].Equals("청산매도") || order_method[0].Equals("시장가"))
                {
                    row["상태"] = "매도중";
                    row["매매진입"] = time2;
                    gridView1_refresh();

                    int error = axKHOpenAPI1.SendOrder("시장가매도", GetScreenNo(), utility.setting_account_number, 2, code, order_acc, 0, "03", "");

                    if (error == 0)
                    {
                        WriteLog_Order($"[{sell_message}/시장가/주문성공] : {code_name}({code}) {order_acc}개\n");
                        WriteLog_Order($"[{sell_message}/시장가/주문상세] : 편입가 {start_price}원, 현재가 {price}원, 수익 {percent}\n");
                        telegram_message($"[{sell_message}/시장가//주문성공] : {code_name}({code}) {order_acc}개 {percent}\n");
                        telegram_message($"[{sell_message}/시장가/주문상세] : 편입가 {start_price}원, 현재가 {price}원, 수익 {percent}\n");
                    }
                    else if (error == -308)
                    {
                        //편입 차트 상태 '매수완료' 변경
                        row["상태"] = "매수완료";
                        gridView1_refresh();

                        WriteLog_Order($"[{sell_message}/시장가//주문실패] : {code_name}({code}) 초당 5회 이상 주문 불가\n");
                        telegram_message($"[{sell_message}/시장가//주문실패] : {code_name}({code}) 초당 5회 이상 주문 불가\n");
                    }
                    else
                    {
                        //편입 차트 상태 '매수완료' 변경
                        row["상태"] = "매수완료";
                        gridView1_refresh();

                        WriteLog_Order($"[{sell_message}/시장가//주문실패] : {code_name}({code}) 에러코드({error})\n");
                        telegram_message($"[{sell_message}/시장가//주문실패] : {code_name}({code}) 에러코드({error})\n");
                    }
                }
                //지정가 주문
                else
                {
                    int edited_price_hoga = hoga_cal(Convert.ToInt32(price.Replace(",", "")), order_method[1].Equals("현재가") ? 0 : Convert.ToInt32(order_method[1].Replace("호가", "")), Convert.ToInt32(row["상한가"].ToString().Replace("호가", "")));

                    row["상태"] = "매도중";
                    row["매매진입"] = time2;
                    gridView1_refresh();

                    int error = axKHOpenAPI1.SendOrder("시장가매도", GetScreenNo(), utility.setting_account_number, 2, code, order_acc, edited_price_hoga, "00", "");

                    if (error == 0)
                    {
                        WriteLog_Order($"[{sell_message}/지정가/주문성공] : {code_name}({code}) {order_acc}개 {edited_price_hoga}원\n");
                        WriteLog_Order($"[{sell_message}/지정가/주문상세] : 편입가 {start_price}원, 현재가 {price}원, 수익 {percent}\n");
                        telegram_message($"[{sell_message}/지정가/주문성공] : {code_name}({code}) {order_acc}개 {edited_price_hoga}원 {percent}\n");
                        telegram_message($"[{sell_message}/지정가/주문상세] : 편입가 {start_price}원, 현재가 {price}원, 수익 {percent}\n");
                    }
                    else if (error == -308)
                    {
                        //편입 차트 상태 '매수완료' 변경
                        row["상태"] = "매수완료";
                        gridView1_refresh();

                        WriteLog_Order($"[{sell_message}/지정가/주문실패] : {code_name}({code}) 초당 5회 이상 주문 불가\n");
                        telegram_message($"[{sell_message}/지정가/주문실패] : {code_name}({code}) 초당 5회 이상 주문 불가\n");
                    }
                    else
                    {
                        //편입 차트 상태 '매수완료' 변경
                        row["상태"] = "매수완료";
                        gridView1_refresh();

                        WriteLog_Order($"[{sell_message}/지정가/주문실패] : {code_name}({code}) 에러코드({error})\n");
                        telegram_message($"[{sell_message}/지정가/주문실패] : {code_name}({code}) 에러코드({error})\n");
                    }
                }
            }

        }

        //------------호가 계산---------------------  
        private int hoga_cal(int price, int hoga, int high)
        {
            int[] hogaUnits = { 1, 5, 10, 50, 100, 500, 1000 }; // 이미지에서 제공된 단위
            int[] hogaRanges = { 0, 2000, 5000, 20000, 50000, 200000, 500000 }; // 이미지에서 제공된 범위

            if (hoga == 0) return price;

            for (int i = hogaRanges.Length - 1; i >= 0; i--)
            {
                if (price >= hogaRanges[i])
                {
                    int increment = hoga * hogaUnits[i];
                    int nextPrice = price + increment;

                    // Check if the next price crosses the range boundary
                    if (i < hogaRanges.Length - 1 && nextPrice >= hogaRanges[i + 1])
                    {
                        // Calculate remaining increment in the new range
                        int remainingIncrement = nextPrice - hogaRanges[i + 1];
                        nextPrice = hoga_cal(hogaRanges[i + 1], remainingIncrement / hogaUnits[i + 1], high);
                    }

                    // Check if nextPrice exceeds the high value
                    if (nextPrice > high)
                    {
                        return high;
                    }

                    return nextPrice;
                }
            }
            return price;
        }

        //------------주문 상태 확인---------------------

        //주문번호가 업데이트 않된 경우가 있어서 임시 저장한다.
        private Queue<string[]> Trade_check_save = new Queue<string[]>();

        private async void onReceiveChejanData(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnReceiveChejanDataEvent e)
        {
            /*
            e.sGubun 0 신규주문
            9001(종목코드,업종코드);912(주문업무분류);913(주문상태)
            900(주문수량);901(주문가격);902(미체결수량);
            905(주문구분);906(매매구분);907(매도수구분);
            910(체결가);911(체결량);
            938(당일매매수수료);939(당일매매세금)

            9201(계좌번호);9203(주문번호);9205(관리자사번);
            302(종목명); 903(체결누계금액);904(원주문번호);908(주문 및 체결시간);909(체결번호)
            1(현재가);27(최우선 매도호가);28(최우선매수호가);
            914(단위체결가);915(단위체결량);919(거부사유);920(화면번호);921(터미널번호);922(신용구분);923(대출일);
            949(?);10010(?);969(?);819(?)
            */


            string tmp0 = Convert.ToString(axKHOpenAPI1.GetChejanData(9001)).Trim();//9001(종목코드,업종코드) A020150
            string tmp1 = Convert.ToString(axKHOpenAPI1.GetChejanData(912)).Trim();//912(주문업무분류); JJ
            string tmp2 = Convert.ToString(axKHOpenAPI1.GetChejanData(913)).Trim();//913(주문상태) 접수 체결
            string tmp3 = Convert.ToString(axKHOpenAPI1.GetChejanData(900)).Trim();//900(주문수량) 18 => 고정값
            string tmp4 = Convert.ToString(axKHOpenAPI1.GetChejanData(901)).Trim();//901(주문가격) 0(시장가)
            string tmp5 = Convert.ToString(axKHOpenAPI1.GetChejanData(902)).Trim();//902(미체결수량) 18 0 16
            string tmp6 = Convert.ToString(axKHOpenAPI1.GetChejanData(905)).Trim();//905(주문구분) +매수 -매도 정정 취소
            string tmp7 = Convert.ToString(axKHOpenAPI1.GetChejanData(906)).Trim();//906(매매구분) 시장가
            string tmp8 = Convert.ToString(axKHOpenAPI1.GetChejanData(907)).Trim();//907(매도수구분) 2(매수) 1(매도)
            string tmp9 = Convert.ToString(axKHOpenAPI1.GetChejanData(910)).Trim();//910(체결가) 25000
            string tmp10 = Convert.ToString(axKHOpenAPI1.GetChejanData(911)).Trim();//911(체결량) 18
            string tmp11 = Convert.ToString(axKHOpenAPI1.GetChejanData(938)).Trim();//938(당일매매수수료) 0 2830 =>누적
            string tmp12 = Convert.ToString(axKHOpenAPI1.GetChejanData(939)).Trim();//939(당일매매세금) 0 0 =>누적
            string tmp13 = Convert.ToString(axKHOpenAPI1.GetChejanData(908)).Trim();//908(주문 및 체결시간)
            string tmp14 = Convert.ToString(axKHOpenAPI1.GetChejanData(302)).Trim();
            string tmp15 = Convert.ToString(axKHOpenAPI1.GetChejanData(9203)).Trim(); //order_number

            string[] tmp = { e.sGubun, tmp0, tmp1, tmp2, tmp3, tmp4, tmp5, tmp6, tmp7, tmp8, tmp9, tmp10, tmp11, tmp12, tmp13, tmp14, tmp15 };

            //접수 단계에서 주문번호 할당할 수 있도록 구성 예정
            //
            if (e.sGubun.Equals("0"))
            {
                WriteLog_System($"[체결수신] : {e.sGubun}/{tmp0}/{tmp1}/{tmp2}/{tmp3}/{tmp4}/{tmp5}/{tmp6}/{tmp7}/{tmp8}/{tmp9}/{tmp10}/{tmp11}/{tmp12}/{tmp13}/{tmp14}/{tmp15}\n");
                WriteLog_Order($"[체결상세/{tmp14}({tmp0})/{e.sGubun}] : {tmp10}/{tmp3}\n");

                //매수확인
                if (tmp8.Equals("2") && tmp5.Equals("0"))
                {
                    Trade_check_save.Enqueue(tmp);
                }
                else if (tmp8.Equals("1") && tmp5.Equals("0"))
                {
                    Trade_check_save.Enqueue(tmp);
                }
            }
        }

        private async void Trade_Check_Event(object sender, EventArgs e)
        {

            if (Trade_check_save.Count != 0)
            {
                string[] tmp = Trade_check_save.Dequeue();

                //매도수구분
                string Gubun = tmp[9].Trim().Equals("2") ? "매수" : "매도";
                //추가로드- 종목코드
                string code = tmp[1].Replace("A", ""); //axKHOpenAPI1.GetChejanData(9001)
                //추가로드 - 종목이름
                string code_name = tmp[15];
                // 누적체결수량/주문수량
                string order_sum = tmp[4]; //axKHOpenAPI1.GetChejanData(900)
                string partial_sum = tmp[11]; //axKHOpenAPI1.GetChejanData(911);
                //미체결수량
                string left_Acc = tmp[6]; //axKHOpenAPI1.GetChejanData(902);
                string order_number = tmp[16];
                string buy_time = tmp[14];
                string sell_time = tmp[14];

                //매수확인
                if (Gubun.Equals("매수") && left_Acc.Equals("0"))
                {
                    lock (table1)
                    {
                        //데이터 업데이트(Independent 모드에서 어떤 조건식으로 주문이 들어갔는지 알지 못하므로 먼저 처리가 끝난순으로 기입한다)
                        var findRows1 = dtCondStock.AsEnumerable()
                                                .Where(row2 => row2.Field<string>("종목코드") == code &&
                                                              row2.Field<string>("상태") == "매수중");

                        if (!findRows1.Any()) return;

                        DataRow row = findRows1.First();
                        //
                        row["주문번호"] = order_number;
                        row["보유수량"] = $"{partial_sum}/{order_sum}";
                        row["매수시각"] = string.Format("{0:D2}:{1:D2}:{2:D2}", int.Parse(buy_time.Substring(0, 2)), int.Parse(buy_time.Substring(2, 2)), int.Parse(buy_time.Substring(4, 2)));
                        //
                        gridView1_refresh();
                    }

                    //체결내역업데이트(주문번호)
                    lock (table3)
                    {
                        dtCondStock_Transaction.Clear();
                    }
                    Transaction_Detail(order_number, "");

                    await Task.Delay(delay1);

                    //계좌보유현황업데이트
                    lock (table2)
                    {
                        dtCondStock_hold.Clear();
                    }
                    Account_before("");

                    await Task.Delay(delay1);

                    /*
                    //HTS에서 매수할 경우
                    else
                    {
                        axKHOpenAPI1.SetInputValue("종목코드", code);
                        axKHOpenAPI1.CommRqData("조건실시간검색_수동/" + order_number + "/" + order_sum, "OPT10001", 0, GetScreenNo());

                        //계좌보유현황업데이트
                        dtCondStock_hold.Clear();
                        Account_before(code);

                        System.Threading.Thread.Sleep(300);

                        //당일 손익 + 당일 손일률 + 당일 수수료 업데이트
                        today_profit_tax_load("");
                    }
                    */
                }
                //매도확인
                else if (Gubun.Equals("매도") && left_Acc.Equals("0"))
                {
                    lock (table1)
                    {
                        //데이터 업데이트(Independent 모드에서 어떤 조건식으로 주문이 들어갔는지 알지 못하므로 먼저 처리가 끝난순으로 기입한다)
                        var findRows2 = dtCondStock.AsEnumerable()
                                                .Where(row2 => row2.Field<string>("종목코드") == code &&
                                                              row2.Field<string>("상태") == "매도중");

                        if (!findRows2.Any()) return;

                        DataRow row = findRows2.First();
                        //
                        if (!utility.duplication_deny)
                        {
                            row["상태"] = "대기";
                            row["주문번호"] = order_number;
                            row["보유수량"] = $"{left_Acc}/0";
                            row["매도시각"] = string.Format("{0:D2}:{1:D2}:{2:D2}", int.Parse(sell_time.Substring(0, 2)), int.Parse(sell_time.Substring(2, 2)), int.Parse(sell_time.Substring(4, 2)));
                            gridView1_refresh();
                        }
                        else
                        {
                            row["상태"] = "매도완료";
                            row["주문번호"] = order_number;
                            row["보유수량"] = $"{left_Acc}/0";
                            row["매도시각"] = string.Format("{0:D2}:{1:D2}:{2:D2}", int.Parse(sell_time.Substring(0, 2)), int.Parse(sell_time.Substring(2, 2)), int.Parse(sell_time.Substring(4, 2)));
                            gridView1_refresh();

                            //모든 화면에서 "code"종목 실시간 해지
                            axKHOpenAPI1.SetRealRemove("ALL", code);

                            //보유 수량 업데이트
                            string[] hold_status = max_hoid.Text.Split('/');
                            int hold = Convert.ToInt32(hold_status[0]);
                            int hold_max = Convert.ToInt32(hold_status[1]);
                            max_hoid.Text = $"{hold - 1}/{hold_max}";
                        }
                    }

                    //체결내역업데이트(주문번호)
                    lock (table3)
                    {
                        dtCondStock_Transaction.Clear();
                    }
                    Transaction_Detail(order_number, "");

                    await Task.Delay(delay1);

                    //계좌보유현황업데이트
                    lock (table2)
                    {
                        dtCondStock_hold.Clear();
                    }
                    Account_before("");

                    await Task.Delay(delay1);

                    //당일 손익 + 당일 손일률 + 당일 수수료 업데이트
                    today_profit_tax_load("매도");

                }
            }
        }

        //--------------------------------------미체결 주문-------------------------------------------------------------(CHECK)   

        private void order_close(string trade_type, string order_number, string code_name, string code, string order_acc)
        {
            TimeSpan t_now = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss"));

            //주문간 간격
            if (utility.term_for_sell)
            {
                TimeSpan t_last2 = TimeSpan.Parse(last_buy_time);

                if (t_now - t_last2 < TimeSpan.FromMilliseconds(Convert.ToInt32(utility.term_for_sell_text)))
                {
                    //WriteLog_Order($"[매도간격] 설정({utility.term_for_sell_text}), 현재({(t_now - t_last2).ToString()})\n");
                    return;
                }
                last_buy_time = t_now.ToString();
            }
            else
            {
                TimeSpan t_last2 = TimeSpan.Parse(last_buy_time);

                if (t_now - t_last2 < TimeSpan.FromMilliseconds(200))
                {
                    //WriteLog_Order($"[매도간격] 설정({utility.term_for_sell_text}), 현재({(t_now - t_last2).ToString()})\n");
                    return;
                }
                last_buy_time = t_now.ToString();
            }

            //주문시간 확인(0정규장, 1시간외종가, 2시간외단일가
            int market_time = 0;

            TimeSpan t_time0 = TimeSpan.Parse("15:30:00");
            TimeSpan t_time1 = TimeSpan.Parse("15:40:00");
            TimeSpan t_time2 = TimeSpan.Parse("16:00:00");
            TimeSpan t_time3 = TimeSpan.Parse("18:00:00");

            // result가 0보다 작으면 time1 < time2
            // result가 0이면 time1 = time2
            // result가 0보다 크면 time1 > time2
            if (t_time0.CompareTo(t_now) <= 0 && t_now.CompareTo(t_time1) < 0)
            {
                WriteLog_Order($"[{trade_type}/ 주문취소/정규장종료] : {code_name}({code}) {order_acc}개\n");
                return;
            }
            else if (t_time1.CompareTo(t_now) <= 0 && t_now.CompareTo(t_time2) < 0)
            {
                market_time = 1;
            }
            else if (t_time2.CompareTo(t_now) <= 0 && t_now.CompareTo(t_time3) < 0)
            {
                market_time = 2;
            }
            else if (t_now.CompareTo(t_time3) >= 0)
            {
                WriteLog_Order($"[{trade_type}/주문취소/시간외단일가종료] : {code_name}({code}) {order_acc}개\n");
                return;
            }

            WriteLog_Order($"[{trade_type}/주문취소/접수] : {code_name}({code}) {order_acc}개\n");
            telegram_message($"[{trade_type}/주문취소/접수] : {code_name}({code}) {order_acc}개\n");

            string time2 = DateTime.Now.ToString("HH:mm:ss");

            //시간외종가
            if (market_time == 1)
            {
                // 1:신규매수, 2:신규매도 3:매수취소, 4:매도취소
                // 81 : 장후시간외종가 62 : 시간외단일가매매 03 : 시장가 00 : 지정가
                if (trade_type.Equals("매수"))
                {
                    int error = axKHOpenAPI1.SendOrder("시간외종가취소", GetScreenNo(), utility.setting_account_number, 3, code, 0, 0, "81", "");

                    if (error == 0)
                    {
                        lock (table3)
                        {
                            dtCondStock_Transaction.Clear();
                        }
                        Transaction_Detail(order_number, "매수취소");
                        //
                        WriteLog_Order($"[{trade_type}/주문취소/시간외종가/취소성공] : {code_name}({code})\n");
                        telegram_message($"[{trade_type}/주문취소/시간외종가//취소성공] : {code_name}({code})\n");
                    }
                    else
                    {
                        WriteLog_Order($"[{trade_type}/주문취소/시간외종가/취소실패] : {code_name}({code})\n");
                        telegram_message($"[{trade_type}/주문취소/시간외종가/취소실패] : {code_name}({code})\n");
                    }
                }
                else
                {
                    int error = axKHOpenAPI1.SendOrder("시간외종가취소", GetScreenNo(), utility.setting_account_number, 4, code, 0, 0, "81", "");

                    if (error == 0)
                    {
                        lock (table3)
                        {
                            dtCondStock_Transaction.Clear();
                        }
                        Transaction_Detail(order_number, "매도취소");
                        //
                        WriteLog_Order($"[{trade_type}/주문취소/시간외종가/취소성공] : {code_name}({code})\n");
                        telegram_message($"[{trade_type}/주문취소/시간외종가//취소성공] : {code_name}({code})\n");
                    }
                    else
                    {
                        WriteLog_Order($"[{trade_type}/주문취소/시간외종가/취소실패] : {code_name}({code})\n");
                        telegram_message($"[{trade_type}/주문취소/시간외종가/취소실패] : {code_name}({code})\n");
                    }
                }
            }
            //시간외단일가
            else if (market_time == 2)
            {
                // 1:신규매수, 2:신규매도 3:매수취소, 4:매도취소
                // 81 : 장후시간외종가 62 : 시간외단일가매매 03 : 시장가 00 : 지정가
                if (trade_type.Equals("매수"))
                {
                    int error = axKHOpenAPI1.SendOrder("시간외단일가취소", GetScreenNo(), utility.setting_account_number, 3, code, 0, 0, "62", "");

                    if (error == 0)
                    {
                        lock (table3)
                        {
                            dtCondStock_Transaction.Clear();
                        }
                        Transaction_Detail(order_number, "매수취소");
                        //
                        WriteLog_Order($"[{trade_type}/주문취소/시간외단일가/취소성공] : {code_name}({code})\n");
                        telegram_message($"[{trade_type}/주문취소/시간외단일가//취소성공] : {code_name}({code})\n");
                    }
                    else
                    {
                        WriteLog_Order($"[{trade_type}/주문취소/시간외단일가/취소실패] : {code_name}({code})\n");
                        telegram_message($"[{trade_type}/주문취소/시간외단일가/취소실패] : {code_name}({code})\n");
                    }
                }
                else
                {
                    int error = axKHOpenAPI1.SendOrder("시간외단일가취소", GetScreenNo(), utility.setting_account_number, 4, code, 0, 0, "62", "");

                    if (error == 0)
                    {
                        lock (table3)
                        {
                            dtCondStock_Transaction.Clear();
                        }
                        Transaction_Detail(order_number, "매도취소");
                        //
                        WriteLog_Order($"[{trade_type}/주문취소/시간외단일가/취소성공] : {code_name}({code})\n");
                        telegram_message($"[{trade_type}/주문취소/시간외단일가/취소성공] : {code_name}({code})\n");
                    }
                    else
                    {
                        WriteLog_Order($"[{trade_type}/주문취소/시간외단일가/취소실패] : {code_name}({code})\n");
                        telegram_message($"[{trade_type}/주문취소/시간외단일가/취소실패] : {code_name}({code})\n");
                    }
                }
            }
            //정규장
            else
            {

                string order_type = buy_condtion_method.Text.Split('/')[0];

                // 1:신규매수, 2:신규매도 3:매수취소, 4:매도취소
                // 81 : 장후시간외종가 62 : 시간외단일가매매 03 : 시장가 00 : 지정가
                if (trade_type.Equals("매수"))
                {
                    int error = axKHOpenAPI1.SendOrder("정규장취소", GetScreenNo(), utility.setting_account_number, 3, code, 0, 0, order_type.Equals("지정가") ? "00" : "03", "");

                    if (error == 0)
                    {
                        lock (table3)
                        {
                            dtCondStock_Transaction.Clear();
                        }
                        Transaction_Detail(order_number, "메수취소");
                        //
                        WriteLog_Order($"[{trade_type}/주문취소/정규장/취소성공] : {code_name}({code})\n");
                        telegram_message($"[{trade_type}/주문취소/정규장//취소성공] : {code_name}({code})\n");
                    }
                    else
                    {
                        WriteLog_Order($"[{trade_type}/주문취소/정규장/취소실패] : {code_name}({code})\n");
                        telegram_message($"[{trade_type}/주문취소/정규장/취소실패] : {code_name}({code})\n");
                    }
                }
                else
                {
                    int error = axKHOpenAPI1.SendOrder("정규장취소", GetScreenNo(), utility.setting_account_number, 4, code, 0, 0, order_type.Equals("지정가") ? "00" : "03", "");

                    if (error == 0)
                    {
                        lock (table3)
                        {
                            dtCondStock_Transaction.Clear();
                        }
                        Transaction_Detail(order_number, "매도취소");
                        //
                        WriteLog_Order($"[{trade_type}/주문취소/정규장/취소성공] : {code_name}({code})\n");
                        telegram_message($"[{trade_type}/주문취소/정규장/취소성공] : {code_name}({code})\n");
                    }
                    else
                    {
                        WriteLog_Order($"[{trade_type}/주문취소/정규장/취소실패] : {code_name}({code})\n");
                        telegram_message($"[{trade_type}/주문취소/정규장/취소실패] : {code_name}({code})\n");
                    }
                }
            }
        }

        //------------조건식 실시간 중단 버튼-------------------

        public void real_time_stop(bool real_price_all_stop)
        {
            //실시간 중단이 선언되면 '실시간시작'이 가능해진다.
            Real_time_stop_btn.Enabled = false;
            Real_time_search_btn.Enabled = true;

            //매수 조건식 중단
            if (utility.buy_condition)
            {
                // 검색된 조건식이 없을시
                if (string.IsNullOrEmpty(utility.Fomula_list_buy_text))
                {
                    WriteLog_System("[실시간매수조건/중단실패] : 조건식없음\n");
                    telegram_message("[실시간매수조건/중단실패] : 조건식없음\n");
                    Real_time_stop_btn.Enabled = true;
                    Real_time_search_btn.Enabled = false;
                }
                else
                {
                    //검색된 매수 조건식이 있을시
                    string[] condition = utility.Fomula_list_buy_text.Split(',');
                    for (int i = 0; i < condition.Length; i++)
                    {
                        string[] tmp = condition[i].Split('^');
                        axKHOpenAPI1.SendConditionStop(GetScreenNo(), tmp[1], Convert.ToInt32(tmp[0])); //조건검색 중지
                        System.Threading.Thread.Sleep(delay1);
                    }
                    WriteLog_System("[실시간매수조건/중단]\n");
                    telegram_message("[실시간매수조건/중단]\n");
                }
            }

            System.Threading.Thread.Sleep(delay1);

            //매수 조건식 중단
            if (utility.sell_condition)
            {
                // 검색된 조건식이 없을시
                if (string.IsNullOrEmpty(utility.Fomula_list_buy_text))
                {
                    WriteLog_System("[실시간매도조건/중단실패] : 조건식없음\n");
                    telegram_message("[실시간매도조건/중단실패] : 조건식없음\n");
                    Real_time_stop_btn.Enabled = true;
                    Real_time_search_btn.Enabled = false;
                }
                else
                {
                    //검색된 매수 조건식이 있을시
                    string[] condition = utility.Fomula_list_sell_text.Split(',');
                    for (int i = 0; i < condition.Length; i++)
                    {
                        string[] tmp = condition[i].Split('^');
                        axKHOpenAPI1.SendConditionStop(GetScreenNo(), tmp[1], Convert.ToInt32(tmp[0])); //조건검색 중지
                        System.Threading.Thread.Sleep(delay1);
                    }
                    WriteLog_System("[실시간매도조건/중단]\n");
                    telegram_message("[실시간매도조건/중단]\n");
                }
            }

            //완전 전체 중단
            if (real_price_all_stop)
            {
                axKHOpenAPI1.SetRealRemove("ALL", "ALL"); //실시간 시세 중지
                timer2.Stop();//계좌 탐색 중단
                //
                if (minuteTimer != null)
                {
                    minuteTimer.Stop();
                    minuteTimer.Dispose();
                    minuteTimer = null;
                }
                //
                WriteLog_System("[실시간시세/중단]\n");
                telegram_message("[실시간시세/중단]\n");
            }
        }

        //--------------------------------------Telegram Function-------------------------------------------------------------  

        private void telegram_function(string message)
        {
            switch (message)
            {
                case "/HELP":
                    telegram_message("[명령어 리스트]\n/HELP : 명령어 리스트\n/REBOOT : 프로그램 재실행\n/SHUTDOWN : 프로그램 종료\n" +
                        "/START : 조건식 시작\n/STOP : 조건식 중단\n/CLEAR : 전체 청산\n/CLEAR_PLUS : 수익 청산\n/CLEAR_MINUS : 손실 청산\n" +
                        "/L1 : 시스템 로그\n/L2 : 주문 로그\n/L3 : 편출입 로그\n" +
                        "/T1 : 편출입 차트\n/T2 : 보유 차트\n/T3 : 매매내역 차트\n");
                    break;
                case "/REBOOT":
                    telegram_message("프로그램 재실행\n");
                    Application.Restart();
                    break;
                case "/SHUTDOWN":
                    telegram_message("프로그램 종료\n");
                    Application.Exit();
                    break;
                case "/START":
                    telegram_message("조건식 실시간 검색 시작\n");
                    real_time_search_btn(this, EventArgs.Empty);
                    break;
                case "/STOP":
                    telegram_message("조건식 실시간 검색 중단\n");
                    real_time_stop_btn(this, EventArgs.Empty);
                    break;
                case "/CLEAR":
                    telegram_message("전체 청산 실행\n");
                    All_clear_btn_Click(this, EventArgs.Empty);
                    break;
                case "/CLEAR_PLUS":
                    telegram_message("수익 청산 실행\n");
                    Profit_clear_btn_Click(this, EventArgs.Empty);
                    break;
                case "/CLEAR_MINUS":
                    telegram_message("손실 청산 실행\n");
                    Loss_clear_btn_Click(this, EventArgs.Empty);
                    break;
                case "/L1":
                    telegram_message("시스템 로그 수신\n");
                    telegram_message($"\n{log_window.Text}\n");
                    break;
                case "/L2":
                    telegram_message("주문 로그 수신\n");
                    telegram_message($"\n{log_window3.Text}\n");
                    break;
                case "/L3":
                    telegram_message("편출입 로그 수신\n");
                    telegram_message($"\n{log_window2.Text}\n");
                    break;
                case "/T1":
                    telegram_message("편출입 차트 수신\n");
                    //
                    string send_meesage = "";
                    //
                    lock (table1)
                    {
                        send_meesage = string.Join("/", dtCondStock.Columns.Cast<DataColumn>().Select(column => column.ColumnName)) + "\n";
                        foreach (DataRow row in dtCondStock.Rows)
                        {
                            send_meesage += "---------------------\n";
                            send_meesage += string.Join("/", row.ItemArray.Select(item => item.ToString())) + "\n";
                        }
                    }
                    send_meesage += "---------------------\n";
                    //
                    telegram_message($"\n{send_meesage}\n");
                    break;
                case "/T2":
                    telegram_message("보유 차트 수신\n");
                    //
                    string send_meesage2 = "";
                    //
                    lock (table2)
                    {
                        send_meesage2 = string.Join("/", dtCondStock_hold.Columns.Cast<DataColumn>().Select(column => column.ColumnName)) + "\n";
                        //
                        foreach (DataRow row in dtCondStock_hold.Rows)
                        {
                            send_meesage2 += "---------------------\n";
                            send_meesage2 += string.Join("/", row.ItemArray.Select(item => item.ToString())) + "\n";
                        }
                    }
                    send_meesage2 += "---------------------\n";
                    //
                    telegram_message($"\n{send_meesage2}\n");
                    break;
                case "/T3":
                    telegram_message("매매내역 차트 수신\n");
                    //
                    string send_meesage3 = "";
                    lock (table3)
                    {
                        send_meesage3 = string.Join("/", dtCondStock_Transaction.Columns.Cast<DataColumn>().Select(column => column.ColumnName)) + "\n";
                        //
                        foreach (DataRow row in dtCondStock_Transaction.Rows)
                        {
                            send_meesage3 += "---------------------\n";
                            send_meesage3 += string.Join("/", row.ItemArray.Select(item => item.ToString())) + "\n";
                        }
                    }
                    send_meesage3 += "---------------------\n";
                    //
                    telegram_message($"\n{send_meesage3}\n");
                    break;
                default:
                    telegram_message("명령어 없음 : 명령어 리스트(/HELP) 요청\n");
                    break;

            }
        }

        //--------------------------------------WEBHOK-------------------------------------------------------------

        /*
         http://your-public-ip:5000/api/webhook/
         향후 443 포트 제거
         {
            "Action": "매수",
            "Code": "A12345"
         } 
        */

        private HttpListener _listener = new HttpListener();
        private readonly string _url = "https://+:443/api/webhook/";

        public void TradingVIew_Listener_Start()
        {
            _listener.Start();
            WriteLog_System("Listening for connections on....\n");
            Task listenTask = HandleIncomingConnections();
            listenTask.GetAwaiter().GetResult();
            _listener.Close();
        }

        private async Task HandleIncomingConnections()
        {
            _listener.Prefixes.Add(_url);

            bool runServer = true;

            while (runServer)
            {
                HttpListenerContext context = await _listener.GetContextAsync();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                if ((request.HttpMethod == "POST") && request.HasEntityBody)
                {
                    using (System.IO.Stream body = request.InputStream)
                    {
                        using (System.IO.StreamReader reader = new System.IO.StreamReader(body, request.ContentEncoding))
                        {
                            string json = reader.ReadToEnd();
                            WriteLog_System($"Received JSON: {json}");

                            // JSON을 파싱하고 특정 함수 호출
                            ProcessWebhook(json);
                        }
                    }
                }

                string responseString = "Received";
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                await output.WriteAsync(buffer, 0, buffer.Length);
                output.Close();

                System.Threading.Thread.Sleep(1000);
            }
        }

        public class WebhookMessage
        {
            public string Action { get; set; }
            public string Code { get; set; }
        }

        private void ProcessWebhook(string json)
        {
            // JSON을 파싱하고 특정 함수 호출 로직 구현
            // 예시: JSON을 객체로 변환하고 처리
            WebhookMessage message = Newtonsoft.Json.JsonConvert.DeserializeObject<WebhookMessage>(json);
            ExecuteSpecificFunction(message);
        }

        private void ExecuteSpecificFunction(WebhookMessage message)
        {
            // 메시지에 따라 함수 실행 로직 구현
            WriteLog_System($"매매: {message.Action}, 종목코드: {message.Code}\n");
        }
    } 
}