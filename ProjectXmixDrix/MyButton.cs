using System.Windows.Forms;

namespace ProjectXmixDrix
{
    public class MyButton : Button
    {
        private static readonly int sr_ButtonSpacing = 5;
        private static readonly int sr_ButtonWidth = 70;
        private static readonly int sr_ButtonHeight = 70;

        public int ButtonSpacing
        {
            get
            {
                return sr_ButtonSpacing;
            }
        }

        public int ButtonWidth
        {
            get
            {
                return sr_ButtonWidth;
            }
        }

        public int ButtonHeight
        {
            get
            {
                return sr_ButtonHeight;
            }
        }

    }
}
