using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Megaman.src.Control
{
	public class RectangleButton : Button
	{


	public RectangleButton(String text, int posX, int posY, int width, int height, int paddingTextX, int paddingTextY,
			Color bgColor): base(text, posX, posY, width, height, paddingTextX, paddingTextY, bgColor)
	{
	}

	//@Override
	public override bool isInButton(int x, int y)
	{
		return (enabled && x >= posX && x <= posX + width && y >= posY && y <= posY + height);
	}

	//@Override
	public override void draw(Graphics g)
	{
			SolidBrush brush = new SolidBrush(Color.White);
		if (enabled)
		{
			switch (state)
			{
				case PressType.NONE:
						//g.setColor(bgColor); 
						brush.Color = bgColor;
						break;
				case PressType.PRESSED:
						//g.setColor(pressedBgColor); 
						brush.Color = pressedBgColor;
						break;
				case PressType.HOVER:
						//g.setColor(hoverBgColor);
						brush.Color = hoverBgColor;
						break;
			}
		}
		else
		{
				//g.setColor(Color.GRAY);
				brush.Color = Color.Gray;
		}
		g.FillRectangle(brush,posX, posY, width, height);

			//g.setColor(Color.PINK);
			brush.Color = Color.Pink;
		g.DrawRectangle(new Pen(Color.Pink) ,posX, posY, width, height);
		g.FillRectangle(brush, posX + 1, posY + 1, width - 2, height - 2);

			//g.setColor(Color.WHITE);
			//g.setFont(new Font("TimesRoman", Font.PLAIN, 14));
			brush.Color = Color.White;
		g.DrawString(text, new Font("TimesRoman", 14), brush, posX + paddingTextX, posY + paddingTextY);
	}
}

}
