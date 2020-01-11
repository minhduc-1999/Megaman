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
    public abstract class ParticularObject : GameObject
    {

        public static readonly int LEAGUE_TEAM = 1;
        public static readonly int ENEMY_TEAM = 2;

        public static readonly int LEFT_DIR = 0;
        public static readonly int RIGHT_DIR = 1;

        public static readonly int ALIVE = 0;
        public static readonly int BEHURT = 1;
        public static readonly int FEY = 2;
        public static readonly int DEATH = 3;
        public static readonly int NOBEHURT = 4;
        private int state = ALIVE;

        private float width;
        private float height;
        private float mass;
        private float speedX;
        private float speedY;
        private int blood;

        private int damage;

        private int direction;

        protected Animation behurtForwardAnim, behurtBackAnim;

        private int teamType;

        private long startTimeNoBeHurt;
        private long timeForNoBeHurt;

        public ParticularObject(float x, float y, float width, float height, float mass, int blood, GameWorldState gameWorld) : base(x, y, gameWorld)
        {

            // posX and posY are the middle coordinate of the object

            setWidth(width);
            setHeight(height);
            setMass(mass);
            setBlood(blood);

            direction = RIGHT_DIR;

        }

        public void setTimeForNoBehurt(long time)
        {
            timeForNoBeHurt = time;
        }

        public long getTimeForNoBeHurt()
        {
            return timeForNoBeHurt;
        }

        public void setState(int state)
        {
            this.state = state;
        }

        public int getState()
        {
            return state;
        }

        public void setDamage(int damage)
        {
            this.damage = damage;
        }

        public int getDamage()
        {
            return damage;
        }


        public void setTeamType(int team)
        {
            teamType = team;
        }

        public int getTeamType()
        {
            return teamType;
        }

        public void setMass(float mass)
        {
            this.mass = mass;
        }

        public float getMass()
        {
            return mass;
        }

        public void setSpeedX(float speedX)
        {
            this.speedX = speedX;
        }

        public float getSpeedX()
        {
            return speedX;
        }

        public void setSpeedY(float speedY)
        {
            this.speedY = speedY;
        }

        public float getSpeedY()
        {
            return speedY;
        }

        public void setBlood(int blood)
        {
            if (blood >= 0)
                this.blood = blood;
            else this.blood = 0;
        }

        public int getBlood()
        {
            return blood;
        }

        public void setWidth(float width)
        {
            this.width = width;
        }

        public float getWidth()
        {
            return width;
        }

        public void setHeight(float height)
        {
            this.height = height;
        }

        public float getHeight()
        {
            return height;
        }

        public void setDirection(int dir)
        {
            direction = dir;
        }

        public int getDirection()
        {
            return direction;
        }

        public abstract void attack();


        public bool isObjectOutOfCameraView()
        {
            if (getPosX() - getGameWorld().camera.getPosX() > getGameWorld().camera.getWidthView() ||
                    getPosX() - getGameWorld().camera.getPosX() < -50
                || getPosY() - getGameWorld().camera.getPosY() > getGameWorld().camera.getHeightView()
                        || getPosY() - getGameWorld().camera.getPosY() < -50)
                return true;
            else return false;
        }

        public Rectangle getBoundForCollisionWithMap()
        {
            Rectangle bound = new Rectangle();
            bound.X = (int)(getPosX() - (getWidth() / 2));
            bound.Y = (int)(getPosY() - (getHeight() / 2));
            bound.Width = (int)getWidth();
            bound.Height = (int)getHeight();
            return bound;
        }

        public void beHurt(int damgeEat)
        {
            setBlood(getBlood() - damgeEat);
            state = BEHURT;
            hurtingCallback();
        }

        //@Override
        public override void Update()
        {
            switch (state)
            {
                case ALIVE:

                    // note: SET DAMAGE FOR OBJECT NO DAMAGE
                    ParticularObject obj = getGameWorld().particularObjectManager.getCollisionWidthEnemyObject(this);
                    if (obj != null)
                    {


                        if (obj.getDamage() > 0)
                        {

                            // switch state to fey if object die


                            MessageBox.Show("eat damage.... from collision with enemy........ " + obj.getDamage());
                            beHurt(obj.getDamage());
                        }

                    }



                    break;

                case BEHURT:
                    if (behurtBackAnim == null)
                    {
                        state = NOBEHURT;
                        startTimeNoBeHurt = System.nanoTime();
                        if (getBlood() == 0)
                            state = FEY;

                    }
                    else
                    {
                        behurtForwardAnim.Update(System.nanoTime());
                        if (behurtForwardAnim.isLastFrame())
                        {
                            behurtForwardAnim.reset();
                            state = NOBEHURT;
                            if (getBlood() == 0)
                                state = FEY;
                            startTimeNoBeHurt = System.nanoTime();
                        }
                    }

                    break;

                case FEY:

                    state = DEATH;

                    break;

                case DEATH:


                    break;

                case NOBEHURT:
                    MessageBox.Show("state = nobehurt");
                    if (System.nanoTime() - startTimeNoBeHurt > timeForNoBeHurt)
                        state = ALIVE;
                    break;
            }

        }

        public void drawBoundForCollisionWithMap(Graphics g2)
        {
            Rectangle rect = getBoundForCollisionWithMap();
            g2.setColor(Color.Blue);
            g2.DrawRectangle(rect.X - (int)getGameWorld().camera.getPosX(), rect.y - (int)getGameWorld().camera.getPosY(), rect.Width, rect.Height);
        }

        public void drawBoundForCollisionWithEnemy(Graphics g2)
        {
            Rectangle rect = getBoundForCollisionWithEnemy();
            g2.setColor(Color.Red);
            g2.DrawRectangle(rect.X - (int)getGameWorld().camera.getPosX(), rect.y - (int)getGameWorld().camera.getPosY(), rect.Width, rect.Height);
        }

        public abstract Rectangle getBoundForCollisionWithEnemy();

        public abstract void draw(Graphics g2);

        public virtual void hurtingCallback() { }

    }

}
