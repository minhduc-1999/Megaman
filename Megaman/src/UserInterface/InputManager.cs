using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Megaman.src.UserInterface
{
    public class InputManager
    {

        private Megaman.src.State.State gameState;

        public InputManager(Megaman.src.State.State state)
        {
            this.gameState = state;
        }

        public void setState(Megaman.src.State.State state)
        {
            gameState = state;
        }

        public void setPressedButton(Keys code)
        {
            gameState.setPressedButton(code);
        }
        public void setReleasedButton(Keys code)
        {
            gameState.setReleasedButton(code);
        }

    }
}
