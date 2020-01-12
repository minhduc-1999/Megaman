using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Megaman.src.Control
{
	public abstract class Button
	{
		public enum PressType { NONE, PRESSED, HOVER}
		//public static readonly int NONE = 0;
		//public static readonly int PRESSED = 1;
		//public static readonly int HOVER = 2;

		protected String text;
		protected int posX;
		protected int posY;
		protected int width;
		protected int height;
		protected int paddingTextX;
		protected int paddingTextY;
		protected bool enabled;

		protected PressType state;
		protected Color bgColor;
		protected Color pressedBgColor;
		protected Color hoverBgColor;

		public Button(String text, int posX, int posY, int width, int height, int paddingTextX, int paddingTextY,
				Color bgColor)
		{
			this.text = text;
			this.posX = posX;
			this.posY = posY;
			this.width = width;
			this.height = height;
			this.paddingTextX = paddingTextX;
			this.paddingTextY = paddingTextY;
			this.bgColor = bgColor;
			enabled = true;
		}

		public void setEnable(bool enabled)
		{
			this.enabled = enabled;
		}

		public void setState(PressType state)
		{
			this.state = state;
		}

		public void setBgColor(Color color)
		{
			bgColor = color;
		}

		public void setHoverBgColor(Color color)
		{
			hoverBgColor = color;
		}
		public void setPressedBgColor(Color color)
		{
			pressedBgColor = color;
		}

		public abstract bool isInButton(int x, int y);
		public abstract void draw(Graphics g);
	}

}
