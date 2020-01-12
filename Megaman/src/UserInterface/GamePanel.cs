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

        public bool isRunning = true;

        public GamePanel() : base()
        {
            //
            buffedContext = BufferedGraphicsManager.Current;
            _gameTime = new GameTime();
            gameState = new GameWorldState(this, _gameTime);
            inputManager = new InputManager(gameState);
            _gameTime.Interval = 50;
            _gameTime.Tick += _gameTime_Tick;
            _gameTime.Start();
        }
        private void _gameTime_Tick(object sender, EventArgs e)
        {
            this.Update(_gameTime);
            this.Draw();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            buffedGraphic = buffedContext.Allocate(e.Graphics, this.DisplayRectangle);
            buffedGraphic.Graphics.Clear(Color.White);
            gameState.Render(buffedGraphic.Graphics);
            buffedGraphic.Render();
        }
     
        public void Update(GameTime gameTime)
        {
            gameState.Update();
        }
        public void Draw()
        {
            this.Invalidate();
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
