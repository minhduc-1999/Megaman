using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Megaman.src.State;
namespace Megaman.src.UserInterface
{
    public class GamePanel : Panel
    {
        Graphics _gameGraphic;
        //Megaman.src.State.State gameState;

        InputManager inputManager;

        private GameTime _gameTime;

        //Thread gameThread;

        public bool isRunning = true;

        public GamePanel():base()
        {
            _gameGraphic = this.CreateGraphics();
            //gameState = new MenuState(this);

            //inputManager = new InputManager(gameState);
            _gameTime = new GameTime();
            _gameTime.Interval = 10;
            _gameTime.Tick += _gameTime_Tick;
        }

        private void _gameTime_Tick(object sender, EventArgs e)
        {
            this.Update(_gameTime);
            this.Draw(_gameGraphic);
        }

        //public void startGame()
        //{
        //    gameThread = new Thread(this);
        //    gameThread.start();
        //}
        //int a = 0;
        //@Override
        //public void run()
        //{

            //long previousTime = System.nanoTime();
            //long currentTime;
            //long sleepTime;

            //long period = 1000000000 / 80;

            //while (isRunning)
            //{

               
               


                //repaint();
                //this.Refresh();

                //    currentTime = System.nanoTime();
                //    sleepTime = period - (currentTime - previousTime);
                //    try
                //    {

                //        if (sleepTime > 0)
                //            Thread.sleep(sleepTime / 1000000);
                //        else Thread.sleep(period / 2000000);

                //    }
                //    catch (Exception e) { }

                //    previousTime = System.nanoTime();
                //}

            //}
        //}

        //public void paint(Graphics g)
        //{

        //    g.drawImage(gameState.getBufferedImage(), 0, 0, this);

        //}
        public void Update(GameTime gameTime)
        {
            //gameState.Update();
        }
        public void Draw(Graphics g)
        {

           // gameState.Render();

        }

        //// @Override
        //public void keyTyped(KeyEvent e) { }

        ////@Override
        //public void keyPressed(KeyEvent e)
        //{
        //    inputManager.setPressedButton(e.getKeyCode());
        //}
        //// @Override
        //public void keyReleased(KeyEvent e)
        //{
        //    inputManager.setReleasedButton(e.getKeyCode());
        //}

        //public void setState(Megaman.src.State.State state)
        //{
        //    gameState = state;
        //    inputManager.setState(state);
        //}
        protected override void OnKeyDown(KeyEventArgs e)
        {
            inputManager.setPressedButton(e.KeyCode);
            base.OnKeyDown(e);
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            inputManager.setReleasedButton(e.KeyCode);
            base.OnKeyUp(e);
        }

    }
}
