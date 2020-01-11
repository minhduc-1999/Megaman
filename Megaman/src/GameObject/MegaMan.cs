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
    public class MegaMan : Human
    {

        public static readonly int RUNSPEED = 3;

        private Animation runForwardAnim, runBackAnim, runShootingForwarAnim, runShootingBackAnim;
        private Animation idleForwardAnim, idleBackAnim, idleShootingForwardAnim, idleShootingBackAnim;
        private Animation dickForwardAnim, dickBackAnim;
        private Animation flyForwardAnim, flyBackAnim, flyShootingForwardAnim, flyShootingBackAnim;
        private Animation landingForwardAnim, landingBackAnim;

        private Animation climWallForward, climWallBack;

        private long lastShootingTime;
        private bool isShooting = false;

        //private AudioClip hurtingSound;
        //private AudioClip shooting1;

        public MegaMan(float x, float y, GameWorldState gameWorld) : base(x, y, 70, 90, 0.1f, 100, gameWorld)
        {
            //shooting1 = CacheDataLoader.getInstance().getSound("bluefireshooting");
            //hurtingSound = CacheDataLoader.getInstance().getSound("megamanhurt");

            setTeamType(LEAGUE_TEAM);

            setTimeForNoBehurt(2000 * 1000000);

            runForwardAnim = CacheDataLoader.getInstance().getAnimation("run");
            runBackAnim = CacheDataLoader.getInstance().getAnimation("run");
            runBackAnim.flipAllImage();

            idleForwardAnim = CacheDataLoader.getInstance().getAnimation("idle");
            idleBackAnim = CacheDataLoader.getInstance().getAnimation("idle");
            idleBackAnim.flipAllImage();

            dickForwardAnim = CacheDataLoader.getInstance().getAnimation("dick");
            dickBackAnim = CacheDataLoader.getInstance().getAnimation("dick");
            dickBackAnim.flipAllImage();

            flyForwardAnim = CacheDataLoader.getInstance().getAnimation("flyingup");
            flyForwardAnim.setIsRepeated(false);
            flyBackAnim = CacheDataLoader.getInstance().getAnimation("flyingup");
            flyBackAnim.setIsRepeated(false);
            flyBackAnim.flipAllImage();

            landingForwardAnim = CacheDataLoader.getInstance().getAnimation("landing");
            landingBackAnim = CacheDataLoader.getInstance().getAnimation("landing");
            landingBackAnim.flipAllImage();

            climWallBack = CacheDataLoader.getInstance().getAnimation("clim_wall");
            climWallForward = CacheDataLoader.getInstance().getAnimation("clim_wall");
            climWallForward.flipAllImage();

            behurtForwardAnim = CacheDataLoader.getInstance().getAnimation("hurting");
            behurtBackAnim = CacheDataLoader.getInstance().getAnimation("hurting");
            behurtBackAnim.flipAllImage();

            idleShootingForwardAnim = CacheDataLoader.getInstance().getAnimation("idleshoot");
            idleShootingBackAnim = CacheDataLoader.getInstance().getAnimation("idleshoot");
            idleShootingBackAnim.flipAllImage();

            runShootingForwarAnim = CacheDataLoader.getInstance().getAnimation("runshoot");
            runShootingBackAnim = CacheDataLoader.getInstance().getAnimation("runshoot");
            runShootingBackAnim.flipAllImage();

            flyShootingForwardAnim = CacheDataLoader.getInstance().getAnimation("flyingupshoot");
            flyShootingBackAnim = CacheDataLoader.getInstance().getAnimation("flyingupshoot");
            flyShootingBackAnim.flipAllImage();

        }

        //@Override
        public override void Update(GameTime gameTime)
        {
            base.Update();
            if (isShooting)
            {
                if (DateTime.Now - lastShootingTime > 500 * 1000000)
                {
                    isShooting = false;
                }
            }

            if (getIsLanding())
            {
                landingBackAnim.Update(DateTime.Now);
                if (landingBackAnim.isLastFrame())
                {
                    setIsLanding(false);
                    landingBackAnim.reset();
                    runForwardAnim.reset();
                    runBackAnim.reset();
                }
            }

        }

        //@Override
        public override Rectangle getBoundForCollisionWithEnemy()
        {
            // TODO Auto-generated method stub
            Rectangle rect = getBoundForCollisionWithMap();

            if (getIsDicking())
            {
                rect.X = (int)getPosX() - 22;
                rect.Y = (int)getPosY() - 20;
                rect.Width = 44;
                rect.Height = 65;
            }
            else
            {
                rect.X = (int)getPosX() - 22;
                rect.Y = (int)getPosY() - 40;
                rect.Width = 44;
                rect.Height = 80;
            }

            return rect;
        }

        //@Override
        public override void draw(Graphics g2)
        {

            switch (getState())
            {

                case MainState.ALIVE:
                case MainState.NOBEHURT:
                    if (getState() == MainState.NOBEHURT && (DateTime.Now / 10000000) % 2 != 1)
                    {
                        MessageBox.Show("Plash...");
                    }
                    else
                    {

                        if (getIsLanding())
                        {

                            if (getDirection() == MainDir.RIGHT_DIR)
                            {
                                landingForwardAnim.setCurrentFrame(landingBackAnim.getCurrentFrame());
                                landingForwardAnim.draw((int)(getPosX() - getGameWorld().camera.getPosX()),
                                        (int)getPosY() - (int)getGameWorld().camera.getPosY() + (getBoundForCollisionWithMap().height / 2 - landingForwardAnim.getCurrentImage().getHeight() / 2),
                                        g2);
                            }
                            else
                            {
                                landingBackAnim.draw((int)(getPosX() - getGameWorld().camera.getPosX()),
                                        (int)getPosY() - (int)getGameWorld().camera.getPosY() + (getBoundForCollisionWithMap().height / 2 - landingBackAnim.getCurrentImage().getHeight() / 2),
                                        g2);
                            }

                        }
                        else if (getIsJumping())
                        {

                            if (getDirection() == MainDir.RIGHT_DIR)
                            {
                                flyForwardAnim.Update(DateTime.Now);
                                if (isShooting)
                                {
                                    flyShootingForwardAnim.setCurrentFrame(flyForwardAnim.getCurrentFrame());
                                    flyShootingForwardAnim.draw((int)(getPosX() - getGameWorld().camera.getPosX()) + 10, (int)getPosY() - (int)getGameWorld().camera.getPosY(), g2);
                                }
                                else
                                    flyForwardAnim.draw((int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY(), g2);
                            }
                            else
                            {
                                flyBackAnim.Update(DateTime.Now);
                                if (isShooting)
                                {
                                    flyShootingBackAnim.setCurrentFrame(flyBackAnim.getCurrentFrame());
                                    flyShootingBackAnim.draw((int)(getPosX() - getGameWorld().camera.getPosX()) - 10, (int)getPosY() - (int)getGameWorld().camera.getPosY(), g2);
                                }
                                else
                                    flyBackAnim.draw((int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY(), g2);
                            }

                        }
                        else if (getIsDicking())
                        {

                            if (getDirection() == MainDir.RIGHT_DIR)
                            {
                                dickForwardAnim.Update(DateTime.Now);
                                dickForwardAnim.draw((int)(getPosX() - getGameWorld().camera.getPosX()),
                                        (int)getPosY() - (int)getGameWorld().camera.getPosY() + (getBoundForCollisionWithMap().height / 2 - dickForwardAnim.getCurrentImage().getHeight() / 2),
                                        g2);
                            }
                            else
                            {
                                dickBackAnim.Update(DateTime.Now);
                                dickBackAnim.draw((int)(getPosX() - getGameWorld().camera.getPosX()),
                                        (int)getPosY() - (int)getGameWorld().camera.getPosY() + (getBoundForCollisionWithMap().height / 2 - dickBackAnim.getCurrentImage().getHeight() / 2),
                                        g2);
                            }

                        }
                        else
                        {
                            if (getSpeedX() > 0)
                            {
                                runForwardAnim.Update(DateTime.Now);
                                if (isShooting)
                                {
                                    runShootingForwarAnim.setCurrentFrame(runForwardAnim.getCurrentFrame() - 1);
                                    runShootingForwarAnim.draw((int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY(), g2);
                                }
                                else
                                    runForwardAnim.draw((int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY(), g2);
                                if (runForwardAnim.getCurrentFrame() == 1) runForwardAnim.setIgnoreFrame(0);
                            }
                            else if (getSpeedX() < 0)
                            {
                                runBackAnim.Update(DateTime.Now);
                                if (isShooting)
                                {
                                    runShootingBackAnim.setCurrentFrame(runBackAnim.getCurrentFrame() - 1);
                                    runShootingBackAnim.draw((int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY(), g2);
                                }
                                else
                                    runBackAnim.draw((int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY(), g2);
                                if (runBackAnim.getCurrentFrame() == 1) runBackAnim.setIgnoreFrame(0);
                            }
                            else
                            {
                                if (getDirection() == MainDir.RIGHT_DIR)
                                {
                                    if (isShooting)
                                    {
                                        idleShootingForwardAnim.Update(DateTime.Now);
                                        idleShootingForwardAnim.draw((int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY(), g2);
                                    }
                                    else
                                    {
                                        idleForwardAnim.Update(DateTime.Now);
                                        idleForwardAnim.draw((int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY(), g2);
                                    }
                                }
                                else
                                {
                                    if (isShooting)
                                    {
                                        idleShootingBackAnim.Update(DateTime.Now);
                                        idleShootingBackAnim.draw((int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY(), g2);
                                    }
                                    else
                                    {
                                        idleBackAnim.Update(DateTime.Now);
                                        idleBackAnim.draw((int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY(), g2);
                                    }
                                }
                            }
                        }
                    }

                    break;

                case MainState.BEHURT:
                    if (getDirection() == MainDir.RIGHT_DIR)
                    {
                        behurtForwardAnim.draw((int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY(), g2);
                    }
                    else
                    {
                        behurtBackAnim.setCurrentFrame(behurtForwardAnim.getCurrentFrame());
                        behurtBackAnim.draw((int)(getPosX() - getGameWorld().camera.getPosX()), (int)getPosY() - (int)getGameWorld().camera.getPosY(), g2);
                    }
                    break;

                case MainState.FEY:

                    break;

            }

            //drawBoundForCollisionWithMap(g2);
            //drawBoundForCollisionWithEnemy(g2);
        }

        //@Override
        public override void run()
        {
            if (getDirection() == MainDir.LEFT_DIR)
                setSpeedX(-3);
            else setSpeedX(3);
        }

        //@Override
        public override void jump()
        {

            if (!getIsJumping())
            {
                setIsJumping(true);
                setSpeedY(-5.0f);
                flyBackAnim.reset();
                flyForwardAnim.reset();
            }
            // for clim wall
            else
            {
                Rectangle rectRightWall = getBoundForCollisionWithMap();
                rectRightWall.X += 1;
                Rectangle rectLeftWall = getBoundForCollisionWithMap();
                rectLeftWall.Y -= 1;

                if (getGameWorld().physicalMap.haveCollisionWithRightWall(rectRightWall) != null && getSpeedX() > 0)
                {
                    setSpeedY(-5.0f);
                    //setSpeedX(-1);
                    flyBackAnim.reset();
                    flyForwardAnim.reset();
                    //setDirection(LEFT_DIR);
                }
                else if (getGameWorld().physicalMap.haveCollisionWithLeftWall(rectLeftWall) != null && getSpeedX() < 0)
                {
                    setSpeedY(-5.0f);
                    //setSpeedX(1);
                    flyBackAnim.reset();
                    flyForwardAnim.reset();
                    //setDirection(RIGHT_DIR);
                }

            }
        }

        //@Override
        public override void dick()
        {
            if (!getIsJumping())
                setIsDicking(true);
        }

        // @Override
        public override void standUp()
        {
            setIsDicking(false);
            idleForwardAnim.reset();
            idleBackAnim.reset();
            dickForwardAnim.reset();
            dickBackAnim.reset();
        }

        //@Override
        public override void stopRun()
        {
            setSpeedX(0);
            runForwardAnim.reset();
            runBackAnim.reset();
            runForwardAnim.unIgnoreFrame(0);
            runBackAnim.unIgnoreFrame(0);
        }

        //@Override
        public override void attack()
        {

            if (!isShooting && !getIsDicking())
            {

               // shooting1.play();

                Bullet bullet = new BlueFire(getPosX(), getPosY(), getGameWorld());
                if (getDirection() == MainDir.LEFT_DIR)
                {
                    bullet.setSpeedX(-10);
                    bullet.setPosX(bullet.getPosX() - 40);
                    if (getSpeedX() != 0 && getSpeedY() == 0)
                    {
                        bullet.setPosX(bullet.getPosX() - 10);
                        bullet.setPosY(bullet.getPosY() - 5);
                    }
                }
                else
                {
                    bullet.setSpeedX(10);
                    bullet.setPosX(bullet.getPosX() + 40);
                    if (getSpeedX() != 0 && getSpeedY() == 0)
                    {
                        bullet.setPosX(bullet.getPosX() + 10);
                        bullet.setPosY(bullet.getPosY() - 5);
                    }
                }
                if (getIsJumping())
                    bullet.setPosY(bullet.getPosY() - 20);

                bullet.setTeamType(getTeamType());
                getGameWorld().bulletManager.addObject(bullet);

                lastShootingTime = DateTime.Now;
                isShooting = true;

            }

        }
        //@Override
        public override void hurtingCallback()
        {
            MessageBox.Show("Call back hurting");
            //hurtingSound.play();
        }

    }

}
