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
    public class RocketBullet : Bullet
    {


    private Animation forwardBulletAnimUp, forwardBulletAnimDown, forwardBulletAnim;
    private Animation backBulletAnimUp, backBulletAnimDown, backBulletAnim;

    private DateTime startTimeForChangeSpeedY;

    public RocketBullet(float x, float y, GameWorldState gameWorld):base(x, y, 30, 30, 1.0f, 10, gameWorld)
    {

        

        backBulletAnimUp = CacheDataLoader.getInstance().getAnimation("rocketUp");
        backBulletAnimDown = CacheDataLoader.getInstance().getAnimation("rocketDown");
        backBulletAnim = CacheDataLoader.getInstance().getAnimation("rocket");

        forwardBulletAnimUp = CacheDataLoader.getInstance().getAnimation("rocketUp");
        forwardBulletAnimUp.flipAllImage();
        forwardBulletAnimDown = CacheDataLoader.getInstance().getAnimation("rocketDown");
        forwardBulletAnimDown.flipAllImage();
        forwardBulletAnim = CacheDataLoader.getInstance().getAnimation("rocket");
        forwardBulletAnim.flipAllImage();

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
            if (getSpeedY() > 0)
            {
                forwardBulletAnimDown.draw(g2, (int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY());
            }
            else if (getSpeedY() < 0)
            {
                forwardBulletAnimUp.draw(g2, (int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY());
            }
            else
                forwardBulletAnim.draw(g2, (int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY());
        }
        else
        {
            if (getSpeedY() > 0)
            {
                backBulletAnimDown.draw(g2,(int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY());
            }
            else if (getSpeedY() < 0)
            {
                backBulletAnimUp.draw(g2,(int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY());
            }
            else
                backBulletAnim.draw(g2,(int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY());
        }
        //drawBoundForCollisionWithEnemy(g2);
    }

    private void changeSpeedY(GameTime gameTime)
    {
        if (gameTime.GetTimeSpanMilis(DateTime.Now) % 3 == 0)
        {
            setSpeedY(getSpeedX());
        }
        else if (gameTime.GetTimeSpanMilis(DateTime.Now) == 1)
        {
            setSpeedY(-getSpeedX());
        }
        else
        {
            setSpeedY(0);

        }
    }

    //@Override
    public override void Update(GameTime gameTime)
    {
        // TODO Auto-generated method stub
        base.Update(gameTime);

        if (gameTime.GetTimeSpanMilis( startTimeForChangeSpeedY) > 500)
        {
            startTimeForChangeSpeedY = DateTime.Now;
            changeSpeedY(gameTime);
        }
    }

    //@Override
    public override void attack(GameTime gameTime) { }

}

}
