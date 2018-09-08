namespace Checkers
{
    public partial class FormGameSettings
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
            this.labelBoardSize = new System.Windows.Forms.Label();
            this.radioButton6By6 = new System.Windows.Forms.RadioButton();
            this.radioButton8By8 = new System.Windows.Forms.RadioButton();
            this.radioButton10By10 = new System.Windows.Forms.RadioButton();
            this.labelPlayers = new System.Windows.Forms.Label();
            this.labelPlayer1 = new System.Windows.Forms.Label();
            this.checkBoxPlayer2 = new System.Windows.Forms.CheckBox();
            this.textBoxPlayer1 = new System.Windows.Forms.TextBox();
            this.textBoxPlayer2 = new System.Windows.Forms.TextBox();
            this.buttonDone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelBoardSize
            // 
            this.labelBoardSize.AutoSize = true;
            this.labelBoardSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBoardSize.Location = new System.Drawing.Point(32, 19);
            this.labelBoardSize.Name = "labelBoardSize";
            this.labelBoardSize.Size = new System.Drawing.Size(125, 26);
            this.labelBoardSize.TabIndex = 0;
            this.labelBoardSize.Text = "Board Size:";
            this.labelBoardSize.Click += new System.EventHandler(this.labelBoardSize_Click);
            // 
            // radioButton6By6
            // 
            this.radioButton6By6.AutoSize = true;
            this.radioButton6By6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton6By6.Location = new System.Drawing.Point(37, 69);
            this.radioButton6By6.Name = "radioButton6By6";
            this.radioButton6By6.Size = new System.Drawing.Size(84, 30);
            this.radioButton6By6.TabIndex = 1;
            this.radioButton6By6.TabStop = true;
            this.radioButton6By6.Text = "6 x 6";
            this.radioButton6By6.UseVisualStyleBackColor = true;
            this.radioButton6By6.CheckedChanged += new System.EventHandler(this.radioButton6By6_CheckedChanged);
            // 
            // radioButton8By8
            // 
            this.radioButton8By8.AutoSize = true;
            this.radioButton8By8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton8By8.Location = new System.Drawing.Point(258, 69);
            this.radioButton8By8.Name = "radioButton8By8";
            this.radioButton8By8.Size = new System.Drawing.Size(84, 30);
            this.radioButton8By8.TabIndex = 2;
            this.radioButton8By8.TabStop = true;
            this.radioButton8By8.Text = "8 x 8";
            this.radioButton8By8.UseVisualStyleBackColor = true;
            this.radioButton8By8.CheckedChanged += new System.EventHandler(this.radioButton8By8_CheckedChanged);
            // 
            // radioButton10By10
            // 
            this.radioButton10By10.AutoSize = true;
            this.radioButton10By10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton10By10.Location = new System.Drawing.Point(480, 69);
            this.radioButton10By10.Name = "radioButton10By10";
            this.radioButton10By10.Size = new System.Drawing.Size(108, 30);
            this.radioButton10By10.TabIndex = 3;
            this.radioButton10By10.TabStop = true;
            this.radioButton10By10.Text = "10 x 10";
            this.radioButton10By10.UseVisualStyleBackColor = true;
            this.radioButton10By10.CheckedChanged += new System.EventHandler(this.radioButton10By10_CheckedChanged);
            // 
            // labelPlayers
            // 
            this.labelPlayers.AutoSize = true;
            this.labelPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlayers.Location = new System.Drawing.Point(33, 131);
            this.labelPlayers.Name = "labelPlayers";
            this.labelPlayers.Size = new System.Drawing.Size(91, 26);
            this.labelPlayers.TabIndex = 4;
            this.labelPlayers.Text = "Players:";
            this.labelPlayers.Click += new System.EventHandler(this.labelPlayers_Click);
            // 
            // labelPlayer1
            // 
            this.labelPlayer1.AutoSize = true;
            this.labelPlayer1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlayer1.Location = new System.Drawing.Point(69, 192);
            this.labelPlayer1.Name = "labelPlayer1";
            this.labelPlayer1.Size = new System.Drawing.Size(98, 26);
            this.labelPlayer1.TabIndex = 5;
            this.labelPlayer1.Text = "Player 1:";
            this.labelPlayer1.Click += new System.EventHandler(this.labelPlayer1_Click);
            // 
            // checkBoxPlayer2
            // 
            this.checkBoxPlayer2.AutoSize = true;
            this.checkBoxPlayer2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxPlayer2.Location = new System.Drawing.Point(74, 259);
            this.checkBoxPlayer2.Name = "checkBoxPlayer2";
            this.checkBoxPlayer2.Size = new System.Drawing.Size(124, 30);
            this.checkBoxPlayer2.TabIndex = 6;
            this.checkBoxPlayer2.Text = "Player 2:";
            this.checkBoxPlayer2.UseVisualStyleBackColor = true;
            this.checkBoxPlayer2.CheckedChanged += new System.EventHandler(this.checkBoxPlayer2_CheckedChanged);
            // 
            // textBoxPlayer1
            // 
            this.textBoxPlayer1.Location = new System.Drawing.Point(249, 192);
            this.textBoxPlayer1.Name = "textBoxPlayer1";
            this.textBoxPlayer1.Size = new System.Drawing.Size(291, 26);
            this.textBoxPlayer1.TabIndex = 7;
            this.textBoxPlayer1.TextChanged += new System.EventHandler(this.textBoxPlayer1_TextChanged);
            // 
            // textBoxPlayer2
            // 
            this.textBoxPlayer2.Enabled = false;
            this.textBoxPlayer2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPlayer2.Location = new System.Drawing.Point(249, 250);
            this.textBoxPlayer2.Name = "textBoxPlayer2";
            this.textBoxPlayer2.Size = new System.Drawing.Size(291, 32);
            this.textBoxPlayer2.TabIndex = 8;
            this.textBoxPlayer2.Text = "[Computer]";
            this.textBoxPlayer2.TextChanged += new System.EventHandler(this.textBoxPlayer2_TextChanged);
            // 
            // buttonDone
            // 
            this.buttonDone.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonDone.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDone.Location = new System.Drawing.Point(446, 305);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(170, 56);
            this.buttonDone.TabIndex = 9;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // FormGameSettings
            // 
            this.ClientSize = new System.Drawing.Size(648, 373);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.textBoxPlayer2);
            this.Controls.Add(this.textBoxPlayer1);
            this.Controls.Add(this.checkBoxPlayer2);
            this.Controls.Add(this.labelPlayer1);
            this.Controls.Add(this.labelPlayers);
            this.Controls.Add(this.radioButton10By10);
            this.Controls.Add(this.radioButton8By8);
            this.Controls.Add(this.radioButton6By6);
            this.Controls.Add(this.labelBoardSize);
            this.Name = "FormGameSettings";
            this.Text = "Game Settings";
            this.Load += new System.EventHandler(this.FormGameSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelBoardSize;
        private System.Windows.Forms.RadioButton radioButton6By6;
        private System.Windows.Forms.RadioButton radioButton8By8;
        private System.Windows.Forms.RadioButton radioButton10By10;
        private System.Windows.Forms.Label labelPlayers;
        private System.Windows.Forms.Label labelPlayer1;
        private System.Windows.Forms.CheckBox checkBoxPlayer2;
        private System.Windows.Forms.TextBox textBoxPlayer1;
        private System.Windows.Forms.TextBox textBoxPlayer2;
        private System.Windows.Forms.Button buttonDone;
    }
}