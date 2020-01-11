using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Megaman
{
    public class GameTime: Timer
    {
        DateTime _tickMark;
        public TimeSpan EslapsedTime
        {
            get
            {
                return ((DateTime.Now - _tickMark) + TimeSpan.FromMilliseconds(this.Interval));
            }
        }
        public GameTime():base()
        {
            this.Tick += GameTime_Tick;
        }

        private void GameTime_Tick(object sender, EventArgs e)
        {
            _tickMark = DateTime.Now;
        }
    }
}
