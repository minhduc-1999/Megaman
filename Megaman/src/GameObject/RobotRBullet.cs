﻿using Megaman.src.Effect;
using Megaman.src.State;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Megaman.src.GameObject
{
    public class RobotRBullet : Bullet
    {


    private Animation forwardBulletAnim, backBulletAnim;

    public RobotRBullet(float x, float y, GameWorldState gameWorld) : base(x, y, 60, 30, 1.0f, 10, gameWorld)
    {
       
        forwardBulletAnim = CacheDataLoader.getInstance().getAnimation("robotRbullet");
        backBulletAnim = CacheDataLoader.getInstance().getAnimation("robotRbullet");
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
            forwardBulletAnim.Update(gameTime);
            forwardBulletAnim.draw(g2, (int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY());
        }
        else
        {
            backBulletAnim.Update(gameTime);
            backBulletAnim.draw(g2, (int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY());
        }
        //drawBoundForCollisionWithEnemy(g2);
    }

   // @Override
    public override void Update(GameTime gameTime)
    {
        // TODO Auto-generated method stub
        base.Update(gameTime);
    }

    //@Override
    public override void attack(GameTime gameTime) { }

}

}
