using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OneChess
{
    public partial class Manager : Form
    {
        public Manager()
        {
            InitializeComponent();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Stage.showname)
            {
                New_Game game = new New_Game();
                game.Show();
            }
            else
                Stage.New = true;
        }

        private void Load1_Click(object sender, EventArgs e)
        {
            if (Match.Exist("Slot1.sav")) Match.Load("Slot1.sav");
        }

        private void Load2_Click(object sender, EventArgs e)
        {
            if (Match.Exist("Slot2.sav")) Match.Load("Slot2.sav");
        }

        private void Load3_Click(object sender, EventArgs e)
        {
            if (Match.Exist("Slot3.sav")) Match.Load("Slot3.sav");
        }

        private void Load4_Click(object sender, EventArgs e)
        {
            if (Match.Exist("Slot4.sav")) Match.Load("Slot4.sav");
        }

        private void Load5_Click(object sender, EventArgs e)
        {
            if (Match.Exist("Slot5.sav")) Match.Load("Slot5.sav");
        }

        private void Load6_Click(object sender, EventArgs e)
        {
            if (Match.Exist("Slot6.sav")) Match.Load("Slot6.sav");
        }

        private void Load7_Click(object sender, EventArgs e)
        {
            if (Match.Exist("Slot7.sav")) Match.Load("Slot7.sav");
        }

        private void Load8_Click(object sender, EventArgs e)
        {
            if (Match.Exist("Slot8.sav")) Match.Load("Slot8.sav");
        }

        private void Load9_Click(object sender, EventArgs e)
        {
            if (Match.Exist("Slot9.sav")) Match.Load("Slot9.sav");
        }

        private void Load10_Click(object sender, EventArgs e)
        {
            if (Match.Exist("Slot10.sav")) Match.Load("Slot10.sav");
        }
        void states()
        {
            Load1.Text = Match.LoadStates(1);
            Load2.Text = Match.LoadStates(2);
            Load3.Text = Match.LoadStates(3);
            Load4.Text = Match.LoadStates(4);
            Load5.Text = Match.LoadStates(5);
            Load6.Text = Match.LoadStates(6);
            Load7.Text = Match.LoadStates(7);
            Load8.Text = Match.LoadStates(8);
            Load9.Text = Match.LoadStates(9);
            Load10.Text = Match.LoadStates(10);
        }

        private void Loadstates_MouseHover(object sender, EventArgs e)
        {
            states();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stage.exit = true;
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options opt = new Options();
            opt.Show();
        }
    }
}