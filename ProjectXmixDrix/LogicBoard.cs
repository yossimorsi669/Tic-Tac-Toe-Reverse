namespace ProjectXmixDrix
{
    public class LogicBoard
    {
        private readonly int r_BoardSize;
        private CellState[,] m_Board;

        public LogicBoard(int i_Size)
        {
            r_BoardSize = i_Size;
            m_Board = new CellState[i_Size, i_Size];
            InitLogicBoard();
        }

        public CellState[,] Board
        {
            get
            {
                return m_Board;
            }
        }

        public bool isValidPoint(Point i_ChoosenPoint, ref string io_Message)
        {
             return checkIfPointInBoardIsTaken(i_ChoosenPoint, ref io_Message);
        }

        private bool checkIfPointInBoardIsTaken(Point i_ChoosenPoint, ref string io_Message)
        {
            bool isSuccessful = true;
            if (m_Board[i_ChoosenPoint.Y , i_ChoosenPoint.X] != CellState.Empty)
            {
                io_Message = "This spot is already taken";
                isSuccessful = false;
            }

            return isSuccessful;
        }

        public void InitLogicBoard()
        {
            foreach (int row in System.Linq.Enumerable.Range(0, r_BoardSize))
            {
                foreach (int col in System.Linq.Enumerable.Range(0, r_BoardSize))
                {
                    m_Board[row, col] = CellState.Empty;
                }

            }

        }

        public void UpdateLogicBoard(Point cell, CellState whichPlayer)
        {
            m_Board[cell.Y , cell.X] = whichPlayer;
        }

    }

}
