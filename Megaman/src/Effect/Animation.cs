using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Megaman.src.Effect
{
    public class Animation
    {

        private String name;

        private bool isRepeated;

        private List<FrameImage> frameImages;
        private int currentFrame;

        private List<Boolean> ignoreFrames;

        private List<Double> delayFrames;
        private double beginTime;

        private bool drawRectFrame;

        public Animation()
        {
            delayFrames = new List<Double>();
            beginTime = 0;
            currentFrame = 0;

            ignoreFrames = new List<Boolean>();

            frameImages = new List<FrameImage>();

            drawRectFrame = false;

            isRepeated = true;
        }

        public Animation(Animation animation)
        {

            beginTime = animation.beginTime;
            currentFrame = animation.currentFrame;
            drawRectFrame = animation.drawRectFrame;
            isRepeated = animation.isRepeated;

            delayFrames = new List<Double>();
            foreach (Double d in animation.delayFrames)
            {
                delayFrames.Add(d);
            }

            ignoreFrames = new List<Boolean>();
            foreach (bool b in animation.ignoreFrames)
            {
                ignoreFrames.Add(b);
            }

            frameImages = new List<FrameImage>();
            foreach (FrameImage f in animation.frameImages)
            {
                frameImages.Add(new FrameImage(f));
            }
        }

        public void setIsRepeated(bool isRepeated)
        {
            this.isRepeated = isRepeated;
        }

        public bool getIsRepeated()
        {
            return isRepeated;
        }

        public bool isIgnoreFrame(int id)
        {
            return ignoreFrames[id];
        }

        public void setIgnoreFrame(int id)
        {
            if (id >= 0 && id < ignoreFrames.Count)
                ignoreFrames[id] = true;
        }

        public void unIgnoreFrame(int id)
        {
            if (id >= 0 && id < ignoreFrames.Count)
                ignoreFrames[id] = false;
        }

        public void setName(String name)
        {
            this.name = name;
        }
        public String getName()
        {
            return name;
        }

        public void setCurrentFrame(int currentFrame)
        {
            if (currentFrame >= 0 && currentFrame < frameImages.Count)
                this.currentFrame = currentFrame;
            else this.currentFrame = 0;
        }
        public int getCurrentFrame()
        {
            return this.currentFrame;
        }

        public void reset()
        {
            currentFrame = 0;
            beginTime = 0;
        }

        public void add(FrameImage frameImage, double timeToNextFrame)
        {

            ignoreFrames.Add(false);
            frameImages.Add(frameImage);
            delayFrames.Add(timeToNextFrame);

        }

        public void setDrawRectFrame(bool b)
        {
            drawRectFrame = b;
        }


        public Image getCurrentImage()
        {
            return frameImages[currentFrame].getImage();
        }

        public void Update(GameTime gameTime)
        {

            //if (beginTime == 0) beginTime = deltaTime;
            //else
            //{

            //    if (deltaTime - beginTime > delayFrames[currentFrame])
            //    {
            //        nextFrame();
            //        beginTime = deltaTime;
            //    }
            //}
            
            beginTime += gameTime.EslapsedTime.TotalMilliseconds;
            if (beginTime >= delayFrames[currentFrame])
            {
                beginTime = 0;
                nextFrame();
            }

        }


        public bool isLastFrame()
        {
            if (currentFrame == frameImages.Count - 1)
                return true;
            else return false;
        }

        private void nextFrame()
        {

            if (currentFrame >= frameImages.Count - 1)
            {

                if (isRepeated) currentFrame = 0;
            }
            else currentFrame++;

            if (ignoreFrames[currentFrame]) nextFrame();

        }



        public void flipAllImage()
        {

            for (int i = 0; i < frameImages.Count; i++)
            {

                Image image = frameImages[i].getImage();

                //AffineTransform tx = AffineTransform.getScaleInstance(-1, 1);
                //tx.translate(-image.getWidth(), 0);

                //AffineTransformOp op = new AffineTransformOp(tx,
                //AffineTransformOp.TYPE_BILINEAR);
                //image = op.filter(image, null);
                image.RotateFlip(RotateFlipType.Rotate180FlipY);
                //frameImages[i].setImage(image);
                
            }

        }
        public void draw(Graphics g2,int x, int y)
        {

            Image image = getCurrentImage();

            g2.DrawImage(image, x , y);
            //g2.DrawImage(image, x - image.Width / 2, y - image.Height / 2);
            if (drawRectFrame)
                g2.DrawRectangle(new Pen(Color.Purple),x - image.Width / 2, x - image.Height / 2, image.Width, image.Height);

        }
    }

}
