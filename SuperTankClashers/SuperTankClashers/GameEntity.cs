using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SuperTankClashers
{
    public abstract class GameEntity
    {
        private int xVelocity = 10;
        private int yVelocity = 10;
        public PictureBox picEntity = new PictureBox();
        public Form1 Game;

        public void SetXVelocity(int xVelocity)
        {
            this.xVelocity = xVelocity;
        }
        public int GetXVelocity()
        {
            return xVelocity;
        }

        public void SetYVelocity(int YVelocity)
        {
            this.yVelocity = YVelocity;
        }

        public int GetYVelocity()
        {
            return yVelocity;
        }

        public GameEntity(Form1 Game)
        {
            if (Game == null) throw new ArgumentNullException(nameof(Game));
            this.Game = Game;
            picEntity.Visible = true;
            picEntity.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        public abstract void Update();

        public bool CheckCollision(GameEntity Other, int xVelocity,int yVelocity)
        {
            if (xVelocity > 0)
                return Other.picEntity.Bounds.Contains(picEntity.Bounds.X + picEntity.Width + xVelocity, picEntity.Bounds.Y) || Other.picEntity.Bounds.Contains(picEntity.Bounds.X + picEntity.Width + xVelocity, picEntity.Bounds.Y + picEntity.Height) || Other.picEntity.Bounds.Contains(picEntity.Bounds.X + picEntity.Width + xVelocity, picEntity.Bounds.Y + picEntity.Height / 2);
            if (xVelocity < 0)
                return Other.picEntity.Bounds.Contains(picEntity.Bounds.X - xVelocity, picEntity.Bounds.Y + picEntity.Height) || Other.picEntity.Bounds.Contains(picEntity.Bounds.X - xVelocity, picEntity.Bounds.Y) || Other.picEntity.Bounds.Contains(picEntity.Bounds.X - xVelocity, picEntity.Bounds.Y + picEntity.Height / 2);
            if (yVelocity < 0)
                return Other.picEntity.Bounds.Contains(picEntity.Bounds.X, picEntity.Bounds.Y - yVelocity) || Other.picEntity.Bounds.Contains(picEntity.Bounds.X + (picEntity.Width / 2), picEntity.Bounds.Y - yVelocity) || Other.picEntity.Bounds.Contains(picEntity.Bounds.X + picEntity.Width, picEntity.Bounds.Y - yVelocity);
            if (yVelocity > 0)
                return Other.picEntity.Bounds.Contains(picEntity.Bounds.X, picEntity.Bounds.Y + picEntity.Height + yVelocity) || Other.picEntity.Bounds.Contains(picEntity.Bounds.X + picEntity.Width, picEntity.Bounds.Y + picEntity.Height + yVelocity) || Other.picEntity.Bounds.Contains(picEntity.Bounds.X + picEntity.Width / 2, picEntity.Bounds.Y + picEntity.Height + yVelocity);
            return false;
        }
        public void Spawn(int x, int y)
        {
            picEntity.Location = new System.Drawing.Point(x, y);
            this.Game.Controls.Add(picEntity);
        }

        public void Despawn()
        {
            this.Game.Controls.Remove(picEntity);
        }

        public int DistanceBetween(GameEntity Other)
        {
            Point PlayerCenter = new Point(picEntity.Left + picEntity.Width / 2, picEntity.Top + picEntity.Height / 2);
            Point WallCenter = new Point(Other.picEntity.Left + Other.picEntity.Width / 2, Other.picEntity.Top + Other.picEntity.Height / 2);
            return Math.Max(Math.Abs(PlayerCenter.X - WallCenter.X) - (picEntity.Width + Other.picEntity.Width) / 2, Math.Abs(PlayerCenter.Y - WallCenter.Y) - (picEntity.Height + Other.picEntity.Height) / 2);
        }

        public void SetPictureBox(PictureBox picBox)
        {
            picEntity = picBox;
        }

        public void SetSize(int Length, int Height)
        {
            picEntity.Size = new Size(Length, Height);
        }
    }
}
