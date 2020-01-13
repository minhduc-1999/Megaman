using Megaman.src.Effect;
using Megaman.src.State;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Megaman.src.GameObject
{
    public class BlueFire : Bullet
    {


        private Animation forwardBulletAnim, backBulletAnim;

        public BlueFire(float x, float y, GameWorldState gameWorld) : base(x, y, 60, 30, 1.0f, 10, gameWorld)
        {

            forwardBulletAnim = CacheDataLoader.getInstance().getAnimation("bluefire");
            backBulletAnim = CacheDataLoader.getInstance().getAnimation("bluefire");
            backBulletAnim.flipAllImage();
        }



        //@Override
        public override Rectangle getBoundForCollisionWithEnemy()
        {
            // TODO Auto-generated method stub
            return getBoundForCollisionWithMap();
        }

        //@Override
        public override void draw(Graphics g2, GameTime gameTime)
        {
            // TODO Auto-generated method stub
            if (getSpeedX() > 0)
            {
                if (!forwardBulletAnim.isIgnoreFrame(0) && forwardBulletAnim.getCurrentFrame() == 3)
                {
                    forwardBulletAnim.setIgnoreFrame(0);
                    forwardBulletAnim.setIgnoreFrame(1);
                    forwardBulletAnim.setIgnoreFrame(2);
                }

                forwardBulletAnim.Update(gameTime);
                forwardBulletAnim.draw(g2,(int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY());
            }
            else
            {
                if (!backBulletAnim.isIgnoreFrame(0) && backBulletAnim.getCurrentFrame() == 3)
                {
                    backBulletAnim.setIgnoreFrame(0);
                    backBulletAnim.setIgnoreFrame(1);
                    backBulletAnim.setIgnoreFrame(2);
                }
                backBulletAnim.Update(gameTime);
                backBulletAnim.draw(g2, (int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY());
            }
            //drawBoundForCollisionWithEnemy(g2);
        }

        //@Override
        public override void Update(GameTime gameTime)
        {
            // TODO Auto-generated method stub
            if (forwardBulletAnim.isIgnoreFrame(0) || backBulletAnim.isIgnoreFrame(0))
                setPosX(getPosX() + getSpeedX());
            ParticularObject obj = getGameWorld().particularObjectManager.getCollisionWidthEnemyObject(this);
            if (obj != null && obj.getState() == MainState.ALIVE)
            {
                setBlood(0);
                obj.setBlood(obj.getBlood() - getDamage());
                obj.setState(MainState.BEHURT);
               // MessageBox.Show("Bullet set behurt for enemy");
            }
        }

        //@Override
        public override void attack(GameTime gameTime) { }

    }
}
