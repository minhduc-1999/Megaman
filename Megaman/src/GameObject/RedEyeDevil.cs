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
    public class RedEyeDevil : ParticularObject
    {

    private Animation forwardAnim, backAnim;

    private DateTime startTimeToShoot;

   // private AudioClip shooting;

    public RedEyeDevil(float x, float y, GameWorldState gameWorld) : base(x, y, 127, 89, 0, 100, gameWorld)
    {
       
        backAnim = CacheDataLoader.getInstance().getAnimation("redeye");
        forwardAnim = CacheDataLoader.getInstance().getAnimation("redeye");
        forwardAnim.flipAllImage();
        startTimeToShoot = DateTime.Now;
        setDamage(10);
        setTimeForNoBehurt(300);
        //shooting = CacheDataLoader.getInstance().getSound("redeyeshooting");
    }

    //@Override
    public override void attack(GameTime gameTime)
    {

        //shooting.play();
        Bullet bullet = new RedEyeBullet(getPosX(), getPosY(), getGameWorld());
        if (getDirection() == MainDir.LEFT_DIR) bullet.setSpeedX(-8);
        else bullet.setSpeedX(8);
        bullet.setTeamType(getTeamType());
        getGameWorld().bulletManager.addObject(bullet);

    }


    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (gameTime.GetTimeSpanMilis( startTimeToShoot) > 1000 )
        {
            attack(gameTime);
            //System.out.println("Red Eye attack");
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

   // @Override
    public override void draw(Graphics g2,GameTime gameTime)
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
                    backAnim.draw(g2,(int)(getPosX() - getGameWorld().camera.getPosX()),
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
