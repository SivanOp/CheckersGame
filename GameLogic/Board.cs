using System;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Collections.Generic;

namespace GameLogic
{
    public class Board
    {
        public enum ePieceNumber
        {
            Empty,
            Player1,
            Player2,
            Player1King,
            Player2King
        }

        public enum eResponseFlag
        {
            NoError = -1,
            InvalidMove,
            HaveToEatEnemy,
            IncorrectGameRules,
            CanEatAgain
        }

        public const int k_SmallBoardSize = 6;
        public const int k_MediumBoardSize = 8;
        public const int k_LargeBoardSize = 10;
        private ePieceNumber[,] m_GameBoard;
        private readonly int r_Size;

        public Board(int i_Size, Player i_Player1, Player i_Player2)
        {
            r_Size = i_Size;
            m_GameBoard = new ePieceNumber[i_Size, i_Size];
            BuildBoard(i_Player1, i_Player2);

        }

        public int Size
        {
            get
            {
                return r_Size;
            }
        }

        public ePieceNumber[,] GameBoard
        {
            get
            {
                return m_GameBoard;
            }
        }

        public ePieceNumber GetBoardPoint(Point i_CurrentPoint)
        {
            return m_GameBoard[i_CurrentPoint.X, i_CurrentPoint.Y];
        }

        public void BuildBoard(Player i_Player1, Player i_Player2)
        {
            int rowsForEachPlayer = (r_Size - 2) / 2;
            const int k_EvenFlag = 1;
            InitializeBoard();
            placePlayerOnBoard(0, rowsForEachPlayer, k_EvenFlag, i_Player2);
            placePlayerOnBoard(r_Size - rowsForEachPlayer, r_Size, k_EvenFlag, i_Player1);
        }

        public void InitializeBoard()
        {
            for (int rows = 0; rows < r_Size; rows++)
            {
                for (int cols = 0; cols < r_Size; cols++)
                {
                    m_GameBoard[rows, cols] = ePieceNumber.Empty;
                }
            }
        }

        private void placePlayerOnBoard(int i_RowStartPosition, int i_RowEndPosition, int i_EvenFlag, Player i_Player)
        {
            for (int rows = i_RowStartPosition; rows < i_RowEndPosition; rows++)
            {
                for (int cols = 0; cols < r_Size; cols++)
                {
                    if (cols % 2 == i_EvenFlag)
                    {
                        m_GameBoard[rows, cols] = (ePieceNumber)i_Player.CheckersNumber;
                        i_Player.CurrentNumOfPieces++;
                    }
                }

                i_EvenFlag = (i_EvenFlag == 0) ? (i_EvenFlag = 1) : (i_EvenFlag = 0);
            }
        }

