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
        public Form1()
        {
            InitializeComponent();

            axKHOpenAPI1.OnEventConnect += onEventConnect;
        }
        private void onEventConnect(object sender, AxKHOpenAPILib._DKHOpenAPIEvents_OnEventConnectEvent e)
        {
            if (e.nErrCode == 0)
                WriteLog("로그인 성공"); // 정상 처리
            else
                WriteLog(e.nErrCode.ToString()); // 에러 발생
        }
        private void WriteLog(string message)
        {
            rtxtLog.AppendText($@"{message}");
        }
    }
}
