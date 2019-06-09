using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperTankClashers
{
    public partial class Form1 : Form
    {
        Player PlayerOne;
        Wall BlackWall;
        public List<GameEntity> Entities = new List<GameEntity>();
        public List<GameEntity> EntitiesToAdd = new List<GameEntity>();
        public Form1()
        {
            InitializeComponent();
        }

        public void AddEntity(GameEntity Entity)
        {
            EntitiesToAdd.Add(Entity);
        }
        public void RemoveEntity(GameEntity Entity)
        {
            EntitiesToAdd.Remove(Entity);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PlayerOne = new Player(this,1);
            Entities.Add(PlayerOne);
            PlayerOne.Spawn(20, 20);
            BlackWall = new Wall(this);
            BlackWall.SetSize(360, 28);
            Entities.Add(BlackWall);
            BlackWall.Spawn(808, 224);
            tmrGame.Enabled = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void TmrGame_Tick(object sender, EventArgs e)
        {
            foreach (GameEntity Entity in Entities)
            {
                Entity.Update(); 
            }
            foreach (GameEntity Entity in EntitiesToAdd)
            {
                Entities.Add(Entity);
            }
            EntitiesToAdd.Clear();

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
        }
    }
}
