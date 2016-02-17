namespace OneChess
{
    partial class New_Game
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
            this.player2 = new System.Windows.Forms.ComboBox();
            this.player1 = new System.Windows.Forms.ComboBox();
            this.OK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // player2
            // 
            this.player2.FormattingEnabled = true;
            this.player2.Location = new System.Drawing.Point(34, 116);
            this.player2.Name = "player2";
            this.player2.Size = new System.Drawing.Size(180, 21);
            this.player2.TabIndex = 12;
            this.player2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.player2_KeyPress);
            // 
            // player1
            // 
            this.player1.FormattingEnabled = true;
            this.player1.Location = new System.Drawing.Point(34, 54);
            this.player1.Name = "player1";
            this.player1.Size = new System.Drawing.Size(180, 21);
            this.player1.TabIndex = 11;
            this.player1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.player1_KeyPress);
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(64, 170);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(123, 36);
            this.OK.TabIndex = 10;
            this.OK.Text = "&OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(34, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(180, 21);
            this.label2.TabIndex = 9;
            this.label2.Text = "Player Name 2";
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(34, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 21);
            this.label1.TabIndex = 8;
            this.label1.Text = "Player Name 1";
            // 
            // New_Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 236);
            this.Controls.Add(this.player2);
            this.Controls.Add(this.player1);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "New_Game";
            this.Text = "New_Game";
            this.Load += new System.EventHandler(this.New_Game_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox player2;
        private System.Windows.Forms.ComboBox player1;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}