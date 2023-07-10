using System;
using System.Windows.Forms;

namespace ProjectXmixDrix
{
    public class Program
    {
        public static void Main()
        {
            FormLogin login = new FormLogin();
            login.ShowDialog();

            if (login.DialogResult == DialogResult.OK)
            {
                int rows = Convert.ToInt32(login.NumericUpDownRows.Value);
                int cols = Convert.ToInt32(login.NumericUpDownCols.Value);
              
                int gameMode = login.CheckBoxPlayer2.Checked ? 1 : 2;

                FormGame gameForm = new FormGame(rows, cols, gameMode, login.TextBoxPlayer1.Text, login.TextBoxPlayer2.Text);
                gameForm.ShowDialog();
            }

        }

    }

}
