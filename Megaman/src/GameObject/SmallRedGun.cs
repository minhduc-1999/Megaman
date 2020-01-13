using Megaman.src.Effect;
using Megaman.src.State;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Megaman.src.GameObject
{
    public class SmallRedGun : ParticularObject
    {

        private Animation forwardAnim, backAnim;

        private DateTime startTimeToShoot;

        public SmallRedGun(float x, float y, GameWorldState gameWorld) : base(x, y, 127, 89, 0, 30, gameWorld)
        {

            backAnim = CacheDataLoader.getInstance().getAnimation("smallredgun");
            forwardAnim = CacheDataLoader.getInstance().getAnimation("smallredgun");
            forwardAnim.flipAllImage();
            startTimeToShoot = DateTime.Now;
            setTimeForNoBehurt(300);
        }

        //@Override
        public override void attack(GameTime gameTime)
        {

            Bullet bullet = new YellowFlowerBullet(getPosX(), getPosY(), getGameWorld());
            bullet.setSpeedX(-3);
            bullet.setSpeedY(3);
            bullet.setTeamType(getTeamType());
            getGameWorld().bulletManager.addObject(bullet);

            bullet = new YellowFlowerBullet(getPosX(), getPosY(), getGameWorld());
            bullet.setSpeedX(3);
            bullet.setSpeedY(3);
            bullet.setTeamType(getTeamType());
            getGameWorld().bulletManager.addObject(bullet);
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (gameTime.GetTimeSpanMilis(startTimeToShoot) > 1000 * 2.0)
            {
                attack(gameTime);
                startTimeToShoot = DateTime.Now;
            }
        }

        //@Override
        public override Rectangle getBoundForCollisionWithEnemy()
        {
            Rectangle rect = getBoundForCollisionWithMap();
            rect.X += 20;
            rect.Width -= 40;

            return rect;
        }

        //@Override
        public override void draw(Graphics g2, GameTime gameTime)
        {
            if (!isObjectOutOfCameraView())
            {
                if (getState() == MainState.NOBEHURT)
                {
                    // plash...
                }
                else
                {
                    if (getDirection() == MainDir.LEFT_DIR)
                    {
                        backAnim.Update(gameTime);
                        backAnim.draw(g2, (int)(getPosX() - getGameWorld().camera.getPosX()),
                                (int)(getPosY() - getGameWorld().camera.getPosY()));
                    }
                    else
                    {
                        forwardAnim.Update(gameTime);
                        forwardAnim.draw(g2, (int)(getPosX() - getGameWorld().camera.getPosX()),
                                (int)(getPosY() - getGameWorld().camera.getPosY()));
                    }
                }
            }
            //drawBoundForCollisionWithEnemy(g2);
        }

    }

}
