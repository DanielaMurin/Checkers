namespace Checkers
{
    public partial class FormCheckersBoard
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
            this.labelPlayer1 = new System.Windows.Forms.Label();
            this.labelPlayer2 = new System.Windows.Forms.Label();
            this.labelPlayer2Score = new System.Windows.Forms.Label();
            this.labelPlayer1Score = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelPlayer1
            // 
            this.labelPlayer1.AutoSize = true;
            this.labelPlayer1.Location = new System.Drawing.Point(104, 28);
            this.labelPlayer1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPlayer1.Name = "labelPlayer1";
            this.labelPlayer1.Size = new System.Drawing.Size(69, 20);
            this.labelPlayer1.TabIndex = 0;
            this.labelPlayer1.Text = "Player1 :";
            this.labelPlayer1.Click += new System.EventHandler(this.labelPlayer1_Click);
            // 
            // labelPlayer2
            // 
            this.labelPlayer2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPlayer2.AutoSize = true;
            this.labelPlayer2.Location = new System.Drawing.Point(472, 28);
            this.labelPlayer2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPlayer2.Name = "labelPlayer2";
            this.labelPlayer2.Size = new System.Drawing.Size(69, 20);
            this.labelPlayer2.TabIndex = 1;
            this.labelPlayer2.Text = "Player2 :";
            this.labelPlayer2.Click += new System.EventHandler(this.labelPlayer2_Click);
            // 
            // labelPlayer2Score
            // 
            this.labelPlayer2Score.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPlayer2Score.AutoSize = true;
            this.labelPlayer2Score.Location = new System.Drawing.Point(572, 28);
            this.labelPlayer2Score.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPlayer2Score.Name = "labelPlayer2Score";
            this.labelPlayer2Score.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelPlayer2Score.Size = new System.Drawing.Size(18, 20);
            this.labelPlayer2Score.TabIndex = 2;
            this.labelPlayer2Score.Text = "0";
            this.labelPlayer2Score.Click += new System.EventHandler(this.labelPlayer2Score_Click);
            // 
            // labelPlayer1Score
            // 
            this.labelPlayer1Score.AutoSize = true;
            this.labelPlayer1Score.Location = new System.Drawing.Point(201, 28);
            this.labelPlayer1Score.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPlayer1Score.Name = "labelPlayer1Score";
            this.labelPlayer1Score.Size = new System.Drawing.Size(18, 20);
            this.labelPlayer1Score.TabIndex = 3;
            this.labelPlayer1Score.Text = "0";
            // 
            // FormCheckersBoard
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(663, 529);
            this.Controls.Add(this.labelPlayer1Score);
            this.Controls.Add(this.labelPlayer2Score);
            this.Controls.Add(this.labelPlayer2);
            this.Controls.Add(this.labelPlayer1);
            this.MaximizeBox = false;
            this.Name = "FormCheckersBoard";
            this.Text = "FormCheckersBoard";
            this.Load += new System.EventHandler(this.formCheckersBoard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPlayer1;
        private System.Windows.Forms.Label labelPlayer2;
        private System.Windows.Forms.Label labelPlayer2Score;
        private System.Windows.Forms.Label labelPlayer1Score;
    }
}