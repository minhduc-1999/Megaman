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
    public class DarkRaise : ParticularObject
    {

    private Animation forwardAnim, backAnim;

    private DateTime startTimeToShoot;
    private float x1, x2;

    public DarkRaise(float x, float y, GameWorldState gameWorld) : base(x, y, 127, 89, 0, 30, gameWorld)
    {
       
        backAnim = CacheDataLoader.getInstance().getAnimation("darkraise");
        forwardAnim = CacheDataLoader.getInstance().getAnimation("darkraise");
        forwardAnim.flipAllImage();
        startTimeToShoot = DateTime.Now;
        setTimeForNoBehurt(300000000);

        x1 = x - 100;
        x2 = x + 100;
        setSpeedX(1);

        setDamage(10);
    }


    public override void attack(GameTime gameTime)
    {

        float megaManX = getGameWorld().megaMan.getPosX();
        float megaManY = getGameWorld().megaMan.getPosY();

        float deltaX = megaManX - getPosX();
        float deltaY = megaManY - getPosY();

        float speed = 3;
        float a = Math.Abs(deltaX / deltaY);

        float speedX = (float)Math.Sqrt(speed * speed * a * a / (a * a + 1));
        float speedY = (float)Math.Sqrt(speed * speed / (a * a + 1));



        Bullet bullet = new DarkRaiseBullet(getPosX(), getPosY(), getGameWorld());

        if (deltaX < 0)
            bullet.setSpeedX(-speedX);
        else bullet.setSpeedX(speedX);
        bullet.setSpeedY(speedY);
        bullet.setTeamType(getTeamType());
        getGameWorld().bulletManager.addObject(bullet);

    }


    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (getPosX() < x1)
            setSpeedX(1);
        else if (getPosX() > x2)
            setSpeedX(-1);
        setPosX(getPosX() + getSpeedX());

        if (gameTime.GetTimeSpanMilis(startTimeToShoot) > 1000  * 1.5)
        {
            attack(gameTime);
            startTimeToShoot = DateTime.Now;
        }
    }

   
    public override Rectangle getBoundForCollisionWithEnemy()
    {
        Rectangle rect = getBoundForCollisionWithMap();
        rect.X += 20;
        rect.Width -= 40;

        return rect;
    }

    
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
                    backAnim.draw(g2,(int)(getPosX() - getGameWorld().camera.getPosX()),
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
