using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Megaman.src.GameObject
{
    public class BulletManager extends ParticularObjectManager
    {

    public BulletManager(GameWorldState gameWorld)
    {
        super(gameWorld);
    }

    @Override
    public void UpdateObjects()
    {
        super.UpdateObjects();
        synchronized(particularObjects){
            for (int id = 0; id < particularObjects.size(); id++)
            {

                ParticularObject object = particularObjects.get(id);

                if (object.isObjectOutOfCameraView() || object.getState() == ParticularObject.DEATH)
                {
                    particularObjects.remove(id);
                    //System.out.println("Remove");
                }
            }
        }
    }



}
}
