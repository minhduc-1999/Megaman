using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Megaman.src.Effect;
using System.IO;

namespace Megaman.src.UserInterface
{
    public class GameFrame : Form1
    {
        public static readonly int SCREEN_WIDTH = 1000;
        public static readonly int SCREEN_HEIGHT = 600;

        GamePanel gamePanel;

        public GameFrame():base()
        {
            this.Size = new Size(SCREEN_WIDTH, SCREEN_HEIGHT);
            this.StartPosition = FormStartPosition.CenterScreen;
            try
            {
                CacheDataLoader.getInstance().LoadData();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }

            gamePanel = new GamePanel();
            gamePanel.Dock = DockStyle.Fill;
            this.Controls.Add(gamePanel);
            gamePanel.Focus();
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            gamePanel.RaiseKeyUpEvent(e);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            gamePanel.RaiseKeyDownEvent(e);
        }
    }
}
