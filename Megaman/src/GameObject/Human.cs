using Megaman.src.State;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Megaman.src.GameObject
{
    public abstract class Human : ParticularObject
    {

        private bool isJumping;
        private bool isDicking;

        private bool isLanding;

        public Human(float x, float y, float width, float height, float mass, int blood, GameWorldState gameWorld) : base(x, y, width, height, mass, blood, gameWorld)
        {
            setState(MainState.ALIVE);
        }

        public abstract void run();

        public abstract void jump();

        public abstract void dick();

        public abstract void standUp();

        public abstract void stopRun();

        public bool getIsJumping()
        {
            return isJumping;
        }

        public void setIsLanding(bool b)
        {
            isLanding = b;
        }

        public bool getIsLanding()
        {
            return isLanding;
        }

        public void setIsJumping(bool isJumping)
        {
            this.isJumping = isJumping;
        }

        public bool getIsDicking()
        {
            return isDicking;
        }

        public void setIsDicking(bool isDicking)
        {
            this.isDicking = isDicking;
        }

        //@Override
        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);

            if (getState() == MainState.ALIVE || getState() == MainState.NOBEHURT)
            {

                if (!isLanding)
                {

                    setPosX(getPosX() + getSpeedX());


                    if (getDirection() == MainDir.LEFT_DIR &&
                            getGameWorld().physicalMap.haveCollisionWithLeftWall(getBoundForCollisionWithMap()) != null)
                    {

                        Rectangle rectLeftWall = getGameWorld().physicalMap.haveCollisionWithLeftWall(getBoundForCollisionWithMap());
                        setPosX(rectLeftWall.X + rectLeftWall.Width + getWidth() / 2);

                    }
                    if (getDirection() == MainDir.RIGHT_DIR &&
                            getGameWorld().physicalMap.haveCollisionWithRightWall(getBoundForCollisionWithMap()) != null)
                    {

                        Rectangle rectRightWall = getGameWorld().physicalMap.haveCollisionWithRightWall(getBoundForCollisionWithMap());
                        setPosX(rectRightWall.X - getWidth() / 2);

                    }



                    /**
                     * Codes below check the posY of megaMan
                     */
                    // plus (+2) because we must check below the character when he's speedY = 0

                    Rectangle boundForCollisionWithMapFuture = getBoundForCollisionWithMap();
                    boundForCollisionWithMapFuture.Y += (getSpeedY() != 0 ? (int)getSpeedY() : 2);
                    Rectangle rectLand = getGameWorld().physicalMap.haveCollisionWithLand(boundForCollisionWithMapFuture);

                    Rectangle rectTop = getGameWorld().physicalMap.haveCollisionWithTop(boundForCollisionWithMapFuture);

                    if (rectTop != Rectangle.Empty)
                    {

                        setSpeedY(0);
                        setPosY(rectTop.Y + getGameWorld().physicalMap.getTileSize() + getHeight() / 2);

                    }
                    else if (rectLand != Rectangle.Empty)
                    {
                        setIsJumping(false);
                        if (getSpeedY() > 0) setIsLanding(true);
                        setSpeedY(0);
                        setPosY(rectLand.Y - getHeight() / 2 - 1);
                    }
                    else
                    {
                        setIsJumping(true);
                        setSpeedY(getSpeedY() + getMass());
                        setPosY(getPosY() + getSpeedY());
                    }
                }
            }
        }

    }
}
