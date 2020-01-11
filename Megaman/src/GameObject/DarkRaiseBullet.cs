﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Megaman.src.GameObject
{
    public class DarkRaiseBullet extends Bullet
    {


    private Animation forwardBulletAnim, backBulletAnim;

    public DarkRaiseBullet(float x, float y, GameWorldState gameWorld)
    {
        super(x, y, 30, 30, 1.0f, 10, gameWorld);
        forwardBulletAnim = CacheDataLoader.getInstance().getAnimation("darkraisebullet");
        backBulletAnim = CacheDataLoader.getInstance().getAnimation("darkraisebullet");
        backBulletAnim.flipAllImage();
    }



    @Override
    public Rectangle getBoundForCollisionWithEnemy()
    {
        // TODO Auto-generated method stub
        return getBoundForCollisionWithMap();
    }

    @Override
    public void draw(Graphics2D g2)
    {
        // TODO Auto-generated method stub
        if (getSpeedX() > 0)
        {
            forwardBulletAnim.Update(System.nanoTime());
            forwardBulletAnim.draw((int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY(), g2);
        }
        else
        {
            backBulletAnim.Update(System.nanoTime());
            backBulletAnim.draw((int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY(), g2);
        }
        //drawBoundForCollisionWithEnemy(g2);
    }

    @Override
    public void Update()
    {
        // TODO Auto-generated method stub
        super.Update();
    }

    @Override
    public void attack() { }

}
}
