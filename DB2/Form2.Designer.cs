namespace DB2
{
    partial class Form2
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.serverName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dbName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.port = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.userName = new System.Windows.Forms.TextBox();
            this.lab4 = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.def = new System.Windows.Forms.Button();
            this.go = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // serverName
            // 
            this.serverName.Location = new System.Drawing.Point(99, 16);
            this.serverName.Name = "serverName";
            this.serverName.Size = new System.Drawing.Size(158, 20);
            this.serverName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "serverName";
            // 
            // dbName
            // 
            this.dbName.Location = new System.Drawing.Point(98, 49);
            this.dbName.Name = "dbName";
            this.dbName.Size = new System.Drawing.Size(159, 20);
            this.dbName.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "db name";
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(98, 85);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(159, 20);
            this.port.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "port";
            // 
            // userName
            // 
            this.userName.Location = new System.Drawing.Point(98, 117);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(159, 20);
            this.userName.TabIndex = 0;
            // 
            // lab4
            // 
            this.lab4.AutoSize = true;
            this.lab4.Location = new System.Drawing.Point(12, 120);
            this.lab4.Name = "lab4";
            this.lab4.Size = new System.Drawing.Size(55, 13);
            this.lab4.TabIndex = 1;
            this.lab4.Text = "userName";
            // 
            // password
            // 
            this.password.Location = new System.Drawing.Point(98, 151);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(159, 20);
            this.password.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 154);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "password";
            // 
            // def
            // 
            this.def.Location = new System.Drawing.Point(13, 185);
            this.def.Name = "def";
            this.def.Size = new System.Drawing.Size(113, 23);
            this.def.TabIndex = 2;
            this.def.Text = "Default connection";
            this.def.UseVisualStyleBackColor = true;
            this.def.Click += new System.EventHandler(this.def_Click);
            // 
            // go
            // 
            this.go.Location = new System.Drawing.Point(144, 185);
            this.go.Name = "go";
            this.go.Size = new System.Drawing.Size(113, 23);
            this.go.TabIndex = 2;
            this.go.Text = "Run";
            this.go.UseVisualStyleBackColor = true;
            this.go.Click += new System.EventHandler(this.go_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 233);
            this.Controls.Add(this.go);
            this.Controls.Add(this.def);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.password);
            this.Controls.Add(this.lab4);
            this.Controls.Add(this.userName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.port);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dbName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.serverName);
            this.Name = "Form2";
            this.Text = "Start new DB connection";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox serverName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox dbName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox userName;
        private System.Windows.Forms.Label lab4;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button def;
        private System.Windows.Forms.Button go;
    }
}