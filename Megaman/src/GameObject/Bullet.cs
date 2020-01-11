using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Megaman.src.GameObject
{
    public abstract class Bullet extends ParticularObject
    {

    public Bullet(float x, float y, float width, float height, float mass, int damage, GameWorldState gameWorld)
    {
        super(x, y, width, height, mass, 1, gameWorld);
        setDamage(damage);
    }

    public abstract void draw(Graphics2D g2d);

    public void Update()
    {
        super.Update();
        setPosX(getPosX() + getSpeedX());
        setPosY(getPosY() + getSpeedY());
        ParticularObject object = getGameWorld().particularObjectManager.getCollisionWidthEnemyObject(this);
        if (object != null && object.getState() == ALIVE)
        {
            setBlood(0);
            object.beHurt(getDamage());
            System.out.println("Bullet set behurt for enemy");
        }
    }

}
}
