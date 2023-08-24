using GameLogic;
using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace FormUI
{
    public partial class GameBoardForm : Form
    {
        private enum eGameStatus
        {
            StillRunning,
            Player1Won,
            Player2Won,
            Tie,
        }

        private Player m_LastPlayer;
        private readonly Player m_Player1 = new Player(string.Empty, 1);
        private readonly Player m_Player2 = new Player(string.Empty, 2);
        private PictureBox m_PrevPictureBox = null;
        private PictureBox m_NextPictureBox = null;
        private Board m_GameBoard;

        public GameBoardForm()
        {
            this.FormClosing += GameBoardForm_FormClosing;
        }

        public void CreateGameBoard(int i_GameBoardSize, Player i_Player1, Player i_Player2, Board i_GameBoard)
        {
            InitializeComponent();
            Size buttonSize = new Size(65, 65);
            Point buttonLocation = new Point(5, 5);
            Size formSize = new Size(5, 5);
            int blackWhiteFlag = 0;
            for (int i = 0; i < i_GameBoardSize; i++)
            {
                if (blackWhiteFlag == 0)
                {
                    blackWhiteFlag = 1;
                }

                else
                {
                    blackWhiteFlag = 0;
                }

                for (int j = 0; j < i_GameBoardSize; j++)
                {
                    PictureBox newSquareOnBoard = new PictureBox();
                    newSquareOnBoard.Size = buttonSize;
                    newSquareOnBoard.Location = buttonLocation;
                    newSquareOnBoard.Click += Key_Pressed;
                    Controls.Add(newSquareOnBoard);
                    if (j % 2 == blackWhiteFlag)
                    {
                        newSquareOnBoard.BackColor = Color.White;
                        if (i >= 0 && i < (i_GameBoardSize - 2) / 2)
                        {
                            newSquareOnBoard.Image = FormUI.Properties.Resources.red_stone;
                            newSquareOnBoard.SizeMode = PictureBoxSizeMode.StretchImage;
                            newSquareOnBoard.Name = "red stone";
                        }

                        else if (i >= (i_GameBoardSize - 2) / 2 + 2 && i < i_GameBoardSize)
                        {
                            newSquareOnBoard.Image = FormUI.Properties.Resources.black_stone;
                            newSquareOnBoard.SizeMode = PictureBoxSizeMode.StretchImage;
                            newSquareOnBoard.Name = "black stone";
                        }
                    }

                    else
                    {
                        newSquareOnBoard.BackColor = Color.Black;
                        newSquareOnBoard.Enabled = false;
                    }

                    newSquareOnBoard.Name += string.Format("{0},{1}", i, j);
                    Controls.Add(newSquareOnBoard);
                    buttonLocation.X += 70;
                }

                formSize.Width = buttonLocation.X + 15;
                buttonLocation.X = 5;
                buttonLocation.Y += 70;
                formSize.Height = buttonLocation.Y + 75;
            }

            this.Size = formSize;
            if (string.IsNullOrEmpty(i_Player1.Name))
            {
                m_Player1.Name = "Player1";
            }

            else
            {
                m_Player1.Name = i_Player1.Name;
            }

            if (string.Equals(i_Player2.Name, "[Computer]"))
            {
                m_Player2.Name = "Computer";
            }

            else if (string.IsNullOrEmpty(i_Player2.Name))
            {
                m_Player2.Name = "Player2";
            }

            else
            {
                m_Player2.Name = i_Player2.Name;
            }

            labelPlayer1NameAndScore.Text = string.Format("{0}:{1}", m_Player1.Name, m_Player1.Score);
            labelPlayer2NameAndScore.Text = string.Format("{0}:{1}", m_Player2.Name, m_Player2.Score);
            m_LastPlayer = i_Player1;
            labelPlayer1NameAndScore.Font = new Font(labelPlayer1NameAndScore.Font.Name, labelPlayer1NameAndScore.Font.Size, FontStyle.Bold);
            m_GameBoard = i_GameBoard;
        }

        private void Key_Pressed(object sender, EventArgs e)
        {
            PictureBox currentPictureBox = (sender as PictureBox);

            if (m_PrevPictureBox == null)
            {
                m_PrevPictureBox = currentPictureBox;
                selectPrevStepPictureBox(currentPictureBox);
            }

            else if (m_PrevPictureBox == currentPictureBox)
            {
                cancelSelectionOfPictureBox(currentPictureBox);
                m_NextPictureBox = m_PrevPictureBox = null;
            }

            else
            {
                m_NextPictureBox = currentPictureBox;
                currentPictureBox.BackColor = Color.CornflowerBlue;
                gameLoop();
                m_PrevPictureBox = null;

            }
        }

        private void selectPrevStepPictureBox(PictureBox i_CurrentPictureBox)
        {
            if (i_CurrentPictureBox.Name.Contains("red stone king"))
            {
                i_CurrentPictureBox.Image = FormUI.Properties.Resources.red_stone_king_selected;
            }

            else if (i_CurrentPictureBox.Name.Contains("black stone king"))
            {
                i_CurrentPictureBox.Image = FormUI.Properties.Resources.black_stone_king_selected;
            }

            else if (i_CurrentPictureBox.Name.Contains("red stone"))
            {
                i_CurrentPictureBox.Image = FormUI.Properties.Resources.red_stone_selected;
            }

            else if (i_CurrentPictureBox.Name.Contains("black stone"))
            {
                i_CurrentPictureBox.Image = FormUI.Properties.Resources.black_stone_selected;
            }
        }

        private void cancelSelectionOfPictureBox(PictureBox i_CurrentPictureBox)
        {
            if (i_CurrentPictureBox.Name.Contains("red stone king"))
            {
                i_CurrentPictureBox.Image = FormUI.Properties.Resources.red_stone_king;
            }

            else if (i_CurrentPictureBox.Name.Contains("black stone king"))
            {
                i_CurrentPictureBox.Image = FormUI.Properties.Resources.black_stone_king;
            }

            else if (i_CurrentPictureBox.Name.Contains("red"))
            {
                i_CurrentPictureBox.Image = FormUI.Properties.Resources.red_stone;
            }

            else if (i_CurrentPictureBox.Name.Contains("black"))
            {
                i_CurrentPictureBox.Image = FormUI.Properties.Resources.black_stone;
            }

            else
            {
                i_CurrentPictureBox.BackColor = Color.White;
            }
        }

        private Point getPointFromPictureBoxName(string i_PictureBoxName)
        {
            string numberRowOrCol = i_PictureBoxName;

            if (numberRowOrCol.Contains("black stone king"))
            {
                numberRowOrCol = numberRowOrCol.Replace("black stone king", "");
            }

            else if (numberRowOrCol.Contains("red stone king"))
            {
                numberRowOrCol = numberRowOrCol.Replace("red stone king", "");
            }

            else if (numberRowOrCol.Contains("black stone"))
            {
                numberRowOrCol = numberRowOrCol.Replace("black stone", "");
            }

            else if (numberRowOrCol.Contains("red stone"))
            {
                numberRowOrCol = numberRowOrCol.Replace("red stone", "");
            }

            int row = int.Parse(numberRowOrCol.Remove(numberRowOrCol.IndexOf(','), numberRowOrCol.Length - numberRowOrCol.IndexOf(',')));
            numberRowOrCol = i_PictureBoxName;
            int col = int.Parse(numberRowOrCol.Remove(0, numberRowOrCol.IndexOf(',') + 1));
            return new Point(row, col);
        }

        private void makeMove()
        {
            Board.eResponseFlag moveStatus = Board.eResponseFlag.NoError;
            string numberRowOrCol = string.Empty;
            Move nextMove = new Move();
            Point enemyPoint;
            bool isKing;
            bool currentPlayerIsComputer;

            if (m_LastPlayer.CheckersNumber == 1)
            {
                labelPlayer1NameAndScore.Font = new Font(labelPlayer1NameAndScore.Font.Name, labelPlayer1NameAndScore.Font.Size, FontStyle.Bold);
                labelPlayer2NameAndScore.Font = new Font(labelPlayer2NameAndScore.Font.Name, labelPlayer2NameAndScore.Font.Size, FontStyle.Regular);
            }

            else
            {
                labelPlayer2NameAndScore.Font = new Font(labelPlayer2NameAndScore.Font.Name, labelPlayer2NameAndScore.Font.Size, FontStyle.Bold);
                labelPlayer1NameAndScore.Font = new Font(labelPlayer1NameAndScore.Font.Name, labelPlayer1NameAndScore.Font.Size, FontStyle.Regular);
            }


            if (m_PrevPictureBox != null && m_NextPictureBox != null)
            {
                Point prevPoint = getPointFromPictureBoxName(m_PrevPictureBox.Name);
                Point nextPoint = getPointFromPictureBoxName(m_NextPictureBox.Name);
                nextMove = new Move(prevPoint, nextPoint);
            }

            if ((m_GameBoard.GetBoardPoint(nextMove.MoveFrom) == Board.ePieceNumber.Player1 ||
                    m_GameBoard.GetBoardPoint(nextMove.MoveFrom) == Board.ePieceNumber.Player1King) &&
                   m_LastPlayer.CheckersNumber != (int)Board.ePieceNumber.Player1)
            {
                moveStatus = Board.eResponseFlag.InvalidMove;
                MessageBox.Show("Invalid move! Please enter valid move!", "invalid move!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cancelSelectionOfPictureBox(m_PrevPictureBox);
                cancelSelectionOfPictureBox(m_NextPictureBox);
                m_NextPictureBox = m_PrevPictureBox = null;
            }

            else if ((m_GameBoard.GetBoardPoint(nextMove.MoveFrom) == Board.ePieceNumber.Player2 ||
                      m_GameBoard.GetBoardPoint(nextMove.MoveFrom) == Board.ePieceNumber.Player2King) &&
                     m_LastPlayer.CheckersNumber != (int)Board.ePieceNumber.Player2)
            {
                moveStatus = Board.eResponseFlag.InvalidMove;
                MessageBox.Show("Invalid move! Please enter valid move!", "invalid move!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cancelSelectionOfPictureBox(m_PrevPictureBox);
                cancelSelectionOfPictureBox(m_NextPictureBox);
                m_NextPictureBox = m_PrevPictureBox = null;
            }

            else
            {
                moveStatus = m_GameBoard.UpdateBoard(nextMove, m_LastPlayer, out enemyPoint, out isKing);
                switch (moveStatus)
                {
                    case Board.eResponseFlag.InvalidMove:
                        MessageBox.Show("Invalid move! Please enter valid move!", "invalid move!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case Board.eResponseFlag.HaveToEatEnemy:
                        MessageBox.Show("You have to eat your enemy!", "invalid move!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case Board.eResponseFlag.IncorrectGameRules:
                        MessageBox.Show("Please follow the game rules!", "invalid move!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                if (moveStatus == Board.eResponseFlag.InvalidMove || moveStatus == Board.eResponseFlag.HaveToEatEnemy || moveStatus == Board.eResponseFlag.IncorrectGameRules)
                {
                    cancelSelectionOfPictureBox(m_PrevPictureBox);
                    cancelSelectionOfPictureBox(m_NextPictureBox);
                    m_NextPictureBox = m_PrevPictureBox = null;
                }

                else
                {
                    updateMoveOnBoardForm(enemyPoint, isKing);

                    if (moveStatus != Board.eResponseFlag.CanEatAgain)
                    {
                        m_LastPlayer = m_LastPlayer.CheckersNumber == m_Player1.CheckersNumber ? m_Player2 : m_Player1;

                        currentPlayerIsComputer = string.Equals(m_LastPlayer.Name, "Computer");
                        if (currentPlayerIsComputer && ((eGameStatus)m_GameBoard.CheckIfWin(m_Player1, m_Player2) == eGameStatus.StillRunning))
                        {
                            makeComputerMove();
                            m_LastPlayer = m_LastPlayer.CheckersNumber == m_Player1.CheckersNumber ? m_Player2 : m_Player1;
                        }

                        if (m_LastPlayer.CheckersNumber == 1)
                        {
                            labelPlayer1NameAndScore.Font = new Font(labelPlayer1NameAndScore.Font.Name, labelPlayer1NameAndScore.Font.Size, FontStyle.Bold);
                            labelPlayer2NameAndScore.Font = new Font(labelPlayer2NameAndScore.Font.Name, labelPlayer2NameAndScore.Font.Size, FontStyle.Regular);
                        }

                        else
                        {
                            labelPlayer2NameAndScore.Font = new Font(labelPlayer2NameAndScore.Font.Name, labelPlayer2NameAndScore.Font.Size, FontStyle.Bold);
                            labelPlayer1NameAndScore.Font = new Font(labelPlayer1NameAndScore.Font.Name, labelPlayer1NameAndScore.Font.Size, FontStyle.Regular);
                        }
                    }
                }
            }
        }

        private void makeComputerMove()
        {
            Point enemyPoint;
            bool isKing;
            Board.eResponseFlag moveStatus;
            moveStatus = pickComputerRandomMove(out enemyPoint, out isKing);

            while (moveStatus != Board.eResponseFlag.NoError && moveStatus != Board.eResponseFlag.CanEatAgain)
            {
                moveStatus = pickComputerRandomMove(out enemyPoint, out isKing);
            }

            updateMoveOnBoardForm(enemyPoint, isKing);
            while (moveStatus == Board.eResponseFlag.CanEatAgain)
            {
                moveStatus = pickComputerRandomMove(out enemyPoint, out isKing);
                while (moveStatus != Board.eResponseFlag.NoError && moveStatus != Board.eResponseFlag.CanEatAgain)
                {
                    moveStatus = pickComputerRandomMove(out enemyPoint, out isKing);
                }

                updateMoveOnBoardForm(enemyPoint, isKing);
            }
        }

        private Board.eResponseFlag pickComputerRandomMove(out Point o_EnemyPoint, out bool o_IsKing)
        {
            Board.eResponseFlag moveStatus;
            Move nextMove = m_LastPlayer.PickRandomComputerMove(m_GameBoard);
            string controlNameFrom = string.Format(@"{0},{1}", nextMove.MoveFrom.X, nextMove.MoveFrom.Y);
            string controlNameTo = string.Format(@"{0},{1}", nextMove.MoveTo.X, nextMove.MoveTo.Y);

            foreach (Control control in Controls)
            {
                if (control.Name.Contains(controlNameFrom))
                {
                    m_PrevPictureBox = control as PictureBox;
                }

                if (control.Name.Contains(controlNameTo))
                {
                    m_NextPictureBox = control as PictureBox;
                }
            }

            moveStatus = m_GameBoard.UpdateBoard(nextMove, m_LastPlayer, out o_EnemyPoint, out o_IsKing);
            return moveStatus;
        }

        private void gameLoop()
        {
            eGameStatus gameStatus = eGameStatus.StillRunning;
            makeMove();
            gameStatus = (eGameStatus)m_GameBoard.CheckIfWin(m_Player1, m_Player2);

            if (gameStatus != eGameStatus.StillRunning)
            {
                m_Player1.CalcPlayersScore(m_GameBoard);
                m_Player2.CalcPlayersScore(m_GameBoard);
                labelPlayer1NameAndScore.Text = string.Format("{0}:{1}", m_Player1.Name, m_Player1.Score.ToString());
                labelPlayer2NameAndScore.Text = string.Format("{0}:{1}", m_Player2.Name, m_Player2.Score.ToString());
                SoundPlayer winningSound = new SoundPlayer(FormUI.Properties.Resources.winning_clap_sound);
                if (gameStatus == eGameStatus.Player1Won)
                {
                    labelPlayer1NameAndScore.ForeColor = Color.Red;
                    labelPlayer1NameAndScore.Font = new Font(labelPlayer1NameAndScore.Font.Name, labelPlayer1NameAndScore.Font.Size, FontStyle.Bold);
                    winningSound.Play();
                    MessageBox.Show(m_Player1.Name + " Won!!!! Congratulations!!!!", "Game Over!");
                }

                else if (gameStatus == eGameStatus.Player2Won)
                {
                    labelPlayer2NameAndScore.ForeColor = Color.Red;
                    labelPlayer2NameAndScore.Font = new Font(labelPlayer2NameAndScore.Font.Name, labelPlayer2NameAndScore.Font.Size, FontStyle.Bold);
                    winningSound.Play();
                    MessageBox.Show(m_Player2.Name + " Won!!!! Congratulations!!!!", "Game Over!");
                }

                else if (gameStatus == eGameStatus.Tie)
                {
                    MessageBox.Show("!!! TIE !!!", "It's a TIE!!!");
                }

                DialogResult dialogResult = MessageBox.Show("would you like to play another game? press Y / N", "Play another game!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    m_GameBoard.BuildBoard(m_Player1, m_Player2);
                    Controls.Clear();
                    CreateGameBoard(m_GameBoard.Size, m_Player1, m_Player2, m_GameBoard);
                }

                else if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("See you next time!", "Quit Game");
                    this.Close();
                }
            }
        }

        private void updateMoveOnBoardForm(Point I_EnemyPoint, bool i_IsKing)
        {
            string prevPictureBoxName = m_PrevPictureBox.Name;
            m_PrevPictureBox.Image = null;
            m_PrevPictureBox.BackColor = Color.White;
            m_PrevPictureBox.Enabled = true;
            m_NextPictureBox.Enabled = true;
            removeStoneName(m_NextPictureBox);

            if (I_EnemyPoint != new Point(-1, -1))
            {
                string controlNameEnemy = string.Format(@"{0},{1}", I_EnemyPoint.X, I_EnemyPoint.Y);
                foreach (Control control in Controls)
                {
                    PictureBox enemyPointPictureBox = (control as PictureBox);
                    if (control.Name.Contains(controlNameEnemy))
                    {
                        enemyPointPictureBox.BackColor = Color.White;
                        enemyPointPictureBox.Image = null;
                        removeStoneName(enemyPointPictureBox);
                    }
                }
            }

            if (i_IsKing && prevPictureBoxName.Contains("red stone"))
            {
                m_NextPictureBox.Image = FormUI.Properties.Resources.red_stone_king;
                m_NextPictureBox.Name = "red stone king" + m_NextPictureBox.Name;
            }

            else if (i_IsKing && prevPictureBoxName.Contains("black stone"))
            {
                m_NextPictureBox.Image = FormUI.Properties.Resources.black_stone_king;
                m_NextPictureBox.Name = "black stone king" + m_NextPictureBox.Name;
            }

            else if (prevPictureBoxName.Contains("red stone"))
            {
                m_NextPictureBox.Image = FormUI.Properties.Resources.red_stone;
                m_NextPictureBox.Name = "red stone" + m_NextPictureBox.Name;
            }

            else if (prevPictureBoxName.Contains("black stone"))
            {
                m_NextPictureBox.Image = FormUI.Properties.Resources.black_stone;
                m_NextPictureBox.Name = "black stone" + m_NextPictureBox.Name;
            }

            m_NextPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            removeStoneName(m_PrevPictureBox);
        }

        private void removeStoneName(PictureBox i_PictureBox)
        {
            if (i_PictureBox.Name.Contains("red stone king"))
            {
                i_PictureBox.Name = i_PictureBox.Name.Replace("red stone king", "");
            }

            else if (i_PictureBox.Name.Contains("black stone king"))
            {
                i_PictureBox.Name = i_PictureBox.Name.Replace("black stone king", "");
            }
            else if (i_PictureBox.Name.Contains("red stone"))
            {
                i_PictureBox.Name = i_PictureBox.Name.Replace("red stone", "");
            }

            else if (i_PictureBox.Name.Contains("black stone"))
            {
                i_PictureBox.Name = i_PictureBox.Name.Replace("black stone", "");
            }
        }

        private void GameBoardForm_FormClosing(Object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to quit game?","Quit!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(dialogResult == DialogResult.Yes)
                {
                    m_Player1.CalcPlayersScore(m_GameBoard);
                    m_Player2.CalcPlayersScore(m_GameBoard);
                    MessageBox.Show(String.Format(@"{0}'s score is: {1} 
{2}'s score is: {3}
Thank you for playing!" , m_Player1.Name, m_Player1.Score, m_Player2.Name, m_Player2.Score));  
                }

                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
