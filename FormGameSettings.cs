using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Checkers;

namespace Checkers
{
    public partial class FormGameSettings : Form
    {
        private int m_BoardSize;
        private string m_Player1;
        private string m_Player2 = "Computer"; // default game is set to computer, will update if PvP is selected
        private Verifier m_Verifier = new Verifier();
        private CheckersGame m_CheckersGame;

        public CheckersGame CheckersGame
        {
            get
            {
                return m_CheckersGame;
            }
        }

        public int BoardSize
        {
            get
            {
               return m_BoardSize;
            }
        }

        public FormGameSettings()
        {
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Game Settings";

            InitializeComponent();
        }

        private void GameSettings_Load(object sender, EventArgs e)
        {
        }

        private void labelBoardSize_Click(object sender, EventArgs e)
        {         
        }

        private void radioButton8By8_CheckedChanged(object sender, EventArgs e)
        {
            m_BoardSize = 8;
        }

        private void radioButton6By6_CheckedChanged(object sender, EventArgs e)
        {
            m_BoardSize = 6;
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            m_Player1 = textBoxPlayer1.Text;
            m_Player2 = textBoxPlayer2.Text;
            
            if(m_Verifier.IsValidPlayerName(m_Player1) && m_Verifier.IsValidPlayerName(m_Player2))
            {
                m_CheckersGame = new CheckersGame(m_BoardSize, m_Player1, m_Player2, checkBoxPlayer2.Checked);
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid Player Input", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }     
        }

        private void textBoxPlayer2_TextChanged(object sender, EventArgs e)
        {
        }

        private void labelPlayer1_Click(object sender, EventArgs e)
        {         
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxPlayer2.Checked == true)
            {
                textBoxPlayer2.Enabled = true;
                textBoxPlayer2.ResetText();
            }
            else
            {
                textBoxPlayer2.Enabled = false;
                textBoxPlayer2.Text = "[Computer]";
            }
        }

        private void radioButton10By10_CheckedChanged(object sender, EventArgs e)
        {
            m_BoardSize = 10;
        }

        private void textBoxPlayer1_TextChanged(object sender, EventArgs e)
        {
        }

        private void labelPlayers_Click(object sender, EventArgs e)
        {
        }

        private void FormGameSettings_Load(object sender, EventArgs e)
        {
        }

        private void FormGameSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show(e.CloseReason.ToString());
        }
    }
}