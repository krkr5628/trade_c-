using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        //Main
        public Form1()
        {
            InitializeComponent();
            Login_btn.Click += login_btn;
            axKHOpenAPI1.OnEventConnect += onEventConnect;
        }
        //로그인
        private void login_btn(object sender, EventArgs e)
        {
            axKHOpenAPI1.CommConnect();
        }
        private void onEventConnect(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent e)
        {
            if (e.nErrCode == 0)
            {
                // 정상 처리
                WriteLog("로그인 성공\n");
                //"ACCOUNT_CNT" : 보유계좌 갯수
                //"ACCLIST" 또는 "ACCNO" : 구분자 ';', 보유계좌 목록                
                string 계좌목록 = axKHOpenAPI1.GetLoginInfo("ACCLIST").Trim();
                //계좌목록은 ';'문자로 분리된 문자열
                //분리된 계좌를 ComboBox에 추가 
                string[] 사용자계좌 = 계좌목록.Split(';');                              
                for (int i = 0; i < 사용자계좌.Length; i++)
                {
                    User_account_list.Items.Add(사용자계좌[i]);
                }
                //사용자 id를 UserId 라벨에 추가
                string 사용자id = axKHOpenAPI1.GetLoginInfo("USER_ID");
                User_id.Text = 사용자id;
                //사용자 이름을 UserName 라벨에 추가
                string 사용자이름 = axKHOpenAPI1.GetLoginInfo("USER_NAME");
                User_name.Text = 사용자이름;
                //접속서버 구분(1 : 모의투자, 나머지: 실거래서버)
                string 접속서버구분 = axKHOpenAPI1.GetLoginInfo("GetServerGubun");
                if(접속서버구분.Equals("1"))
                {
                    User_connection.Text = "모의투자\n";
                }
                else
                {
                    User_connection.Text = "실제투자\n";
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
                else if(방화벽.Equals("1"))
                {
                    Fire_wall.Text = "설정\n";
                }
                else
                {
                    Fire_wall.Text = "해지\n";
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
        //로그창
        private void WriteLog(string message)
        {
            log_window.AppendText($@"{message}");
        }
    }
}
