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
    public class GameFrame : Form
    {

        public static readonly int SCREEN_WIDTH = 1000;
        public static readonly int SCREEN_HEIGHT = 600;

        GamePanel gamePanel;

        public GameFrame():base()
        {
            //Toolkit toolkit = this.getToolkit();
            //Dimension solution = toolkit.getScreenSize();
            this.Size = new Size(SCREEN_WIDTH, SCREEN_HEIGHT);
            try
            {
                CacheDataLoader.getInstance().LoadData();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }

            //this.setBounds((solution.width - SCREEN_WIDTH) / 2, (solution.height - SCREEN_HEIGHT) / 2, SCREEN_WIDTH, SCREEN_HEIGHT);


            gamePanel = new GamePanel();
            gamePanel.Dock = DockStyle.Fill;
            //addKeyListener(gamePanel);
            this.Controls.Add(gamePanel);
            //add(gamePanel);
        }
        //public void startGame()
        //{

        //    gamePanel.startGame();
        //    this.Visible = true;

        //}

        //public static void main(String arg[])
        //{

        //    GameFrame gameFrame = new GameFrame();
        //    gameFrame.startGame();

        //}

    }
}
