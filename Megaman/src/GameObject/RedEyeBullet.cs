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
    public class RedEyeBullet : Bullet
    {


    private Animation forwardBulletAnim, backBulletAnim;

    public RedEyeBullet(float x, float y, GameWorldState gameWorld):  base(x, y, 30, 30, 1.0f, 10, gameWorld)
    {
      
        forwardBulletAnim = CacheDataLoader.getInstance().getAnimation("redeyebullet");
        backBulletAnim = CacheDataLoader.getInstance().getAnimation("redeyebullet");
        backBulletAnim.flipAllImage();
    }



    //@Override
    public override Rectangle getBoundForCollisionWithEnemy()
    {
        // TODO Auto-generated method stub
        return getBoundForCollisionWithMap();
    }

    //@Override
    public override void draw(Graphics g2,GameTime gameTime)
    {
        // TODO Auto-generated method stub
        if (getSpeedX() > 0)
        {
            forwardBulletAnim.Update(gameTime);
            forwardBulletAnim.draw(g2,(int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY());
        }
        else
        {
            backBulletAnim.Update(gameTime);
            backBulletAnim.draw(g2, (int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY());
        }
        //drawBoundForCollisionWithEnemy(g2);
    }

    //@Override
    public override void Update(GameTime gameTime)
    {
        // TODO Auto-generated method stub
        base.Update(gameTime);
    }

    //@Override
    public override void attack(GameTime gameTime) { }

}

}
