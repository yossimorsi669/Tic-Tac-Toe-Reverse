namespace ProjectXmixDrix
{
    public class Player
    {
        readonly string r_PlayerName;
        private CellState m_Mark;
        private int m_Score = 0;
        
        public string PlayerName
        {
            get
            {
                return r_PlayerName;
            }
     
        }

        public Player(CellState i_Mark, string i_PlayerName)
        {
            m_Mark = i_Mark;
            r_PlayerName = i_PlayerName;
        }

        public CellState Mark
        {
            get
            {
                return m_Mark;
            }

            set
            {
                m_Mark = value;
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

    }

}
