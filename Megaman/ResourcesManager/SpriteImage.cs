using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Megaman.ResourcesManager
{
    public class SpriteImage
    {
        private Image _image;
        private Point _frameSize;
        private Point _sheetSize;
        private int _collisionOffset;

        public Image Image { get => _image; }
        public Point FrameSize { get => _frameSize; }
        public Point SheetSize { get => _sheetSize; }
        public int CollisionOffset { get => _collisionOffset; }
        public SpriteImage(Image image, Point sheetsize, int collOffset)
        {
            _image = image;
            _sheetSize = sheetsize;
            _collisionOffset = collOffset;
            SetFrameSize();
        }
        public void SetFrameSize()
        {
            int width, heith;
            width = _image.Width / _sheetSize.X;
            heith = _image.Height / _sheetSize.Y;
            _frameSize = new Point(width, heith);
        }
    }
}
