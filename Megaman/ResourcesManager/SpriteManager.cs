using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Megaman.ResourcesManager;
namespace Megaman.ResourcesManager
{
    public class SpriteManager
    {
        private Dictionary<string, Dictionary<string, SpriteImage>> _sprites;
        public SpriteManager()
        {
            _sprites = new Dictionary<string, Dictionary<string, SpriteImage>>();
        }
        public void AddSprite(string name, string action, SpriteImage image)
        {
            if (_sprites.ContainsKey(name))
            {
                if (_sprites[name].ContainsKey(action))
                    return;
                _sprites[name].Add(action, image);
            }
            else
            {
                _sprites.Add(name, new Dictionary<string, SpriteImage>());
                _sprites[name].Add(action, image);
            }
        }
        public SpriteImage GetSprite(string name, string action)
        {
            if (_sprites.ContainsKey(name))
                if (_sprites[name].ContainsKey(action))
                    return _sprites[name][action];
            throw new NullReferenceException();
        }
       

    }
}
