using Megaman.src.Control;
using Megaman.src.UserInterface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Button = Megaman.src.Control.Button;

namespace Megaman.src.State
{
    public class MenuState : State
    {


        public const int NUMBER_OF_BUTTON = 2;
        private Image bufferedImage;
        Graphics graphicsPaint;

        private Megaman.src.Control.Button[] buttons;
        private int buttonSelected = 0;
        private bool canContinueGame = false;

        public MenuState(GamePanel gamePanel,GameTime time) : base(gamePanel,time)
        {
            //bufferedImage = new Image(GameFrame.SCREEN_WIDTH, GameFrame.SCREEN_HEIGHT, BufferedImage.TYPE_INT_ARGB);
            buttons = new Button[NUMBER_OF_BUTTON];
            //buttons = new Megaman.src.Control.Button[NUMBER_OF_BUTTON]();
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
                    buttons[i].setState(Control.Button.PressType.HOVER);
                }
                else
                {
                    buttons[i].setState(Control.Button.PressType.NONE);
                }
            }
        }

        // @Override
        public override void Render(Graphics g2)
        {
            SolidBrush brush = new SolidBrush(Color.White);
            //if (bufferedImage == null)
            //{
            //    bufferedImage = new Image(GameFrame.SCREEN_WIDTH, GameFrame.SCREEN_HEIGHT, BufferedImage.TYPE_INT_ARGB);
            //    return;
            //}
            //graphicsPaint = bufferedImage.getGraphics();
            //if (graphicsPaint == null)
            //{
            //    graphicsPaint = bufferedImage.getGraphics();
            //    return;
            //}
            //graphicsPaint.setColor(Color.Cyan);
            brush.Color = Color.Cyan;
            //g2.FillRectangle(brush,0, 0, bufferedImage.Width, bufferedImage.Height);
            foreach (Button bt in buttons)
            {
                bt.draw(g2);
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
                    gamePanel.setState(new GameWorldState(gamePanel, gameTime));
                    break;

                case 1:
                    //System.exit(0);
                    Application.Exit();
                    break;
            }
        }
    }
}
