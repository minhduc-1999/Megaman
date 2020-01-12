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
    public class FinalBoss : Human
    {

    private Animation idleforward, idleback;
    private Animation shootingforward, shootingback;
    private Animation slideforward, slideback;

    private DateTime startTimeForAttacked;

    private Dictionary<String, long> timeAttack = new Dictionary<string, long>();
    private String[] attackType = new String[4];
    private int attackIndex = 0;
    private DateTime lastAttackTime;

    public FinalBoss(float x, float y, GameWorldState gameWorld) : base(x, y, 110, 150, 0.1f, 100, gameWorld)
    {
       
        idleback = CacheDataLoader.getInstance().getAnimation("boss_idle");
        idleforward = CacheDataLoader.getInstance().getAnimation("boss_idle");
        idleforward.flipAllImage();

        shootingback = CacheDataLoader.getInstance().getAnimation("boss_shooting");
        shootingforward = CacheDataLoader.getInstance().getAnimation("boss_shooting");
        shootingforward.flipAllImage();

        slideback = CacheDataLoader.getInstance().getAnimation("boss_slide");
        slideforward = CacheDataLoader.getInstance().getAnimation("boss_slide");
        slideforward.flipAllImage();

        setTimeForNoBehurt(500);
        setDamage(10);

        attackType[0] = "NONE";
        attackType[1] = "shooting";
        attackType[2] = "NONE";
        attackType[3] = "slide";

        timeAttack.Add("NONE", 2000);
        timeAttack.Add("shooting", 500);
        timeAttack.Add("slide", 5000);

    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (getGameWorld().megaMan.getPosX() > getPosX())
            setDirection(MainDir.RIGHT_DIR);
        else setDirection(MainDir.LEFT_DIR);

        if (gameTime.GetTimeSpanMilis( startTimeForAttacked) == 0)
            startTimeForAttacked = DateTime.Now;
        else if (gameTime.GetTimeSpanMilis(startTimeForAttacked) > 300)
        {
            attack(gameTime);
            startTimeForAttacked =DateTime.Now;
        }

        if (!attackType[attackIndex].Equals("NONE"))
        {
            if (attackType[attackIndex].Equals("shooting"))
            {

                Bullet bullet = new RocketBullet(getPosX(), getPosY() - 50, getGameWorld());
                if (getDirection() == MainDir.LEFT_DIR) bullet.setSpeedX(-4);
                else bullet.setSpeedX(4);
                bullet.setTeamType(getTeamType());
                getGameWorld().bulletManager.addObject(bullet);

            }
            else if (attackType[attackIndex].Equals("slide"))
            {

                if (getGameWorld().physicalMap.haveCollisionWithLeftWall(getBoundForCollisionWithMap()) != null)
                    setSpeedX(5);
                if (getGameWorld().physicalMap.haveCollisionWithRightWall(getBoundForCollisionWithMap()) != null)
                    setSpeedX(-5);


                setPosX(getPosX() + getSpeedX());
            }
        }
        else
        {
            // stop attack
            setSpeedX(0);
        }

    }

    //@Override
    public override void run()
    {

    }

    //@Override
    public override void jump()
    {
        setSpeedY(-5);
    }

    //@Override
    public override void dick()
    {


    }

    //@Override
    public override void standUp()
    {


    }

   // @Override
    public override void stopRun()
    {


    }

    //@Override
    public override void attack(GameTime gameTime)
    {

        // only switch state attack

        if (gameTime.GetTimeSpanMilis( lastAttackTime) > timeAttack[attackType[attackIndex]])
        {
            lastAttackTime = DateTime.Now;

            attackIndex++;
            if (attackIndex >= attackType.Length) attackIndex = 0;

            if (attackType[attackIndex].Equals("slide"))
            {
                if (getPosX() < getGameWorld().megaMan.getPosX()) setSpeedX(5);
                else setSpeedX(-5);
            }

        }

    }

    //@Override
    public override Rectangle getBoundForCollisionWithEnemy()
    {
        if (attackType[attackIndex].Equals("slide"))
        {
            Rectangle rect = getBoundForCollisionWithMap();
            rect.Y += 100;
            rect.Height -= 100;
            return rect;
        }
        else
            return getBoundForCollisionWithMap();
    }

    //@Override
    public override void draw(Graphics g2, GameTime gameTime)
    {

        if (getState() == MainState.NOBEHURT && (gameTime.GetTimeSpanMilis(DateTime.Now)) % 2 != 1)
        {
            MessageBox.Show("Plash...");
        }
        else
        {

            if (attackType[attackIndex].Equals("NONE"))
            {
                if (getDirection() == MainDir.RIGHT_DIR)
                {
                    idleforward.Update(gameTime);
                    idleforward.draw(g2, (int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY());
                }
                else
                {
                    idleback.Update(gameTime);
                    idleback.draw(g2,(int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY());
                }
            }
            else if (attackType[attackIndex].Equals("shooting"))
            {

                if (getDirection() == MainDir.RIGHT_DIR)
                {
                    shootingforward.Update(gameTime);
                    shootingforward.draw(g2,(int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY());
                }
                else
                {
                    shootingback.Update(gameTime);
                    shootingback.draw(g2,(int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY());
                }

            }
            else if (attackType[attackIndex].Equals("slide"))
            {
                if (getSpeedX() > 0)
                {
                    slideforward.Update(gameTime);
                    slideforward.draw(g2,(int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY() + 50);
                }
                else
                {
                    slideback.Update(gameTime);
                    slideback.draw(g2, (int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY() + 50);
                }
            }
        }
        // drawBoundForCollisionWithEnemy(g2);
    }

}
}
