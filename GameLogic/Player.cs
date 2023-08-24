using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Collections.Generic;

namespace GameLogic
{
    public class Player
    {
        private string m_Name;
        private int m_Score;
        private readonly int r_CheckersNumber;
        private readonly int r_KingNumber;
        private int m_CurrentNumOfPieces;

        public Player(string i_Name, int i_CheckersNumber)
        {
            m_Name = i_Name;
            r_CheckersNumber = i_CheckersNumber;
            m_Score = 0;
            m_CurrentNumOfPieces = 0;
            r_KingNumber = r_CheckersNumber + 2;
        }

        public string Name
        {
            get
            {
                return m_Name;
            }

            set
            {
                m_Name = value;
            }
        }

        public int Score
        {
            get
            {
                return m_Score;
            }

            set
            {
                m_Score = value;
            }
        }

        public int CheckersNumber
        {
            get
            {
                return r_CheckersNumber;
            }
        }

        public int KingNumber
        {
            get
            {
                return r_KingNumber;
            }
        }

        public int CurrentNumOfPieces
        {
            get
            {
                return m_CurrentNumOfPieces;
            }

            set
            {
                m_CurrentNumOfPieces = value;
            }
        }

        public Move PickRandomComputerMove(Board i_GameBoard)
        {
            Random rndMove = new Random();
            List<Move> possibleComputerMoves = CreatePossibleMovesList(i_GameBoard);
            int moveIndex = rndMove.Next(possibleComputerMoves.Count);
            Move nextMove = possibleComputerMoves[moveIndex];
            return nextMove;
        }

        public List<Move> CreatePossibleMovesList(Board i_GameBoard)
        {
            Point moveFrom;
            List<Move> currentPlayerPossibleMoves = new List<Move>();
            for (int rows = 0; rows < i_GameBoard.Size; rows++)
            {
                for (int cols = 0; cols < i_GameBoard.Size; cols++)
                {
                    moveFrom = new Point(rows, cols);
                    if (r_CheckersNumber == (int)Board.ePieceNumber.Player1)
                    {
                        if (i_GameBoard.GetBoardPoint(moveFrom) == Board.ePieceNumber.Player1 ||
                            i_GameBoard.GetBoardPoint(moveFrom) == Board.ePieceNumber.Player1King)
                        {
                            addToListPossibleMovesOfPlayer1(moveFrom, currentPlayerPossibleMoves, i_GameBoard);
                        }

                        if (i_GameBoard.GetBoardPoint(moveFrom) == Board.ePieceNumber.Player1King)
                        {
                            addToListPossibleMovesOfPlayer2(moveFrom, currentPlayerPossibleMoves, i_GameBoard);
                        }
                    }

                    else
                    {
                        if (i_GameBoard.GetBoardPoint(moveFrom) == Board.ePieceNumber.Player2 ||
                            i_GameBoard.GetBoardPoint(moveFrom) == Board.ePieceNumber.Player2King)
                        {
                            addToListPossibleMovesOfPlayer2(moveFrom, currentPlayerPossibleMoves, i_GameBoard);
                        }

                        if (i_GameBoard.GetBoardPoint(moveFrom) == Board.ePieceNumber.Player2King)
                        {
                            addToListPossibleMovesOfPlayer1(moveFrom, currentPlayerPossibleMoves, i_GameBoard);
                        }
                    }
                }
            }

            return currentPlayerPossibleMoves;
        }

