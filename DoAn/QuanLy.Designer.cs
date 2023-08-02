namespace DoAn
{
    partial class QuanLy
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
            this.fLP_banan = new System.Windows.Forms.FlowLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_monan = new System.Windows.Forms.ComboBox();
            this.numUD_slmon = new System.Windows.Forms.NumericUpDown();
            this.button_themmon = new System.Windows.Forms.Button();
            this.comboBox_loaidoan = new System.Windows.Forms.ComboBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_giatien = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numUD_giamgia = new System.Windows.Forms.NumericUpDown();
            this.button_thanhtoan = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lv_showbill = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.adminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label3 = new System.Windows.Forms.Label();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUD_slmon)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUD_giamgia)).BeginInit();
            this.panel3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fLP_banan
            // 
            this.fLP_banan.AutoScroll = true;
            this.fLP_banan.Location = new System.Drawing.Point(10, 73);
            this.fLP_banan.Name = "fLP_banan";
            this.fLP_banan.Size = new System.Drawing.Size(876, 195);
            this.fLP_banan.TabIndex = 10;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.comboBox_monan);
            this.panel5.Controls.Add(this.numUD_slmon);
            this.panel5.Controls.Add(this.button_themmon);
            this.panel5.Controls.Add(this.comboBox_loaidoan);
            this.panel5.Location = new System.Drawing.Point(532, 268);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(374, 118);
            this.panel5.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(192, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Số lượng";
            // 
            // comboBox_monan
            // 
            this.comboBox_monan.FormattingEnabled = true;
            this.comboBox_monan.Location = new System.Drawing.Point(3, 61);
            this.comboBox_monan.Name = "comboBox_monan";
            this.comboBox_monan.Size = new System.Drawing.Size(176, 24);
            this.comboBox_monan.TabIndex = 4;
            // 
            // numUD_slmon
            // 
            this.numUD_slmon.Location = new System.Drawing.Point(195, 63);
            this.numUD_slmon.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numUD_slmon.Name = "numUD_slmon";
            this.numUD_slmon.Size = new System.Drawing.Size(57, 22);
            this.numUD_slmon.TabIndex = 3;
            this.numUD_slmon.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // button_themmon
            // 
            this.button_themmon.Location = new System.Drawing.Point(268, 32);
            this.button_themmon.Name = "button_themmon";
            this.button_themmon.Size = new System.Drawing.Size(87, 53);
            this.button_themmon.TabIndex = 2;
            this.button_themmon.Text = "Thêm món";
            this.button_themmon.UseVisualStyleBackColor = true;
            this.button_themmon.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox_loaidoan
            // 
            this.comboBox_loaidoan.FormattingEnabled = true;
            this.comboBox_loaidoan.Location = new System.Drawing.Point(3, 25);
            this.comboBox_loaidoan.Name = "comboBox_loaidoan";
            this.comboBox_loaidoan.Size = new System.Drawing.Size(176, 24);
            this.comboBox_loaidoan.TabIndex = 0;
            this.comboBox_loaidoan.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged_1);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.textBox_giatien);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.numUD_giamgia);
            this.panel4.Controls.Add(this.button_thanhtoan);
            this.panel4.Location = new System.Drawing.Point(532, 395);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(375, 163);
            this.panel4.TabIndex = 8;
            this.panel4.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4_Paint);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(105, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 18);
            this.label5.TabIndex = 12;
            this.label5.Text = "Giảm giá";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(178, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "%";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // textBox_giatien
            // 
            this.textBox_giatien.Location = new System.Drawing.Point(212, 42);
            this.textBox_giatien.Name = "textBox_giatien";
            this.textBox_giatien.ReadOnly = true;
            this.textBox_giatien.Size = new System.Drawing.Size(125, 22);
            this.textBox_giatien.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(228, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 29);
            this.label1.TabIndex = 9;
            this.label1.Text = "Giá tiền";
            // 
            // numUD_giamgia
            // 
            this.numUD_giamgia.Location = new System.Drawing.Point(99, 84);
            this.numUD_giamgia.Name = "numUD_giamgia";
            this.numUD_giamgia.Size = new System.Drawing.Size(73, 22);
            this.numUD_giamgia.TabIndex = 7;
            // 
            // button_thanhtoan
            // 
            this.button_thanhtoan.Location = new System.Drawing.Point(233, 83);
            this.button_thanhtoan.Name = "button_thanhtoan";
            this.button_thanhtoan.Size = new System.Drawing.Size(104, 57);
            this.button_thanhtoan.TabIndex = 3;
            this.button_thanhtoan.Text = "Thanh toán";
            this.button_thanhtoan.UseVisualStyleBackColor = true;
            this.button_thanhtoan.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lv_showbill);
            this.panel3.Location = new System.Drawing.Point(10, 268);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(516, 290);
            this.panel3.TabIndex = 7;
            // 
            // lv_showbill
            // 
            this.lv_showbill.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lv_showbill.GridLines = true;
            this.lv_showbill.HideSelection = false;
            this.lv_showbill.Location = new System.Drawing.Point(0, 6);
            this.lv_showbill.Name = "lv_showbill";
            this.lv_showbill.Size = new System.Drawing.Size(509, 281);
            this.lv_showbill.TabIndex = 0;
            this.lv_showbill.UseCompatibleStateImageBehavior = false;
            this.lv_showbill.View = System.Windows.Forms.View.Details;
            this.lv_showbill.SelectedIndexChanged += new System.EventHandler(this.lv_showbill_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Tên món";
            this.columnHeader1.Width = 91;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Số lượng";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 106;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Đơn giá";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 91;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Thành tiền";
            this.columnHeader4.Width = 70;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adminToolStripMenuItem,
            this.xToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(912, 28);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // adminToolStripMenuItem
            // 
            this.adminToolStripMenuItem.Name = "adminToolStripMenuItem";
            this.adminToolStripMenuItem.Size = new System.Drawing.Size(65, 24);
            this.adminToolStripMenuItem.Text = "admin";
            this.adminToolStripMenuItem.Click += new System.EventHandler(this.adminToolStripMenuItem_Click);
            // 
            // xToolStripMenuItem
            // 
            this.xToolStripMenuItem.Name = "xToolStripMenuItem";
            this.xToolStripMenuItem.Size = new System.Drawing.Size(60, 24);
            this.xToolStripMenuItem.Text = "Menu";
            this.xToolStripMenuItem.Click += new System.EventHandler(this.xToolStripMenuItem_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(258, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(435, 36);
            this.label3.TabIndex = 11;
            this.label3.Text = "Phần mềm quản lý quán nước";
            // 
            // QuanLy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 570);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.fLP_banan);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.menuStrip1);
            this.Name = "QuanLy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QuanLy";
            this.Load += new System.EventHandler(this.QuanLy_Load_1);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUD_slmon)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUD_giamgia)).EndInit();
            this.panel3.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel fLP_banan;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.NumericUpDown numUD_slmon;
        private System.Windows.Forms.Button button_themmon;
        private System.Windows.Forms.ComboBox comboBox_loaidoan;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numUD_giamgia;
        private System.Windows.Forms.Button button_thanhtoan;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ListView lv_showbill;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem adminToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBox_monan;
        private System.Windows.Forms.TextBox textBox_giatien;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem xToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}