using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Megaman.src.Effect
{
    public class FrameImage
    {

        private String name;
        private Image image;

        public FrameImage(String name, Image image)
        {
            this.name = name;
            this.image = image;
        }

        public FrameImage(FrameImage frameImage)
        {
            //image = new Image(frameImage.getWidthImage(),
            //        frameImage.getHeightImage(), frameImage.image.getType());
            image = (Image)frameImage.getImage().Clone();
            //Graphics g = image.getGraphics();
            //g.drawImage(frameImage.image, 0, 0, null);
            name = frameImage.name;
        }

        public void draw(Graphics g2, int x, int y)
        {

            g2.DrawImage(image, x - image.Width / 2, y - image.Height / 2);

        }

        public FrameImage()
        {
            this.name = null;
            image = null;
        }

        public int getWidthImage()
        {
            return image.Width;
        }

        public int getHeightImage()
        {
            return image.Height;
        }

        public void setName(String name)
        {
            this.name = name;
        }
        public String getName()
        {
            return name;
        }

        public Image getImage()
        {
            return image;
        }
        public void setImage(Image image)
        {
            this.image = image;
        }

    }
}
