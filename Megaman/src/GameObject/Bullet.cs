using Megaman.src.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Megaman.src.GameObject
{
    public abstract class Bullet : ParticularObject
    {

        public Bullet(float x, float y, float width, float height, float mass, int damage, GameWorldState gameWorld) : base(x, y, width, height, mass, 1, gameWorld)
        {
            setDamage(damage);
        }

        public override void draw(Graphics g2d, GameTime gameTime) { }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            setPosX(getPosX() + getSpeedX());
            setPosY(getPosY() + getSpeedY());
            ParticularObject obj = getGameWorld().particularObjectManager.getCollisionWidthEnemyObject(this);
            if (obj != null && obj.getState() == MainState.ALIVE)
            {
                setBlood(0);
                obj.beHurt(getDamage());
                //MessageBox.Show("Bullet set behurt for enemy");
            }
        }

    }
}
