using Megaman.src.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Megaman.src.GameObject
{
    public class BulletManager : ParticularObjectManager
    {

    public BulletManager(GameWorldState gameWorld):base(gameWorld)
    {
      
    }

    //@Override
    public override void UpdateObjects()
    {
        base.UpdateObjects();
        synchronized(particularObjects){
            for (int id = 0; id < particularObjects.Count; id++)
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
