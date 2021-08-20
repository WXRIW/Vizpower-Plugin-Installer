namespace Vizpower_Plugin_Installer
{
    partial class Installer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Installer));
            this.ButtonInstall = new System.Windows.Forms.Button();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.ButtonUnistall = new System.Windows.Forms.Button();
            this.ButtonNavigate = new System.Windows.Forms.Button();
            this.Label5 = new System.Windows.Forms.Label();
            this.CheckBoxAgreement = new System.Windows.Forms.CheckBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.TextBoxLocation = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonInstall
            // 
            this.ButtonInstall.Location = new System.Drawing.Point(77, 208);
            this.ButtonInstall.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonInstall.Name = "ButtonInstall";
            this.ButtonInstall.Size = new System.Drawing.Size(150, 40);
            this.ButtonInstall.TabIndex = 19;
            this.ButtonInstall.Text = "安装(&I)";
            this.ButtonInstall.UseVisualStyleBackColor = true;
            this.ButtonInstall.Click += new System.EventHandler(this.ButtonInstall_Click);
            this.ButtonInstall.MouseEnter += new System.EventHandler(this.ButtonInstall_MouseEnter);
            this.ButtonInstall.MouseLeave += new System.EventHandler(this.ButtonInstall_MouseLeave);
            this.ButtonInstall.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ButtonInstall_MouseMove);
            // 
            // PictureBox1
            // 
            this.PictureBox1.Location = new System.Drawing.Point(-4, 224);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(41, 35);
            this.PictureBox1.TabIndex = 22;
            this.PictureBox1.TabStop = false;
            // 
            // ButtonUnistall
            // 
            this.ButtonUnistall.Location = new System.Drawing.Point(267, 208);
            this.ButtonUnistall.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonUnistall.Name = "ButtonUnistall";
            this.ButtonUnistall.Size = new System.Drawing.Size(150, 40);
            this.ButtonUnistall.TabIndex = 20;
            this.ButtonUnistall.Text = "卸载(&U)";
            this.ButtonUnistall.UseVisualStyleBackColor = true;
            this.ButtonUnistall.Click += new System.EventHandler(this.ButtonUnistall_Click);
            this.ButtonUnistall.MouseEnter += new System.EventHandler(this.ButtonUnistall_MouseEnter);
            this.ButtonUnistall.MouseLeave += new System.EventHandler(this.ButtonUnistall_MouseLeave);
            // 
            // ButtonNavigate
            // 
            this.ButtonNavigate.Location = new System.Drawing.Point(386, 91);
            this.ButtonNavigate.Margin = new System.Windows.Forms.Padding(2);
            this.ButtonNavigate.Name = "ButtonNavigate";
            this.ButtonNavigate.Size = new System.Drawing.Size(100, 37);
            this.ButtonNavigate.TabIndex = 21;
            this.ButtonNavigate.Text = "浏览(&N)";
            this.ButtonNavigate.UseVisualStyleBackColor = true;
            this.ButtonNavigate.Click += new System.EventHandler(this.ButtonNavigate_Click);
            this.ButtonNavigate.MouseEnter += new System.EventHandler(this.ButtonNavigate_MouseEnter);
            this.ButtonNavigate.MouseLeave += new System.EventHandler(this.ButtonNavigate_MouseLeave);
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Label5.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.Label5.ForeColor = System.Drawing.Color.Blue;
            this.Label5.Location = new System.Drawing.Point(341, 175);
            this.Label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(144, 20);
            this.Label5.TabIndex = 18;
            this.Label5.Text = "用户协议和免责声明";
            this.Label5.Click += new System.EventHandler(this.Label5_Click);
            // 
            // CheckBoxAgreement
            // 
            this.CheckBoxAgreement.AutoSize = true;
            this.CheckBoxAgreement.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.CheckBoxAgreement.Location = new System.Drawing.Point(229, 174);
            this.CheckBoxAgreement.Margin = new System.Windows.Forms.Padding(2);
            this.CheckBoxAgreement.Name = "CheckBoxAgreement";
            this.CheckBoxAgreement.Size = new System.Drawing.Size(121, 24);
            this.CheckBoxAgreement.TabIndex = 12;
            this.CheckBoxAgreement.Text = "已阅读并同意";
            this.CheckBoxAgreement.UseVisualStyleBackColor = true;
            this.CheckBoxAgreement.CheckedChanged += new System.EventHandler(this.CheckBoxAgreement_CheckedChanged);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Label4.Font = new System.Drawing.Font("Microsoft YaHei", 9F);
            this.Label4.ForeColor = System.Drawing.Color.Blue;
            this.Label4.Location = new System.Drawing.Point(11, 175);
            this.Label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(189, 20);
            this.Label4.TabIndex = 16;
            this.Label4.Text = "使用说明（点我！点我！）";
            this.Label4.Click += new System.EventHandler(this.Label4_Click);
            // 
            // TextBoxLocation
            // 
            this.TextBoxLocation.Location = new System.Drawing.Point(17, 132);
            this.TextBoxLocation.Margin = new System.Windows.Forms.Padding(2);
            this.TextBoxLocation.Name = "TextBoxLocation";
            this.TextBoxLocation.Size = new System.Drawing.Size(468, 29);
            this.TextBoxLocation.TabIndex = 14;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(11, 98);
            this.Label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(229, 23);
            this.Label3.TabIndex = 17;
            this.Label3.Text = "无限宝 iMeeting.exe 位置：";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.ForeColor = System.Drawing.Color.Red;
            this.Label2.Location = new System.Drawing.Point(11, 40);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(452, 46);
            this.Label2.TabIndex = 15;
            this.Label2.Text = "本程序释放的文件会引起部分杀毒软件的误报，如果出现，\r\n请更换杀毒软件（建议使用火绒）";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.ForeColor = System.Drawing.Color.Red;
            this.Label1.Location = new System.Drawing.Point(11, 9);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(316, 23);
            this.Label1.TabIndex = 13;
            this.Label1.Text = "为了阻挡无限宝的打击，本程序诞生了！";
            // 
            // Installer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(499, 258);
            this.Controls.Add(this.ButtonInstall);
            this.Controls.Add(this.PictureBox1);
            this.Controls.Add(this.ButtonUnistall);
            this.Controls.Add(this.ButtonNavigate);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.CheckBoxAgreement);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.TextBoxLocation);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 10F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Installer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "无限宝第三方插件安装程序";
            this.Load += new System.EventHandler(this.Installer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button ButtonInstall;
        internal System.Windows.Forms.PictureBox PictureBox1;
        internal System.Windows.Forms.Button ButtonUnistall;
        internal System.Windows.Forms.Button ButtonNavigate;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.CheckBox CheckBoxAgreement;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.TextBox TextBoxLocation;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
    }
}

