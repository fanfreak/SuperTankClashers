using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SuperTankClashers
{
    class InputManager
    {
        Form1 Game;
        Actions ActionForKey;
        Actions PrivateCurrentActions;
        Dictionary<Keys, Actions> KeyBindings = new Dictionary<Keys, Actions>();
        int PlayerNumber;
        public InputManager(Form1 Game,int PlayerNumber)
        {
            if (Game == null) throw new ArgumentNullException(nameof(Game));
            this.Game = Game;
            this.Game.KeyDown += Game_KeyDown;
            this.Game.KeyUp += Game_KeyUp;
            this.PlayerNumber = PlayerNumber;
            LoadKeyBindings();
        }

        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            PrivateCurrentActions &= ~ActionForKey;
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (KeyBindings.TryGetValue(e.KeyCode,out ActionForKey))
            {
                PrivateCurrentActions |= ActionForKey;
            }
        }

        private void LoadKeyBindings()
        {
            string[] lines = File.ReadAllLines("Player" + PlayerNumber + ".txt");
            foreach (string line in lines)
            {
                string[] KeyCodeAndAction = line.Split('=');
                Keys KeyCode = (Keys)int.Parse(KeyCodeAndAction[0]);
                Actions PlayerAction = (Actions)int.Parse(KeyCodeAndAction[1]);
                KeyBindings.Add(KeyCode, PlayerAction);
            }
        }
        public Actions CurrentActions { get { return PrivateCurrentActions; } }
    }
}
