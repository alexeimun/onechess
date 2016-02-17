using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace OneChess
{
    public partial class New_Game : Form
    {
        public New_Game()
        {
            InitializeComponent();
        }
        string path1 = "Name1.dll", path2 = "Name.dll";

        private void OK_Click(object sender, EventArgs e)
        {
            Stage.user1 = player1.Text;
            Stage.user2 = player2.Text;
            WriteStream(path1, player1);
            WriteStream(path2, player2);
            Stage.New = true;
            this.Close();
        }

        private void New_Game_Load(object sender, EventArgs e)
        {
            ReadStream(path1, player1);
            ReadStream(path2, player2);
            OK.Enabled = player1.Text.Length > 0 && player2.Text.Length > 0;
        }

        private void ReadStream(string Path, ComboBox player)
        {
            FileStream fs = new FileStream(Path, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sr = new StreamReader(Path);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                player.Items.Add(line);
                player.Text = player.Items[player.Items.Count - 1].ToString();
            }
            sr.Close();
            fs.Close();
        }
        private void WriteStream(string Path, ComboBox player)
        {
            if (Parsing(Path, player))
            {
                StreamWriter sw = File.AppendText(Path);
                sw.WriteLine(player.Text);
                sw.Close();
                player.Items.Add(player.Text);
                player.Text = string.Empty;
            }
        }
        private bool Parsing(string Path, ComboBox player)
        {
            StreamReader sr = new StreamReader(Path);
            String line;
            while ((line = sr.ReadLine()) != null)
                if (player.Text == line) return false;
            sr.Close();
            return true;
        }

        private void player1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (player1.Text.Length > 1 && player2.Text.Length > 1 &&
           player1.Text.Length < 8 && player2.Text.Length < 8) OK.Enabled = true;
            else OK.Enabled = false;
        }

        private void player2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (player1.Text.Length > 1 && player2.Text.Length > 1 &&
               player1.Text.Length < 8 && player2.Text.Length < 8) OK.Enabled = true;
            else OK.Enabled = false;
        }
    }
}