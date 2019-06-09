using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace SuperTankClashers
{
    class Player : GameEntity
    {
        //global variables
        List<Bullet> Bullets = new List<Bullet>();
        int PlayerNumber,CoolDown=5;
        InputManager Controls;
        Dictionary<string, Bitmap> UsedSprites = new Dictionary<string,Bitmap>();
        public Player(Form1 Game,int PlayerNumber) : base(Game)
        {
            picEntity.Size = new Size(40, 40);
            this.PlayerNumber = PlayerNumber;
            Controls = new InputManager(this.Game, PlayerNumber);            
            switch (PlayerNumber)
            {
                case 1:
                  //initialize colors
                  UsedSprites.Add("Up", Resource1.RedTankUp);
                  UsedSprites.Add("Down", Resource1.RedTank);
                  UsedSprites.Add("Left", Resource1.RedTankLeft);
                  UsedSprites.Add("Right", Resource1.RedTankRight);
                  break;
                case 2:
                    break;

            }
        }

        public override void Update()
        {
            foreach (GameEntity Entity in this.Game.Entities)
            {
                if (Entity is Wall)
                {
                    if (CheckCollision(Entity,GetXVelocity(),GetYVelocity()))
                    {
                        if (DistanceBetween(Entity)>=0)
                        {
                            switch (Controls.CurrentActions)
                            {
                                case Actions.Up:
                                    SetYVelocity(-DistanceBetween(Entity));
                                    break;
                                case Actions.Down:
                                    SetYVelocity(DistanceBetween(Entity));
                                    break;
                                case Actions.Left:
                                    SetXVelocity(-DistanceBetween(Entity));
                                    break;
                                case Actions.Right:
                                    SetXVelocity(DistanceBetween(Entity));
                                    break;
                            }
                        }
                        else
                        {
                            switch (Controls.CurrentActions)
                            {
                                case Actions.Up:
                                    SetYVelocity(15);
                                    break;
                                case Actions.Down:
                                    SetYVelocity(DistanceBetween(Entity));
                                    break;
                                case Actions.Left:
                                    SetXVelocity(-DistanceBetween(Entity));
                                    break;
                                case Actions.Right:
                                    SetXVelocity(DistanceBetween(Entity));
                                    break;
                            }
                        }
                    }
                }
                if (Entity is Bullet)
                {
                    this.Despawn();
                }
            }
            Move();
            SetAnimation();



            if (Actions.Shoot == Controls.CurrentActions)
            {
                if (CoolDown > 0)
                {
                    CoolDown--;
                    Shoot();
                }
                else
                {

                }
            }
            //reset velocities
            SetXVelocity(10);
            SetYVelocity(10);
        }


        public void Move()
        {
            int xVelocity = GetXVelocity(), yVelocity = GetYVelocity();
            //left
            if (Actions.Left == Controls.CurrentActions)
                picEntity.Left -= xVelocity;
            //right
            if (Actions.Right == Controls.CurrentActions)
                picEntity.Left += xVelocity;
            //up
            if (Actions.Up == Controls.CurrentActions)
                picEntity.Top -= yVelocity;
            //down
            if (Actions.Down == Controls.CurrentActions)
                picEntity.Top += yVelocity;  
        }
        public void SetAction(string Input)
        {
           
        }
        public void SetAnimation()
        {
            //left
            if (Actions.Left == Controls.CurrentActions)
                picEntity.Image = UsedSprites["Left"];
            //right
            if (Actions.Right == Controls.CurrentActions)
                picEntity.Image = UsedSprites["Right"];
            //up
            if (Actions.Up == Controls.CurrentActions)
                picEntity.Image = UsedSprites["Up"];
            //down
            if (Actions.Down == Controls.CurrentActions)
                picEntity.Image = UsedSprites["Down"];
            //simply use (Actions.Up | Actions.Down == Controls.CurrentActions) for diganoal directions.
        }
        public void Shoot()
        {
            Bullet ShootingBullet = new Bullet(this.Game);
            if (Bullets.Count() < 3)
            {
                if (Actions.Left == Controls.CurrentActions)
                {
                }
                //right
                if (Actions.Right == Controls.CurrentActions)
                    ShootingBullet.Spawn(this.picEntity.Bounds.X + this.picEntity.Width + ShootingBullet.picEntity.Bounds.X, this.picEntity.Bounds.Y);
                //up
                if (Actions.Up == Controls.CurrentActions)
                    picEntity.Image = UsedSprites["Up"];
                //down
                if (Actions.Down == Controls.CurrentActions)
                    picEntity.Image = UsedSprites["Down"];

                ShootingBullet.Spawn(this.picEntity.Bounds.X - ShootingBullet.picEntity.Width, this.picEntity.Bounds.Y);
                ShootingBullet.SetXVelocity(35);
                ShootingBullet.SetXVelocity(0);
                Bullets.Add(ShootingBullet);
                this.Game.AddEntity(ShootingBullet);
            }
        }

        void BulletRemoved(Bullet Bullet)
        {
            Bullets.Remove(Bullet);
        }

    }
}
