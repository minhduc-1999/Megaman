using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Megaman.ResourcesManager;
namespace Megaman.Character
{
    public class Zero : Character, ICanAttack, ICanJump
    {
        private Megaman.Sprite.UserControlSprite _sprite;
        public Zero(Panel gameSreen, SpriteImage sprImage) : base()
        {
            _sprite = new Sprite.UserControlSprite(gameSreen, sprImage.Image, new Vector2(100, 100), sprImage.FrameSize, new Point(0, 0), sprImage.SheetSize, new Vector2(2, 0), sprImage.CollisionOffset);
        }
        public override void LoadContent(Image src)
        { 

        }
        public override void Update()
        {
            _sprite.Update();
        }
        public override void Draw()
        {
            _sprite.Draw();
        }
        public void Jump()
        {

        }
        public void Attack()
        {

        }
    }
}
