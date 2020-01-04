using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Megaman.Character
{
    public abstract class Character
    {
        public Character()
        {

        }
        public virtual void LoadContent(Image src)
        { 
        }
        public abstract void Update();
        public abstract void Draw();
    }
}
