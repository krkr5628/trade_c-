
namespace WindowsFormsApp1
{
    partial class Auto_Run_Update
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Time_label = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.end_time_text = new System.Windows.Forms.TextBox();
            this.end_time = new System.Windows.Forms.Label();
            this.start_time_text = new System.Windows.Forms.TextBox();
            this.run_status = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.start_time = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Time_label
            // 
            this.Time_label.BackColor = System.Drawing.Color.LimeGreen;
            this.Time_label.Font = new System.Drawing.Font("굴림", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Time_label.ForeColor = System.Drawing.Color.Yellow;
            this.Time_label.Location = new System.Drawing.Point(168, 34);
            this.Time_label.Name = "Time_label";
            this.Time_label.Size = new System.Drawing.Size(269, 54);
            this.Time_label.TabIndex = 0;
            this.Time_label.Text = "24 02-21 (수) 14:50:30";
            this.Time_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timetimer);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.86232F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.9604F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.70297F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.9604F));
            this.tableLayoutPanel1.Controls.Add(this.end_time_text, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.end_time, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.start_time_text, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.run_status, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.start_time, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 102);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(425, 100);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // end_time_text
            // 
            this.end_time_text.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.end_time_text.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.end_time_text.Location = new System.Drawing.Point(179, 53);
            this.end_time_text.Name = "end_time_text";
            this.end_time_text.Size = new System.Drawing.Size(119, 30);
            this.end_time_text.TabIndex = 6;
            this.end_time_text.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // end_time
            // 
            this.end_time.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.end_time.AutoSize = true;
            this.end_time.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.end_time.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.end_time.Location = new System.Drawing.Point(179, 0);
            this.end_time.Name = "end_time";
            this.end_time.Size = new System.Drawing.Size(119, 50);
            this.end_time.TabIndex = 1;
            this.end_time.Text = "종료시각";
            this.end_time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // start_time_text
            // 
            this.start_time_text.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.start_time_text.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.start_time_text.Location = new System.Drawing.Point(57, 53);
            this.start_time_text.Name = "start_time_text";
            this.start_time_text.Size = new System.Drawing.Size(116, 30);
            this.start_time_text.TabIndex = 5;
            this.start_time_text.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // run_status
            // 
            this.run_status.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.run_status.AutoSize = true;
            this.run_status.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.run_status.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.run_status.Location = new System.Drawing.Point(304, 0);
            this.run_status.Name = "run_status";
            this.run_status.Size = new System.Drawing.Size(118, 50);
            this.run_status.TabIndex = 2;
            this.run_status.Text = "실행상태";
            this.run_status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(3, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 50);
            this.label4.TabIndex = 3;
            this.label4.Text = "값";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.label7.Location = new System.Drawing.Point(304, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 50);
            this.label7.TabIndex = 6;
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // start_time
            // 
            this.start_time.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.start_time.AutoSize = true;
            this.start_time.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.start_time.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.start_time.Location = new System.Drawing.Point(57, 0);
            this.start_time.Name = "start_time";
            this.start_time.Size = new System.Drawing.Size(116, 50);
            this.start_time.TabIndex = 0;
            this.start_time.Text = "시작시각";
            this.start_time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.checkBox1.Location = new System.Drawing.Point(12, 66);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(148, 22);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "자동실행여부";
            this.checkBox1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Info;
            this.button1.Location = new System.Drawing.Point(57, 241);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(151, 55);
            this.button1.TabIndex = 3;
            this.button1.Text = "실행";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Info;
            this.button2.Location = new System.Drawing.Point(246, 241);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(148, 55);
            this.button2.TabIndex = 4;
            this.button2.Text = "설정저장";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // Auto_Run_Update
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 331);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Time_label);
            this.Name = "Auto_Run_Update";
            this.Text = "Form3";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Time_label;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label end_time;
        private System.Windows.Forms.Label run_status;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label start_time;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox start_time_text;
        private System.Windows.Forms.TextBox end_time_text;
    }
}