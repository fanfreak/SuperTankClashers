using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTankClashers
{
    class Wall : GameEntity
    {
        public Wall(Form1 Game) : base(Game)
        {
            picEntity.BackColor = System.Drawing.Color.Black;
        }
        public override void Update() { }
    }
}
