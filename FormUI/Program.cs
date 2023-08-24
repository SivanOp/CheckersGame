using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GameLogic;

namespace FormUI
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GameSettingsForm gameSettings = new GameSettingsForm();
            Application.Run(gameSettings);

            if (gameSettings.GameStart)
            {
                Player player1 = new Player(gameSettings.Player1Name, 1);
                Player player2 = new Player(gameSettings.Player2Name, 2);
                GameBoardForm gameBoardUI = new GameBoardForm();
                Board gameBoardLogic = new Board((int)gameSettings.SizeOfBoard, player1, player2);
                gameBoardUI.CreateGameBoard(gameSettings.SizeOfBoard, player1, player2, gameBoardLogic);
                Application.Run(gameBoardUI);
            }
        }
    }
}