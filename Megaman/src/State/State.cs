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
    public abstract class State
    {
        protected GameTime gameTime;
        protected GamePanel gamePanel;

        public State(GamePanel gamePanel, GameTime time)
        {
            this.gamePanel = gamePanel;
            this.gameTime = time;
        }

        public abstract void Update();
        public abstract void Render(Graphics g2);
        public abstract Image getBufferedImage();

        public abstract void setPressedButton(Keys code);
        public abstract void setReleasedButton(Keys code);
    }
}
