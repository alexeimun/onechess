using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace OneChess
{
    public static class Match
    {
        private static string[] lines;
        private static Keys[] key;
        private static Keys[] oldKeys = new Keys[0];

        public static void Slots()
        {
            KeyboardState keyboard = Keyboard.GetState();
            key = keyboard.GetPressedKeys();
            if (!Ask.move)
            {
                for (int i = 0; i < key.Length; i++)
                {
                    bool allow = false;
                    for (int j = 0; j < oldKeys.Length; j++)
                        allow = key[i] == oldKeys[j];
                    if (!allow)
                    {
                        if (keyboard.IsKeyDown(Keys.LeftShift) ||
                           keyboard.IsKeyDown(Keys.RightShift))
                        {
                            switch (key[i])
                            {
                                case Keys.F1:
                                    Save("Slot1.sav"); break;
                                case Keys.F2:
                                    Save("Slot2.sav"); break;
                                case Keys.F3:
                                    Save("Slot3.sav"); break;
                                case Keys.F4:
                                    Save("Slot4.sav"); break;
                                case Keys.F5:
                                    Save("Slot5.sav"); break;
                                case Keys.F6:
                                    Save("Slot6.sav"); break;
                                case Keys.F7:
                                    Save("Slot7.sav"); break;
                                case Keys.F8:
                                    Save("Slot8.sav"); ; break;
                                case Keys.F9:
                                    Save("Slot9.sav"); break;
                                case Keys.F10:
                                    Save("Slot10.sav"); break;
                            }
                        }
                        else
                        {
                            switch (key[i])
                            {
                                case Keys.F1:
                                    Load("Slot1.sav"); break;
                                case Keys.F2:
                                    Load("Slot2.sav"); break;
                                case Keys.F3:
                                    Load("Slot3.sav"); break;
                                case Keys.F4:
                                    Load("Slot4.sav"); break;
                                case Keys.F5:
                                    Load("Slot5.sav"); break;
                                case Keys.F6:
                                    Load("Slot6.sav"); break;
                                case Keys.F7:
                                    Load("Slot7.sav"); break;
                                case Keys.F8:
                                    Load("Slot8.sav"); break;
                                case Keys.F9:
                                    Load("Slot9.sav"); break;
                                case Keys.F10:
                                    Load("Slot10.sav"); break;
                                case Keys.Tab:
                                case Keys.Space:
                                    Ask.Switch();
                                    break;
                            }
                        }
                    }
                }
            }
            oldKeys = key;
        }
        public static void Save(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            fs.Close();
            StreamWriter sw = File.AppendText(path);
            //Writing pawns
            for (int r = 0; r < 8; r++)
                sw.WriteLine(Ask.White.Pawn[r].X + "-" + Ask.White.Pawn[r].Y + "-" + Ask.White.Pawn[r].Visible + "-" + Ask.Black.Pawn[r].X + "-" + Ask.Black.Pawn[r].Y + "-" + Ask.Black.Pawn[r].Visible);

            //Writing Rooks
            for (int r = 0; r < 2; r++)
                sw.WriteLine(Ask.White.Rook[r].X + "-" + Ask.White.Rook[r].Y + "-" + Ask.White.Rook[r].Visible + "-" + Ask.White.Rook[r].castle + "-" +
                    Ask.Black.Rook[r].X + "-" + Ask.Black.Rook[r].Y + "-" + Ask.Black.Rook[r].Visible + "-" + Ask.White.Rook[r].castle);
            //Writing Horse
            for (int r = 0; r < 2; r++)
                sw.WriteLine(Ask.White.Knight[r].X + "-" + Ask.White.Knight[r].Y + "-" + Ask.White.Knight[r].Visible + "-" + Ask.Black.Knight[r].X + "-" + Ask.Black.Knight[r].Y + "-" + Ask.Black.Knight[r].Visible);
            //Writing Bishop
            for (int r = 0; r < 2; r++)
                sw.WriteLine(Ask.White.Bishop[r].X + "-" + Ask.White.Bishop[r].Y + "-" + Ask.White.Bishop[r].Visible + "-" + Ask.Black.Bishop[r].X + "-" + Ask.Black.Bishop[r].Y + "-" + Ask.Black.Bishop[r].Visible);

            //Writing King
            sw.WriteLine(Ask.White.King.X + "-" + Ask.White.King.Y + "-" + Ask.White.King.castle + "-" + Ask.Black.King.X + "-" + Ask.Black.King.Y + "-" + Ask.Black.King.castle);
            //Writing Queen
            sw.WriteLine(Ask.White.Queen.X + "-" + Ask.White.Queen.Y + "-" + Ask.White.Queen.Visible + "-" + Ask.Black.Queen.X + "-" + Ask.Black.Queen.Y + "-" + Ask.Black.Queen.Visible);
            //Writing Current Turn & porcent of each other
            sw.WriteLine(Ask.White.Earn + "-" + Ask.Black.Earn + "-" + Ask.Turn + "-" + Stage.user1 + "-" + Stage.user2 + "-" + Ask.White.Rol);
            string initial = "", initial2 = "";
            for (int r = 0; r < 8; r++)
                initial += Ask.White.Pawn[r].initial + (r != 7 ? '-' : ' ').ToString();
            for (int r = 0; r < 8; r++)
                initial2 += Ask.Black.Pawn[r].initial + (r != 7 ? '-' : ' ').ToString();
            sw.WriteLine(initial);
            sw.WriteLine(initial2);
            sw.Close();
        }
        public static void Load(string path)
        {
            if (File.Exists(path))
            {
                StreamReader sr = new StreamReader(path);
                String Line; int c = 0;
                while ((Line = sr.ReadLine()) != null)
                {
                    lines = Line.Split('-');
                    if (c < 8)
                    {
                        Ask.White.Pawn[c].X = int.Parse(lines[0]);
                        Ask.White.Pawn[c].Y = int.Parse(lines[1]);
                        Ask.White.Pawn[c].Visible = bool.Parse(lines[2]);
                        Ask.Black.Pawn[c].X = int.Parse(lines[3]);
                        Ask.Black.Pawn[c].Y = int.Parse(lines[4]);
                        Ask.Black.Pawn[c].Visible = bool.Parse(lines[5]);
                    }
                    else if (c > 7 && c < 10)
                    {
                        Ask.White.Rook[c % 2 == 0 ? 0 : 1].X = int.Parse(lines[0]);
                        Ask.White.Rook[c % 2 == 0 ? 0 : 1].Y = int.Parse(lines[1]);
                        Ask.White.Rook[c % 2 == 0 ? 0 : 1].Visible = bool.Parse(lines[2]);
                        Ask.White.Rook[c % 2 == 0 ? 0 : 1].castle = bool.Parse(lines[3]);
                        Ask.Black.Rook[c % 2 == 0 ? 0 : 1].X = int.Parse(lines[4]);
                        Ask.Black.Rook[c % 2 == 0 ? 0 : 1].Y = int.Parse(lines[5]);
                        Ask.Black.Rook[c % 2 == 0 ? 0 : 1].Visible = bool.Parse(lines[6]);
                        Ask.Black.Rook[c % 2 == 0 ? 0 : 1].castle = bool.Parse(lines[7]);
                    }
                    else if (c > 9 && c < 12)
                    {
                        Ask.White.Knight[c % 2 == 0 ? 0 : 1].X = int.Parse(lines[0]);
                        Ask.White.Knight[c % 2 == 0 ? 0 : 1].Y = int.Parse(lines[1]);
                        Ask.White.Knight[c % 2 == 0 ? 0 : 1].Visible = bool.Parse(lines[2]);
                        Ask.Black.Knight[c % 2 == 0 ? 0 : 1].X = int.Parse(lines[3]);
                        Ask.Black.Knight[c % 2 == 0 ? 0 : 1].Y = int.Parse(lines[4]);
                        Ask.Black.Knight[c % 2 == 0 ? 0 : 1].Visible = bool.Parse(lines[5]);
                    }
                    else if (c > 11 && c < 14)
                    {
                        Ask.White.Bishop[c % 2 == 0 ? 0 : 1].X = int.Parse(lines[0]);
                        Ask.White.Bishop[c % 2 == 0 ? 0 : 1].Y = int.Parse(lines[1]);
                        Ask.White.Bishop[c % 2 == 0 ? 0 : 1].Visible = bool.Parse(lines[2]);
                        Ask.Black.Bishop[c % 2 == 0 ? 0 : 1].X = int.Parse(lines[3]);
                        Ask.Black.Bishop[c % 2 == 0 ? 0 : 1].Y = int.Parse(lines[4]);
                        Ask.Black.Bishop[c % 2 == 0 ? 0 : 1].Visible = bool.Parse(lines[5]);
                    }
                    else if (c == 14)
                    {
                        Ask.White.King.X = int.Parse(lines[0]);
                        Ask.White.King.Y = int.Parse(lines[1]);
                        Ask.White.King.castle = bool.Parse(lines[2]);
                        Ask.Black.King.X = int.Parse(lines[3]);
                        Ask.Black.King.Y = int.Parse(lines[4]);
                        Ask.Black.King.castle = bool.Parse(lines[5]);
                    }
                    else if (c == 15)
                    {
                        Ask.White.Queen.X = int.Parse(lines[0]);
                        Ask.White.Queen.Y = int.Parse(lines[1]);
                        Ask.White.Queen.Visible = bool.Parse(lines[2]);
                        Ask.Black.Queen.X = int.Parse(lines[3]);
                        Ask.Black.Queen.Y = int.Parse(lines[4]);
                        Ask.Black.Queen.Visible = bool.Parse(lines[5]);
                    }
                    else if (c == 16)
                    {
                        Ask.White.Earn = float.Parse(lines[0]);
                        Ask.Black.Earn = float.Parse(lines[1]);
                        Ask.Turn = bool.Parse(lines[2]);
                        Stage.user1 = lines[3].ToString();
                        Stage.user2 = lines[4].ToString();
                        Ask.White.Rol = bool.Parse(lines[5]);
                        Ask.Black.Rol = !Ask.White.Rol;
                    }
                    else if (c == 17)
                    {
                        for (int r = 0; r < 8; r++)
                            Ask.White.Pawn[r].initial = bool.Parse(lines[r]);
                    }
                    else if (c == 18)
                    {
                        for (int r = 0; r < 8; r++)
                            Ask.Black.Pawn[r].initial = bool.Parse(lines[r]);
                    }
                    c++;
                }
                sr.Close();
                Stage.Tokinst();
                Ask.Dot.Cleanse();
                Ask.White.Localize();
                Ask.Black.Localize();
                EatenToken(Ask.Black);
                EatenToken(Ask.White);
                Ask.Dot.BoardSort();
                Ask.pal = true;
            }
        }
        public static string LoadStates(int slot)
        {
            string state = slot + ". ";
            if (File.Exists("Slot" + slot + ".sav"))
            {
                StreamReader sr = new StreamReader("Slot" + slot + ".sav");
                String Line; int c = 0;
                while ((Line = sr.ReadLine()) != null)
                {
                    if (c == 16)
                    {
                        lines = Line.Split('-');
                        state += lines[3] + " (" + (int)(float.Parse(lines[0])) + "%)" + " Vs " + lines[4] + " (" + (int)(float.Parse(lines[1])) + "%)";
                    }
                    c++;
                }
                sr.Close();
            }
            else
                state += "  ?????? ";
            return state + "  F" + slot;
        }
        public static void LoadOptions()
        {
            if (File.Exists("Options.opt"))
            {
                StreamReader sr = new StreamReader("Options.opt");
                String Line;
                while ((Line = sr.ReadLine()) != null)
                {
                    string[] lines = Line.Split(',');
                    Stage.showturn = bool.Parse(lines[1]);
                    Stage.switchframe = bool.Parse(lines[2]);
                    Stage.style = bool.Parse(lines[3]);
                    Stage.showname = bool.Parse(lines[4]);
                }
                sr.Close();
            }
        }
        public static bool Exist(string path)
        {
            return File.Exists(path);
        }
        public static void EatenToken(Player player)
        {
            if (!player.Queen.Visible)
                Stage.EarnedToken(player.Queen.Texture, player.Id == '2' ? 0 : 1);
            for (int i = 0; i < 8; i++)
                if (!player.Pawn[i].Visible)
                    Stage.EarnedToken(player.Pawn[i].Texture, player.Id == '2' ? 0 : 1);
            for (int i = 0; i < 2; i++)
            {
                if (!player.Bishop[i].Visible)
                    Stage.EarnedToken(player.Bishop[i].Texture, player.Id == '2' ? 0 : 1);
                if (!player.Knight[i].Visible)
                    Stage.EarnedToken(player.Knight[i].Texture, player.Id == '2' ? 0 : 1);
                if (!player.Rook[i].Visible)
                    Stage.EarnedToken(player.Rook[i].Texture, player.Id == '2' ? 0 : 1);
            }
        }
    }
}
