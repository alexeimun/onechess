namespace OneChess
{
    partial class Options
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
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.op5 = new System.Windows.Forms.CheckBox();
            this.op4 = new System.Windows.Forms.CheckBox();
            this.op3 = new System.Windows.Forms.CheckBox();
            this.op1 = new System.Windows.Forms.CheckBox();
            this.op2 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.ImageKey = "(none)";
            this.button1.Location = new System.Drawing.Point(104, 206);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 30);
            this.button1.TabIndex = 6;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.op5);
            this.groupBox1.Controls.Add(this.op4);
            this.groupBox1.Controls.Add(this.op3);
            this.groupBox1.Controls.Add(this.op1);
            this.groupBox1.Controls.Add(this.op2);
            this.groupBox1.Location = new System.Drawing.Point(12, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(305, 157);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // op5
            // 
            this.op5.AutoSize = true;
            this.op5.Checked = true;
            this.op5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.op5.Location = new System.Drawing.Point(22, 123);
            this.op5.Name = "op5";
            this.op5.Size = new System.Drawing.Size(256, 17);
            this.op5.TabIndex = 4;
            this.op5.Text = "Ask for Player Names before to start a new game";
            this.op5.UseVisualStyleBackColor = true;
            // 
            // op4
            // 
            this.op4.AutoSize = true;
            this.op4.Location = new System.Drawing.Point(22, 100);
            this.op4.Name = "op4";
            this.op4.Size = new System.Drawing.Size(85, 17);
            this.op4.TabIndex = 3;
            this.op4.Text = "Classic Style";
            this.op4.UseVisualStyleBackColor = true;
            // 
            // op3
            // 
            this.op3.AutoSize = true;
            this.op3.Checked = true;
            this.op3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.op3.Location = new System.Drawing.Point(22, 77);
            this.op3.Name = "op3";
            this.op3.Size = new System.Drawing.Size(155, 17);
            this.op3.TabIndex = 2;
            this.op3.Text = "Switch Frame Automatically";
            this.op3.UseVisualStyleBackColor = true;
            // 
            // op1
            // 
            this.op1.AutoSize = true;
            this.op1.Checked = true;
            this.op1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.op1.Location = new System.Drawing.Point(22, 31);
            this.op1.Name = "op1";
            this.op1.Size = new System.Drawing.Size(247, 17);
            this.op1.TabIndex = 0;
            this.op1.Text = "Open Automatically the Chess Manager at start";
            this.op1.UseVisualStyleBackColor = true;
            // 
            // op2
            // 
            this.op2.AutoSize = true;
            this.op2.Checked = true;
            this.op2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.op2.Location = new System.Drawing.Point(22, 54);
            this.op2.Name = "op2";
            this.op2.Size = new System.Drawing.Size(139, 17);
            this.op2.TabIndex = 1;
            this.op2.Text = "Show Player Turn Label";
            this.op2.UseVisualStyleBackColor = true;
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 259);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Options";
            this.Text = "Options";
            this.Load += new System.EventHandler(this.Options_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox op5;
        private System.Windows.Forms.CheckBox op4;
        private System.Windows.Forms.CheckBox op3;
        private System.Windows.Forms.CheckBox op1;
        private System.Windows.Forms.CheckBox op2;
    }
}