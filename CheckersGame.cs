using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers
{
    public class CheckersGame
    {
        private Controller m_Controller;
        private Verifier m_Verifier = new Verifier();
        private bool m_IsPvP;

        public Controller Controller
        {
            get
            {
                return m_Controller;
            }
        }

        public CheckersGame(int i_BoardSize, string i_FirstPlayerName, string i_SecondPlayerName, bool i_IsHuman)
        {
            const bool v_IsHuman = true;

            if(i_IsHuman == true)
            {
                m_IsPvP = true;
            }
            else
            {
                m_IsPvP = false;
            }

            Player firstPlayer = new Player('X', i_FirstPlayerName, v_IsHuman);

            if (m_IsPvP)
            {
                Player secondPlayer = new Player('O', i_SecondPlayerName, v_IsHuman);
                m_Controller = new Controller(firstPlayer, secondPlayer, m_Verifier);
            }
            else
            {
                Player secondPlayer = new Player('O', i_SecondPlayerName, !v_IsHuman);
                m_Controller = new Controller(firstPlayer, secondPlayer, m_Verifier);
            }
        }

        public void RunGame(ref CheckersBoard io_CheckersBoard)
        {
            m_Controller.StartGame(ref io_CheckersBoard); // start function that controls game flow
        }
    }
}