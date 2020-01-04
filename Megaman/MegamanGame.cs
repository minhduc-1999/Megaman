using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Megaman
{
    public partial class MegamanGame : Form
    {
        //Manager
        private Megaman.ResourcesManager.SpriteManager _spriteManager;
        //
        //Character
        Megaman.Character.Zero _mainZero;
        //
        public MegamanGame()
        {
            InitializeComponent();
            //
            _spriteManager = new ResourcesManager.SpriteManager();
            LoadContent();
            //
            _mainZero = new Character.Zero(_panelGame, _spriteManager.GetSprite("Zero", "Run"));
            //
            _gameTime.Interval = 100;
            _gameTime.Start();
        }
        public void LoadContent()
        {
            _spriteManager.AddSprite("Zero", "Stand", new ResourcesManager.SpriteImage(Properties.Resources.ZeroStand, new Point(4,1), 0));
            _spriteManager.AddSprite("Zero", "StandShot", new ResourcesManager.SpriteImage(Properties.Resources.ZeroStandShot, new Point(1, 2), 0));
            _spriteManager.AddSprite("Zero", "Run", new ResourcesManager.SpriteImage(Properties.Resources.ZeroRun, new Point(9, 2), 0));
        }
        public void UpdateGame()
        {
            _mainZero.Update();
        }
        public void Draw()
        {
            _mainZero.Draw();
        }

        private void _gameTime_Tick(object sender, EventArgs e)
        {
            UpdateGame();
            Draw();
        }
    }
}
