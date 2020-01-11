using Megaman.src.UserInterface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Megaman.src.State
{
    public class MenuState : State
    {


        public const int NUMBER_OF_BUTTON = 2;
        private Image bufferedImage;
        Graphics graphicsPaint;

        private Button[] buttons;
        private int buttonSelected = 0;
        private bool canContinueGame = false;

        public MenuState(GamePanel gamePanel) : base(gamePanel)
        {
            bufferedImage = new Image(GameFrame.SCREEN_WIDTH, GameFrame.SCREEN_HEIGHT, BufferedImage.TYPE_INT_ARGB);

            buttons = new Button[NUMBER_OF_BUTTON];
            buttons[0] = new RectangleButton("NEW GAME", 300, 100, 100, 40, 15, 25, Color.Orange);
            buttons[0].setHoverBgColor(Color.Blue);
            buttons[0].setPressedBgColor(Color.Green);

            //		buttons[1] = new RectangleButton("CONTINUE", 300, 160, 100, 40, 15, 25, Color.ORANGE);
            //		buttons[1].setHoverBgColor(Color.BLUE);
            //		buttons[1].setPressedBgColor(Color.GREEN);


            buttons[1] = new RectangleButton("EXIT", 300, 160, 100, 40, 15, 25, Color.Orange);
            buttons[1].setHoverBgColor(Color.Blue);
            buttons[1].setPressedBgColor(Color.Green);
        }

        //@Override
        public override void Update()
        {
            for (int i = 0; i < NUMBER_OF_BUTTON; i++)
            {
                if (i == buttonSelected)
                {
                    buttons[i].setState(Button.HOVER);
                }
                else
                {
                    buttons[i].setState(Button.NONE);
                }
            }
        }

        // @Override
        public override void Render()
        {
            if (bufferedImage == null)
            {
                bufferedImage = new Image(GameFrame.SCREEN_WIDTH, GameFrame.SCREEN_HEIGHT, BufferedImage.TYPE_INT_ARGB);
                return;
            }
            graphicsPaint = bufferedImage.getGraphics();
            if (graphicsPaint == null)
            {
                graphicsPaint = bufferedImage.getGraphics();
                return;
            }
            graphicsPaint.setColor(Color.Cyan);
            graphicsPaint.fillRect(0, 0, bufferedImage.getWidth(), bufferedImage.getHeight());
            foreach (Button bt in buttons)
            {
                bt.draw(graphicsPaint);
            }
        }

        //@Override
        public override Image getBufferedImage()
        {
            return bufferedImage;
        }

        //@Override
        public override void setPressedButton(Keys code)
        {
            switch (code)
            {
                case Keys.Down:
                    buttonSelected++;
                    if (buttonSelected >= NUMBER_OF_BUTTON)
                    {
                        buttonSelected = 0;
                    }
                    break;
                case Keys.Up:
                    buttonSelected--;
                    if (buttonSelected < 0)
                    {
                        buttonSelected = NUMBER_OF_BUTTON - 1;
                    }
                    break;
                case Keys.Enter:
                    actionMenu();
                    break;
            }
        }

        //@Override
        public override void setReleasedButton(Keys code) { }

        private void actionMenu()
        {
            switch (buttonSelected)
            {
                case 0:
                    gamePanel.setState(new GameWorldState(gamePanel));
                    break;

                case 1:
                    //System.exit(0);
                    Application.Exit();
                    break;
            }
        }
    }
}