        public eResponseFlag UpdateBoard(Move i_NextMove, Player i_Player, out Point enemyPoint, out bool isKing)
        {
            bool wasEaten, canEat;
            bool currentPieceIsKing1 = false;
            bool currentPieceIsKing2 = false;
            bool stepFlag = false;
            enemyPoint = new Point(-1, -1);
            eResponseFlag responseFlag = eResponseFlag.NoError;
            int enemyValue = 0;
            Point rightPoint, leftPoint;

            if (!CheckIfMoveIsInBoardLimit(i_NextMove.MoveTo))
            {
                responseFlag = eResponseFlag.InvalidMove;
            }

            else if (m_GameBoard[i_NextMove.MoveTo.X, i_NextMove.MoveTo.Y] != ePieceNumber.Empty)
            {
                responseFlag = eResponseFlag.InvalidMove;
            }

            if (m_GameBoard[i_NextMove.MoveFrom.X, i_NextMove.MoveFrom.Y] == ePieceNumber.Player1King)
            {
                currentPieceIsKing1 = true;
            }

            if (m_GameBoard[i_NextMove.MoveFrom.X, i_NextMove.MoveFrom.Y] == ePieceNumber.Player2King)
            {
                currentPieceIsKing2 = true;
            }

            isKing = currentPieceIsKing1 || currentPieceIsKing2;

            if (i_Player.CheckersNumber == (int)ePieceNumber.Player1 || currentPieceIsKing1 || currentPieceIsKing2) //player1
            {
                if (currentPieceIsKing1 || currentPieceIsKing2)
                {
                    canEat = CheckIfPlayer1OrKing2CanEat(out wasEaten, i_NextMove, out rightPoint, out leftPoint)
                             || CheckIfPlayer2OrKing1CanEat(out wasEaten, i_NextMove, out rightPoint, out leftPoint);
                }

                else
                {
                    canEat = CheckIfPlayer1OrKing2CanEat(out wasEaten, i_NextMove, out rightPoint, out leftPoint);
                }

                if (canEat)
                {
                    if (wasEaten)
                    {
                        if (currentPieceIsKing1)
                        {
                            m_GameBoard[i_NextMove.MoveTo.X, i_NextMove.MoveTo.Y] = ePieceNumber.Player1King;
                            stepFlag = true;
                        }

                        else if (currentPieceIsKing2)
                        {
                            m_GameBoard[i_NextMove.MoveTo.X, i_NextMove.MoveTo.Y] = ePieceNumber.Player2King;
                            stepFlag = true;
                        }

                        else
                        {
                            m_GameBoard[i_NextMove.MoveTo.X, i_NextMove.MoveTo.Y] = ePieceNumber.Player1;
                        }

                        m_GameBoard[i_NextMove.MoveFrom.X, i_NextMove.MoveFrom.Y] = ePieceNumber.Empty;
                        if (i_NextMove.MoveTo.Y - i_NextMove.MoveFrom.Y > 0)
                        {
                            if ((currentPieceIsKing1 || currentPieceIsKing2) &&
                               CheckIfMoveIsInBoardLimit(new Point(i_NextMove.MoveFrom.X + 1, i_NextMove.MoveFrom.Y + 1)))
                            {
                                enemyPoint = new Point(i_NextMove.MoveFrom.X + 1, i_NextMove.MoveFrom.Y + 1);
                                enemyValue = (int)m_GameBoard[enemyPoint.X, enemyPoint.Y];
                                m_GameBoard[enemyPoint.X, enemyPoint.Y] = ePieceNumber.Empty;
                            }

                            if (CheckIfMoveIsInBoardLimit(new Point(i_NextMove.MoveFrom.X - 1, i_NextMove.MoveFrom.Y + 1)))
                            {
                                enemyPoint = new Point(i_NextMove.MoveFrom.X - 1, i_NextMove.MoveFrom.Y + 1);
                                enemyValue = (int)m_GameBoard[enemyPoint.X, enemyPoint.Y];
                                m_GameBoard[enemyPoint.X, enemyPoint.Y] = ePieceNumber.Empty;
                            }
                        }

                        else
                        {
                            if ((currentPieceIsKing1 || currentPieceIsKing2) &&
                               CheckIfMoveIsInBoardLimit(new Point(i_NextMove.MoveFrom.X + 1, i_NextMove.MoveFrom.Y - 1)))
                            {
                                enemyPoint = new Point(i_NextMove.MoveFrom.X + 1, i_NextMove.MoveFrom.Y - 1);
                                enemyValue = (int)m_GameBoard[enemyPoint.X, enemyPoint.Y];
                                m_GameBoard[enemyPoint.X, enemyPoint.Y] = ePieceNumber.Empty;
                            }

                            if (CheckIfMoveIsInBoardLimit(new Point(i_NextMove.MoveFrom.X - 1, i_NextMove.MoveFrom.Y - 1)))
                            {
                                enemyPoint = new Point(i_NextMove.MoveFrom.X - 1, i_NextMove.MoveFrom.Y - 1);
                                enemyValue = (int)m_GameBoard[enemyPoint.X, enemyPoint.Y];
                                m_GameBoard[enemyPoint.X, enemyPoint.Y] = ePieceNumber.Empty;
                            }
                        }

                        i_Player.CurrentNumOfPieces--;
                        if (checkIfCanEatAgain(currentPieceIsKing1, currentPieceIsKing2, i_Player, i_NextMove.MoveTo))
                        {
                            responseFlag = eResponseFlag.CanEatAgain;
                        }
                    }

                    else
                    {
                        responseFlag = eResponseFlag.HaveToEatEnemy;
                    }
                }

                else
                {
                    if (checkIfAnyPieceCanEat(i_Player))
                    {
                        responseFlag = eResponseFlag.HaveToEatEnemy;
                    }

                    else if (i_NextMove.MoveTo.X == i_NextMove.MoveFrom.X - 1
                       && (i_NextMove.MoveTo.Y == i_NextMove.MoveFrom.Y + 1
                           || i_NextMove.MoveTo.Y == i_NextMove.MoveFrom.Y - 1))
                    {
                        if (currentPieceIsKing1)
                        {
                            m_GameBoard[i_NextMove.MoveTo.X, i_NextMove.MoveTo.Y] = ePieceNumber.Player1King;
                            stepFlag = true;
                        }

                        else if (currentPieceIsKing2)
                        {
                            m_GameBoard[i_NextMove.MoveTo.X, i_NextMove.MoveTo.Y] = ePieceNumber.Player2King;
                            stepFlag = true;
                        }

                        else
                        {
                            m_GameBoard[i_NextMove.MoveTo.X, i_NextMove.MoveTo.Y] = ePieceNumber.Player1;
                        }

                        m_GameBoard[i_NextMove.MoveFrom.X, i_NextMove.MoveFrom.Y] = ePieceNumber.Empty;

                    }

                    else
                    {
                        responseFlag = eResponseFlag.IncorrectGameRules;
                    }
                }
            }

            if (!stepFlag && ((currentPieceIsKing1 || currentPieceIsKing2) || i_Player.CheckersNumber == (int)ePieceNumber.Player2)) //player2 
            {
                responseFlag = eResponseFlag.NoError;

                if (currentPieceIsKing1 || currentPieceIsKing2)
                {
                    canEat = CheckIfPlayer2OrKing1CanEat(out wasEaten, i_NextMove, out rightPoint, out leftPoint) ||
                             CheckIfPlayer1OrKing2CanEat(out wasEaten, i_NextMove, out rightPoint, out leftPoint);
                }

                else
                {
                    canEat = CheckIfPlayer2OrKing1CanEat(out wasEaten, i_NextMove, out rightPoint, out leftPoint);
                }

                if (canEat)
                {
                    if (wasEaten)
                    {
                        if (currentPieceIsKing1)
                        {
                            m_GameBoard[i_NextMove.MoveTo.X, i_NextMove.MoveTo.Y] = ePieceNumber.Player1King;
                        }

                        else if (currentPieceIsKing2)
                        {
                            m_GameBoard[i_NextMove.MoveTo.X, i_NextMove.MoveTo.Y] = ePieceNumber.Player2King;
                        }

                        else
                        {
                            m_GameBoard[i_NextMove.MoveTo.X, i_NextMove.MoveTo.Y] = ePieceNumber.Player2;
                        }

                        m_GameBoard[i_NextMove.MoveFrom.X, i_NextMove.MoveFrom.Y] = ePieceNumber.Empty;
                        if (i_NextMove.MoveTo.Y - i_NextMove.MoveFrom.Y > 0)
                        {
                            if ((currentPieceIsKing1 || currentPieceIsKing2) &&
                                CheckIfMoveIsInBoardLimit(new Point(i_NextMove.MoveFrom.X - 1, i_NextMove.MoveFrom.Y + 1)))
                            {
                                enemyPoint = new Point(i_NextMove.MoveFrom.X - 1, i_NextMove.MoveFrom.Y + 1);
                                enemyValue = (int)m_GameBoard[enemyPoint.X, enemyPoint.Y];
                                m_GameBoard[enemyPoint.X, enemyPoint.Y] = ePieceNumber.Empty;
                            }

                            if (CheckIfMoveIsInBoardLimit(new Point(i_NextMove.MoveFrom.X + 1, i_NextMove.MoveFrom.Y + 1)))
                            {
                                enemyPoint = new Point(i_NextMove.MoveFrom.X + 1, i_NextMove.MoveFrom.Y + 1);
                                enemyValue = (int)m_GameBoard[enemyPoint.X, enemyPoint.Y];
                                m_GameBoard[enemyPoint.X, enemyPoint.Y] = ePieceNumber.Empty;
                            }
                        }

                        else
                        {
                            if ((currentPieceIsKing1 || currentPieceIsKing2) &&
                                CheckIfMoveIsInBoardLimit(new Point(i_NextMove.MoveFrom.X - 1, i_NextMove.MoveFrom.Y - 1)))
                            {
                                enemyPoint = new Point(i_NextMove.MoveFrom.X - 1, i_NextMove.MoveFrom.Y - 1);
                                enemyValue = (int)m_GameBoard[enemyPoint.X, enemyPoint.Y];
                                m_GameBoard[enemyPoint.X, enemyPoint.Y] = ePieceNumber.Empty;
                            }

                            if (CheckIfMoveIsInBoardLimit(new Point(i_NextMove.MoveFrom.X + 1, i_NextMove.MoveFrom.Y - 1)))
                            {
                                enemyPoint = new Point(i_NextMove.MoveFrom.X + 1, i_NextMove.MoveFrom.Y - 1);
                                enemyValue = (int)m_GameBoard[enemyPoint.X, enemyPoint.Y];
                                m_GameBoard[enemyPoint.X, enemyPoint.Y] = ePieceNumber.Empty;
                            }
                        }

                        i_Player.CurrentNumOfPieces--;
                        if (checkIfCanEatAgain(currentPieceIsKing1, currentPieceIsKing2, i_Player, i_NextMove.MoveTo))
                        {
                            responseFlag = eResponseFlag.CanEatAgain;
                        }
                    }

                    else
                    {
                        responseFlag = eResponseFlag.HaveToEatEnemy;
                    }
                }

                else
                {
                    if (checkIfAnyPieceCanEat(i_Player))
                    {
                        responseFlag = eResponseFlag.HaveToEatEnemy;
                    }

                    else if (i_NextMove.MoveTo.X == i_NextMove.MoveFrom.X + 1
                       && (i_NextMove.MoveTo.Y == i_NextMove.MoveFrom.Y + 1
                           || i_NextMove.MoveTo.Y == i_NextMove.MoveFrom.Y - 1))
                    {
                        if (currentPieceIsKing2)
                        {
                            m_GameBoard[i_NextMove.MoveTo.X, i_NextMove.MoveTo.Y] = ePieceNumber.Player2King;
                        }

                        else if (currentPieceIsKing1)
                        {
                            m_GameBoard[i_NextMove.MoveTo.X, i_NextMove.MoveTo.Y] = ePieceNumber.Player1King;
                        }

                        else
                        {
                            m_GameBoard[i_NextMove.MoveTo.X, i_NextMove.MoveTo.Y] = ePieceNumber.Player2;
                        }

                        m_GameBoard[i_NextMove.MoveFrom.X, i_NextMove.MoveFrom.Y] = ePieceNumber.Empty;
                    }

                    else
                    {
                        responseFlag = eResponseFlag.IncorrectGameRules;
                    }
                }
            }

            if (responseFlag == eResponseFlag.NoError || responseFlag == eResponseFlag.CanEatAgain)
            {
                if (checkIfKing(i_NextMove.MoveTo) && !currentPieceIsKing1 && !currentPieceIsKing2)
                {
                    if (m_GameBoard[i_NextMove.MoveTo.X, i_NextMove.MoveTo.Y] == ePieceNumber.Player1) //update player1 to be king
                    {
                        m_GameBoard[i_NextMove.MoveTo.X, i_NextMove.MoveTo.Y] = ePieceNumber.Player1King;
                    }

                    else if (m_GameBoard[i_NextMove.MoveTo.X, i_NextMove.MoveTo.Y] == ePieceNumber.Player2) //update player2 to be king
                    {
                        m_GameBoard[i_NextMove.MoveTo.X, i_NextMove.MoveTo.Y] = ePieceNumber.Player2King;
                    }

                    isKing = true;
                }
            }


            return responseFlag;
        }

