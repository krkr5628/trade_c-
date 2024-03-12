
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
            this.extra = new System.Windows.Forms.TextBox();
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
            this.time = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timetimer = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.button3 = new System.Windows.Forms.Button();
            this.Trade_setting = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.User_connection = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Keyboard_wall = new System.Windows.Forms.Label();
            this.Fire_wall = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.button4 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.sell_condtion_method = new System.Windows.Forms.TextBox();
            this.buy_condtion_method = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.total_money = new System.Windows.Forms.TextBox();
            this.User_money = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.buy_condition = new System.Windows.Forms.ComboBox();
            this.sell_condtion = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.axKHOpenAPI1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.groupBox4.SuspendLayout();
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
            this.Login_btn.Size = new System.Drawing.Size(106, 62);
            this.Login_btn.TabIndex = 1;
            this.Login_btn.Text = "UPDATE";
            this.Login_btn.UseVisualStyleBackColor = false;
            this.Login_btn.Click += new System.EventHandler(this.Login_btn_Click);
            // 
            // log_window
            // 
            this.log_window.AcceptsTab = true;
            this.log_window.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.log_window.Location = new System.Drawing.Point(7, 28);
            this.log_window.Margin = new System.Windows.Forms.Padding(4);
            this.log_window.Name = "log_window";
            this.log_window.Size = new System.Drawing.Size(443, 604);
            this.log_window.TabIndex = 2;
            this.log_window.Text = "";
            // 
            // axKHOpenAPI1
            // 
            this.axKHOpenAPI1.Enabled = true;
            this.axKHOpenAPI1.Location = new System.Drawing.Point(1104, 50);
            this.axKHOpenAPI1.Margin = new System.Windows.Forms.Padding(4);
            this.axKHOpenAPI1.Name = "axKHOpenAPI1";
            this.axKHOpenAPI1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axKHOpenAPI1.OcxState")));
            this.axKHOpenAPI1.Size = new System.Drawing.Size(225, 112);
            this.axKHOpenAPI1.TabIndex = 0;
            this.axKHOpenAPI1.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 72);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1306, 448);
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
            this.tableLayoutPanel1.Controls.Add(this.textBox13, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox12, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.User_money, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.total_money, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox9, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox8, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox7, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox6, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.extra, 3, 1);
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
            // extra
            // 
            this.extra.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.extra.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.extra.Location = new System.Drawing.Point(1183, 47);
            this.extra.Margin = new System.Windows.Forms.Padding(4);
            this.extra.Name = "extra";
            this.extra.ReadOnly = true;
            this.extra.Size = new System.Drawing.Size(132, 28);
            this.extra.TabIndex = 16;
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
            this.User_name.Size = new System.Drawing.Size(98, 56);
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
            this.label2.Size = new System.Drawing.Size(98, 56);
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
            this.label3.Size = new System.Drawing.Size(98, 56);
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
            this.label5.Size = new System.Drawing.Size(98, 56);
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
            this.User_id.Size = new System.Drawing.Size(98, 56);
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(1351, 126);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(456, 159);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // Fomula_list
            // 
            this.Fomula_list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fomula_list.FormattingEnabled = true;
            this.Fomula_list.Location = new System.Drawing.Point(155, 57);
            this.Fomula_list.Margin = new System.Windows.Forms.Padding(4);
            this.Fomula_list.Name = "Fomula_list";
            this.Fomula_list.Size = new System.Drawing.Size(144, 26);
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
            this.Real_time_search_btn.Location = new System.Drawing.Point(151, 106);
            this.Real_time_search_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Real_time_search_btn.Name = "Real_time_search_btn";
            this.Real_time_search_btn.Size = new System.Drawing.Size(152, 53);
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
            this.Stock_search_btn.Location = new System.Drawing.Point(303, 0);
            this.Stock_search_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Stock_search_btn.Name = "Stock_search_btn";
            this.Stock_search_btn.Size = new System.Drawing.Size(153, 53);
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
            this.label9.Size = new System.Drawing.Size(143, 53);
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
            this.label12.Size = new System.Drawing.Size(143, 53);
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
            this.Fomula_search_btn.Location = new System.Drawing.Point(303, 53);
            this.Fomula_search_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Fomula_search_btn.Name = "Fomula_search_btn";
            this.Fomula_search_btn.Size = new System.Drawing.Size(153, 53);
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
            this.Normal_search_btn.Size = new System.Drawing.Size(151, 53);
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
            this.Real_time_stop_btn.Location = new System.Drawing.Point(303, 106);
            this.Real_time_stop_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Real_time_stop_btn.Name = "Real_time_stop_btn";
            this.Real_time_stop_btn.Size = new System.Drawing.Size(153, 53);
            this.Real_time_stop_btn.TabIndex = 10;
            this.Real_time_stop_btn.Text = "실시간 중단";
            this.Real_time_stop_btn.UseVisualStyleBackColor = false;
            // 
            // Stock_code
            // 
            this.Stock_code.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Stock_code.Location = new System.Drawing.Point(155, 4);
            this.Stock_code.Margin = new System.Windows.Forms.Padding(4);
            this.Stock_code.Name = "Stock_code";
            this.Stock_code.Size = new System.Drawing.Size(144, 28);
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
            this.Main_menu});
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
            // time
            // 
            this.time.Location = new System.Drawing.Point(0, 0);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(100, 22);
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
            this.dataGridView2.Location = new System.Drawing.Point(6, 74);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 62;
            this.dataGridView2.RowTemplate.Height = 30;
            this.dataGridView2.Size = new System.Drawing.Size(1306, 222);
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
            this.tableLayoutPanel3.Controls.Add(this.Trade_setting, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.button14, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.button2, 3, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(17, 50);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(641, 62);
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
            this.button3.Size = new System.Drawing.Size(106, 62);
            this.button3.TabIndex = 4;
            this.button3.Text = "매매내역";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // Trade_setting
            // 
            this.Trade_setting.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Trade_setting.BackColor = System.Drawing.Color.SteelBlue;
            this.Trade_setting.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Trade_setting.ForeColor = System.Drawing.Color.Transparent;
            this.Trade_setting.Location = new System.Drawing.Point(106, 0);
            this.Trade_setting.Margin = new System.Windows.Forms.Padding(0);
            this.Trade_setting.Name = "Trade_setting";
            this.Trade_setting.Size = new System.Drawing.Size(106, 62);
            this.Trade_setting.TabIndex = 2;
            this.Trade_setting.Text = "매매설정";
            this.Trade_setting.UseVisualStyleBackColor = false;
            // 
            // button14
            // 
            this.button14.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button14.BackColor = System.Drawing.Color.SteelBlue;
            this.button14.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button14.ForeColor = System.Drawing.Color.Transparent;
            this.button14.Location = new System.Drawing.Point(424, 0);
            this.button14.Margin = new System.Windows.Forms.Padding(0);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(106, 62);
            this.button14.TabIndex = 5;
            this.button14.Text = "동의사항";
            this.button14.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.SteelBlue;
            this.button2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button2.ForeColor = System.Drawing.Color.Transparent;
            this.button2.Location = new System.Drawing.Point(318, 0);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(106, 62);
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
            this.User_connection.Size = new System.Drawing.Size(131, 62);
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
            this.label4.Size = new System.Drawing.Size(98, 56);
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
            this.Keyboard_wall.Size = new System.Drawing.Size(98, 56);
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
            this.Fire_wall.Size = new System.Drawing.Size(107, 56);
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
            this.label10.Size = new System.Drawing.Size(98, 56);
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
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1069, 56);
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
            this.tableLayoutPanel5.Controls.Add(this.button10, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.button12, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.button13, 2, 1);
            this.tableLayoutPanel5.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tableLayoutPanel5.Location = new System.Drawing.Point(11, 27);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 4;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(439, 168);
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
            this.button4.Size = new System.Drawing.Size(109, 42);
            this.button4.TabIndex = 5;
            this.button4.Text = "동작시간";
            this.button4.UseVisualStyleBackColor = false;
            // 
            // button10
            // 
            this.button10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button10.BackColor = System.Drawing.Color.DimGray;
            this.button10.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button10.ForeColor = System.Drawing.Color.Transparent;
            this.button10.Location = new System.Drawing.Point(218, 0);
            this.button10.Margin = new System.Windows.Forms.Padding(0);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(109, 42);
            this.button10.TabIndex = 11;
            this.button10.Text = "당일청산";
            this.button10.UseVisualStyleBackColor = false;
            // 
            // button12
            // 
            this.button12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button12.BackColor = System.Drawing.Color.MediumOrchid;
            this.button12.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button12.ForeColor = System.Drawing.Color.Transparent;
            this.button12.Location = new System.Drawing.Point(0, 42);
            this.button12.Margin = new System.Windows.Forms.Padding(0);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(109, 42);
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
            this.button13.Location = new System.Drawing.Point(218, 42);
            this.button13.Margin = new System.Windows.Forms.Padding(0);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(109, 42);
            this.button13.TabIndex = 14;
            this.button13.Text = "손실(%)";
            this.button13.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.log_window);
            this.groupBox1.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.Location = new System.Drawing.Point(1351, 513);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(456, 646);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "LOG";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel7);
            this.groupBox2.Controls.Add(this.dataGridView2);
            this.groupBox2.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox2.Location = new System.Drawing.Point(17, 306);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1319, 314);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "계좌보유 현황";
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 8;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel7.Controls.Add(this.button1, 4, 0);
            this.tableLayoutPanel7.Controls.Add(this.button9, 5, 0);
            this.tableLayoutPanel7.Controls.Add(this.button11, 6, 0);
            this.tableLayoutPanel7.Controls.Add(this.label15, 0, 0);
            this.tableLayoutPanel7.Location = new System.Drawing.Point(7, 27);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(1301, 42);
            this.tableLayoutPanel7.TabIndex = 11;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(651, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 36);
            this.button1.TabIndex = 2;
            this.button1.Text = "전체 청산";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button9.Location = new System.Drawing.Point(813, 3);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(156, 36);
            this.button9.TabIndex = 3;
            this.button9.Text = "수익 청산";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            this.button11.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button11.Location = new System.Drawing.Point(975, 3);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(156, 36);
            this.button11.TabIndex = 4;
            this.button11.Text = "손실 청산";
            this.button11.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(156, 42);
            this.label15.TabIndex = 14;
            this.label15.Text = "갱신주기";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel6);
            this.groupBox3.Controls.Add(this.dataGridView1);
            this.groupBox3.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox3.Location = new System.Drawing.Point(17, 626);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1319, 536);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "실시간 조건검색 현황";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 8;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel6.Controls.Add(this.sell_condtion, 5, 0);
            this.tableLayoutPanel6.Controls.Add(this.label19, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.label18, 6, 0);
            this.tableLayoutPanel6.Controls.Add(this.label17, 4, 0);
            this.tableLayoutPanel6.Controls.Add(this.label16, 2, 0);
            this.tableLayoutPanel6.Controls.Add(this.sell_condtion_method, 7, 0);
            this.tableLayoutPanel6.Controls.Add(this.buy_condtion_method, 3, 0);
            this.tableLayoutPanel6.Controls.Add(this.buy_condition, 1, 0);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(6, 28);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(1306, 36);
            this.tableLayoutPanel6.TabIndex = 11;
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label19.ForeColor = System.Drawing.Color.Crimson;
            this.label19.Location = new System.Drawing.Point(3, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(157, 36);
            this.label19.TabIndex = 15;
            this.label19.Text = "매수조건";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label18.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label18.Location = new System.Drawing.Point(981, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(157, 36);
            this.label18.TabIndex = 17;
            this.label18.Text = "매도방식";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label17.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label17.Location = new System.Drawing.Point(655, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(157, 36);
            this.label17.TabIndex = 16;
            this.label17.Text = "매도조건";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.label16.ForeColor = System.Drawing.Color.Crimson;
            this.label16.Location = new System.Drawing.Point(329, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(157, 36);
            this.label16.TabIndex = 15;
            this.label16.Text = "매수방식";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sell_condtion_method
            // 
            this.sell_condtion_method.Location = new System.Drawing.Point(1144, 3);
            this.sell_condtion_method.Name = "sell_condtion_method";
            this.sell_condtion_method.ReadOnly = true;
            this.sell_condtion_method.Size = new System.Drawing.Size(158, 30);
            this.sell_condtion_method.TabIndex = 13;
            // 
            // buy_condtion_method
            // 
            this.buy_condtion_method.Location = new System.Drawing.Point(492, 3);
            this.buy_condtion_method.Name = "buy_condtion_method";
            this.buy_condtion_method.ReadOnly = true;
            this.buy_condtion_method.Size = new System.Drawing.Size(155, 30);
            this.buy_condtion_method.TabIndex = 11;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tableLayoutPanel5);
            this.groupBox4.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox4.Location = new System.Drawing.Point(1351, 306);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(456, 201);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "설정 요약";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox1.Location = new System.Drawing.Point(659, 47);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(123, 28);
            this.textBox1.TabIndex = 26;
            // 
            // textBox6
            // 
            this.textBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox6.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox6.Location = new System.Drawing.Point(1052, 47);
            this.textBox6.Margin = new System.Windows.Forms.Padding(4);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(123, 28);
            this.textBox6.TabIndex = 27;
            // 
            // textBox7
            // 
            this.textBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox7.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox7.Location = new System.Drawing.Point(921, 47);
            this.textBox7.Margin = new System.Windows.Forms.Padding(4);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(123, 28);
            this.textBox7.TabIndex = 28;
            // 
            // textBox8
            // 
            this.textBox8.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox8.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox8.Location = new System.Drawing.Point(790, 47);
            this.textBox8.Margin = new System.Windows.Forms.Padding(4);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(123, 28);
            this.textBox8.TabIndex = 29;
            // 
            // textBox9
            // 
            this.textBox9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox9.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox9.Location = new System.Drawing.Point(135, 47);
            this.textBox9.Margin = new System.Windows.Forms.Padding(4);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(123, 28);
            this.textBox9.TabIndex = 30;
            // 
            // total_money
            // 
            this.total_money.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.total_money.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.total_money.Location = new System.Drawing.Point(4, 47);
            this.total_money.Margin = new System.Windows.Forms.Padding(4);
            this.total_money.Name = "total_money";
            this.total_money.ReadOnly = true;
            this.total_money.Size = new System.Drawing.Size(123, 28);
            this.total_money.TabIndex = 31;
            // 
            // User_money
            // 
            this.User_money.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.User_money.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.User_money.Location = new System.Drawing.Point(397, 47);
            this.User_money.Margin = new System.Windows.Forms.Padding(4);
            this.User_money.Name = "User_money";
            this.User_money.ReadOnly = true;
            this.User_money.Size = new System.Drawing.Size(123, 28);
            this.User_money.TabIndex = 32;
            // 
            // textBox12
            // 
            this.textBox12.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox12.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox12.Location = new System.Drawing.Point(266, 47);
            this.textBox12.Margin = new System.Windows.Forms.Padding(4);
            this.textBox12.Name = "textBox12";
            this.textBox12.ReadOnly = true;
            this.textBox12.Size = new System.Drawing.Size(123, 28);
            this.textBox12.TabIndex = 33;
            // 
            // textBox13
            // 
            this.textBox13.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox13.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox13.Location = new System.Drawing.Point(528, 47);
            this.textBox13.Margin = new System.Windows.Forms.Padding(4);
            this.textBox13.Name = "textBox13";
            this.textBox13.ReadOnly = true;
            this.textBox13.Size = new System.Drawing.Size(123, 28);
            this.textBox13.TabIndex = 34;
            // 
            // buy_condition
            // 
            this.buy_condition.Enabled = false;
            this.buy_condition.FormattingEnabled = true;
            this.buy_condition.Location = new System.Drawing.Point(166, 3);
            this.buy_condition.Name = "buy_condition";
            this.buy_condition.Size = new System.Drawing.Size(157, 28);
            this.buy_condition.TabIndex = 18;
            // 
            // sell_condtion
            // 
            this.sell_condtion.Enabled = false;
            this.sell_condtion.FormattingEnabled = true;
            this.sell_condtion.Location = new System.Drawing.Point(818, 3);
            this.sell_condtion.Name = "sell_condtion";
            this.sell_condtion.Size = new System.Drawing.Size(157, 28);
            this.sell_condtion.TabIndex = 19;
            // 
            // Trade_Auto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1830, 1180);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.timetimer);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.User_connection);
            this.Controls.Add(this.axKHOpenAPI1);
            this.Controls.Add(this.time);
            this.MainMenuStrip = this.menu;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Trade_Auto";
            this.Text = "Trade_Auto";
            this.Load += new System.EventHandler(this.Trade_Auto_Load);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.groupBox4.ResumeLayout(false);
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
        private System.Windows.Forms.TextBox extra;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem Main_menu;
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
        private System.Windows.Forms.Button Trade_setting;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TextBox sell_condtion_method;
        private System.Windows.Forms.TextBox buy_condtion_method;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.TextBox User_money;
        private System.Windows.Forms.TextBox total_money;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox sell_condtion;
        private System.Windows.Forms.ComboBox buy_condition;
    }
}