        private void addToListPossibleMovesOfPlayer1(Point i_MoveFrom, List<Move> i_PossibleMovesList, Board i_GameBoard)
        {
            Point eatingStepRight, eatingStepLeft;
            Move newPossibleMove;
            Point possibleNextPoint = new Point(i_MoveFrom.X - 1, i_MoveFrom.Y - 1);
            if (i_GameBoard.CheckIfMoveIsInBoardLimit(possibleNextPoint)) // Left
            {
                if (i_GameBoard.GetBoardPoint(possibleNextPoint) == Board.ePieceNumber.Empty)
                {
                    i_PossibleMovesList.Add(new Move(i_MoveFrom, possibleNextPoint));

                }
            }

            possibleNextPoint = new Point(i_MoveFrom.X - 1, i_MoveFrom.Y + 1);
            if (i_GameBoard.CheckIfMoveIsInBoardLimit(possibleNextPoint)) // Right
            {
                if (i_GameBoard.GetBoardPoint(possibleNextPoint) == Board.ePieceNumber.Empty)
                {
                    i_PossibleMovesList.Add(new Move(i_MoveFrom, possibleNextPoint));
                }
            }

            possibleNextPoint = new Point(i_MoveFrom.X, i_MoveFrom.Y);
            if (i_GameBoard.CheckIfPlayer1OrKing2CanEat(out bool wasEaten, new Move(possibleNextPoint, new Point(-1, -1)), out eatingStepRight, out eatingStepLeft))
            {
                if (eatingStepRight != new Point(-1, -1))
                {
                    newPossibleMove = new Move(i_MoveFrom, eatingStepRight);
                    i_PossibleMovesList.Add(newPossibleMove);
                }

                if (eatingStepLeft != new Point(-1, -1))
                {
                    newPossibleMove = new Move(i_MoveFrom, eatingStepLeft);
                    i_PossibleMovesList.Add(newPossibleMove);
                }
            }

        }

        private void addToListPossibleMovesOfPlayer2(Point i_MoveFrom, List<Move> i_PossibleMovesList, Board i_GameBoard)
        {
            Point eatingStepRight, eatingStepLeft;
            Move newPossibleMove;
            Point possibleNextPoint = new Point(i_MoveFrom.X + 1, i_MoveFrom.Y - 1);
            if (i_GameBoard.CheckIfMoveIsInBoardLimit(possibleNextPoint)) // Left
            {
                if (i_GameBoard.GetBoardPoint(possibleNextPoint) == 0)
                {
                    i_PossibleMovesList.Add(new Move(i_MoveFrom, possibleNextPoint));
                }
            }

            possibleNextPoint = new Point(i_MoveFrom.X + 1, i_MoveFrom.Y + 1);
            if (i_GameBoard.CheckIfMoveIsInBoardLimit(possibleNextPoint)) // Right
            {
                if (i_GameBoard.GetBoardPoint(possibleNextPoint) == 0)
                {
                    i_PossibleMovesList.Add(new Move(i_MoveFrom, possibleNextPoint));
                }
            }

            possibleNextPoint = new Point(i_MoveFrom.X, i_MoveFrom.Y);
            if (i_GameBoard.CheckIfPlayer2OrKing1CanEat(out bool wasEaten, new Move(possibleNextPoint, new Point(-1, -1)), out eatingStepRight, out eatingStepLeft))
            {
                if (eatingStepRight != new Point(-1, -1))
                {
                    newPossibleMove = new Move(i_MoveFrom, eatingStepRight);
                    i_PossibleMovesList.Add(newPossibleMove);
                }

                if (eatingStepLeft != new Point(-1, -1))
                {
                    newPossibleMove = new Move(i_MoveFrom, eatingStepLeft);
                    i_PossibleMovesList.Add(newPossibleMove);
                }
            }
        }

        public void CalcPlayersScore(Board i_GameBoard)
        {
            for (int rows = 0; rows < i_GameBoard.Size; rows++)
            {
                for (int cols = 0; cols < i_GameBoard.Size; cols++)
                {
                    if (i_GameBoard.GetBoardPoint(new Point(rows, cols)) == (Board.ePieceNumber)r_CheckersNumber)
                    {
                        m_Score++;
                    }

                    else if (i_GameBoard.GetBoardPoint(new Point(rows, cols)) == (Board.ePieceNumber)r_KingNumber)
                    {
                        m_Score = m_Score + 4;
                    }
                }
            }
        }
    }
}