        private bool checkIfAnyPieceCanEat(Player i_Player)
        {
            bool wasEaten;
            Point moveFromPoint;
            Point moveToPoint = new Point(-1, -1);
            Move possibleMove;
            for (int rows = 0; rows < r_Size; rows++)
            {
                for (int cols = 0; cols < r_Size; cols++)
                {
                    moveFromPoint = new Point(rows, cols);
                    possibleMove = new Move(moveFromPoint, moveToPoint);
                    if (i_Player.CheckersNumber == (int)ePieceNumber.Player1)
                    {
                        if (m_GameBoard[rows, cols] == ePieceNumber.Player1 || m_GameBoard[rows, cols] == ePieceNumber.Player1King)// player1
                        {
                            if (CheckIfPlayer1OrKing2CanEat(out wasEaten, possibleMove, out Point rightPoint, out Point leftPoint))
                            {
                                return true;
                            }
                        }

                        if (m_GameBoard[rows, cols] == ePieceNumber.Player1King)// king of player1
                        {
                            if (CheckIfPlayer2OrKing1CanEat(out wasEaten, possibleMove, out Point rightPoint, out Point leftPoint))
                            {
                                return true;
                            }
                        }
                    }

                    else // player2 or player2 king
                    {
                        if (m_GameBoard[rows, cols] == ePieceNumber.Player2 || m_GameBoard[rows, cols] == ePieceNumber.Player2King) //player2
                        {
                            if (CheckIfPlayer2OrKing1CanEat(out wasEaten, possibleMove, out Point rightPoint, out Point leftPoint))
                            {
                                return true;
                            }
                        }

                        if (m_GameBoard[rows, cols] == ePieceNumber.Player2King) //king of player2
                        {
                            if (CheckIfPlayer1OrKing2CanEat(out wasEaten, possibleMove, out Point rightPoint, out Point leftPoint))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public bool CheckIfPlayer1OrKing2CanEat(out bool i_WasEaten, Move i_NextMove, out Point i_RightPoint, out Point i_LeftPoint)
        {
            Point enemyPointLeft = new Point();
            Point enemyPointRight = new Point();
            Point emptyPointRight = new Point();
            Point emptyPointLeft = new Point();
            i_WasEaten = false;
            bool canEat = false;

            i_RightPoint = new Point(-1, -1);
            i_LeftPoint = new Point(-1, -1);
            enemyPointRight.X = i_NextMove.MoveFrom.X - 1;
            enemyPointRight.Y = i_NextMove.MoveFrom.Y + 1;
            emptyPointRight.X = i_NextMove.MoveFrom.X - 2;
            emptyPointRight.Y = i_NextMove.MoveFrom.Y + 2;

            if (CheckIfMoveIsInBoardLimit(enemyPointRight) &&
                CheckIfMoveIsInBoardLimit(emptyPointRight) && CheckIfMoveIsInBoardLimit(i_NextMove.MoveFrom))
            {
                if (m_GameBoard[i_NextMove.MoveFrom.X, i_NextMove.MoveFrom.Y] == ePieceNumber.Player2King &&
                    ((GetBoardPoint(enemyPointRight) == ePieceNumber.Player1 || GetBoardPoint(enemyPointRight) == ePieceNumber.Player1King) &&
                     GetBoardPoint(emptyPointRight) == ePieceNumber.Empty))
                {
                    canEat = true;
                    i_RightPoint = emptyPointRight;
                }

                else if (m_GameBoard[i_NextMove.MoveFrom.X, i_NextMove.MoveFrom.Y] != ePieceNumber.Player2King &&
                         (GetBoardPoint(enemyPointRight) == ePieceNumber.Player2 || GetBoardPoint(enemyPointRight) == ePieceNumber.Player2King) &&
                         GetBoardPoint(emptyPointRight) == ePieceNumber.Empty) //right
                {
                    canEat = true;
                    i_RightPoint = emptyPointRight;
                }
            }

            enemyPointLeft.X = i_NextMove.MoveFrom.X - 1;
            enemyPointLeft.Y = i_NextMove.MoveFrom.Y - 1;
            emptyPointLeft.X = i_NextMove.MoveFrom.X - 2;
            emptyPointLeft.Y = i_NextMove.MoveFrom.Y - 2;
            if (CheckIfMoveIsInBoardLimit(enemyPointLeft) && CheckIfMoveIsInBoardLimit(emptyPointLeft) && CheckIfMoveIsInBoardLimit(i_NextMove.MoveFrom))
            {
                if (m_GameBoard[i_NextMove.MoveFrom.X, i_NextMove.MoveFrom.Y] == ePieceNumber.Player2King &&
                    ((GetBoardPoint(enemyPointLeft) == ePieceNumber.Player1 || GetBoardPoint(enemyPointLeft) == ePieceNumber.Player1King) &&
                     GetBoardPoint(emptyPointLeft) == ePieceNumber.Empty))
                {
                    canEat = true;
                    i_LeftPoint = emptyPointLeft;
                }

                else if (m_GameBoard[i_NextMove.MoveFrom.X, i_NextMove.MoveFrom.Y] != ePieceNumber.Player2King &&
                         (GetBoardPoint(enemyPointLeft) == ePieceNumber.Player2 || GetBoardPoint(enemyPointLeft) == ePieceNumber.Player2King) &&
                         GetBoardPoint(emptyPointLeft) == ePieceNumber.Empty) //left
                {
                    canEat = true;
                    i_LeftPoint = emptyPointLeft;
                }
            }

            if (canEat)
            {
                enemyPointRight.X = i_NextMove.MoveFrom.X - 2;
                enemyPointRight.Y = i_NextMove.MoveFrom.Y + 2;

                enemyPointLeft.X = i_NextMove.MoveFrom.X - 2;
                enemyPointLeft.Y = i_NextMove.MoveFrom.Y - 2;

                //in case the player chose the step that can eat 
                if ((i_NextMove.MoveTo == enemyPointRight || i_NextMove.MoveTo == enemyPointLeft) && i_NextMove.MoveTo != new Point(-1, -1))
                {
                    i_WasEaten = true;
                }
            }

            return canEat;
        }

        public bool CheckIfPlayer2OrKing1CanEat(out bool i_WasEaten, Move i_NextMove, out Point i_RightPoint, out Point i_LeftPoint) // O  || KingX
        {
            Point enemyPointLeft = new Point();
            Point enemyPointRight = new Point();
            Point emptyPointRight = new Point();
            Point emptyPointLeft = new Point();
            i_RightPoint = new Point(-1, -1);
            i_LeftPoint = new Point(-1, -1);

            i_WasEaten = false;
            bool canEat = false;
            enemyPointRight.X = i_NextMove.MoveFrom.X + 1;
            enemyPointRight.Y = i_NextMove.MoveFrom.Y + 1;
            emptyPointRight.X = i_NextMove.MoveFrom.X + 2;
            emptyPointRight.Y = i_NextMove.MoveFrom.Y + 2;

            if (CheckIfMoveIsInBoardLimit(enemyPointRight) && CheckIfMoveIsInBoardLimit(emptyPointRight) && CheckIfMoveIsInBoardLimit(i_NextMove.MoveFrom))
            {
                if (m_GameBoard[i_NextMove.MoveFrom.X, i_NextMove.MoveFrom.Y] == ePieceNumber.Player1King &&
                    ((GetBoardPoint(enemyPointRight) == ePieceNumber.Player2 || GetBoardPoint(enemyPointRight) == ePieceNumber.Player2King) &&
                     GetBoardPoint(emptyPointRight) == ePieceNumber.Empty))
                {
                    canEat = true;
                    i_RightPoint = emptyPointRight;
                }

                else if (m_GameBoard[i_NextMove.MoveFrom.X, i_NextMove.MoveFrom.Y] != ePieceNumber.Player1King &&
                         (GetBoardPoint(enemyPointRight) == ePieceNumber.Player1 || GetBoardPoint(enemyPointRight) == ePieceNumber.Player1King) &&
                         GetBoardPoint(emptyPointRight) == ePieceNumber.Empty) //right
                {
                    canEat = true;
                    i_RightPoint = emptyPointRight;
                }
            }

            enemyPointLeft.X = i_NextMove.MoveFrom.X + 1;
            enemyPointLeft.Y = i_NextMove.MoveFrom.Y - 1;
            emptyPointLeft.X = i_NextMove.MoveFrom.X + 2;
            emptyPointLeft.Y = i_NextMove.MoveFrom.Y - 2;
            if (CheckIfMoveIsInBoardLimit(enemyPointLeft) && CheckIfMoveIsInBoardLimit(emptyPointLeft) && CheckIfMoveIsInBoardLimit(i_NextMove.MoveFrom))
            {
                if (m_GameBoard[i_NextMove.MoveFrom.X, i_NextMove.MoveFrom.Y] == ePieceNumber.Player1King &&
                   ((GetBoardPoint(enemyPointLeft) == ePieceNumber.Player2 || GetBoardPoint(enemyPointLeft) == ePieceNumber.Player2King) &&
                    GetBoardPoint(emptyPointLeft) == ePieceNumber.Empty))
                {
                    canEat = true;
                    i_LeftPoint = emptyPointLeft;
                }

                else if (m_GameBoard[i_NextMove.MoveFrom.X, i_NextMove.MoveFrom.Y] != ePieceNumber.Player1King &&
                         (GetBoardPoint(enemyPointLeft) == ePieceNumber.Player1 || GetBoardPoint(enemyPointLeft) == ePieceNumber.Player1King) &&
                         GetBoardPoint(emptyPointLeft) == ePieceNumber.Empty) //left
                {
                    canEat = true;
                    i_LeftPoint = emptyPointLeft;
                }
            }

            if (canEat)
            {
                enemyPointRight.X = i_NextMove.MoveFrom.X + 2;
                enemyPointRight.Y = i_NextMove.MoveFrom.Y + 2;
                enemyPointLeft.X = i_NextMove.MoveFrom.X + 2;
                enemyPointLeft.Y = i_NextMove.MoveFrom.Y - 2;

                //in case the player chose the step that can eat 
                if ((i_NextMove.MoveTo == enemyPointRight || i_NextMove.MoveTo == enemyPointLeft) && i_NextMove.MoveTo != new Point(-1, -1))
                {
                    i_WasEaten = true;
                }
            }

            return canEat;
        }

        public bool CheckIfMoveIsInBoardLimit(Point i_NextMovePoint)
        {
            if (i_NextMovePoint.X >= r_Size || i_NextMovePoint.X < 0 || i_NextMovePoint.Y >= r_Size
               || i_NextMovePoint.Y < 0)
            {
                return false;
            }

            return true;
        }

        private bool checkIfKing(Point i_NextMovePoint)
        {
            bool isKing = false;

            if (m_GameBoard[i_NextMovePoint.X, i_NextMovePoint.Y] == ePieceNumber.Player1 && i_NextMovePoint.X == (int)ePieceNumber.Empty)
            {
                isKing = true;
            }

            if (m_GameBoard[i_NextMovePoint.X, i_NextMovePoint.Y] == ePieceNumber.Player2 && i_NextMovePoint.X == r_Size - 1)
            {
                isKing = true;
            }

            return isKing;
        }

        private bool checkIfCanEatAgain(bool i_CurrentPieceIsKing1, bool i_CurrentPieceIsKing2, Player i_Player, Point i_NewStartPoint)
        {
            bool canEatAgain = false;
            bool wasEaten = false;
            Move nextMove = new Move(i_NewStartPoint, new Point(-1, -1));
            Point rightPoint, leftPoint;

            if (i_Player.CheckersNumber == (int)ePieceNumber.Player1 || i_CurrentPieceIsKing1 || i_CurrentPieceIsKing2)
            {

                if (i_CurrentPieceIsKing1 || i_CurrentPieceIsKing2)
                {
                    canEatAgain = CheckIfPlayer1OrKing2CanEat(out wasEaten, nextMove, out rightPoint, out leftPoint) ||
                                  CheckIfPlayer2OrKing1CanEat(out wasEaten, nextMove, out rightPoint, out leftPoint);
                }

                else
                {
                    canEatAgain = CheckIfPlayer1OrKing2CanEat(out wasEaten, nextMove, out rightPoint, out leftPoint);
                }
            }

            if (i_Player.CheckersNumber == (int)ePieceNumber.Player2 || i_CurrentPieceIsKing1 || i_CurrentPieceIsKing2)
            {
                if (i_CurrentPieceIsKing1 || i_CurrentPieceIsKing2)
                {
                    canEatAgain = CheckIfPlayer2OrKing1CanEat(out wasEaten, nextMove, out rightPoint, out leftPoint) ||
                                  CheckIfPlayer1OrKing2CanEat(out wasEaten, nextMove, out rightPoint, out leftPoint);
                }

                else
                {
                    canEatAgain = CheckIfPlayer2OrKing1CanEat(out wasEaten, nextMove, out rightPoint, out leftPoint);
                }
            }

            return canEatAgain;
        }

        public int CheckIfWin(Player i_Player1, Player i_Player2)
        {
            int statusFlag = 0;
            List<Move> player1PossibleMoves = i_Player1.CreatePossibleMovesList(this);
            List<Move> player2PossibleMoves = i_Player2.CreatePossibleMovesList(this);

            if (player2PossibleMoves.Count == 0) //Player1 won
            {
                statusFlag = 1;
            }

            else if (player1PossibleMoves.Count == 0) //Player2 won
            {
                statusFlag = 2;
            }

            else if (player1PossibleMoves.Count == 0 && player2PossibleMoves.Count == 0) // Tie
            {
                statusFlag = 3;
            }

            return statusFlag;
        }
    }
}

