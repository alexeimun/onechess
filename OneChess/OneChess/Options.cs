using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace OneChess
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }
        string path = "Options.opt";
        private void button1_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            fs.Close();
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine(op1.Checked + "," + op2.Checked + "," + op3.Checked + "," + op4.Checked + "," + op5.Checked);
            sw.Close();
            Match.LoadOptions();
            Close();
        }

        private void Options_Load(object sender, EventArgs e)
        {
            if (File.Exists(path))
            {
                StreamReader sr = new StreamReader(path);
                String Line;
                while ((Line = sr.ReadLine()) != null)
                {
                    string[] lines = Line.Split(',');
                    op1.Checked = bool.Parse(lines[0]);
                    op2.Checked = bool.Parse(lines[1]);
                    op3.Checked = bool.Parse(lines[2]);
                    op4.Checked = bool.Parse(lines[3]);
                    op5.Checked = bool.Parse(lines[4]);
                }
                sr.Close();
            }
        }
    }
}