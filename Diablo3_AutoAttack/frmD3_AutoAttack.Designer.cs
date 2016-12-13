namespace Diablo3_AutoAttack
{
    partial class frmD3_AutoAttacker
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmD3_AutoAttacker));
            this.txtHotkeyAbort = new System.Windows.Forms.TextBox();
            this.txtHotkeyStart = new System.Windows.Forms.TextBox();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.chkLeft = new System.Windows.Forms.CheckBox();
            this.chkRight = new System.Windows.Forms.CheckBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.btnMiuns = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtDelay = new System.Windows.Forms.TextBox();
            this.chk4 = new System.Windows.Forms.CheckBox();
            this.chk3 = new System.Windows.Forms.CheckBox();
            this.chk2 = new System.Windows.Forms.CheckBox();
            this.chk1 = new System.Windows.Forms.CheckBox();
            this.chkHotkeyFun = new System.Windows.Forms.CheckBox();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtHotkeyAbort
            // 
            this.txtHotkeyAbort.BackColor = System.Drawing.Color.White;
            this.txtHotkeyAbort.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtHotkeyAbort.Location = new System.Drawing.Point(165, 25);
            this.txtHotkeyAbort.Name = "txtHotkeyAbort";
            this.txtHotkeyAbort.ReadOnly = true;
            this.txtHotkeyAbort.Size = new System.Drawing.Size(73, 25);
            this.txtHotkeyAbort.TabIndex = 39;
            this.txtHotkeyAbort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtHotkeyAbort.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtHotkeyAbort_KeyDown);
            // 
            // txtHotkeyStart
            // 
            this.txtHotkeyStart.BackColor = System.Drawing.Color.White;
            this.txtHotkeyStart.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtHotkeyStart.Location = new System.Drawing.Point(42, 25);
            this.txtHotkeyStart.Name = "txtHotkeyStart";
            this.txtHotkeyStart.ReadOnly = true;
            this.txtHotkeyStart.Size = new System.Drawing.Size(73, 25);
            this.txtHotkeyStart.TabIndex = 38;
            this.txtHotkeyStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtHotkeyStart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtHotkeyStart_KeyDown);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.txtHotkeyAbort);
            this.GroupBox1.Controls.Add(this.txtHotkeyStart);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Controls.Add(this.Label4);
            this.GroupBox1.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.GroupBox1.Location = new System.Drawing.Point(57, 10);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(244, 63);
            this.GroupBox1.TabIndex = 52;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Hotkey";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Label1.Location = new System.Drawing.Point(6, 28);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(37, 17);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Start";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Label4.Location = new System.Drawing.Point(121, 28);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(43, 17);
            this.Label4.TabIndex = 36;
            this.Label4.Text = "Abort";
            // 
            // chkLeft
            // 
            this.chkLeft.AutoSize = true;
            this.chkLeft.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chkLeft.Location = new System.Drawing.Point(216, 82);
            this.chkLeft.Name = "chkLeft";
            this.chkLeft.Size = new System.Drawing.Size(50, 21);
            this.chkLeft.TabIndex = 51;
            this.chkLeft.Text = "Left";
            this.chkLeft.UseVisualStyleBackColor = true;
            // 
            // chkRight
            // 
            this.chkRight.AutoSize = true;
            this.chkRight.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chkRight.Location = new System.Drawing.Point(278, 82);
            this.chkRight.Name = "chkRight";
            this.chkRight.Size = new System.Drawing.Size(59, 21);
            this.chkRight.TabIndex = 50;
            this.chkRight.Text = "Rgiht";
            this.chkRight.UseVisualStyleBackColor = true;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Label3.Location = new System.Drawing.Point(248, 118);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(26, 17);
            this.Label3.TabIndex = 49;
            this.Label3.Text = "ms";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Label2.Location = new System.Drawing.Point(84, 118);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(76, 17);
            this.Label2.TabIndex = 48;
            this.Label2.Text = "Delay base:";
            // 
            // btnMiuns
            // 
            this.btnMiuns.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnMiuns.ForeColor = System.Drawing.Color.Black;
            this.btnMiuns.Location = new System.Drawing.Point(208, 145);
            this.btnMiuns.Name = "btnMiuns";
            this.btnMiuns.Size = new System.Drawing.Size(36, 23);
            this.btnMiuns.TabIndex = 47;
            this.btnMiuns.Text = "-";
            this.btnMiuns.UseVisualStyleBackColor = true;
            this.btnMiuns.Click += new System.EventHandler(this.btnMiuns_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnAdd.ForeColor = System.Drawing.Color.Black;
            this.btnAdd.Location = new System.Drawing.Point(170, 145);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(36, 23);
            this.btnAdd.TabIndex = 46;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtDelay
            // 
            this.txtDelay.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtDelay.Location = new System.Drawing.Point(170, 112);
            this.txtDelay.MaxLength = 3;
            this.txtDelay.Name = "txtDelay";
            this.txtDelay.ReadOnly = true;
            this.txtDelay.Size = new System.Drawing.Size(72, 25);
            this.txtDelay.TabIndex = 45;
            this.txtDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chk4
            // 
            this.chk4.AutoSize = true;
            this.chk4.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chk4.Location = new System.Drawing.Point(173, 82);
            this.chk4.Name = "chk4";
            this.chk4.Size = new System.Drawing.Size(35, 21);
            this.chk4.TabIndex = 44;
            this.chk4.Text = "4";
            this.chk4.UseVisualStyleBackColor = true;
            // 
            // chk3
            // 
            this.chk3.AutoSize = true;
            this.chk3.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chk3.Location = new System.Drawing.Point(123, 82);
            this.chk3.Name = "chk3";
            this.chk3.Size = new System.Drawing.Size(35, 21);
            this.chk3.TabIndex = 43;
            this.chk3.Text = "3";
            this.chk3.UseVisualStyleBackColor = true;
            // 
            // chk2
            // 
            this.chk2.AutoSize = true;
            this.chk2.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chk2.Location = new System.Drawing.Point(73, 82);
            this.chk2.Name = "chk2";
            this.chk2.Size = new System.Drawing.Size(35, 21);
            this.chk2.TabIndex = 42;
            this.chk2.Text = "2";
            this.chk2.UseVisualStyleBackColor = true;
            // 
            // chk1
            // 
            this.chk1.AutoSize = true;
            this.chk1.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chk1.Location = new System.Drawing.Point(23, 82);
            this.chk1.Name = "chk1";
            this.chk1.Size = new System.Drawing.Size(35, 21);
            this.chk1.TabIndex = 41;
            this.chk1.Text = "1";
            this.chk1.UseVisualStyleBackColor = true;
            // 
            // chkHotkeyFun
            // 
            this.chkHotkeyFun.AutoSize = true;
            this.chkHotkeyFun.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.chkHotkeyFun.Location = new System.Drawing.Point(23, 174);
            this.chkHotkeyFun.Name = "chkHotkeyFun";
            this.chkHotkeyFun.Size = new System.Drawing.Size(170, 21);
            this.chkHotkeyFun.TabIndex = 53;
            this.chkHotkeyFun.Text = "Remain hotkey function";
            this.chkHotkeyFun.UseVisualStyleBackColor = true;
            // 
            // frmD3_AutoAttacker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 199);
            this.Controls.Add(this.chkHotkeyFun);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.chkLeft);
            this.Controls.Add(this.chkRight);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.btnMiuns);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtDelay);
            this.Controls.Add(this.chk4);
            this.Controls.Add(this.chk3);
            this.Controls.Add(this.chk2);
            this.Controls.Add(this.chk1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "frmD3_AutoAttacker";
            this.Text = "Diablo3 AutoAttacker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmD3_AutoAttacker_Closing);
            this.Load += new System.EventHandler(this.frmD3_AutoAttacker_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmD3_AutoAttacker_KeyDown);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtHotkeyAbort;
        internal System.Windows.Forms.TextBox txtHotkeyStart;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.CheckBox chkLeft;
        internal System.Windows.Forms.CheckBox chkRight;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button btnMiuns;
        internal System.Windows.Forms.Button btnAdd;
        internal System.Windows.Forms.TextBox txtDelay;
        internal System.Windows.Forms.CheckBox chk4;
        internal System.Windows.Forms.CheckBox chk3;
        internal System.Windows.Forms.CheckBox chk2;
        internal System.Windows.Forms.CheckBox chk1;
        internal System.Windows.Forms.CheckBox chkHotkeyFun;
    }
}

