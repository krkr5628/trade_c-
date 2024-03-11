using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
            //save & load
            save_button.Click += setting_save;
            setting_open.Click += setting_load;
            //auto load
            setting_load_auto();
            //조건식 로딩
            //


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
            //운영 시간
            String[] time_tmp = reader.ReadLine().Split('/');
            market_start_time.Text = time_tmp[1];
            market_end_time.Text = time_tmp[2];
            //계좌 번호
            String[] account_tmp = reader.ReadLine().Split('/');
            setting_account_number.Text = account_tmp[1];
            //초기 자산
            String[] balance_tmp = reader.ReadLine().Split('/');
            initial_balance.Text = balance_tmp[1];
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
            //
            for (int i = 0; i < user_account.Length; i++)
            {
                account_list.Items.Add(user_account[i]);
            }

            //
            Fomula_list_buy.Items.AddRange(Condition);
            Fomula_list_sell.Items.AddRange(Condition);
            if (Fomula_list_buy.Items.Count > 0)
            {
                Fomula_list_buy.SelectedIndex = 0;
                Fomula_list_sell.SelectedIndex = 0;
            }
        }


        private void Setting_Load(object sender, EventArgs e)
        {

        }
    }
}
