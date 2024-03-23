using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Auto_Run_Update : Form
    {
        public string filepath = "C:\\Users\\krkr5\\OneDrive\\바탕 화면\\project\\password\\system_setting.txt";
        public bool auto_run;
        public string program_start;
        public string program_stop;
        public bool load_complete = false;

        static public bool Operation = true;
        static public bool Operation_start = true;

        public Auto_Run_Update()
        {
            InitializeComponent();

            timer1.Start();

            //
            button1.Click += Button1_Click;
            button2.Click += Button2_Click;

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            trade_auto = new Trade_Auto();
            trade_auto.FormClosed += Trade_auto_FormClosed;
            trade_auto.ShowDialog(); //form2 닫기 전까지 form1 제어 불가능
        }

        private void Trade_auto_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form_close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "파일 저장 경로 지정하세요";
            saveFileDialog.Filter = "텍스트 파일 (*.txt)|*.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string textToSave = "자동실행/" + checkBox1.Checked.ToString() + "\n" + "자동실행/" + "자동운영시간/" + start_time_text.Text + "/" + end_time_text.Text;

                // 사용자가 선택한 파일 경로
                string filePath = saveFileDialog.FileName;

                //파일에 텍스트 저장
                System.IO.File.WriteAllText(filePath, textToSave);
            }
        }

        private void timetimer(object sender, EventArgs e)
        {
            //시간표시
            Time_label.Text = DateTime.Now.ToString("yy MM-dd (ddd) HH:mm:ss");

            //
            file_load();

            //
            if (Operation && load_complete && auto_run) Opeartion_Time();
        }

        private void file_load()
        {
            StreamReader reader = new StreamReader(filepath);

            //자동실행
            String[] program_auto_run_allow = reader.ReadLine().Split('/');
            auto_run = Convert.ToBoolean(program_auto_run_allow[1]);
            checkBox1.Checked = auto_run;

            //자동 운영 시간
            String[] time_tmp = reader.ReadLine().Split('/');
            program_start = time_tmp[1];
            start_time_text.Text = time_tmp[1];
            program_stop = time_tmp[2];
            end_time_text.Text = time_tmp[2];

            reader.Close();
            //
            load_complete = true;
        }

        private Trade_Auto trade_auto;
        private bool isTradeAutoOpened = false;

        private void Opeartion_Time()
        {
            //운영시간 확인
            DateTime t_now = DateTime.Now;
            DateTime t_start = DateTime.Parse(program_start);
            DateTime t_end = DateTime.Parse(program_stop);

            //운영시간 아님
            if (!isTradeAutoOpened && trade_auto == null && t_now >= t_start && t_now <= t_end)
            {
                trade_auto = new Trade_Auto();
                trade_auto.ShowDialog(); //form2 닫기 전까지 form1 제어 불가능
                isTradeAutoOpened = true;
                label7.Text = "실행";
            }
            else if (isTradeAutoOpened && trade_auto != null && t_now > t_end)
            {
                Form_close();
                isTradeAutoOpened = false;
                label7.Text = "종료";
            }
        }

        private void Form_close()
        {
            if (trade_auto != null)
            {
                MessageBox.Show("이게마나");
                trade_auto.Close(); //폼을 닫고 닫기 이벤트를 발생, 폼이 닫힌 후에도 폼 객체는 메모리에 남음
                trade_auto.Dispose(); //폼이 사용한 모든 리소스(메모리, 핸들 등)를 해제
                trade_auto = null; //폼 객체에 대한 참조를 제거하여 리소스 누수(memory leak)를 방지
            }
        }
    }
}
