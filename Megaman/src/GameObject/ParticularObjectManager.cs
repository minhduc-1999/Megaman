using Megaman.src.State;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Megaman.src.GameObject
{
    public class ParticularObjectManager
    {

        protected List<ParticularObject> particularObjects;

        private GameWorldState gameWorld;

        public ParticularObjectManager(GameWorldState gameWorld)
        {

            particularObjects = new List<ParticularObject>();
            this.gameWorld = gameWorld;

        }

        public GameWorldState getGameWorld()
        {
            return gameWorld;
        }

        public void addObject(ParticularObject particularObject)
        {


            lock(particularObjects){
                particularObjects.Add(particularObject);
            }

        }

        public void RemoveObject(ParticularObject particularObject)
        {
            lock(particularObjects){

                for (int id = 0; id < particularObjects.Count; id++)
                {

                    ParticularObject obj = particularObjects[id];
                    if (obj == particularObject)
                        particularObjects.RemoveAt(id);

                }
            }
        }

        public ParticularObject getCollisionWidthEnemyObject(ParticularObject obj)
        {
            lock(particularObjects){
                for (int id = 0; id < particularObjects.Count; id++)
                {

                    ParticularObject objectInList = particularObjects[id];

                    if (obj.getTeamType() != objectInList.getTeamType() &&
                            obj.getBoundForCollisionWithEnemy().IntersectsWith(objectInList.getBoundForCollisionWithEnemy()))
                    {
                        return objectInList;
                    }
                }
            }
            return null;
        }

        public virtual void UpdateObjects(GameTime gameTime)
        {
            lock(particularObjects){
                for (int id = 0; id < particularObjects.Count; id++)
                {

                    ParticularObject obj = particularObjects[id];


                    if (!obj.isObjectOutOfCameraView()) obj.Update(gameTime);

                    if (obj.getState() == GameObject.MainState.DEATH)
                    {
                        particularObjects.RemoveAt(id);
                    }
                }
            }

            //System.out.println("Camerawidth  = "+camera.getWidth());

        }

        public void draw(Graphics g2,GameTime gameTime)
        {
            lock(particularObjects){
                foreach (ParticularObject obj in particularObjects)
                    if (!obj.isObjectOutOfCameraView()) obj.draw(g2,gameTime);
            }
        }

    }

}
