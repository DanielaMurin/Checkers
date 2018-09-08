using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Checkers
{
    public class Verifier
    {
        public int ReturnValidBoardSize()
        {
            int sizeboardSizeToReturn;
            const bool v_InvalidInput = true;
            StringBuilder inputBuilder = new StringBuilder();
            //// the loop's purpose is to ensure the user writing a valid input
            //// the int.tryparse function checks for any other characters other than digits
            //// and there is another condition that checks if it is one of the three posibilities for a board size
            inputBuilder.Append(Console.ReadLine());
			
            while (v_InvalidInput)
            {
                bool result = int.TryParse(inputBuilder.ToString(), out sizeboardSizeToReturn);

                // check if the number entered is a number and a valid size
                if (result && (sizeboardSizeToReturn == 6 || sizeboardSizeToReturn == 8 || sizeboardSizeToReturn == 10))
                {
                    return sizeboardSizeToReturn;
                }
                else
                {
                    Console.Write("The input is incorrect, please try again: ");
                    inputBuilder.Length = 0; // clearing the stringbuilder string
                    inputBuilder.Append(Console.ReadLine());
                }
            }
        }

        public bool IsValidPlayerName(string i_InputStr)
        {
            bool invalidInput;

            //// length is up to 20 characters with no spaces included
            if (i_InputStr.Length <= 20 && noSpaceInString(i_InputStr))
            {
                invalidInput = true;
            }
            else
            {
                invalidInput = false;
            }

            return invalidInput;
        }

        //// checks that no spaces were inserted in the input
        private bool noSpaceInString(string i_InputStr)
        {
            bool isNoBackSpacesInString = true;

            for (int i = 0; i < i_InputStr.Length; i++)
            {
                if (i_InputStr[i] == ' ')
                {
                    isNoBackSpacesInString = false;
                    break;
                }
            }

            return isNoBackSpacesInString;
        }

        //// checks valid user input and if PvP or PvE mode
        public bool IsPvPMode()
        {
            StringBuilder inputBuilder = new StringBuilder();
            bool pvPMode = false;
            bool isInvalidInput = true;

            inputBuilder.Append(Console.ReadLine());
            while (isInvalidInput)
            {
                if (inputBuilder.ToString() == "1")
                {
                    isInvalidInput = false;
                    pvPMode = true;
                }
                else if (inputBuilder.ToString() == "2")
                {
                    isInvalidInput = false;
                    pvPMode = false;
                }
                else
                {
                    Console.Write("The input is incorrect, please try again: ");
                    inputBuilder.Length = 0; // clearing the stringbuilder string
                    inputBuilder.Append(Console.ReadLine());
                }
            }

            return pvPMode;
        }

        //// checks if user surrendered
        public bool DidPlayerSurrender(StringBuilder i_InputBuilder)
        {
            bool didSurrender = i_InputBuilder.ToString() == "Q";

            return didSurrender;
        }
        //// check and return valid move according to current player's checker piece
        public eMoveList ReturnMoveType(char i_PlayerPiece, char[,] i_CheckersBoard, int i_CurrRow, int i_CurrCol, int i_nextRow, int i_NextCol)
        {
            eMoveList moveType = eMoveList.InvalidMove;

            if (i_CheckersBoard[i_CurrRow, i_CurrCol] == 'X' && i_PlayerPiece == 'X')
            {
                moveType = tryUpValidMove(i_CheckersBoard, i_CurrRow, i_CurrCol, i_nextRow, i_NextCol);
            }
            else if (i_CheckersBoard[i_CurrRow, i_CurrCol] == 'O' && i_PlayerPiece == 'O')
            {
                moveType = tryDownValidMove(i_CheckersBoard, i_CurrRow, i_CurrCol, i_nextRow, i_NextCol);
            }
            else if ((i_CheckersBoard[i_CurrRow, i_CurrCol] == 'K' && i_PlayerPiece == 'X') || 
                (i_CheckersBoard[i_CurrRow, i_CurrCol] == 'U' && i_PlayerPiece == 'O'))
            {
                eMoveList moveUp, moveDown;
                moveUp = tryUpValidMove(i_CheckersBoard, i_CurrRow, i_CurrCol, i_nextRow, i_NextCol);
                moveDown = tryDownValidMove(i_CheckersBoard, i_CurrRow, i_CurrCol, i_nextRow, i_NextCol);

                if (moveUp == eMoveList.InvalidMove && moveDown == eMoveList.InvalidMove)
                {
                    moveType = eMoveList.InvalidMove;
                }
                else if (moveUp == eMoveList.InvalidMove)
                {
                    moveType = moveDown;
                }
                else
                {
                    moveType = moveUp;
                }
            }
            else
            {
                //// i_CheckersBoard[i_CurrRow, i_CurrCol] == ' ' (no available piece)
                moveType = eMoveList.InvalidMove;
            }

            return moveType;
        }

        private eMoveList tryUpValidMove(char[,] i_CheckersBoard, int i_CurrRow, int i_CurrCol, int i_NextRow, int i_NextCol)
        {
            eMoveList moveType = eMoveList.InvalidMove;

            if (i_CurrRow - i_NextRow <= 2 && i_CurrRow > i_NextRow && Math.Abs(i_CurrCol - i_NextCol) <= 2 &&
                Math.Abs(i_CurrCol - i_NextCol) > 0 && i_CheckersBoard[i_NextRow, i_NextCol] == ' ')
            {
                //// 1 skip for regular movement (move)
                if (i_CurrRow - i_NextRow == 1 && Math.Abs(i_CurrCol - i_NextCol) == 1)
                {
                    moveType = eMoveList.Move;
                }
                else
                {
                    ////jump
                    if (i_CheckersBoard[i_CurrRow, i_CurrCol] == 'K' || i_CheckersBoard[i_CurrRow, i_CurrCol] == 'X')
                    {
                        //// right jump i_NextRow + 1, i_NextCol - 1
                        if (i_CurrRow - 2 == i_NextRow && i_CurrCol + 2 == i_NextCol &&
                           (i_CheckersBoard[i_CurrRow - 1, i_CurrCol + 1] == 'O' || i_CheckersBoard[i_CurrRow - 1, i_CurrCol + 1] == 'U'))
                        {
                            moveType = eMoveList.JumpUpRight;
                        }
                        else if (i_CurrRow - 2 == i_NextRow && i_CurrCol - 2 == i_NextCol &&
                                (i_CheckersBoard[i_CurrRow - 1, i_CurrCol - 1] == 'O' || i_CheckersBoard[i_CurrRow - 1, i_CurrCol - 1] == 'U'))
                        {
                            //// left jump i_NextRow + 1, i_NextCol + 1
                            moveType = eMoveList.JumpUpLeft;
                        }
                    }
                    else if (i_CheckersBoard[i_CurrRow, i_CurrCol] == 'U')
                    {
                        //// right jump i_NextRow + 1, i_NextCol - 1
                        if (i_CurrRow - 2 == i_NextRow && i_CurrCol + 2 == i_NextCol &&
                           (i_CheckersBoard[i_CurrRow - 1, i_CurrCol + 1] == 'X' || i_CheckersBoard[i_CurrRow - 1, i_CurrCol + 1] == 'K'))
                        {
                            moveType = eMoveList.JumpUpRight;
                        }
                        else if (i_CurrRow - 2 == i_NextRow && i_CurrCol - 2 == i_NextCol &&
                                (i_CheckersBoard[i_CurrRow - 1, i_CurrCol - 1] == 'X' || i_CheckersBoard[i_CurrRow - 1, i_CurrCol - 1] == 'K'))
                        {
                            //// left jump i_NextRow + 1, i_NextCol + 1
                            moveType = eMoveList.JumpUpLeft;
                        }
                    }
                }
            }

            return moveType;
        }

        private eMoveList tryDownValidMove(char[,] i_CheckersBoard, int i_CurrRow, int i_CurrCol, int i_NextRow, int i_NextCol)
        {
            eMoveList moveType = eMoveList.InvalidMove;

            if (i_NextRow - i_CurrRow <= 2 && i_CurrRow < i_NextRow && Math.Abs(i_CurrCol - i_NextCol) <= 2 &&
                Math.Abs(i_CurrCol - i_NextCol) > 0 && i_CheckersBoard[i_NextRow, i_NextCol] == ' ')
            {
                //// 1 skip for regular movement
                if (i_NextRow - i_CurrRow == 1 && Math.Abs(i_CurrCol - i_NextCol) == 1)
                {
                    moveType = eMoveList.Move;
                }
                else
                {
                    //// 2 skips for a "jump" move
                    if (i_CheckersBoard[i_CurrRow, i_CurrCol] == 'U' || i_CheckersBoard[i_CurrRow, i_CurrCol] == 'O')
                    {
                        //// right jump
                        if (i_CurrRow + 2 == i_NextRow && i_CurrCol + 2 == i_NextCol &&
                           (i_CheckersBoard[i_CurrRow + 1, i_CurrCol + 1] == 'X' || i_CheckersBoard[i_CurrRow + 1, i_CurrCol + 1] == 'K'))
                        {
                            moveType = eMoveList.JumpDownRight;
                        }
                        else if (i_CurrRow + 2 == i_NextRow && i_CurrCol - 2 == i_NextCol &&
                                (i_CheckersBoard[i_CurrRow + 1, i_CurrCol - 1] == 'X' || i_CheckersBoard[i_CurrRow + 1, i_CurrCol - 1] == 'K'))
                        {
                            //// left jump
                            moveType = eMoveList.JumpDownLeft;
                        }
                    }
                    else if (i_CheckersBoard[i_CurrRow, i_CurrCol] == 'K')
                    {
                        //// right jump
                        if (i_CurrRow + 2 == i_NextRow && i_CurrCol + 2 == i_NextCol &&
                           (i_CheckersBoard[i_CurrRow + 1, i_CurrCol + 1] == 'O' || i_CheckersBoard[i_CurrRow + 1, i_CurrCol + 1] == 'U'))
                        {
                            moveType = eMoveList.JumpDownRight;
                        }
                        else if (i_CurrRow + 2 == i_NextRow && i_CurrCol - 2 == i_NextCol &&
                                (i_CheckersBoard[i_CurrRow + 1, i_CurrCol - 1] == 'O' || i_CheckersBoard[i_CurrRow + 1, i_CurrCol - 1] == 'U'))
                        {
                            //// left jump
                            moveType = eMoveList.JumpDownLeft;
                        }
                    }
                }
            }

            return moveType;
        }

        //// check the whole checkers board (matrix) if any moves are available for both players
        public bool IsMovePossible(char[,] i_CheckersBoard, ref eGameStatus io_GameStatus)
        {
            bool isPlayer1MovePossible = false;
            bool isPlayer2MovePossible = false;
            bool isMovePossible = true;

            //// if each player has at least one move available for them then the game continues
            for (int row = 0; row < i_CheckersBoard.GetLength(0); row++)
            {
                for (int col = 0; col < i_CheckersBoard.GetLength(0); col++)
                {
                    if (isPlayer1MovePossible == false)
                    {
                        isPlayer1MovePossible = player1PossibleMove(i_CheckersBoard, row, col);
                    }

                    if (isPlayer2MovePossible == false)
                    {
                        isPlayer2MovePossible = player2PossibleMove(i_CheckersBoard, row, col);
                    }
                }
            }

            io_GameStatus = ReturnGameStatus(isPlayer1MovePossible, isPlayer2MovePossible); // update game status if either or both players is/are unable to move
            //// returning a false value to signify that at least one player cannot move
            if (io_GameStatus != eGameStatus.Ongoing) 
            {
                isMovePossible = false;
            }

            return isMovePossible;
        }

        public eGameStatus ReturnGameStatus(bool i_Player1Status, bool i_Player2Status)
        {
            eGameStatus gameStatus = eGameStatus.Ongoing;
            //// if both players are able to move then continue game
            if (i_Player1Status && i_Player2Status)
            {
                gameStatus = eGameStatus.Ongoing;
            }
            else if (i_Player1Status == false && i_Player2Status == false)
            {
                //// if both players are unable to move then its a draw
                gameStatus = eGameStatus.Draw;
            }
            else if (i_Player1Status == false)
            {
                //// if the first player is stuck then the second player automatically wins
                gameStatus = eGameStatus.PlayerTwoWin;
            }
            else
            {
                //// if the second player is stuck then the first player automatically wins (i_Player2Status == false)
                gameStatus = eGameStatus.PlayerOneWin;
            }

            return gameStatus;
        }
        //// check to see avaliable moves for player one
        private bool player1PossibleMove(char[,] i_CheckersBoard, int i_Row, int i_Col)
        {
            bool player1Move = false;

            if (i_CheckersBoard[i_Row, i_Col] == 'X')
            {
                player1Move = isUpMovePossible(i_CheckersBoard, i_Row, i_Col);
            }
            else if (i_CheckersBoard[i_Row, i_Col] == 'K')
            {
                player1Move = isKingMovePossible(i_CheckersBoard, i_Row, i_Col);
            }

            return player1Move;
        }
        //// check to see avaliable moves for player two
        private bool player2PossibleMove(char[,] i_CheckersBoard, int i_Row, int i_Col)
        {
            bool player2Move = false;
            if (i_CheckersBoard[i_Row, i_Col] == 'O')
            {
                player2Move = isDownMovePossible(i_CheckersBoard, i_Row, i_Col);
            }
            else if (i_CheckersBoard[i_Row, i_Col] == 'U')
            {
                player2Move = isKingMovePossible(i_CheckersBoard, i_Row, i_Col);
            }

            return player2Move;
        }

        private bool isUpMovePossible(char[,] i_CheckersBoard, int i_Row, int i_Col)
        {
            bool isUpMovePossible = false;

            if (isCoordinateInBoard(i_Row - 1, i_Col + 1, i_CheckersBoard.GetLength(0)) || isCoordinateInBoard(i_Row - 1, i_Col - 1, i_CheckersBoard.GetLength(0)))
            {
                isUpMovePossible = true;
            }

            return isUpMovePossible;
        }

        private bool isDownMovePossible(char[,] i_CheckersBoard, int i_Row, int i_Col)
        {
            bool isDownMovePossible = false;

            if (isCoordinateInBoard(i_Row + 1, i_Col + 1, i_CheckersBoard.GetLength(0)) || isCoordinateInBoard(i_Row + 1, i_Col - 1, i_CheckersBoard.GetLength(0)))
            {
                isDownMovePossible = true;
            }

            return isDownMovePossible;
        }
        //// a function that combines the move up and move down functions to check whether a king piece can move 
        private bool isKingMovePossible(char[,] i_CheckersBoard, int i_Row, int i_Col)
        {
            bool upMovePossible, downMovePossible;
            bool kingMovePossible = false;

            upMovePossible = isUpMovePossible(i_CheckersBoard, i_Row, i_Col);
            downMovePossible = isDownMovePossible(i_CheckersBoard, i_Row, i_Col);

            if (downMovePossible || upMovePossible)
            {
                kingMovePossible = true;
            }

            return kingMovePossible;
        }

        public bool IsJumpPossible(char[,] i_CheckersBoard, Player i_CurrentPlayer)
        {
            bool isJumpPossible = false;

            int boardRow = 0;
            int boardCol = 0;
            ////scan across the board once we find a possible jump for the current player then we can stop the loop
            while (boardRow < i_CheckersBoard.GetLength(0) && !isJumpPossible)
            {
                boardCol = 0;

                while (boardCol < i_CheckersBoard.GetLength(0) && !isJumpPossible)
                {
                    //// check if jump is possible according to user turn and piece
                    isJumpPossible = jumpClassification(i_CheckersBoard, i_CurrentPlayer.CheckersPiece, boardRow, boardCol);
                    boardCol++;
                }

                boardRow++;
            }

            return isJumpPossible;
        }

        private bool isPlayer1JumpPossible(char[,] i_CheckersBoard, int i_Row, int i_Col)
        {
            bool rightJumpPossible = false;
            bool leftJumpPossible = false;
            bool isPlayer1JumpPossible = false;
            char checkerPiece;

            checkerPiece = i_CheckersBoard[i_Row, i_Col];
            //// king jump
            if (isCoordinateInBoard(i_Row + 2, i_Col + 2, i_CheckersBoard.GetLength(0)) && i_CheckersBoard[i_Row, i_Col] == 'K') 
            {
                if (i_CheckersBoard[i_Row + 2, i_Col + 2] == ' ' && (i_CheckersBoard[i_Row + 1, i_Col + 1] == 'O' || i_CheckersBoard[i_Row + 1, i_Col + 1] == 'U'))
                {
                    rightJumpPossible = true;
                }
            }

            if (isCoordinateInBoard(i_Row + 2, i_Col - 2, i_CheckersBoard.GetLength(0)) && i_CheckersBoard[i_Row, i_Col] == 'K') 
            {
                //// king jump
                if (i_CheckersBoard[i_Row + 2, i_Col - 2] == ' ' && (i_CheckersBoard[i_Row + 1, i_Col - 1] == 'O' || i_CheckersBoard[i_Row + 1, i_Col - 1] == 'U'))
                {
                    leftJumpPossible = true;
                }
            }

            if (isCoordinateInBoard(i_Row - 2, i_Col + 2, i_CheckersBoard.GetLength(0))) 
            {
                //// X and king Jump
                if (i_CheckersBoard[i_Row - 2, i_Col + 2] == ' ' && (i_CheckersBoard[i_Row - 1, i_Col + 1] == 'O' || i_CheckersBoard[i_Row - 1, i_Col + 1] == 'U'))
                {
                    rightJumpPossible = true;
                }
            }

            if (isCoordinateInBoard(i_Row - 2, i_Col - 2, i_CheckersBoard.GetLength(0))) 
            {
                //// X and king Jump
                if (i_CheckersBoard[i_Row - 2, i_Col - 2] == ' ' && (i_CheckersBoard[i_Row - 1, i_Col - 1] == 'O' || i_CheckersBoard[i_Row - 1, i_Col - 1] == 'U'))
                {
                    leftJumpPossible = true;
                }
            }

            if (leftJumpPossible || rightJumpPossible)
            {
                isPlayer1JumpPossible = true;
            }

            return isPlayer1JumpPossible;
        }

        private bool isPlayer2JumpPossible(char[,] i_CheckersBoard, int i_Row, int i_Col)
        {
            bool leftJumpPossible = false;
            bool rightJumpPossible = false;
            bool isPlayer2JumpPossible = false;
            char currPiece = i_CheckersBoard[i_Row, i_Col];

            if (isCoordinateInBoard(i_Row + 2, i_Col + 2, i_CheckersBoard.GetLength(0)))
            {
                if (i_CheckersBoard[i_Row + 2, i_Col + 2] == ' ' && (i_CheckersBoard[i_Row + 1, i_Col + 1] == 'X' || i_CheckersBoard[i_Row + 1, i_Col + 1] == 'K'))
                {
                    rightJumpPossible = true;
                }
            }

            if (isCoordinateInBoard(i_Row + 2, i_Col - 2, i_CheckersBoard.GetLength(0)))
            {
                if (i_CheckersBoard[i_Row + 2, i_Col - 2] == ' ' && (i_CheckersBoard[i_Row + 1, i_Col - 1] == 'X' || i_CheckersBoard[i_Row + 1, i_Col - 1] == 'K'))
                {
                    leftJumpPossible = true;
                }
            }

            if (isCoordinateInBoard(i_Row - 2, i_Col + 2, i_CheckersBoard.GetLength(0)) && i_CheckersBoard[i_Row, i_Col] == 'U') 
            {
                //// king jump
                if (i_CheckersBoard[i_Row - 2, i_Col + 2] == ' ' && (i_CheckersBoard[i_Row - 1, i_Col + 1] == 'X' || i_CheckersBoard[i_Row - 1, i_Col + 1] == 'K'))
                {
                    rightJumpPossible = true;
                }
            }

            if (isCoordinateInBoard(i_Row - 2, i_Col - 2, i_CheckersBoard.GetLength(0)) && i_CheckersBoard[i_Row, i_Col] == 'U') 
            {
                //// king jump
                if (i_CheckersBoard[i_Row - 2, i_Col - 2] == ' ' && (i_CheckersBoard[i_Row - 1, i_Col - 1] == 'X' || i_CheckersBoard[i_Row - 1, i_Col + 1] == 'K'))
                {
                    leftJumpPossible = true;
                }
            }

            if (leftJumpPossible || rightJumpPossible)
            {
                isPlayer2JumpPossible = true;
            }

            return isPlayer2JumpPossible;
        }
        ////checks player piece and matches it to the piece selected in the board
        private bool jumpClassification(char[,] i_CheckersBoard, char i_PlayerPiece, int i_Row, int i_Col)
        {
            bool isJumpPossible = false;

            if (i_PlayerPiece == 'X' && (i_CheckersBoard[i_Row, i_Col] == 'X' || i_CheckersBoard[i_Row, i_Col] == 'K'))
            {
                isJumpPossible = isPlayer1JumpPossible(i_CheckersBoard, i_Row, i_Col);
            }
            else if (i_PlayerPiece == 'O' && (i_CheckersBoard[i_Row, i_Col] == 'O' || i_CheckersBoard[i_Row, i_Col] == 'U'))
            {
                isJumpPossible = isPlayer2JumpPossible(i_CheckersBoard, i_Row, i_Col);
            }

            return isJumpPossible;
        }

        private bool isCoordinateInBoard(int i_Row, int i_Col, int i_BoardSize)
        {
            bool isCoordInBoard;

            if (i_Row < i_BoardSize && i_Col < i_BoardSize && i_Row >= 0 && i_Col >= 0)
            {
                isCoordInBoard = true;
            }
            else
            {
                isCoordInBoard = false;
            }

            return isCoordInBoard;
        }
        ////asks the user if he wishes to play again
        public bool ContinueGame()
        {
            StringBuilder inputBuilder = new StringBuilder();
            bool didContinue = false;
            bool isInvalidInput = true;

            inputBuilder.Append(Console.ReadLine());
            while (isInvalidInput)
            {
                if (inputBuilder.ToString() == "1")
                {
                    isInvalidInput = false;
                    didContinue = false;
                }
                else if (inputBuilder.ToString() == "2")
                {
                    isInvalidInput = false;
                    didContinue = true;
                }
                else
                {
                    Console.Write("The input is incorrect, please try again: ");
                    inputBuilder.Length = 0; // clearing the stringbuilder string
                    inputBuilder.Append(Console.ReadLine());
                }
            }

            return didContinue;
        }
    }
}