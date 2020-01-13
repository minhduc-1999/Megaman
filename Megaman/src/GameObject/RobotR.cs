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
    public class RobotR : ParticularObject
    {

    private Animation forwardAnim, backAnim;

    private DateTime startTimeToShoot;
    private float x1, x2, y1, y2;

    //private AudioClip shooting;

    public RobotR(float x, float y, GameWorldState gameWorld) : base(x, y, 127, 89, 0, 100, gameWorld)
    {
        backAnim = CacheDataLoader.getInstance().getAnimation("robotR");
        forwardAnim = CacheDataLoader.getInstance().getAnimation("robotR");
        forwardAnim.flipAllImage();
        startTimeToShoot = DateTime.Now;
        setTimeForNoBehurt(300);
        setDamage(10);

        x1 = x - 100;
        x2 = x + 100;
        y1 = y - 50;
        y2 = y + 50;

        setSpeedX(1);
        setSpeedY(1);

        //shooting = CacheDataLoader.getInstance().getSound("robotRshooting");
    }

    //@Override
    public override void attack(GameTime gameTime)
    {

        //shooting.play();
        Bullet bullet = new RobotRBullet(getPosX(), getPosY(), getGameWorld());

        if (getDirection() == MainDir.LEFT_DIR)
            bullet.setSpeedX(5);
        else bullet.setSpeedX(-5);
        bullet.setTeamType(getTeamType());
        getGameWorld().bulletManager.addObject(bullet);

    }


    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (getPosX() - getGameWorld().megaMan.getPosX() > 0) setDirection(MainDir.RIGHT_DIR);
        else setDirection(MainDir.LEFT_DIR);

        if (getPosX() < x1)
            setSpeedX(1);
        else if (getPosX() > x2)
            setSpeedX(-1);
        setPosX(getPosX() + getSpeedX());

        if (getPosY() < y1)
            setSpeedY(1);
        else if (getPosY() > y2)
            setSpeedY(-1);
        setPosY(getPosY() + getSpeedY());

        if (gameTime.GetTimeSpanMilis(startTimeToShoot )> 1000 * 1.5)
        {
            attack(gameTime);
            startTimeToShoot = DateTime.Now;
        }
    }

   // @Override
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
            if (getState() == MainState.NOBEHURT )
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
                    forwardAnim.draw(g2,(int)(getPosX() - getGameWorld().camera.getPosX()),
                            (int)(getPosY() - getGameWorld().camera.getPosY()));
                }
            }
        }
        //drawBoundForCollisionWithEnemy(g2);
    }

}
}
