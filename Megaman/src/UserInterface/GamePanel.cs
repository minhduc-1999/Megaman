using Megaman.src.Effect;
using Megaman.src.State;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Megaman.src.State;
namespace Megaman.src.UserInterface
{
    public class GamePanel : Panel
    {
        Megaman.src.State.State gameState;
        BufferedGraphicsContext buffedContext;
        BufferedGraphics buffedGraphic;
        InputManager inputManager;

        private GameTime _gameTime;

        //Thread gameThread;
        DateTime curTime, preTime;
        //public bool isRunning = true;
        private bool run = true;
        public GamePanel() : base()
        {
            this.DoubleBuffered = true;
            buffedContext = BufferedGraphicsManager.Current;
            _gameTime = new GameTime();
            gameState = new GameWorldState(this, _gameTime);
            inputManager = new InputManager(gameState);
            _gameTime.Interval = 1000 / 80;
            _gameTime.Tick += _gameTime_Tick;
            _gameTime.Start();
        }
        private void _gameTime_Tick(object sender, EventArgs e)
        {
            if (run)
            {
                preTime = DateTime.Now;
                this.Update(_gameTime);
                this.Refresh();
                curTime = DateTime.Now;
                if ((curTime - preTime).TotalMilliseconds > _gameTime.Interval)
                    run = false;
            }
            else
            {
                if ((DateTime.Now - preTime).TotalMilliseconds > _gameTime.Interval)
                    run = true;
            }


        }
        protected override void OnPaint(PaintEventArgs e)
        {
            buffedGraphic = buffedContext.Allocate(e.Graphics, this.DisplayRectangle);
            gameState.Render(buffedGraphic.Graphics);
            e.Graphics.Clear(Color.White);
            buffedGraphic.Render();
        }

        public void Update(GameTime gameTime)
        {
            gameState.Update();
        }
        public void setState(Megaman.src.State.State state)
        {
            gameState = state;
            inputManager.setState(state);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            inputManager.setPressedButton(e.KeyCode);

        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            inputManager.setReleasedButton(e.KeyCode);
        }
        public void RaiseKeyDownEvent(KeyEventArgs e)
        {
            this.OnKeyDown(e);
        }
        public void RaiseKeyUpEvent(KeyEventArgs e)
        {
            this.OnKeyUp(e);
        }
    }
}
