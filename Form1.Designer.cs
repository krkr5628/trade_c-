
namespace WindowsFormsApp1
{
    partial class Trade_Auto
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Trade_Auto));
            this.Login_btn = new System.Windows.Forms.Button();
            this.log_window = new System.Windows.Forms.RichTextBox();
            this.axKHOpenAPI1 = new AxKHOpenAPILib.AxKHOpenAPI();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.User_money = new System.Windows.Forms.TextBox();
            this.total_balance = new System.Windows.Forms.Label();
            this.current_balance = new System.Windows.Forms.Label();
            this.total_profit = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.User_name = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.User_id = new System.Windows.Forms.Label();
            this.User_account_list = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Fomula_list = new System.Windows.Forms.ComboBox();
            this.Real_time_search_btn = new System.Windows.Forms.Button();
            this.Stock_search_btn = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.Fomula_search_btn = new System.Windows.Forms.Button();
            this.Normal_search_btn = new System.Windows.Forms.Button();
            this.Real_time_stop_btn = new System.Windows.Forms.Button();
            this.Stock_code = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menu = new System.Windows.Forms.MenuStrip();
            this.Main_menu = new System.Windows.Forms.ToolStripMenuItem();
            this.Order_setting_menu = new System.Windows.Forms.ToolStripMenuItem();
            this.System_setting_menu = new System.Windows.Forms.ToolStripMenuItem();
            this.time = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timetimer = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.User_connection = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Keyboard_wall = new System.Windows.Forms.Label();
            this.Fire_wall = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.axKHOpenAPI1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // Login_btn
            // 
            this.Login_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Login_btn.BackColor = System.Drawing.Color.SteelBlue;
            this.Login_btn.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Login_btn.ForeColor = System.Drawing.Color.Transparent;
            this.Login_btn.Location = new System.Drawing.Point(0, 0);
            this.Login_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Login_btn.Name = "Login_btn";
            this.Login_btn.Size = new System.Drawing.Size(106, 61);
            this.Login_btn.TabIndex = 1;
            this.Login_btn.Text = "UPDATE";
            this.Login_btn.UseVisualStyleBackColor = false;
            this.Login_btn.Click += new System.EventHandler(this.Login_btn_Click);
            // 
            // log_window
            // 
            this.log_window.AcceptsTab = true;
            this.log_window.Location = new System.Drawing.Point(1351, 513);
            this.log_window.Margin = new System.Windows.Forms.Padding(4);
            this.log_window.Name = "log_window";
            this.log_window.Size = new System.Drawing.Size(463, 464);
            this.log_window.TabIndex = 2;
            this.log_window.Text = "";
            // 
            // axKHOpenAPI1
            // 
            this.axKHOpenAPI1.Enabled = true;
            this.axKHOpenAPI1.Location = new System.Drawing.Point(1182, 106);
            this.axKHOpenAPI1.Margin = new System.Windows.Forms.Padding(4);
            this.axKHOpenAPI1.Name = "axKHOpenAPI1";
            this.axKHOpenAPI1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axKHOpenAPI1.OcxState")));
            this.axKHOpenAPI1.Size = new System.Drawing.Size(150, 75);
            this.axKHOpenAPI1.TabIndex = 0;
            this.axKHOpenAPI1.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(17, 513);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1319, 464);
            this.dataGridView1.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 10;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Controls.Add(this.label6, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.User_money, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.total_balance, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.current_balance, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.total_profit, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label8, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.label11, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.label13, 8, 0);
            this.tableLayoutPanel1.Controls.Add(this.label14, 9, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(17, 198);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1319, 87);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.LightSlateGray;
            this.label6.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(397, 0);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 43);
            this.label6.TabIndex = 4;
            this.label6.Text = "예수금";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // User_money
            // 
            this.User_money.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.User_money.Location = new System.Drawing.Point(397, 47);
            this.User_money.Margin = new System.Windows.Forms.Padding(4);
            this.User_money.Name = "User_money";
            this.User_money.ReadOnly = true;
            this.User_money.Size = new System.Drawing.Size(123, 28);
            this.User_money.TabIndex = 16;
            // 
            // total_balance
            // 
            this.total_balance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.total_balance.AutoSize = true;
            this.total_balance.BackColor = System.Drawing.Color.LightSlateGray;
            this.total_balance.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.total_balance.ForeColor = System.Drawing.Color.White;
            this.total_balance.Location = new System.Drawing.Point(4, 0);
            this.total_balance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.total_balance.Name = "total_balance";
            this.total_balance.Size = new System.Drawing.Size(123, 43);
            this.total_balance.TabIndex = 17;
            this.total_balance.Text = "총자산";
            this.total_balance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // current_balance
            // 
            this.current_balance.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.current_balance.AutoSize = true;
            this.current_balance.BackColor = System.Drawing.Color.LightSlateGray;
            this.current_balance.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.current_balance.ForeColor = System.Drawing.Color.White;
            this.current_balance.Location = new System.Drawing.Point(135, 0);
            this.current_balance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.current_balance.Name = "current_balance";
            this.current_balance.Size = new System.Drawing.Size(123, 43);
            this.current_balance.TabIndex = 18;
            this.current_balance.Text = "현자산(장전)";
            this.current_balance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // total_profit
            // 
            this.total_profit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.total_profit.AutoSize = true;
            this.total_profit.BackColor = System.Drawing.Color.LightSlateGray;
            this.total_profit.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.total_profit.ForeColor = System.Drawing.Color.White;
            this.total_profit.Location = new System.Drawing.Point(266, 0);
            this.total_profit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.total_profit.Name = "total_profit";
            this.total_profit.Size = new System.Drawing.Size(123, 43);
            this.total_profit.TabIndex = 19;
            this.total_profit.Text = "총수익";
            this.total_profit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.LightSlateGray;
            this.label1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(528, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 43);
            this.label1.TabIndex = 20;
            this.label1.Text = "매입금액";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.LightSlateGray;
            this.label7.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(659, 0);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 43);
            this.label7.TabIndex = 21;
            this.label7.Text = "평가금액";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.LightSlateGray;
            this.label8.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(790, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(123, 43);
            this.label8.TabIndex = 22;
            this.label8.Text = "평가손익";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.LightSlateGray;
            this.label11.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(921, 0);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(123, 43);
            this.label11.TabIndex = 23;
            this.label11.Text = "평가손익률";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.LightSlateGray;
            this.label13.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(1052, 0);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(123, 43);
            this.label13.TabIndex = 24;
            this.label13.Text = "당일손익";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.LightSlateGray;
            this.label14.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(1183, 0);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(132, 43);
            this.label14.TabIndex = 25;
            this.label14.Text = "당일손익률";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // User_name
            // 
            this.User_name.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.User_name.AutoSize = true;
            this.User_name.BackColor = System.Drawing.Color.White;
            this.User_name.Location = new System.Drawing.Point(322, 0);
            this.User_name.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.User_name.Name = "User_name";
            this.User_name.Size = new System.Drawing.Size(98, 55);
            this.User_name.TabIndex = 6;
            this.User_name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.LightSlateGray;
            this.label2.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(4, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 55);
            this.label2.TabIndex = 0;
            this.label2.Text = "ID";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.LightSlateGray;
            this.label3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(428, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 55);
            this.label3.TabIndex = 1;
            this.label3.Text = "계좌번호";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.LightSlateGray;
            this.label5.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(216, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 55);
            this.label5.TabIndex = 3;
            this.label5.Text = "NAME";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // User_id
            // 
            this.User_id.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.User_id.AutoSize = true;
            this.User_id.BackColor = System.Drawing.Color.White;
            this.User_id.Location = new System.Drawing.Point(110, 0);
            this.User_id.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.User_id.Name = "User_id";
            this.User_id.Size = new System.Drawing.Size(98, 55);
            this.User_id.TabIndex = 5;
            this.User_id.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // User_account_list
            // 
            this.User_account_list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.User_account_list.FormattingEnabled = true;
            this.User_account_list.ItemHeight = 18;
            this.User_account_list.Location = new System.Drawing.Point(534, 4);
            this.User_account_list.Margin = new System.Windows.Forms.Padding(4);
            this.User_account_list.Name = "User_account_list";
            this.User_account_list.Size = new System.Drawing.Size(98, 26);
            this.User_account_list.TabIndex = 11;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel2.Controls.Add(this.Fomula_list, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.Real_time_search_btn, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.Stock_search_btn, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label9, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label12, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.Fomula_search_btn, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.Normal_search_btn, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.Real_time_stop_btn, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.Stock_code, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(1344, 126);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(463, 159);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // Fomula_list
            // 
            this.Fomula_list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fomula_list.FormattingEnabled = true;
            this.Fomula_list.Location = new System.Drawing.Point(158, 57);
            this.Fomula_list.Margin = new System.Windows.Forms.Padding(4);
            this.Fomula_list.Name = "Fomula_list";
            this.Fomula_list.Size = new System.Drawing.Size(146, 26);
            this.Fomula_list.TabIndex = 12;
            // 
            // Real_time_search_btn
            // 
            this.Real_time_search_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Real_time_search_btn.BackColor = System.Drawing.Color.SteelBlue;
            this.Real_time_search_btn.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Real_time_search_btn.ForeColor = System.Drawing.Color.Transparent;
            this.Real_time_search_btn.Location = new System.Drawing.Point(154, 106);
            this.Real_time_search_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Real_time_search_btn.Name = "Real_time_search_btn";
            this.Real_time_search_btn.Size = new System.Drawing.Size(154, 53);
            this.Real_time_search_btn.TabIndex = 7;
            this.Real_time_search_btn.Text = "실시간 검색";
            this.Real_time_search_btn.UseVisualStyleBackColor = false;
            // 
            // Stock_search_btn
            // 
            this.Stock_search_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Stock_search_btn.BackColor = System.Drawing.Color.OliveDrab;
            this.Stock_search_btn.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Stock_search_btn.ForeColor = System.Drawing.Color.Transparent;
            this.Stock_search_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Stock_search_btn.Location = new System.Drawing.Point(308, 0);
            this.Stock_search_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Stock_search_btn.Name = "Stock_search_btn";
            this.Stock_search_btn.Size = new System.Drawing.Size(155, 53);
            this.Stock_search_btn.TabIndex = 7;
            this.Stock_search_btn.Text = "종목 조회";
            this.Stock_search_btn.UseVisualStyleBackColor = false;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.LightSlateGray;
            this.label9.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(4, 0);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(146, 53);
            this.label9.TabIndex = 1;
            this.label9.Text = "종목 코드";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.LightSlateGray;
            this.label12.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(4, 53);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(146, 53);
            this.label12.TabIndex = 2;
            this.label12.Text = "조건식";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Fomula_search_btn
            // 
            this.Fomula_search_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fomula_search_btn.BackColor = System.Drawing.Color.OliveDrab;
            this.Fomula_search_btn.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Fomula_search_btn.ForeColor = System.Drawing.Color.Transparent;
            this.Fomula_search_btn.Location = new System.Drawing.Point(308, 53);
            this.Fomula_search_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Fomula_search_btn.Name = "Fomula_search_btn";
            this.Fomula_search_btn.Size = new System.Drawing.Size(155, 53);
            this.Fomula_search_btn.TabIndex = 8;
            this.Fomula_search_btn.Text = "조건식 조회";
            this.Fomula_search_btn.UseVisualStyleBackColor = false;
            // 
            // Normal_search_btn
            // 
            this.Normal_search_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Normal_search_btn.BackColor = System.Drawing.Color.OliveDrab;
            this.Normal_search_btn.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Normal_search_btn.ForeColor = System.Drawing.Color.Transparent;
            this.Normal_search_btn.Location = new System.Drawing.Point(0, 106);
            this.Normal_search_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Normal_search_btn.Name = "Normal_search_btn";
            this.Normal_search_btn.Size = new System.Drawing.Size(154, 53);
            this.Normal_search_btn.TabIndex = 9;
            this.Normal_search_btn.Text = "일반 검색";
            this.Normal_search_btn.UseVisualStyleBackColor = false;
            // 
            // Real_time_stop_btn
            // 
            this.Real_time_stop_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Real_time_stop_btn.BackColor = System.Drawing.Color.Crimson;
            this.Real_time_stop_btn.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Real_time_stop_btn.ForeColor = System.Drawing.Color.Transparent;
            this.Real_time_stop_btn.Location = new System.Drawing.Point(308, 106);
            this.Real_time_stop_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Real_time_stop_btn.Name = "Real_time_stop_btn";
            this.Real_time_stop_btn.Size = new System.Drawing.Size(155, 53);
            this.Real_time_stop_btn.TabIndex = 10;
            this.Real_time_stop_btn.Text = "실시간 중단";
            this.Real_time_stop_btn.UseVisualStyleBackColor = false;
            // 
            // Stock_code
            // 
            this.Stock_code.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Stock_code.Location = new System.Drawing.Point(158, 4);
            this.Stock_code.Margin = new System.Windows.Forms.Padding(4);
            this.Stock_code.Name = "Stock_code";
            this.Stock_code.Size = new System.Drawing.Size(146, 28);
            this.Stock_code.TabIndex = 13;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // menu
            // 
            this.menu.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Main_menu,
            this.Order_setting_menu,
            this.System_setting_menu});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1830, 33);
            this.menu.TabIndex = 7;
            this.menu.Text = "menuStrip1";
            // 
            // Main_menu
            // 
            this.Main_menu.Name = "Main_menu";
            this.Main_menu.Size = new System.Drawing.Size(64, 29);
            this.Main_menu.Text = "메인";
            // 
            // Order_setting_menu
            // 
            this.Order_setting_menu.Name = "Order_setting_menu";
            this.Order_setting_menu.Size = new System.Drawing.Size(106, 29);
            this.Order_setting_menu.Text = "거래 설정";
            // 
            // System_setting_menu
            // 
            this.System_setting_menu.Name = "System_setting_menu";
            this.System_setting_menu.Size = new System.Drawing.Size(124, 29);
            this.System_setting_menu.Text = "시스템 설정";
            // 
            // time
            // 
            this.time.Location = new System.Drawing.Point(0, 0);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(100, 23);
            this.time.TabIndex = 8;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.ClockEvent);
            // 
            // timetimer
            // 
            this.timetimer.BackColor = System.Drawing.Color.DarkGreen;
            this.timetimer.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.timetimer.ForeColor = System.Drawing.Color.MediumAquamarine;
            this.timetimer.Location = new System.Drawing.Point(1487, 50);
            this.timetimer.Margin = new System.Windows.Forms.Padding(0);
            this.timetimer.Name = "timetimer";
            this.timetimer.Size = new System.Drawing.Size(320, 60);
            this.timetimer.TabIndex = 9;
            this.timetimer.Text = "24 02-21 (수) 14:50:30";
            this.timetimer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(17, 306);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 62;
            this.dataGridView2.RowTemplate.Height = 30;
            this.dataGridView2.Size = new System.Drawing.Size(1319, 187);
            this.dataGridView2.TabIndex = 10;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel3.Controls.Add(this.Login_btn, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.button3, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.button1, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.button2, 5, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(17, 50);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(641, 61);
            this.tableLayoutPanel3.TabIndex = 11;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.Color.SteelBlue;
            this.button3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button3.ForeColor = System.Drawing.Color.Transparent;
            this.button3.Location = new System.Drawing.Point(212, 0);
            this.button3.Margin = new System.Windows.Forms.Padding(0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(106, 61);
            this.button3.TabIndex = 4;
            this.button3.Text = "매매내역";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.SteelBlue;
            this.button1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.ForeColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(106, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 61);
            this.button1.TabIndex = 2;
            this.button1.Text = "매매설정";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.SteelBlue;
            this.button2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button2.ForeColor = System.Drawing.Color.Transparent;
            this.button2.Location = new System.Drawing.Point(530, 0);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(111, 61);
            this.button2.TabIndex = 3;
            this.button2.Text = "업데이트  내역";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // User_connection
            // 
            this.User_connection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.User_connection.BackColor = System.Drawing.Color.Gainsboro;
            this.User_connection.Font = new System.Drawing.Font("맑은 고딕", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.User_connection.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.User_connection.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.User_connection.Location = new System.Drawing.Point(1351, 50);
            this.User_connection.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.User_connection.Name = "User_connection";
            this.User_connection.Size = new System.Drawing.Size(132, 60);
            this.User_connection.TabIndex = 13;
            this.User_connection.Text = "모의";
            this.User_connection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.LightSlateGray;
            this.label4.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(640, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 55);
            this.label4.TabIndex = 2;
            this.label4.Text = "KEYBOARD";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Keyboard_wall
            // 
            this.Keyboard_wall.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Keyboard_wall.AutoSize = true;
            this.Keyboard_wall.BackColor = System.Drawing.Color.White;
            this.Keyboard_wall.Location = new System.Drawing.Point(746, 0);
            this.Keyboard_wall.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Keyboard_wall.Name = "Keyboard_wall";
            this.Keyboard_wall.Size = new System.Drawing.Size(98, 55);
            this.Keyboard_wall.TabIndex = 15;
            this.Keyboard_wall.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Fire_wall
            // 
            this.Fire_wall.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fire_wall.AutoSize = true;
            this.Fire_wall.BackColor = System.Drawing.Color.White;
            this.Fire_wall.Location = new System.Drawing.Point(958, 0);
            this.Fire_wall.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Fire_wall.Name = "Fire_wall";
            this.Fire_wall.Size = new System.Drawing.Size(106, 55);
            this.Fire_wall.TabIndex = 9;
            this.Fire_wall.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.LightSlateGray;
            this.label10.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(852, 0);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 55);
            this.label10.TabIndex = 14;
            this.label10.Text = "FIREWALL";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tableLayoutPanel4.ColumnCount = 10;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.Controls.Add(this.User_account_list, 5, 0);
            this.tableLayoutPanel4.Controls.Add(this.label3, 4, 0);
            this.tableLayoutPanel4.Controls.Add(this.User_name, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.User_id, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.label5, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.Fire_wall, 9, 0);
            this.tableLayoutPanel4.Controls.Add(this.label10, 8, 0);
            this.tableLayoutPanel4.Controls.Add(this.Keyboard_wall, 7, 0);
            this.tableLayoutPanel4.Controls.Add(this.label4, 6, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(17, 126);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1068, 55);
            this.tableLayoutPanel4.TabIndex = 12;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 4;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel5.Controls.Add(this.button4, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.button5, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.button6, 2, 1);
            this.tableLayoutPanel5.Controls.Add(this.button7, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.button8, 2, 2);
            this.tableLayoutPanel5.Controls.Add(this.button9, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.button10, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.button11, 2, 3);
            this.tableLayoutPanel5.Controls.Add(this.button12, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.button13, 2, 4);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(1351, 306);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 5;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(456, 187);
            this.tableLayoutPanel5.TabIndex = 5;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.button4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button4.ForeColor = System.Drawing.Color.Transparent;
            this.button4.Location = new System.Drawing.Point(0, 0);
            this.button4.Margin = new System.Windows.Forms.Padding(0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(114, 37);
            this.button4.TabIndex = 5;
            this.button4.Text = "동작시간";
            this.button4.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button5.BackColor = System.Drawing.Color.LightBlue;
            this.button5.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button5.ForeColor = System.Drawing.Color.Transparent;
            this.button5.Location = new System.Drawing.Point(0, 37);
            this.button5.Margin = new System.Windows.Forms.Padding(0);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(114, 37);
            this.button5.TabIndex = 6;
            this.button5.Text = "매수조건";
            this.button5.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.BackColor = System.Drawing.Color.OrangeRed;
            this.button6.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button6.ForeColor = System.Drawing.Color.Transparent;
            this.button6.Location = new System.Drawing.Point(228, 37);
            this.button6.Margin = new System.Windows.Forms.Padding(0);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(114, 37);
            this.button6.TabIndex = 7;
            this.button6.Text = "매도조건";
            this.button6.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.BackColor = System.Drawing.Color.LightBlue;
            this.button7.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button7.ForeColor = System.Drawing.Color.Transparent;
            this.button7.Location = new System.Drawing.Point(0, 74);
            this.button7.Margin = new System.Windows.Forms.Padding(0);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(114, 37);
            this.button7.TabIndex = 8;
            this.button7.Text = "매매방식";
            this.button7.UseVisualStyleBackColor = false;
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button8.BackColor = System.Drawing.Color.OrangeRed;
            this.button8.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button8.ForeColor = System.Drawing.Color.Transparent;
            this.button8.Location = new System.Drawing.Point(228, 74);
            this.button8.Margin = new System.Windows.Forms.Padding(0);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(114, 37);
            this.button8.TabIndex = 9;
            this.button8.Text = "매매방식";
            this.button8.UseVisualStyleBackColor = false;
            // 
            // button9
            // 
            this.button9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button9.BackColor = System.Drawing.Color.LightBlue;
            this.button9.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button9.ForeColor = System.Drawing.Color.Transparent;
            this.button9.Location = new System.Drawing.Point(0, 111);
            this.button9.Margin = new System.Windows.Forms.Padding(0);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(114, 37);
            this.button9.TabIndex = 10;
            this.button9.Text = "분할여부";
            this.button9.UseVisualStyleBackColor = false;
            // 
            // button10
            // 
            this.button10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button10.BackColor = System.Drawing.Color.DimGray;
            this.button10.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button10.ForeColor = System.Drawing.Color.Transparent;
            this.button10.Location = new System.Drawing.Point(228, 0);
            this.button10.Margin = new System.Windows.Forms.Padding(0);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(114, 37);
            this.button10.TabIndex = 11;
            this.button10.Text = "당일청산";
            this.button10.UseVisualStyleBackColor = false;
            // 
            // button11
            // 
            this.button11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button11.BackColor = System.Drawing.Color.OrangeRed;
            this.button11.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button11.ForeColor = System.Drawing.Color.Transparent;
            this.button11.Location = new System.Drawing.Point(228, 111);
            this.button11.Margin = new System.Windows.Forms.Padding(0);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(114, 37);
            this.button11.TabIndex = 12;
            this.button11.Text = "분할여부";
            this.button11.UseVisualStyleBackColor = false;
            // 
            // button12
            // 
            this.button12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button12.BackColor = System.Drawing.Color.MediumOrchid;
            this.button12.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button12.ForeColor = System.Drawing.Color.Transparent;
            this.button12.Location = new System.Drawing.Point(0, 148);
            this.button12.Margin = new System.Windows.Forms.Padding(0);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(114, 39);
            this.button12.TabIndex = 13;
            this.button12.Text = "이익(%)";
            this.button12.UseVisualStyleBackColor = false;
            // 
            // button13
            // 
            this.button13.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button13.BackColor = System.Drawing.Color.MediumPurple;
            this.button13.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button13.ForeColor = System.Drawing.Color.Transparent;
            this.button13.Location = new System.Drawing.Point(228, 148);
            this.button13.Margin = new System.Windows.Forms.Padding(0);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(114, 39);
            this.button13.TabIndex = 14;
            this.button13.Text = "손실(%)";
            this.button13.UseVisualStyleBackColor = false;
            // 
            // Trade_Auto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1830, 1003);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.tableLayoutPanel5);
            this.Controls.Add(this.timetimer);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.User_connection);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.log_window);
            this.Controls.Add(this.axKHOpenAPI1);
            this.Controls.Add(this.time);
            this.MainMenuStrip = this.menu;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Trade_Auto";
            this.Text = "Trade_Auto";
            ((System.ComponentModel.ISupportInitialize)(this.axKHOpenAPI1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxKHOpenAPILib.AxKHOpenAPI axKHOpenAPI1;
        private System.Windows.Forms.Button Login_btn;
        private System.Windows.Forms.RichTextBox log_window;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label User_name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox User_account_list;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label User_id;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button Real_time_search_btn;
        private System.Windows.Forms.Button Stock_search_btn;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button Fomula_search_btn;
        private System.Windows.Forms.Button Normal_search_btn;
        private System.Windows.Forms.Button Real_time_stop_btn;
        private System.Windows.Forms.ComboBox Fomula_list;
        private System.Windows.Forms.TextBox Stock_code;
        private System.Windows.Forms.TextBox User_money;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem Main_menu;
        private System.Windows.Forms.ToolStripMenuItem Order_setting_menu;
        private System.Windows.Forms.ToolStripMenuItem System_setting_menu;
        private System.Windows.Forms.Label time;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label timetimer;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label User_connection;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label Keyboard_wall;
        private System.Windows.Forms.Label Fire_wall;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label total_balance;
        private System.Windows.Forms.Label current_balance;
        private System.Windows.Forms.Label total_profit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
    }
}

