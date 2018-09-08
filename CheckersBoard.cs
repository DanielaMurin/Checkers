using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers
{
    public enum eCol
    {
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        H,
        I,
        J
    }

    public enum eRow
    {
        a,
        b,
        c,
        d,
        e,
        f,
        g,
        h,
        i,
        j
    }

    public class CheckersBoard
    {
        private int m_BoardSize;
        private char[,] m_CheckersBoard;

        public CheckersBoard(int i_BoardSize)
        {
            m_BoardSize = i_BoardSize;
            m_CheckersBoard = new char[m_BoardSize, m_BoardSize];
        }

        public char[,] CheckerBoard
        {
            get
            {
                return m_CheckersBoard;
            }

            set
            {
                m_CheckersBoard = value;
            }
        }

        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }

            set
            {
                m_BoardSize = value;
            }
        }

        public void InitialPiecePlacement()
        {
            int row, col;
            //// upper rows of soldiers
            for (row = 0; row < (m_BoardSize - 2) / 2; row++)
            {
                for (col = 0; col < m_BoardSize; col++)
                {
                    if ((row + col) % 2 == 1)
                    {
                        CheckerBoard[row, col] = 'O';
                    }
                    else
                    {
                        CheckerBoard[row, col] = ' ';
                    }
                }
            }

            // lower rows of soldiers
            for (row = (m_BoardSize + 2) / 2; row < m_BoardSize; row++)
            {
                for (col = 0; col < m_BoardSize; col++)
                {
                    if ((row + col) % 2 == 1)
                    {
                        CheckerBoard[row, col] = 'X';
                    }
                    else
                    {
                        CheckerBoard[row, col] = ' ';
                    }
                }
            }

            // two empty middle rows
            for (row = (m_BoardSize / 2) - 1; row <= m_BoardSize / 2; row++)
            {
                for (col = 0; col < m_BoardSize; col++)
                {
                    CheckerBoard[row, col] = ' ';
                }
            }
        }
    }
}