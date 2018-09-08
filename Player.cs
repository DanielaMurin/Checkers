using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers
{
    public class Player
    {
        private char m_CheckersPiece; // indicates who is player1 (X) and who is player2 (O)
        private string m_PlayerName;
        private string m_PlayerMove;
        private int m_PlayerScore;
        private bool m_IsHuman;

        //// properties declarations
        public char CheckersPiece
        {
            get
            {
                return m_CheckersPiece;
            }

            set
            {
                m_CheckersPiece = value;
            }
        }

        public string PlayerName
        {
            get
            {
                return m_PlayerName;
            }

            set
            {
                m_PlayerName = value;
            }
        }

        public string PlayerMove
        {
            get
            {
                return m_PlayerMove;
            }

            set
            {
                m_PlayerMove = value;
            }
        }

        public int PlayerScore
        {
            get
            {
                return m_PlayerScore;
            }

            set
            {
                m_PlayerScore = value;
            }
        }

        public bool IsHuman
        {
            get
            {
                return m_IsHuman;
            }

            set
            {
                m_IsHuman = value;
            }
        }

        public Player(char i_PlayerPiece, string i_PlayerName, bool i_IsHumanPlayer)
        {
            m_CheckersPiece = i_PlayerPiece;
            m_PlayerName = i_PlayerName;
            m_PlayerScore = 0;
            m_IsHuman = i_IsHumanPlayer;
        }

        //// move type tells us in which direction the piece moves and current location specifies where the piece was before the move
        //// erase previous location of piece and update to new location
        public void checkersPieceMovement(char[,] i_CheckersBoard, int i_CurrRow, int i_CurrCol, int i_NextRow, int i_NextCol, char i_PlayerPiece, eMoveList i_MoveType)
        {
            i_CheckersBoard[i_NextRow, i_NextCol] = i_PlayerPiece; // player piece is the checkers piece that the user moves
            i_CheckersBoard[i_CurrRow, i_CurrCol] = ' ';

            if (i_MoveType.ToString().Contains("Jump"))
            {
                chessPieceConquer(i_CheckersBoard, i_CurrRow, i_CurrCol, i_MoveType);
            }

            //// if checkers piece reaches opponent's front line then change it to king accordingly
            changeSoldierToKing(i_CheckersBoard, i_NextRow, i_NextCol);
        }

        //// if jump happened then erase "conquer" opponent's checkers piece
        //// move type tells us in which direction the jump occurred and current row/col specifies were we were before the jump
        //// we then know the location of the checkers piece to erase
        private void chessPieceConquer(char[,] i_CheckersBoard, int i_CurrRow, int i_CurrCol, eMoveList i_MoveType)
        {
            if (i_MoveType == eMoveList.JumpDownLeft)
            {
                i_CheckersBoard[i_CurrRow + 1, i_CurrCol - 1] = ' ';
            }
            else if (i_MoveType == eMoveList.JumpDownRight)
            {
                i_CheckersBoard[i_CurrRow + 1, i_CurrCol + 1] = ' ';
            }
            else if (i_MoveType == eMoveList.JumpUpLeft)
            {
                i_CheckersBoard[i_CurrRow - 1, i_CurrCol - 1] = ' ';
            }
            else if (i_MoveType == eMoveList.JumpUpRight)
            {
                i_CheckersBoard[i_CurrRow - 1, i_CurrCol + 1] = ' ';
            }
        }
        //// check if checkers piece reached the beginning of the opponent's board, if yes then turn it to king
        private void changeSoldierToKing(char[,] i_CheckersBoard, int i_Row, int i_Col)
        {
            if (m_CheckersPiece == 'X' && i_Row == 0)
            {
                i_CheckersBoard[i_Row, i_Col] = 'K';
            }
            else if (m_CheckersPiece == 'O' && i_Row == i_CheckersBoard.GetLength(0) - 1)
            {
                i_CheckersBoard[i_Row, i_Col] = 'U';
            }
        }

        //// generate random valid AI move
        public StringBuilder GenerateAIMove(int i_BoardSize)
        {
            eCol randomCol;
            eRow randomRow;
            StringBuilder generationBuilder = new StringBuilder();

            generateRandomColAndRow(out randomRow, out randomCol, i_BoardSize);
            generationBuilder.Append(randomCol).Append(randomRow);
            generationBuilder.Append('>');
            generateRandomColAndRow(out randomRow, out randomCol, i_BoardSize);
            generationBuilder.Append(randomCol).Append(randomRow);

            return generationBuilder;
        }

        //// generate random number and cast it to enum 
        private void generateRandomColAndRow(out eRow o_RandomRow, out eCol o_RandomCol, int i_BoardSize)
        {
            Random random = new Random();

            o_RandomCol = (eCol)random.Next(i_BoardSize);

            o_RandomRow = (eRow)random.Next(i_BoardSize);
        }
    }
}