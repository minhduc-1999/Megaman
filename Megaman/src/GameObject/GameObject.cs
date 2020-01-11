using Megaman.src.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Megaman.src.GameObject
{
	public abstract class GameObject
	{
		public enum MainState { ALIVE, BEHURT, FEY, DEATH, NOBEHURT };
		private float posX;
		private float posY;

		private GameWorldState gameWorld;

		public GameObject(float x, float y, GameWorldState gameWorld)
		{
			posX = x;
			posY = y;
			this.gameWorld = gameWorld;
		}

		public void setPosX(float x)
		{
			posX = x;
		}

		public float getPosX()
		{
			return posX;
		}

		public void setPosY(float y)
		{
			posY = y;
		}

		public float getPosY()
		{
			return posY;
		}

		public GameWorldState getGameWorld()
		{
			return gameWorld;
		}

		public abstract void Update(GameTime gameTime);

	}

}
