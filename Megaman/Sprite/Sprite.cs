using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Megaman.Sprite
{
    public abstract class Sprite
    {
        protected Control _gameScreen;
        protected Image _spriteImage;
        protected Vector2 _position;
        protected PictureBox _sprite;
        protected Vector2 _speed;
        protected Point _frameSize;
        protected Point _currentFrame;
        protected Point _sheetSize;
        protected int _collisionOffset;
        public Sprite(Control gameSreen,Image image, Vector2 pos, Point frameSize, Point curFrame, Point sheetSize, Vector2 speed, int collisOffset)
        {
            _sprite = new PictureBox();
            _sprite.Size = new Size(50, 80);
            _sprite.SizeMode = PictureBoxSizeMode.StretchImage;
            _gameScreen = gameSreen;
            _spriteImage = image;
            _position = pos;
            _frameSize = frameSize;
            _currentFrame = curFrame;
            _sheetSize = sheetSize;
            _speed = speed;
            _collisionOffset = collisOffset;
            _gameScreen.Controls.Add(_sprite);
        }
        public virtual void Update()
        {
            ++_currentFrame.X;
            if (_currentFrame.X >= _sheetSize.X)
            {
                _currentFrame.X = 0;
                ++_currentFrame.Y;
                if (_currentFrame.Y >= _sheetSize.Y)
                    _currentFrame.Y = 0;
            }
        }
        public virtual void Draw()
        {
            _sprite.Image = GetFrame(_spriteImage);
            _sprite.Location = new Point((int)_position.X,(int) _position.Y);
        }
        //public abstract Vector2 direction
        //{
        //    get;
        //}
        public Rectangle collisionRect
        {
            get
            {
                return new Rectangle(
                (int)_position.X + _collisionOffset,
                (int)_position.Y + _collisionOffset,
                _frameSize.X - (_collisionOffset * 2),
                _frameSize.Y - (_collisionOffset * 2));
            }
        }
        public Image GetFrame(Image src)
        {
            Rectangle cropRect = new Rectangle(
                _currentFrame.X * _frameSize.X,
                _currentFrame.Y * _frameSize.Y,
                _frameSize.X, _frameSize.Y);
            Bitmap bmpImage = new Bitmap(src);
            return bmpImage.Clone(cropRect, bmpImage.PixelFormat);
        }
    }
   
}
