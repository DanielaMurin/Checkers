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
    public partial class FormCheckersBoard : Form
    {
        private CheckersBoard m_CheckersBoard;
        private Button m_PrevButton; // previously selected button (where the chess piece resides)
        private int m_PrevRow;
        private int m_PrevCol;
        private CheckersGame m_CheckersGame;
        private Button[,] m_ButtonMatrix;
        private int m_Width;
        private int m_LeftSide; // left side of matrix

        public FormCheckersBoard(int i_BoardSize, CheckersGame i_CheckersGame)
        {
            m_CheckersGame = i_CheckersGame;

            this.StartPosition = FormStartPosition.CenterScreen;

            m_CheckersBoard = new CheckersBoard(i_BoardSize);

            i_CheckersGame.RunGame(ref m_CheckersBoard); // runs logic, updates checkersboard

            generateButtonMatrix();

            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.Fixed3D;

            labelPlayer1.Text = m_CheckersGame.Controller.FirstPlayerName + ":";

            labelPlayer2.Text = m_CheckersGame.Controller.SecondPlayerName + ":";

            this.Left = m_LeftSide;
            this.Width = m_Width + 90;

            UserClosing = false;
            this.FormClosing += new FormClosingEventHandler(formCheckersBoard_FormClosing);

            m_CheckersGame.Controller.m_ChessPieceMoved += updateMatrix; // update matrix is notified whenever we update the checkersboard in the logic section and updates the UI
        }

        private void formCheckersBoard_Load(object sender, EventArgs e)
        {
        }

        private void matrixButtonClick(object sender, EventArgs e)
        {
            if (m_CheckersGame.Controller.isValidPlayerTurn() == true)
            {
                int boardSize = m_CheckersBoard.BoardSize;

                if (sender is Button)
                {
                    Button button = sender as Button;

                    //// translate array index (cellNum) to matrix num
                    int row = (int)button.Tag / boardSize;
                    int col = (int)button.Tag % boardSize;

                    if (m_CheckersBoard.CheckerBoard[row, col] != ' ')
                    {
                        //// user choice a cell with a checkers piece
                        if (button.BackColor == Color.LightBlue)
                        {
                            //// if background color is light blue that means the user already choice a piece to move with
                            //// if user chose a previous checkers piece to move with then delete the previous checkers piece (because we moved)
                            m_PrevButton.BackColor = Color.Black; // after move finishes, then the previous button returns to original color
                        }
                        else
                        {
                            button.BackColor = Color.LightBlue; // user chooses checkers piece to move with, change bg color to light blue
                        }

                        //// user clicked on button with a checkers piece attached to it, that means he wants to move said checkers piece
                        //// so we'll set this button to the previous button because it will soon be moved
                        m_PrevButton = button;
                        m_PrevRow = row;
                        m_PrevCol = col;
                    }
                    else
                    {
                        //// user chose a blank cell
                        //// check if user previously selected a piece to move with
                        if (m_PrevButton != null && m_PrevButton.BackColor == Color.LightBlue)
                        {
                            //// user tried to move, send data to logic
                            m_CheckersGame.Controller.PlayerTurn(m_PrevRow, m_PrevCol, row, col);
                            //// previously selected button color returns to black
                            m_PrevButton.BackColor = Color.Black;

                            if (m_CheckersGame.Controller.GameStatus != eGameStatus.Ongoing)
                            {
                                //// if game ends, update UI score label
                                labelPlayer1Score.Text = m_CheckersGame.Controller.FirstPlayerScore.ToString();
                                labelPlayer2Score.Text = m_CheckersGame.Controller.SecondPlayerScore.ToString();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// update button matrix using updated char matrix that we recieve from logic
        /// </summary>
        private void updateMatrix()
        {
            int boardSize = m_CheckersBoard.BoardSize;

            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    if ((col + row) % 2 == 1)
                    {
                        if (m_CheckersBoard.CheckerBoard[row, col].ToString() == "X")
                        {
                            m_ButtonMatrix[row, col].BackgroundImage = Properties.Resources.BlackChecker;
                            m_ButtonMatrix[row, col].BackgroundImageLayout = ImageLayout.Stretch;
                        }
                        else if (m_CheckersBoard.CheckerBoard[row, col].ToString() == "O")
                        {
                            m_ButtonMatrix[row, col].BackgroundImage = Properties.Resources.RedChecker;
                            m_ButtonMatrix[row, col].BackgroundImageLayout = ImageLayout.Stretch;
                        }
                        else if (m_CheckersBoard.CheckerBoard[row, col].ToString() == "K")
                        {
                            m_ButtonMatrix[row, col].BackgroundImage = Properties.Resources.KingBlackChecker;
                            m_ButtonMatrix[row, col].BackgroundImageLayout = ImageLayout.Stretch;
                        }
                        else if (m_CheckersBoard.CheckerBoard[row, col].ToString() == "U")
                        {
                            m_ButtonMatrix[row, col].BackgroundImage = Properties.Resources.KingRedChecker;
                            m_ButtonMatrix[row, col].BackgroundImageLayout = ImageLayout.Stretch;
                        }
                        else
                        {
                            m_ButtonMatrix[row, col].BackgroundImage = null;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// upon frist game, generate button matrix for the first time
        /// </summary>
        private void generateButtonMatrix()
        {
            int boardSize = m_CheckersBoard.BoardSize;

            m_ButtonMatrix = new Button[boardSize, boardSize];

            int cellNum = 0;
            int col = 0;
            int row = 0;

            //// create and position button matrix 
            //// disable unneeded buttons
            for (row = 0; row < boardSize; row++)
            {
                for (col = 0; col < boardSize; col++)
                {
                    m_ButtonMatrix[row, col] = new Button()
                    {
                        Height = 80,
                        Width = 80,
                        Location = new Point((col * 80) + 60, (row * 80) + 80)
                    };

                    if (row == 0 && col == 0)
                    {
                        m_LeftSide = (col * 80) + 60;
                    }

                    if (((col + row) % 2) == 0)
                    {
                        m_ButtonMatrix[row, col].Enabled = false;
                        m_ButtonMatrix[row, col].BackColor = Color.Red;
                    }
                    else
                    {
                        if (m_CheckersBoard.CheckerBoard[row, col].ToString() == "X")
                        {
                            m_ButtonMatrix[row, col].BackgroundImage = Properties.Resources.BlackChecker;
                            m_ButtonMatrix[row, col].BackgroundImageLayout = ImageLayout.Stretch;
                        }
                        else if (m_CheckersBoard.CheckerBoard[row, col].ToString() == "O")
                        {
                            m_ButtonMatrix[row, col].BackgroundImage = Properties.Resources.RedChecker;
                            m_ButtonMatrix[row, col].BackgroundImageLayout = ImageLayout.Stretch;
                        }

                        m_ButtonMatrix[row, col].BackColor = Color.Black;
                    }

                    this.Controls.Add(m_ButtonMatrix[row, col]);
                    m_ButtonMatrix[row, col].Tag = cellNum++; // saves cell number to later translate it to a matrix number through a few basic math operations
                    m_ButtonMatrix[row, col].Click += matrixButtonClick;
                }
            }

            col--;
            m_Width = col * 60;
        }

        private void labelPlayer1_Click(object sender, EventArgs e)
        {
        }

        private void labelPlayer2_Click(object sender, EventArgs e)
        {
        }

        private void labelPlayer2Score_Click(object sender, EventArgs e)
        {
        }

        public bool UserClosing { get; set; }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            UserClosing = true;
            this.Close();
        }

        private void formCheckersBoard_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Play another game?", "Dialog Title", MessageBoxButtons.YesNoCancel);

                if (result == DialogResult.No)
                {
                    //// user ends entire game
                    Application.Exit();
                }
                else if (result == DialogResult.Yes)
                {
                    //// user quits game round
                    m_CheckersGame.Controller.PlayerLose(); // update game status in logic if user quits using the winform controls (not logic)

                    //// update player score
                    labelPlayer1Score.Text = m_CheckersGame.Controller.FirstPlayerScore.ToString();
                    labelPlayer2Score.Text = m_CheckersGame.Controller.SecondPlayerScore.ToString();

                    m_CheckersGame.Controller.StartGame(ref m_CheckersBoard);

                    e.Cancel = true;
                }
                else
                {
                    //// user continues game
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}