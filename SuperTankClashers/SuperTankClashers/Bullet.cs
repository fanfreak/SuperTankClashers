using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTankClashers
{
    class Bullet : GameEntity
    {
        int LifeTime = 5;
        public Bullet(Form1 Game) : base(Game)
        {
            picEntity.Image = Resource1.Bullet;
            SetSize(22,23);
        }

        private void Move()
        {
            picEntity.Left += GetXVelocity();
            picEntity.Top += GetYVelocity();
        }
        public override void Update()
        {
            if (LifeTime > 0)
            {
                LifeTime--;
                Move();
            }
            else
                this.Despawn();   
        }
    }
}
