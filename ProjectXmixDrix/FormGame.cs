using System;
using System.Windows.Forms;

namespace ProjectXmixDrix
{
    public class FormGame : Form
    {
        private readonly int r_Rows;
        private readonly int r_Cols;
        private readonly int r_GameMode;
        private GameLogic m_Game;
        private MyButton m_MyButton;
        private Button[,] m_Buttons;
        private Label m_LabelScores;
        private int m_CurrPlayer;
        

        public FormGame(int i_Rows, int i_Cols, int i_GameMode, string i_Player1Name, string i_Player2Name = "")
        {
            m_MyButton = new MyButton();
            this.r_Rows = i_Rows;
            this.r_Cols = i_Cols;
            this.r_GameMode = i_GameMode;
            this.m_CurrPlayer = 1;
            this.Text = "TicTacToeMisere";
            this.Width = i_Cols * (m_MyButton.ButtonWidth + m_MyButton.ButtonSpacing) + 30;  // Adjust the width based on the number of columns
            this.Height = i_Rows * (m_MyButton.ButtonHeight + m_MyButton.ButtonSpacing) + 90; // Adjust the height based on the number of rows
            if(r_GameMode == 1)
            {
                if(i_Player1Name == "")
                {
                    i_Player1Name = "Player1";
                }

                if (i_Player2Name == "")
                {
                    i_Player2Name = "Player2";
                }

                m_Game = new GameLogic(r_Rows, i_Player1Name, i_Player2Name);
            }
            else
            {
                if (i_Player1Name == "")
                {
                    i_Player1Name = "Player1";
                }

                m_Game = new GameLogic(r_Rows, i_Player1Name, i_Player2Name);
            }
            createGameBoardButtons();
            createLabelScore();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void createGameBoardButtons()
        {
            this.Controls.Clear();

            m_Buttons = new Button[r_Rows, r_Cols];

            for (int row = 0; row < r_Rows; row++)
            {
                for (int col = 0; col < r_Cols; col++)
                {
                    Button button = new Button();
                    button.Width = m_MyButton.ButtonWidth;
                    button.Height = m_MyButton.ButtonHeight;
                    button.Left = col * (m_MyButton.ButtonWidth + m_MyButton.ButtonSpacing) + 2 * m_MyButton.ButtonSpacing;
                    button.Top = row * (m_MyButton.ButtonHeight + m_MyButton.ButtonSpacing) + m_MyButton.ButtonSpacing;
                    button.Click += Button_Click;

                    m_Buttons[row, col] = button;
                    this.Controls.Add(button);
                }

            }

        }

        private void createLabelScore()
        {
            int scoreLocationFromTop = r_Rows * (m_MyButton.ButtonHeight + m_MyButton.ButtonSpacing) + 20;
            int scoreLocationFromLeft = r_Cols / 2 * (m_MyButton.ButtonWidth + m_MyButton.ButtonSpacing);
            m_LabelScores = new Label();
            m_LabelScores.Location = new System.Drawing.Point(scoreLocationFromLeft, scoreLocationFromTop); // Adjust the position as needed
            m_LabelScores.AutoSize = true;
            this.Controls.Add(m_LabelScores);
            updateLabelScore();
        }

        private void updateLabelScore()
        {
            string player1Name = m_Game.Player1.PlayerName;
            string plarer2Name = m_Game.Player2.PlayerName;
            int player1Score = m_Game.Player1.Score;
            int player2Score = m_Game.Player2.Score;
            if (r_GameMode == 1)
            {
                m_LabelScores.Text = $"{player1Name}: {player1Score}   |   {plarer2Name}: {player2Score}";
            }
            else
            {
                m_LabelScores.Text = $"{player1Name}: {player1Score}   |   {plarer2Name}: {player2Score}";
            }

        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            bool isWinner = false;
            bool isTie = false;
            int row = clickedButton.Top / (clickedButton.Height + m_MyButton.ButtonSpacing);
            int col = clickedButton.Left / (clickedButton.Width + m_MyButton.ButtonSpacing);
            Point point = new Point(col, row);
            if (m_CurrPlayer == 1)
            {
                m_Game.MakeMove(CellState.Player1, point, ref isWinner, ref isTie);
                clickedButton.Text = "X";
                winnerOrTieMessagesAndNewRoundOrClose(isWinner, isTie, m_Game.Player2);

            }
            else
            {
                m_Game.MakeMove(CellState.Player2, point, ref isWinner, ref isTie);
                clickedButton.Text = "O";
                winnerOrTieMessagesAndNewRoundOrClose(isWinner, isTie, m_Game.Player1);

            }

            if (!(isWinner || isTie))
            {
                clickedButton.Enabled = false;
                m_CurrPlayer = m_CurrPlayer == 1 ? 2 : 1;
            }

            if (r_GameMode == 2 && m_CurrPlayer == 2 && !isWinner && !isTie)
            {

                string pcMove = m_Game.GetComputerMove(); // Get the PC's move from the game logic
                point = convertStringNumToPoint(pcMove);
                Button pcButton = getButtonFromPoint(point);

                if (pcButton != null)
                {
                    m_Game.MakeMove(CellState.Player2, point, ref isWinner, ref isTie);
                    pcButton.Text = "O";
                    pcButton.Enabled = false;
                    m_CurrPlayer = 1; 
                    winnerOrTieMessagesAndNewRoundOrClose(isWinner, isTie, m_Game.Player1);

                }

            }

            if(isWinner || isTie)
            {
                m_CurrPlayer = 1;
            }

        }

        private void winnerOrTieMessagesAndNewRoundOrClose(bool i_IsWinner, bool i_IsTie, Player i_Player)
        {
            string message = string.Empty;
            DialogResult result = DialogResult.None;

            if (i_IsWinner)
            {
                message = $"The Winner is {i_Player.PlayerName}!{Environment.NewLine}Whould you like to play another round?";
                updateLabelScore();
                result = MessageBox.Show(message, "A Win!", MessageBoxButtons.YesNo);
            }

            if (i_IsTie)
            {
                message = $"Tie!{Environment.NewLine}Whould you like to play another round?";
                updateLabelScore();
                result = MessageBox.Show(message, "A Tie!", MessageBoxButtons.YesNo);
            }

            if (i_IsTie || i_IsWinner)
            {
                if (result == DialogResult.Yes)
                {
                    m_Game.ClearBoardForNewRound();
                    m_Game.TurnCount = 0;
                    createGameBoardButtons();
                    createLabelScore();
                }
                else
                {
                    this.Close();
                }

            }

        }

        private Point convertStringNumToPoint(string i_StringNum)
        {
            int row, column;
            string[] moveParts = i_StringNum.Split(' ');
            row = int.Parse(moveParts[0]);
            column = int.Parse(moveParts[1]);
            Point newPoint = new Point(column, row);
            return newPoint;
        }

        private Button getButtonFromPoint(Point point)
        {
            foreach (Button button in m_Buttons)
            {
                int row = button.Top / (button.Height + m_MyButton.ButtonSpacing);
                int col = button.Left / (button.Width + m_MyButton.ButtonSpacing);

                if (row == point.Y && col == point.X)
                {
                    return button;
                }

            }

            return null;
        }

    }

}

