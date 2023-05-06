
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
            this.Fire_wall = new System.Windows.Forms.Label();
            this.User_name = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.User_account_list = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.User_id = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Real_time_search_btn = new System.Windows.Forms.Button();
            this.Stock_search_btn = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.Fomula_search_btn = new System.Windows.Forms.Button();
            this.Normal_search_btn = new System.Windows.Forms.Button();
            this.Real_time_stop_btn = new System.Windows.Forms.Button();
            this.Fomula_list = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.User_connection = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.Keyboard_wall = new System.Windows.Forms.Label();
            this.Stock_code = new System.Windows.Forms.TextBox();
            this.User_money = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.axKHOpenAPI1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
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
            this.Login_btn.Location = new System.Drawing.Point(12, 12);
            this.Login_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Login_btn.Name = "Login_btn";
            this.Login_btn.Size = new System.Drawing.Size(82, 50);
            this.Login_btn.TabIndex = 1;
            this.Login_btn.Text = "로그인";
            this.Login_btn.UseVisualStyleBackColor = false;
            // 
            // log_window
            // 
            this.log_window.Location = new System.Drawing.Point(12, 177);
            this.log_window.Name = "log_window";
            this.log_window.Size = new System.Drawing.Size(386, 341);
            this.log_window.TabIndex = 2;
            this.log_window.Text = "";
            // 
            // axKHOpenAPI1
            // 
            this.axKHOpenAPI1.Enabled = true;
            this.axKHOpenAPI1.Location = new System.Drawing.Point(1020, 12);
            this.axKHOpenAPI1.Name = "axKHOpenAPI1";
            this.axKHOpenAPI1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axKHOpenAPI1.OcxState")));
            this.axKHOpenAPI1.Size = new System.Drawing.Size(100, 50);
            this.axKHOpenAPI1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(404, 177);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(716, 341);
            this.dataGridView1.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Controls.Add(this.User_name, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label5, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.User_id, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.User_account_list, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label10, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.Fire_wall, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.Keyboard_wall, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.User_money, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.User_connection, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 65);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(514, 106);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // Fire_wall
            // 
            this.Fire_wall.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fire_wall.AutoSize = true;
            this.Fire_wall.BackColor = System.Drawing.Color.White;
            this.Fire_wall.Location = new System.Drawing.Point(428, 70);
            this.Fire_wall.Name = "Fire_wall";
            this.Fire_wall.Size = new System.Drawing.Size(83, 36);
            this.Fire_wall.TabIndex = 9;
            this.Fire_wall.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // User_name
            // 
            this.User_name.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.User_name.AutoSize = true;
            this.User_name.BackColor = System.Drawing.Color.White;
            this.User_name.Location = new System.Drawing.Point(258, 0);
            this.User_name.Name = "User_name";
            this.User_name.Size = new System.Drawing.Size(79, 35);
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
            this.label2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 35);
            this.label2.TabIndex = 0;
            this.label2.Text = "아이디";
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
            this.label3.Location = new System.Drawing.Point(3, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 35);
            this.label3.TabIndex = 1;
            this.label3.Text = "계좌번호";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.LightSlateGray;
            this.label4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(173, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 36);
            this.label4.TabIndex = 2;
            this.label4.Text = "키보드보안";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.LightSlateGray;
            this.label5.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(173, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 35);
            this.label5.TabIndex = 3;
            this.label5.Text = "사용자 이름";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // User_account_list
            // 
            this.User_account_list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.User_account_list.FormattingEnabled = true;
            this.User_account_list.ItemHeight = 12;
            this.User_account_list.Location = new System.Drawing.Point(88, 38);
            this.User_account_list.Name = "User_account_list";
            this.User_account_list.Size = new System.Drawing.Size(79, 20);
            this.User_account_list.TabIndex = 11;
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
            this.label6.Location = new System.Drawing.Point(173, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 35);
            this.label6.TabIndex = 4;
            this.label6.Text = "예수금";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // User_id
            // 
            this.User_id.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.User_id.AutoSize = true;
            this.User_id.BackColor = System.Drawing.Color.White;
            this.User_id.Location = new System.Drawing.Point(88, 0);
            this.User_id.Name = "User_id";
            this.User_id.Size = new System.Drawing.Size(79, 35);
            this.User_id.TabIndex = 5;
            this.User_id.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(796, 65);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(324, 106);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // Real_time_search_btn
            // 
            this.Real_time_search_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Real_time_search_btn.BackColor = System.Drawing.Color.SteelBlue;
            this.Real_time_search_btn.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Real_time_search_btn.ForeColor = System.Drawing.Color.Transparent;
            this.Real_time_search_btn.Location = new System.Drawing.Point(107, 70);
            this.Real_time_search_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Real_time_search_btn.Name = "Real_time_search_btn";
            this.Real_time_search_btn.Size = new System.Drawing.Size(108, 36);
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
            this.Stock_search_btn.Location = new System.Drawing.Point(215, 0);
            this.Stock_search_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Stock_search_btn.Name = "Stock_search_btn";
            this.Stock_search_btn.Size = new System.Drawing.Size(109, 35);
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
            this.label9.Location = new System.Drawing.Point(3, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 35);
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
            this.label12.Location = new System.Drawing.Point(3, 35);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(101, 35);
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
            this.Fomula_search_btn.Location = new System.Drawing.Point(215, 35);
            this.Fomula_search_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Fomula_search_btn.Name = "Fomula_search_btn";
            this.Fomula_search_btn.Size = new System.Drawing.Size(109, 35);
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
            this.Normal_search_btn.Location = new System.Drawing.Point(0, 70);
            this.Normal_search_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Normal_search_btn.Name = "Normal_search_btn";
            this.Normal_search_btn.Size = new System.Drawing.Size(107, 36);
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
            this.Real_time_stop_btn.Location = new System.Drawing.Point(215, 70);
            this.Real_time_stop_btn.Margin = new System.Windows.Forms.Padding(0);
            this.Real_time_stop_btn.Name = "Real_time_stop_btn";
            this.Real_time_stop_btn.Size = new System.Drawing.Size(109, 36);
            this.Real_time_stop_btn.TabIndex = 10;
            this.Real_time_stop_btn.Text = "실시간 중단";
            this.Real_time_stop_btn.UseVisualStyleBackColor = false;
            // 
            // Fomula_list
            // 
            this.Fomula_list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fomula_list.FormattingEnabled = true;
            this.Fomula_list.Location = new System.Drawing.Point(110, 38);
            this.Fomula_list.Name = "Fomula_list";
            this.Fomula_list.Size = new System.Drawing.Size(102, 20);
            this.Fomula_list.TabIndex = 12;
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
            this.label7.Location = new System.Drawing.Point(3, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 36);
            this.label7.TabIndex = 12;
            this.label7.Text = "접속구분";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // User_connection
            // 
            this.User_connection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.User_connection.AutoSize = true;
            this.User_connection.BackColor = System.Drawing.Color.White;
            this.User_connection.Location = new System.Drawing.Point(88, 70);
            this.User_connection.Name = "User_connection";
            this.User_connection.Size = new System.Drawing.Size(79, 36);
            this.User_connection.TabIndex = 13;
            this.User_connection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.LightSlateGray;
            this.label10.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(343, 70);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 36);
            this.label10.TabIndex = 14;
            this.label10.Text = "방화벽";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Keyboard_wall
            // 
            this.Keyboard_wall.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Keyboard_wall.AutoSize = true;
            this.Keyboard_wall.BackColor = System.Drawing.Color.White;
            this.Keyboard_wall.Location = new System.Drawing.Point(258, 70);
            this.Keyboard_wall.Name = "Keyboard_wall";
            this.Keyboard_wall.Size = new System.Drawing.Size(79, 36);
            this.Keyboard_wall.TabIndex = 15;
            this.Keyboard_wall.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Stock_code
            // 
            this.Stock_code.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Stock_code.Location = new System.Drawing.Point(110, 3);
            this.Stock_code.Name = "Stock_code";
            this.Stock_code.Size = new System.Drawing.Size(102, 21);
            this.Stock_code.TabIndex = 13;
            // 
            // User_money
            // 
            this.User_money.Location = new System.Drawing.Point(258, 38);
            this.User_money.Name = "User_money";
            this.User_money.ReadOnly = true;
            this.User_money.Size = new System.Drawing.Size(79, 21);
            this.User_money.TabIndex = 16;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // Trade_Auto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 530);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.log_window);
            this.Controls.Add(this.Login_btn);
            this.Controls.Add(this.axKHOpenAPI1);
            this.Name = "Trade_Auto";
            this.Text = "Trade_Auto";
            ((System.ComponentModel.ISupportInitialize)(this.axKHOpenAPI1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AxKHOpenAPILib.AxKHOpenAPI axKHOpenAPI1;
        private System.Windows.Forms.Button Login_btn;
        private System.Windows.Forms.RichTextBox log_window;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label Fire_wall;
        private System.Windows.Forms.Label User_name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
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
        private System.Windows.Forms.Label User_connection;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label Keyboard_wall;
        private System.Windows.Forms.TextBox Stock_code;
        private System.Windows.Forms.TextBox User_money;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}

