using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Checkers;

namespace Checkers
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FormGameSettings test1 = new FormGameSettings();
            Application.Run(test1);
            if(test1.DialogResult == DialogResult.OK)
            {
                FormCheckersBoard test = new FormCheckersBoard(test1.BoardSize, test1.CheckersGame);
                Application.Run(test);
            }

            ///////CheckersBoard m_CheckersBoard = test.CheckerBoard;
            ///////test1.CheckerGame.RunGame(ref m_CheckersBoard);
        }
    }
}
