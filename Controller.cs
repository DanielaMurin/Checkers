using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Checkers
{
    public enum eMoveList
    {
        InvalidMove,
        Move,
        JumpUpLeft,
        JumpUpRight,
        JumpDownLeft,
        JumpDownRight,
        Quit
    }

    public enum eGameStatus
    {
        Ongoing,
        WaitingOnCorrectMove,
        Draw,
        PlayerOneWin,
        PlayerTwoWin
    }

    public enum ePlayerStatus
    {
        MustJump,
        MustMove,
        UnableToMove
    }

    public delegate void MoveEventHandler();

    public class Controller
    {
        private Verifier m_Verifier; // verifies and updates proper 
        private CheckersBoard m_CheckersBoard; // holds all information regarding the physical board
        private Player m_CurrentPlayer;
        private Player m_PrevPlayer;
        private StringBuilder m_UserInputBuilder = new StringBuilder(); // holds user input
        private Player m_FirstPlayer;
        private Player m_SecondPlayer;
        private eGameStatus m_GameStatus = eGameStatus.Ongoing; // updates game status
        private int m_CurrJumpRow;
        private int m_CurrJumpCol;

        public event MoveEventHandler m_ChessPieceMoved;

        public Controller(Player i_FirstPlayer, Player i_SecondPlayer, Verifier i_Verifier)
        {
            m_Verifier = i_Verifier;
            m_FirstPlayer = i_FirstPlayer;
            m_SecondPlayer = i_SecondPlayer;
            m_CurrentPlayer = m_FirstPlayer;
        }

        public eGameStatus GameStatus
        {
            get
            {
                return m_GameStatus;
            }
        }

        public int FirstPlayerScore
        {
            get
            {
                return m_FirstPlayer.PlayerScore;
            }
        }

        public int SecondPlayerScore
        {
            get
            {
                return m_SecondPlayer.PlayerScore;
            }
        }

        public string FirstPlayerName
        {
            get
            {
                return m_FirstPlayer.PlayerName;
            }
        }

        public string SecondPlayerName
        {
            get
            {
                return m_SecondPlayer.PlayerName;
            }
        }

        public void StartGame(ref CheckersBoard io_CheckersBoard)
        {
            m_CheckersBoard = io_CheckersBoard;
            m_CheckersBoard.InitialPiecePlacement(); // initialize piece placement 
            m_PrevPlayer = m_SecondPlayer; // player whose turn is up next (previous after first turn)                          
            m_CurrentPlayer = m_FirstPlayer; // switch between current and prev player
            m_CurrentPlayer.PlayerMove = null; // holds comment of current player's turn, make sure it is empty
            m_PrevPlayer.PlayerMove = null; // holds comment of previous player's turn, make sure it is empty
        }

        public void PlayerTurn(int i_CurrRow, int i_CurrCol, int i_NextRow, int i_NextCol)
        {
            ePlayerStatus currPlayerStatus;
            DialogResult result = DialogResult.None;

            currPlayerStatus = checkAvailableMoves(); // check available moves for player (mustjump, mustmove)

            if (currPlayerStatus != ePlayerStatus.UnableToMove)
            {
                if (m_CurrentPlayer.IsHuman)
                {
                    currPlayerTurn(currPlayerStatus, i_CurrRow, i_CurrCol, i_NextRow, i_NextCol); // get valid move, implement it and update board
                }
                else
                {
                    currAIPlayerTurn(currPlayerStatus, i_CurrRow, i_CurrCol, i_NextRow, i_NextCol);
                }
            }

            currPlayerStatus = checkAvailableMoves();

            if (m_GameStatus == eGameStatus.PlayerOneWin)
            {
                result = showMessageBox(1); // send player 1 num
            }
            else if (m_GameStatus == eGameStatus.PlayerTwoWin)
            {
                result = showMessageBox(2); // send player 2 num
            }
            else if(m_GameStatus == eGameStatus.Draw)
            {
                MessageBox.Show("Tie! Another Round?", "Congratulations...?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            }

            if (result == DialogResult.Yes)
            {
                m_CheckersBoard.InitialPiecePlacement();
                StartGame(ref m_CheckersBoard);
            }
        }

        private DialogResult showMessageBox(int i_PlayerNum)
        {
            updateGameResult(m_GameStatus, m_CheckersBoard.CheckerBoard, m_FirstPlayer, m_SecondPlayer);
            return MessageBox.Show(string.Format("Player {0} Wins \n Another Round?", i_PlayerNum.ToString()), "Congratulations", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        }

        private ePlayerStatus checkAvailableMoves()
        {
            bool mustJump, mustMove;
            ePlayerStatus moveStatus;

            //// check if jump is possible then user must jump, return the proper status
            mustJump = m_Verifier.IsJumpPossible(m_CheckersBoard.CheckerBoard, m_CurrentPlayer);

            if (mustJump == true)
            {
                moveStatus = ePlayerStatus.MustJump;
            }
            else
            {
                //// if jump is not possible then check if move is possible
                mustMove = m_Verifier.IsMovePossible(m_CheckersBoard.CheckerBoard, ref m_GameStatus);

                if (mustMove == true)
                {
                    moveStatus = ePlayerStatus.MustMove;
                }
                else
                {
                    //// if neither jump or move is possible then player is unable to move (player loses or game ends in a draw)
                    moveStatus = ePlayerStatus.UnableToMove;
                }
            }

            return moveStatus;
        }

        private void currPlayerTurn(ePlayerStatus i_CurrPlayerStatus, int i_CurrRow, int i_CurrCol, int i_NextRow, int i_NextCol)
        {
            bool isContinuousJump = false;

            eMoveList moveType;
            int currRow = i_CurrRow, currCol = i_CurrCol, nextRow = i_NextRow, nextCol = i_NextCol;

            moveType = returnValidMove(ref currRow, ref currCol, ref nextRow, ref nextCol); // get the user's move type (jump, move) if the input is invalid then returnValidMove loops until valid input is recieved 

            //// must jump (also check continuous jump)
            if (i_CurrPlayerStatus == ePlayerStatus.MustJump || isContinuousJump == true)
            {
                if (moveType.ToString().Contains("Jump"))
                {
                    m_CurrentPlayer.checkersPieceMovement(m_CheckersBoard.CheckerBoard, currRow, currCol, nextRow, nextCol, m_CheckersBoard.CheckerBoard[currRow, currCol], moveType);
                    i_CurrPlayerStatus = checkAvailableMoves(); // check if continuous jumps are available
                    m_ChessPieceMoved.Invoke();

                    if (i_CurrPlayerStatus != ePlayerStatus.MustJump)
                    {
                        turnEnd();
                        isContinuousJump = false;
                    }
                    else
                    {
                        isContinuousJump = true;
                        m_CurrJumpRow = nextRow;
                        m_CurrJumpCol = nextCol;
                    }
                }
                else if (isContinuousJump == false)
                {
                    if (moveType.ToString().Contains("Jump"))
                    {
                        m_CurrentPlayer.checkersPieceMovement(m_CheckersBoard.CheckerBoard, currRow, currCol, nextRow, nextCol, m_CheckersBoard.CheckerBoard[currRow, currCol], moveType); // updates matrix and notifies form's matrix update

                        m_ChessPieceMoved.Invoke();

                        i_CurrPlayerStatus = checkAvailableMoves(); // check if continuous jumps are available

                        if (i_CurrPlayerStatus != ePlayerStatus.MustJump)
                        {
                            turnEnd();
                        }
                    }
                    else
                    {
                        MessageBox.Show("You must jump", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                //// regular move, already received valid move
                if (moveType != eMoveList.Quit && moveType != eMoveList.InvalidMove)
                {
                    //// move/erase the checkers piece(s) after valid play
                    m_CurrentPlayer.checkersPieceMovement(m_CheckersBoard.CheckerBoard, currRow, currCol, nextRow, nextCol, m_CheckersBoard.CheckerBoard[currRow, currCol], moveType); // updates matrix and notifies form's matrix update
                    m_ChessPieceMoved.Invoke();
                    turnEnd();
                }
                else
                {
                    MessageBox.Show("Invalid Player Move", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            i_CurrPlayerStatus = checkAvailableMoves();

            if (i_CurrPlayerStatus == ePlayerStatus.UnableToMove)
            {
                PlayerLose();
            }
        }

        private void currAIPlayerTurn(ePlayerStatus i_CurrPlayerStatus, int i_CurrRow, int i_CurrCol, int i_NextRow, int i_NextCol)
        {
            bool isContinuousJump = false;
            eMoveList moveType;
            int currRow = i_CurrRow, currCol = i_CurrCol, nextRow = i_NextRow, nextCol = i_NextCol;

            //// game gets stuck on generating valid AI move, if valid AI move was to be returned then the game would continue as planned
            moveType = returnValidAIMove(ref currRow, ref currCol, ref nextRow, ref nextCol); 

            if (i_CurrPlayerStatus == ePlayerStatus.MustJump || isContinuousJump == true)
            {
                if (moveType.ToString().Contains("Jump"))
                {
                    m_CurrentPlayer.checkersPieceMovement(m_CheckersBoard.CheckerBoard, currRow, currCol, nextRow, nextCol, m_CheckersBoard.CheckerBoard[currRow, currCol], moveType); // updates matrix and notifies form's matrix update
                    i_CurrPlayerStatus = checkAvailableMoves(); // check if continuous jumps are available
                    m_ChessPieceMoved.Invoke();

                    if (i_CurrPlayerStatus != ePlayerStatus.MustJump)
                    {
                        turnEnd();
                        isContinuousJump = false;
                    }
                    else
                    {
                        isContinuousJump = true;
                        m_CurrJumpRow = nextRow;
                        m_CurrJumpCol = nextCol;
                    }
                }
                else if (isContinuousJump == false)
                {
                    if (moveType.ToString().Contains("Jump"))
                    {
                        m_CurrentPlayer.checkersPieceMovement(m_CheckersBoard.CheckerBoard, currRow, currCol, nextRow, nextCol, m_CheckersBoard.CheckerBoard[currRow, currCol], moveType); // updates matrix and notifies form's matrix update

                        m_ChessPieceMoved.Invoke();

                        i_CurrPlayerStatus = checkAvailableMoves(); // check if continuous jumps are available

                        if (i_CurrPlayerStatus != ePlayerStatus.MustJump)
                        {
                            turnEnd();
                        }
                    }
                }
            }
            else
            {
                //// regular move, already received valid move
                if (moveType != eMoveList.Quit && moveType != eMoveList.InvalidMove)
                {
                    //// move/erase the checkers piece(s) after valid play
                    m_CurrentPlayer.checkersPieceMovement(m_CheckersBoard.CheckerBoard, currRow, currCol, nextRow, nextCol, m_CheckersBoard.CheckerBoard[currRow, currCol], moveType);
                    m_ChessPieceMoved.Invoke();
                    turnEnd();
                }
            }
        }

        private eMoveList returnValidMove(ref int io_CurrRow, ref int io_CurrCol, ref int io_NextRow, ref int io_NextCol)
        {
            eMoveList moveType = eMoveList.InvalidMove;

            //// did not quit, get valid input
            //// checks if input is valid according to player's checkers piece

            moveType = m_Verifier.ReturnMoveType(m_CurrentPlayer.CheckersPiece, m_CheckersBoard.CheckerBoard, io_CurrRow, io_CurrCol, io_NextRow, io_NextCol);

            return moveType;
        }

        private eMoveList returnValidAIMove(ref int io_CurrRow, ref int io_CurrCol, ref int io_NextRow, ref int io_NextCol)
        {
            eMoveList moveType = eMoveList.InvalidMove;
			
            while (moveType == eMoveList.InvalidMove)
            {
                m_UserInputBuilder = m_CurrentPlayer.GenerateAIMove(m_CheckersBoard.BoardSize);

                moveType = m_Verifier.ReturnMoveType(m_CurrentPlayer.CheckersPiece, m_CheckersBoard.CheckerBoard, io_CurrRow, io_CurrCol, io_NextRow, io_NextCol); // checks if input is valid according to player
            }

            return moveType;
        }

        private void turnEnd()
        {
            if (m_CurrentPlayer == m_FirstPlayer)
            {
                m_CurrentPlayer = m_SecondPlayer;
                m_PrevPlayer = m_FirstPlayer;
            }
            else
            {
                m_CurrentPlayer = m_FirstPlayer;
                m_PrevPlayer = m_SecondPlayer;
            }
        }

        private eGameStatus userQuit()
        {
            bool playerOneStatus = true, playerTwoStatus = true;

            if (m_CurrentPlayer.CheckersPiece == 'X')
            {
                // player1 loses
                playerOneStatus = false;
            }
            else
            {
                // player2 loses
                playerTwoStatus = false;
            }

            // gets game status according to who quit 
            return m_Verifier.ReturnGameStatus(playerOneStatus, playerTwoStatus);
        }

        /// <summary>
        /// updates game result by reading game status and updating winner's game score
        /// </summary>
        private void updateGameResult(eGameStatus i_GameStatus, char[,] i_CheckersBoard, Player i_FirstPlayer, Player i_SecondPlayer, bool i_DidQuitOccur = false)
        {
            int score = 0;
            if (i_GameStatus == eGameStatus.PlayerOneWin)
            {
                if (i_DidQuitOccur)
                {
                    score = getGameScore(i_GameStatus, i_CheckersBoard, i_FirstPlayer);
                }
                else
                {
                    score = getGameScore(i_GameStatus, i_CheckersBoard);
                }

                i_FirstPlayer.PlayerScore += score;
            }
            else
            {
                if (i_DidQuitOccur)
                {
                    score = getGameScore(i_GameStatus, i_CheckersBoard, i_SecondPlayer);
                }
                else
                {
                    score = getGameScore(i_GameStatus, i_CheckersBoard);
                }

                i_SecondPlayer.PlayerScore += score;
            }
        }

        private int getGameScore(eGameStatus i_GameStatus, char[,] i_CheckersBoard, Player i_Player = null)
        {
            int playerOneScore = 0, playerTwoScore = 0, score;

            //// count each player's pieces
            foreach (char element in i_CheckersBoard)
            {
                if (element == 'X')
                {
                    playerOneScore++;
                }
                else if (element == 'K')
                {
                    playerOneScore += 4;
                }
                else if (element == 'O')
                {
                    playerTwoScore++;
                }
                else if (element == 'U')
                {
                    playerTwoScore += 4;
                }
            }
            //// if player == null  that means that no one quit and the winner gets the correct score
            if (i_Player == null)
            {
                score = Math.Abs(playerTwoScore - playerOneScore);
            }
            else if (i_Player.CheckersPiece == 'O')
            {
                ////if a player did quit we decided to award the winner points for each of his remaining soldiers / kings                
                score = playerTwoScore;
            }
            else
            {
                score = playerOneScore;
            }

            return score;
        }
        
        /// <summary>
        /// checks to see which player quit
        /// </summary>
        public void PlayerLose()
        {
            if(m_CurrentPlayer == m_FirstPlayer)
            {
                m_GameStatus = eGameStatus.PlayerTwoWin;
            }
            else
            {
                m_GameStatus = eGameStatus.PlayerOneWin;
            }

            updateGameResult(GameStatus, m_CheckersBoard.CheckerBoard, m_FirstPlayer, m_SecondPlayer, true);
        }

        public bool isValidPlayerTurn()
        {
            bool isValidPlayerTurn;

            if(m_CurrentPlayer == m_SecondPlayer && m_CurrentPlayer.IsHuman == false)
            {
                isValidPlayerTurn = false;
                MessageBox.Show("Computer Player's Move", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                isValidPlayerTurn = true;
            }

            return isValidPlayerTurn;
        }
    }
}