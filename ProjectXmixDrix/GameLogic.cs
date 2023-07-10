namespace ProjectXmixDrix
{
    public class GameLogic
    {
        private readonly Player r_Player1;
        private readonly Player r_Player2;
        private readonly int r_BoardSize;
        private LogicBoard m_Board;
        private int m_TurnCount = 0;

        public Player Player1
        {
            get
            {
                return r_Player1;
            }

        }

        public Player Player2
        {
            get
            {
                return r_Player2;
            }

        }

        public GameLogic(int i_Size, string i_Player1Name, string i_Player2Name)
        {
            r_BoardSize = i_Size;
            m_Board = new LogicBoard(r_BoardSize);
            r_Player1 = new Player(CellState.Player1, i_Player1Name);
            if (i_Player2Name == string.Empty)
            {
                r_Player2 = new Player(CellState.Player2, "PC");
            }
            else
            {
                r_Player2 = new Player(CellState.Player2, i_Player2Name);
            }

        }

        public void ClearBoardForNewRound()
        {
            m_Board = new LogicBoard(r_BoardSize);
        }

        public int TurnCount
        {
            get
            {
                return m_TurnCount;
            }

            set
            {
                m_TurnCount = value;
            }

        }

        public void MakeMove(CellState i_PlayerNum, Point i_ChoosenPoint, ref bool io_IsWinner, ref bool io_IsTie)
        {
            string message = string.Empty;
            if (m_Board.isValidPoint(i_ChoosenPoint, ref message))
            {
                m_Board.UpdateLogicBoard(i_ChoosenPoint, i_PlayerNum);
                m_TurnCount++;
            }

            if (IsGameWon(i_PlayerNum, i_ChoosenPoint))
            {
                if (i_PlayerNum == CellState.Player1)
                {
                    r_Player2.Score++;
                }
                else
                {
                    r_Player1.Score++;
                }

                io_IsWinner = true;
                //m_IsGameOver = true;
            }
            else if (IsGameTie())
            {
                r_Player1.Score++;
                r_Player2.Score++;
                io_IsTie = true;
                //m_IsGameOver = true;
            }

        }

        public string GetComputerMove() // AI SMART MOVE OF COMPUTER (IN THE BTTM OF THIS CLASS)
        {
            int[,] cellScores = new int[r_BoardSize, r_BoardSize];
            CalculateCellScores(cellScores);
            return GetBestMoveFromScores(cellScores);
        }

        public bool IsGameTie()
        {
            return m_TurnCount == r_BoardSize * r_BoardSize;
        }

        public bool IsGameWon(CellState i_Player, Point i_CurrentPoint)
        {
            bool isGameWon = false;
            isGameWon = isGameWon || checkWinInCol(i_Player, i_CurrentPoint) || checkWinInRow(i_Player, i_CurrentPoint)
                         || checkWinInDiagonal(i_Player, i_CurrentPoint) || checkWinIn2ndDiagonal(i_Player, i_CurrentPoint);
            return isGameWon;
        }

        private bool checkWinInCol(CellState i_Player, Point i_Point)
        {
            bool isGameWon = false;
            int countStreak = 0;

            if (m_TurnCount >= r_BoardSize)
            {
                foreach (int i in System.Linq.Enumerable.Range(0, r_BoardSize))
                {
                    if (m_Board.Board[i, i_Point.X] != i_Player)
                    {
                        break;
                    }
                    else
                    {
                        countStreak++;
                        if (countStreak == r_BoardSize)
                        {
                            isGameWon = true;
                        }

                    }

                }

            }

            return isGameWon;
        }

        private bool checkWinInRow(CellState i_Player, Point i_Point)
        {
            bool isGameWon = false;
            int countStreak = 0;

            if (m_TurnCount >= r_BoardSize)
            {
                foreach (int i in System.Linq.Enumerable.Range(0, r_BoardSize))
                {
                    if (m_Board.Board[i_Point.Y , i] != i_Player)
                    {
                        break;
                    }
                    else
                    {
                        countStreak++;
                        if (countStreak == r_BoardSize)
                        {
                            isGameWon = true;
                        }

                    }

                }

            }

            return isGameWon;
        }

        private bool checkWinInDiagonal(CellState i_Player, Point i_Point)
        {
            bool isGameWon = false;
            int countStreak = 0;

            if (m_TurnCount >= r_BoardSize && i_Point.Y == i_Point.X)
            {
                foreach (int i in System.Linq.Enumerable.Range(0, r_BoardSize))
                {
                    if (m_Board.Board[i, i] != i_Player)
                    {
                        break;
                    }
                    else
                    {
                        countStreak++;

                        if (countStreak == r_BoardSize)
                        {
                            isGameWon = true;
                        }

                    }

                }

            }

            return isGameWon;
        }

        private bool checkWinIn2ndDiagonal(CellState i_Player, Point i_Point)
        {
            bool isGameWon = false;
            int countStreak = 0;

            if (m_TurnCount >= r_BoardSize && i_Point.Y + i_Point.X + 1 == r_BoardSize)
            {
                foreach (int i in System.Linq.Enumerable.Range(0, r_BoardSize))
                {
                    int j = r_BoardSize - 1 - i;
                    if (m_Board.Board[i, j] != i_Player)
                    {
                        break;
                    }
                    else
                    {
                        countStreak++;
                        if (countStreak == r_BoardSize)
                        {
                            isGameWon = true;
                        }

                    }

                }

            }

            return isGameWon;
        }

        private string GetBestMoveFromScores(int[,] io_CellScores)
        {
            int maxScore = -(r_BoardSize * r_BoardSize);
            int maxRow = 0, maxCol = 0, rowNum = 0, colNum = -1;

            foreach (var cell in io_CellScores)
            {
                colNum++;
                if (colNum == r_BoardSize)
                {
                    colNum = 0;
                    rowNum++;
                }

                if (cell > maxScore && m_Board.Board[rowNum, colNum] == CellState.Empty)
                {
                    maxScore = cell;
                    maxRow = rowNum;
                    maxCol = colNum;
                }

            }

            string bestMove = $"{maxRow } {maxCol}";

            return bestMove;
        }

        // The main AI to calculate better computer move
        private void CalculateCellScores(int[,] io_CellScoresMatrix)
        {
            /*here we calculate the scores of each cell int the matrix with the same size of the game board when each
            cell represent the same cell number in the game board. the cell that will get the maximum value it will
            be the computer move*/

            // Iterate over the board and update the scores
            for (int row = 0; row < r_BoardSize; row++)
            {
                for (int col = 0; col < r_BoardSize; col++)
                {
                    if (m_Board.Board[row, col] != CellState.Empty)
                    {
                        /* with those bool param we check if in the same path that possible for sequence marks by both players
                        cus then it will be a good move for the computer (of course considering other scenarios that will
                        be count later int the function */
                        bool hasOtherPlayerMarkInRow = HasOtherPlayerMark(m_Board.Board[row, col], row, 0, 0, 1);
                        bool hasOtherPlayerMarkInColumn = HasOtherPlayerMark(m_Board.Board[row, col], 0, col, 1, 0);
                        bool hasOtherPlayerMarkInDiagonal = HasOtherPlayerMark(m_Board.Board[row, col], 0, 0, 1, 1);
                        bool hasOtherPlayerMarkInReverseDiagonal = HasOtherPlayerMark(m_Board.Board[row, col], 0, r_BoardSize - 1, 1, -1);
                        /* giving bad points for choose move in the same row or col or diagnoals of each player, 
                           if its the computer it will get -2 points if its the user it will get -1 point */
                        for (int i = 0; i < r_BoardSize; i++)
                        {
                            if (m_Board.Board[row, i] == CellState.Empty && !hasOtherPlayerMarkInRow)
                            {
                                if (m_Board.Board[row, col] == r_Player2.Mark)
                                {
                                    io_CellScoresMatrix[row, i] -= 2;
                                }
                                else
                                {
                                    io_CellScoresMatrix[row, i]--;
                                }

                            }
                            else if (m_Board.Board[row, i] == CellState.Empty)
                            {
                                io_CellScoresMatrix[row, i] = io_CellScoresMatrix[row, i];
                            }

                            if (m_Board.Board[i, col] == CellState.Empty && !hasOtherPlayerMarkInColumn)
                            {
                                if (m_Board.Board[row, col] == r_Player2.Mark)
                                {
                                    io_CellScoresMatrix[i, col] -= 2;
                                }
                                else
                                {
                                    io_CellScoresMatrix[i, col]--;
                                }

                            }
                            else if (m_Board.Board[i, col] == CellState.Empty)
                            {
                                continue;
                            }

                        }

                        if (row == col)
                        {
                            for (int i = 0; i < r_BoardSize; i++)
                            {
                                if (m_Board.Board[i, i] == CellState.Empty && !hasOtherPlayerMarkInDiagonal)
                                {
                                    if (m_Board.Board[row, col] == r_Player2.Mark)
                                    {
                                        io_CellScoresMatrix[i, i] -= 2;
                                    }
                                    else
                                    {
                                        io_CellScoresMatrix[i, i]--;
                                    }

                                }
                                else if (m_Board.Board[i, i] == CellState.Empty)
                                {
                                    continue;
                                }

                            }

                        }

                        if (row + col == r_BoardSize - 1)
                        {
                            for (int i = 0; i < r_BoardSize; i++)
                            {
                                if (m_Board.Board[i, r_BoardSize - 1 - i] == CellState.Empty && !hasOtherPlayerMarkInReverseDiagonal)
                                {
                                    if (m_Board.Board[row, col] == r_Player2.Mark)
                                    {
                                        io_CellScoresMatrix[i, r_BoardSize - 1 - i] -= 2;
                                    }
                                    else
                                    {
                                        io_CellScoresMatrix[i, r_BoardSize - 1 - i]--;
                                    }

                                }
                                else if (m_Board.Board[i, r_BoardSize - 1 - i] == CellState.Empty)
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool HasOtherPlayerMark(CellState i_CurrentPlayer, int i_StartRow, int i_StartCol, int i_RowIncrement, int i_ColIncrement)
        {
            for (int i = 0; i < r_BoardSize; i++)
            {
                int row = i_StartRow + (i * i_RowIncrement);
                int col = i_StartCol + (i * i_ColIncrement);

                if (m_Board.Board[row, col] != CellState.Empty && m_Board.Board[row, col] != i_CurrentPlayer)
                {
                    return true;
                }

            }

            return false;
        }
    }
}
