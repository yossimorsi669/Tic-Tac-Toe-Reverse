using System;
using System.Windows.Forms;

namespace ProjectXmixDrix
{
    public class FormLogin : Form
    {
        private TextBox m_TextBoxPlayer1;
        private TextBox m_TextBoxPlayer2;
        private NumericUpDown m_NumericUpDownRows;
        private NumericUpDown m_NumericUpDownCols;
        private Button m_ButtonStartGame;
        private Label m_LabelPlayers;
        private Label m_LabelPlayer1;
        private CheckBox m_CheckBoxPlayer2;
        private Label m_LabelPlayer2;
        private Label m_LabelBoardSize;
        private Label m_LabelRows;
        private Label m_LabelCols;
        
        public TextBox TextBoxPlayer1
        {
            get { return m_TextBoxPlayer1; }
            set { m_TextBoxPlayer1 = value; }
        }

        public TextBox TextBoxPlayer2
        {
            get { return m_TextBoxPlayer2; }
            set { m_TextBoxPlayer2 = value; }
        }

        public NumericUpDown NumericUpDownRows
        {
            get { return m_NumericUpDownRows; }
            set { m_NumericUpDownRows = value; }
        }

        public NumericUpDown NumericUpDownCols
        {
            get { return m_NumericUpDownCols; }
            set { m_NumericUpDownCols = value; }
        }

        public CheckBox CheckBoxPlayer2
        {
            get { return m_CheckBoxPlayer2; }
            set { m_CheckBoxPlayer2 = value; }
        }

        public FormLogin()
        {
            initializeComponents();
        }
        
        private void initializeComponents()
        {
            this.Text = "Login";
            this.Width = 400;
            this.Height = 350;

            int xMargin = 20;
            int yMargin = 20;
            int controlWidth = 100;
            int controlHeight = 20;
            int verticalSpacing = 30;

            m_LabelPlayers = new Label();
            m_LabelPlayers.Text = "Players:";
            m_LabelPlayers.Location = new System.Drawing.Point(xMargin, yMargin);

            m_LabelPlayer1 = new Label();
            m_LabelPlayer1.Text = "Player 1:";
            m_LabelPlayer1.Location = new System.Drawing.Point(xMargin, yMargin + verticalSpacing);

            m_TextBoxPlayer1 = new TextBox();
            m_TextBoxPlayer1.Location = new System.Drawing.Point(xMargin + controlWidth, yMargin + verticalSpacing);

            m_CheckBoxPlayer2 = new CheckBox();
            m_CheckBoxPlayer2.Text = "Enable Player 2";
            m_CheckBoxPlayer2.Location = new System.Drawing.Point(xMargin, yMargin + 2 * verticalSpacing);
            m_CheckBoxPlayer2.CheckedChanged += CheckBoxPlayer2_CheckedChanged;

            m_LabelPlayer2 = new Label();
            m_LabelPlayer2.Text = "Player 2:";
            m_LabelPlayer2.Location = new System.Drawing.Point(xMargin, yMargin + 3 * verticalSpacing);

            m_TextBoxPlayer2 = new TextBox();
            m_TextBoxPlayer2.Enabled = false;
            m_TextBoxPlayer2.Location = new System.Drawing.Point(xMargin + controlWidth, yMargin + 3 * verticalSpacing);

            m_LabelBoardSize = new Label();
            m_LabelBoardSize.Text = "Board Size:";
            m_LabelBoardSize.Location = new System.Drawing.Point(xMargin, yMargin + 5 * verticalSpacing);

            m_LabelRows = new Label();
            m_LabelRows.Text = "Rows:";
            m_LabelRows.Location = new System.Drawing.Point(xMargin, yMargin + 6 * verticalSpacing);

            m_NumericUpDownRows = new NumericUpDown();
            m_NumericUpDownRows.Minimum = 4;
            m_NumericUpDownRows.Maximum = 10;
            m_NumericUpDownRows.Location = new System.Drawing.Point(xMargin + controlWidth, yMargin + 6 * verticalSpacing);
            m_NumericUpDownRows.Width = controlWidth / 2; // Adjust the width of the NumericUpDown control
            m_NumericUpDownRows.ValueChanged += NumericUpDownRows_ValueChanged;

            m_LabelCols = new Label();
            m_LabelCols.Text = "Cols:";
            m_LabelCols.Location = new System.Drawing.Point(xMargin + controlWidth + m_NumericUpDownRows.Width + 10, yMargin + 6 * verticalSpacing);

            m_NumericUpDownCols = new NumericUpDown();
            m_NumericUpDownCols.Minimum = 4;
            m_NumericUpDownCols.Maximum = 10;
            m_NumericUpDownCols.Location = new System.Drawing.Point(xMargin + controlWidth + m_NumericUpDownRows.Width + 10 + m_LabelCols.Width, yMargin + 6 * verticalSpacing);
            m_NumericUpDownCols.Width = controlWidth / 2; // Adjust the width of the NumericUpDown control
            m_NumericUpDownCols.ValueChanged += NumericUpDownCols_ValueChanged;

            m_ButtonStartGame = new Button();
            m_ButtonStartGame.Text = "Start Game!";
            m_ButtonStartGame.Location = new System.Drawing.Point(xMargin + controlWidth + m_NumericUpDownRows.Width + 10, yMargin + 8 * verticalSpacing);
            m_ButtonStartGame.Click += ButtonStartGame_Click;

            this.Controls.Add(m_LabelPlayers);
            this.Controls.Add(m_LabelPlayer1);
            this.Controls.Add(m_TextBoxPlayer1);
            this.Controls.Add(m_CheckBoxPlayer2);
            this.Controls.Add(m_LabelPlayer2);
            this.Controls.Add(m_TextBoxPlayer2);
            this.Controls.Add(m_LabelBoardSize);
            this.Controls.Add(m_LabelRows);
            this.Controls.Add(m_NumericUpDownRows);
            this.Controls.Add(m_LabelCols);
            this.Controls.Add(m_NumericUpDownCols);
            this.Controls.Add(m_ButtonStartGame);
        }


        private void NumericUpDownRows_ValueChanged(object sender, EventArgs e)
        {
            m_NumericUpDownCols.Value = m_NumericUpDownRows.Value;
        }

        private void NumericUpDownCols_ValueChanged(object sender, EventArgs e)
        {
            m_NumericUpDownRows.Value = m_NumericUpDownCols.Value;
        }

        private void CheckBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            if (m_CheckBoxPlayer2.Checked) // Against another player
            {
                m_TextBoxPlayer2.Enabled = true;
            }
            else // Against CP
            {
                m_TextBoxPlayer2.Enabled = false;
            }

        }

        private void ButtonStartGame_Click(object sender,  EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }

}
