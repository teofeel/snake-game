using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace snake_game
{
    class Body : Label
    {
        public Body(int x, int y)
        {
            Location = new System.Drawing.Point(x, y);
            Size = new System.Drawing.Size(20, 20);
            BackColor = Color.Orange;
            Enabled = false;
        }
    }
}
