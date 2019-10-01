namespace AirTicketOffice
{
    partial class Authorization
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.picCaptcha = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.labellog = new System.Windows.Forms.Label();
            this.labelcap = new System.Windows.Forms.Label();
            this.textBoxCap = new System.Windows.Forms.TextBox();
            this.labelpass = new System.Windows.Forms.Label();
            this.buttoncapt = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.buttonToComeIn = new System.Windows.Forms.Button();
            this.buttonRegistration = new System.Windows.Forms.Button();
            this.radioButtonUser = new System.Windows.Forms.RadioButton();
            this.radioButtonAdmin = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(48)))));
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(498, 30);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseMove);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(48)))));
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(46, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "Авторизация";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox1_MouseMove);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(38)))), ((int)(((byte)(48)))));
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(468, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 30);
            this.label2.TabIndex = 3;
            this.label2.Text = "X";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Click += new System.EventHandler(this.Label2_Click);
            this.label2.MouseEnter += new System.EventHandler(this.Label2_MouseEnter);
            this.label2.MouseLeave += new System.EventHandler(this.Label2_MouseLeave);
            // 
            // picCaptcha
            // 
            this.picCaptcha.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picCaptcha.BackColor = System.Drawing.Color.Transparent;
            this.picCaptcha.Location = new System.Drawing.Point(276, 46);
            this.picCaptcha.Name = "picCaptcha";
            this.picCaptcha.Size = new System.Drawing.Size(210, 102);
            this.picCaptcha.TabIndex = 4;
            this.picCaptcha.TabStop = false;
            this.picCaptcha.Click += new System.EventHandler(this.PicCaptcha_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(20, 71);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(177, 20);
            this.textBox1.TabIndex = 5;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(20, 113);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '•';
            this.textBox2.Size = new System.Drawing.Size(177, 20);
            this.textBox2.TabIndex = 5;
            // 
            // labellog
            // 
            this.labellog.AutoSize = true;
            this.labellog.BackColor = System.Drawing.Color.White;
            this.labellog.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labellog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(145)))), ((int)(((byte)(157)))));
            this.labellog.Location = new System.Drawing.Point(16, 48);
            this.labellog.Name = "labellog";
            this.labellog.Size = new System.Drawing.Size(51, 20);
            this.labellog.TabIndex = 6;
            this.labellog.Text = "Логин";
            // 
            // labelcap
            // 
            this.labelcap.AutoSize = true;
            this.labelcap.BackColor = System.Drawing.Color.Transparent;
            this.labelcap.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelcap.ForeColor = System.Drawing.Color.Black;
            this.labelcap.Location = new System.Drawing.Point(273, 149);
            this.labelcap.Name = "labelcap";
            this.labelcap.Size = new System.Drawing.Size(116, 22);
            this.labelcap.TabIndex = 7;
            this.labelcap.Text = "Введите капчу";
            // 
            // textBoxCap
            // 
            this.textBoxCap.Location = new System.Drawing.Point(276, 171);
            this.textBoxCap.Name = "textBoxCap";
            this.textBoxCap.Size = new System.Drawing.Size(210, 20);
            this.textBoxCap.TabIndex = 5;
            // 
            // labelpass
            // 
            this.labelpass.AutoSize = true;
            this.labelpass.BackColor = System.Drawing.Color.White;
            this.labelpass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelpass.Font = new System.Drawing.Font("Trebuchet MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelpass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(145)))), ((int)(((byte)(157)))));
            this.labelpass.Location = new System.Drawing.Point(16, 92);
            this.labelpass.Name = "labelpass";
            this.labelpass.Size = new System.Drawing.Size(59, 20);
            this.labelpass.TabIndex = 7;
            this.labelpass.Text = "Пароль";
            // 
            // buttoncapt
            // 
            this.buttoncapt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttoncapt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(57)))), ((int)(((byte)(53)))));
            this.buttoncapt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttoncapt.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttoncapt.ForeColor = System.Drawing.Color.White;
            this.buttoncapt.Location = new System.Drawing.Point(385, 200);
            this.buttoncapt.Name = "buttoncapt";
            this.buttoncapt.Size = new System.Drawing.Size(101, 35);
            this.buttoncapt.TabIndex = 8;
            this.buttoncapt.Text = "Проверить";
            this.buttoncapt.UseVisualStyleBackColor = false;
            this.buttoncapt.Click += new System.EventHandler(this.Buttoncapt_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.Location = new System.Drawing.Point(8, 46);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(259, 189);
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // buttonToComeIn
            // 
            this.buttonToComeIn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(229)))));
            this.buttonToComeIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonToComeIn.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonToComeIn.ForeColor = System.Drawing.Color.White;
            this.buttonToComeIn.Location = new System.Drawing.Point(20, 185);
            this.buttonToComeIn.Name = "buttonToComeIn";
            this.buttonToComeIn.Size = new System.Drawing.Size(87, 40);
            this.buttonToComeIn.TabIndex = 10;
            this.buttonToComeIn.Text = "Войти";
            this.buttonToComeIn.UseVisualStyleBackColor = false;
            this.buttonToComeIn.Click += new System.EventHandler(this.ButtonToComeIn_Click);
            // 
            // buttonRegistration
            // 
            this.buttonRegistration.BackColor = System.Drawing.Color.White;
            this.buttonRegistration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRegistration.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRegistration.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(179)))), ((int)(((byte)(66)))));
            this.buttonRegistration.Location = new System.Drawing.Point(149, 191);
            this.buttonRegistration.Name = "buttonRegistration";
            this.buttonRegistration.Size = new System.Drawing.Size(106, 28);
            this.buttonRegistration.TabIndex = 11;
            this.buttonRegistration.Text = "Регистрация";
            this.buttonRegistration.UseVisualStyleBackColor = false;
            this.buttonRegistration.Click += new System.EventHandler(this.ButtonReg_Click);
            // 
            // radioButtonUser
            // 
            this.radioButtonUser.AutoSize = true;
            this.radioButtonUser.BackColor = System.Drawing.Color.White;
            this.radioButtonUser.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonUser.Location = new System.Drawing.Point(20, 149);
            this.radioButtonUser.Name = "radioButtonUser";
            this.radioButtonUser.Size = new System.Drawing.Size(103, 22);
            this.radioButtonUser.TabIndex = 12;
            this.radioButtonUser.TabStop = true;
            this.radioButtonUser.Text = "Пользователь";
            this.radioButtonUser.UseVisualStyleBackColor = false;
            // 
            // radioButtonAdmin
            // 
            this.radioButtonAdmin.AutoSize = true;
            this.radioButtonAdmin.BackColor = System.Drawing.Color.White;
            this.radioButtonAdmin.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonAdmin.Location = new System.Drawing.Point(130, 149);
            this.radioButtonAdmin.Name = "radioButtonAdmin";
            this.radioButtonAdmin.Size = new System.Drawing.Size(112, 22);
            this.radioButtonAdmin.TabIndex = 12;
            this.radioButtonAdmin.TabStop = true;
            this.radioButtonAdmin.Text = "Администратор";
            this.radioButtonAdmin.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(497, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1, 217);
            this.panel1.TabIndex = 13;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 30);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1, 217);
            this.panel2.TabIndex = 13;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(1, 246);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(496, 1);
            this.panel3.TabIndex = 13;
            // 
            // Authorization
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(193)))), ((int)(((byte)(239)))));
            this.ClientSize = new System.Drawing.Size(498, 247);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.radioButtonAdmin);
            this.Controls.Add(this.radioButtonUser);
            this.Controls.Add(this.buttonRegistration);
            this.Controls.Add(this.buttonToComeIn);
            this.Controls.Add(this.buttoncapt);
            this.Controls.Add(this.labelpass);
            this.Controls.Add(this.labelcap);
            this.Controls.Add(this.labellog);
            this.Controls.Add(this.textBoxCap);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.picCaptcha);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Authorization";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "authorization";
            this.Load += new System.EventHandler(this.Authorization_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCaptcha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox picCaptcha;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label labellog;
        private System.Windows.Forms.Label labelcap;
        private System.Windows.Forms.TextBox textBoxCap;
        private System.Windows.Forms.Label labelpass;
        private System.Windows.Forms.Button buttoncapt;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button buttonToComeIn;
        private System.Windows.Forms.Button buttonRegistration;
        private System.Windows.Forms.RadioButton radioButtonUser;
        private System.Windows.Forms.RadioButton radioButtonAdmin;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
    }
}