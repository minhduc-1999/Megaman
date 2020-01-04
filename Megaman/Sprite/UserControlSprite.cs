using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;
using System.Windows.Forms;

namespace Megaman.Sprite
{
    public class UserControlSprite : Sprite
    {
        public UserControlSprite(Control screen,Image image, Vector2 pos, Point frameSize, Point curFrame, Point sheetSize, Vector2 speed, int collisOffset)
            : base(screen, image, pos, frameSize, curFrame, sheetSize, speed, collisOffset)
        {
        }
        public override void Update()
        {
            base.Update();
        }
        public override void Draw()
        {
            base.Draw();
        }
    }
}
