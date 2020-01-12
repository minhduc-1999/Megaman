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
    public override void UpdateObjects(GameTime gameTime)
    {
        base.UpdateObjects(gameTime);
        lock(particularObjects){
            for (int id = 0; id < particularObjects.Count; id++)
            {

                ParticularObject obj = particularObjects[id];

                if (obj.isObjectOutOfCameraView() || obj.getState() == GameObject.MainState.DEATH)
                {
                    particularObjects.RemoveAt(id);
                    //System.out.println("Remove");
                }
            }
        }
    }



}
}
